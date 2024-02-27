using Tecnm.Proyecto1.Core.Enums;

namespace Tecnm.Proyecto1.Core.Entities;

public class Usuario
{
    public string? Nom_usuario { get; set;}
    public double Ingresos { get; set; }
    public int Opcion { get; set; }
    public string? Concepto { get; set; }
    public CategoriasType categoria { get; set; }
}