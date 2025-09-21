using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Domain.Interfaces
{
    public interface ISalesDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DatabaseFacade Database { get; }
    }
}
