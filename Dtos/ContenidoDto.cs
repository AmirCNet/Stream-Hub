using System.ComponentModel.DataAnnotations;

namespace StreamHub.Dtos;

public class ContenidoDto
{
    public int? Id { get; set; }
    
    public string Titulo { get; set; } = string.Empty;
    
    public string Descripcion { get; set; } = string.Empty;
    
    public string Tipo { get; set; } = string.Empty;
    
    public string Genero { get; set; } = string.Empty;
    
    public string ClasificacionEdad { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty; 
}
