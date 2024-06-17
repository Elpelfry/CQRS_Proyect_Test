using System.ComponentModel.DataAnnotations;

namespace API.Domain.Entities;

public class Prioridades
{
    [Key]
    public int Id { get; set; }
    public string? Descripcion { get; set; }
    public int DiasCompromiso { get; set; }
}
