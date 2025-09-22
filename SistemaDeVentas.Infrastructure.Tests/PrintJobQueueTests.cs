using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;
using SistemaDeVentas.Infrastructure.Services.Printer;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class PrintJobQueueTests
{
    private readonly Mock<ILogger<PrintJobQueue>> _loggerMock;
    private readonly PrintJobQueue _queue;

    public PrintJobQueueTests()
    {
        _loggerMock = new Mock<ILogger<PrintJobQueue>>();
        _queue = new PrintJobQueue(_loggerMock.Object);
    }

    [Fact]
    public async Task EnqueueAsync_ShouldAddJobToQueue()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 };
        var priority = PrintJobPriority.Normal;

        // Act
        var result = await _queue.EnqueueAsync(data, priority);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task DequeueAsync_ShouldReturnJobsInPriorityOrder()
    {
        // Arrange
        var lowPriorityData = new byte[] { 0x4C, 0x4F, 0x57 };
        var normalPriorityData = new byte[] { 0x4E, 0x4F, 0x52, 0x4D, 0x41, 0x4C };
        var highPriorityData = new byte[] { 0x48, 0x49, 0x47, 0x48 };

        // Enqueue in reverse priority order
        await _queue.EnqueueAsync(lowPriorityData, PrintJobPriority.Low);
        await _queue.EnqueueAsync(normalPriorityData, PrintJobPriority.Normal);
        await _queue.EnqueueAsync(highPriorityData, PrintJobPriority.High);

        // Act & Assert
        var firstJob = await _queue.DequeueAsync();
        firstJob.Should().NotBeNull();
        firstJob!.Priority.Should().Be(PrintJobPriority.High);
        firstJob.Data.Should().BeEquivalentTo(highPriorityData);

        var secondJob = await _queue.DequeueAsync();
        secondJob.Should().NotBeNull();
        secondJob!.Priority.Should().Be(PrintJobPriority.Normal);
        secondJob.Data.Should().BeEquivalentTo(normalPriorityData);

        var thirdJob = await _queue.DequeueAsync();
        thirdJob.Should().NotBeNull();
        thirdJob!.Priority.Should().Be(PrintJobPriority.Low);
        thirdJob.Data.Should().BeEquivalentTo(lowPriorityData);
    }

    [Fact]
    public async Task DequeueAsync_WhenQueueIsEmpty_ShouldReturnNull()
    {
        // Act
        var result = await _queue.DequeueAsync();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CancelAsync_WithPendingJob_ShouldCancelSuccessfully()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 };
        var enqueueResult = await _queue.EnqueueAsync(data, PrintJobPriority.Normal);
        var jobId = enqueueResult.Value;

        // Act
        var cancelResult = await _queue.CancelAsync(jobId);

        // Assert
        cancelResult.IsSuccess.Should().BeTrue();
        cancelResult.Value.Should().BeTrue();

        // Verify job status
        var statusResult = await _queue.GetStatusAsync(jobId);
        statusResult.IsSuccess.Should().BeTrue();
        statusResult.Value.Should().Be(PrintJobStatus.Cancelled);
    }

    [Fact]
    public async Task CancelAsync_WithNonPendingJob_ShouldFail()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 };
        var enqueueResult = await _queue.EnqueueAsync(data, PrintJobPriority.Normal);
        var jobId = enqueueResult.Value;

        // Dequeue to change status to Processing
        await _queue.DequeueAsync();

        // Act
        var cancelResult = await _queue.CancelAsync(jobId);

        // Assert
        cancelResult.IsFailed.Should().BeTrue();
        cancelResult.Errors.First().Message.Should().Contain("Solo se pueden cancelar trabajos pendientes");
    }

    [Fact]
    public async Task CancelAsync_WithNonExistentJob_ShouldFail()
    {
        // Act
        var result = await _queue.CancelAsync(Guid.NewGuid());

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("Trabajo no encontrado");
    }

    [Fact]
    public async Task GetStatusAsync_WithValidJobId_ShouldReturnStatus()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 };
        var enqueueResult = await _queue.EnqueueAsync(data, PrintJobPriority.Normal);
        var jobId = enqueueResult.Value;

        // Act
        var result = await _queue.GetStatusAsync(jobId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(PrintJobStatus.Pending);
    }

    [Fact]
    public async Task GetStatusAsync_WithInvalidJobId_ShouldFail()
    {
        // Act
        var result = await _queue.GetStatusAsync(Guid.NewGuid());

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("Trabajo no encontrado");
    }

    [Fact]
    public async Task GetPendingJobsAsync_ShouldReturnOnlyPendingJobs()
    {
        // Arrange
        var data1 = new byte[] { 0x1 };
        var data2 = new byte[] { 0x2 };
        var data3 = new byte[] { 0x3 };

        await _queue.EnqueueAsync(data1, PrintJobPriority.Normal);
        var job2Result = await _queue.EnqueueAsync(data2, PrintJobPriority.Normal);
        await _queue.EnqueueAsync(data3, PrintJobPriority.Normal);

        // Process one job
        await _queue.DequeueAsync();

        // Act
        var result = await _queue.GetPendingJobsAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        var pendingJobs = result.Value.ToList();
        pendingJobs.Should().HaveCount(2);
        pendingJobs.Should().Contain(j => j.Id == job2Result.Value);
    }

    [Fact]
    public async Task GetMetricsAsync_ShouldReturnCorrectMetrics()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 };
        await _queue.EnqueueAsync(data, PrintJobPriority.Normal);
        await _queue.EnqueueAsync(data, PrintJobPriority.High);

        // Process one job
        var job = await _queue.DequeueAsync();
        _queue.MarkCompleted(job!.Id, TimeSpan.FromSeconds(1));

        // Act
        var result = await _queue.GetMetricsAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        var metrics = result.Value;
        metrics.TotalJobs.Should().Be(2);
        metrics.PendingJobs.Should().Be(1);
        metrics.ProcessingJobs.Should().Be(0);
        metrics.CompletedJobs.Should().Be(1);
        metrics.FailedJobs.Should().Be(0);
        metrics.CancelledJobs.Should().Be(0);
        metrics.AverageProcessingTime.Should().Be(1.0);
        metrics.SuccessRate.Should().Be(1.0);
    }

    [Fact]
    public async Task CleanupAsync_ShouldRemoveOldCompletedJobs()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 };
        var oldDate = DateTime.UtcNow.AddDays(-2);

        await _queue.EnqueueAsync(data, PrintJobPriority.Normal);
        var job = await _queue.DequeueAsync();
        _queue.MarkCompleted(job!.Id, TimeSpan.FromSeconds(1));

        // Manually set old updated date
        job.UpdatedAt = oldDate;

        // Act
        var result = await _queue.CleanupAsync(DateTime.UtcNow.AddDays(-1));

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(1);
    }

    [Fact]
    public async Task MarkCompleted_ShouldUpdateStatusAndMetrics()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 };
        var enqueueResult = await _queue.EnqueueAsync(data, PrintJobPriority.Normal);
        var jobId = enqueueResult.Value;
        var job = await _queue.DequeueAsync();
        var processingTime = TimeSpan.FromSeconds(2.5);

        // Act
        _queue.MarkCompleted(jobId, processingTime);

        // Assert
        job!.Status.Should().Be(PrintJobStatus.Completed);
        job.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task MarkFailed_ShouldRetryIfPossible()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 };
        var enqueueResult = await _queue.EnqueueAsync(data, PrintJobPriority.Normal);
        var jobId = enqueueResult.Value;
        var job = await _queue.DequeueAsync();

        // Act
        _queue.MarkFailed(jobId, "Connection error");

        // Assert
        job!.Status.Should().Be(PrintJobStatus.Pending); // Should be re-queued
        job.RetryCount.Should().Be(1);
        job.ErrorMessage.Should().Be("Connection error");
    }

    [Fact]
    public async Task MarkFailed_ShouldFailPermanentlyAfterMaxRetries()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 };
        var enqueueResult = await _queue.EnqueueAsync(data, PrintJobPriority.Normal);
        var jobId = enqueueResult.Value;
        var job = await _queue.DequeueAsync();
        // Set retry count to max
        job!.RetryCount = 5;

        // Act
        _queue.MarkFailed(jobId, "Persistent error");

        // Assert
        job.Status.Should().Be(PrintJobStatus.Failed);
        job.RetryCount.Should().Be(5); // Should not increment beyond max
        job.ErrorMessage.Should().Be("Persistent error");
    }
}