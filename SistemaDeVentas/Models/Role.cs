using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Role
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public string? View { get; set; }

    public string? Username { get; set; }

    public int? State { get; set; }

    public byte? ActionAdd { get; set; }

    public byte? ActionEdit { get; set; }

    public byte? ActionDelete { get; set; }
}