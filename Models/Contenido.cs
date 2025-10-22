namespace StreamHub.Models;

public class Contenido
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public string? Tipo { get; set; }
    public string? Genero { get; set; }
    public string? Url { get; set; }
    public DateTime FechaCreacion { get; set; }
}