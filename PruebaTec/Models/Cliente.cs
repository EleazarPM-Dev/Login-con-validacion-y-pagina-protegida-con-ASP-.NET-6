using System;
using System.Collections.Generic;

namespace PruebaTec.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string? Usuario { get; set; }

    public string? Correo { get; set; }

    public string? Contraseña { get; set; }

    public byte[]? PerfilImg { get; set; }

    public bool? ClienteValidacion { get; set; }
}
