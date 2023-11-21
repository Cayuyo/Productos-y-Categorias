#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

public class Producto
{

    [Key]
    public int ProductoId { get; set; }
    [Required(ErrorMessage = "El Nombre es obligatorio.")]
    [MinLength(2, ErrorMessage = "El Nombre debe tener al menos 2 caracteres.")]
    [StringLength(45, ErrorMessage = "El Nombre debe tener como máximo 45 caracteres.")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
    [Column(TypeName = "TEXT")]
    public string Descripcion { get; set; }
    [Required(ErrorMessage = "El campo Cantidad es obligatorio.")]
    [Range(1, double.MaxValue, ErrorMessage = "La Cantidad debe ser mayor o igual a 1.")]
    public double Precio { get; set; }
    public DateTime Fecha_Creacion { get; set; }
    public DateTime Fecha_Modificacion { get; set; }

    public List<Asociacion> AssocCategoria { get; set; } = new List<Asociacion>();

    public Producto()
    {
        Fecha_Creacion = DateTime.Now;
        Fecha_Modificacion = DateTime.Now;
        FormatearFechas();

    }
    private void FormatearFechas()
    {
        string formatoChileno = "dd-MM-yyyy HH:mm:ss";
        Fecha_Creacion = DateTime.ParseExact(Fecha_Creacion.ToString(formatoChileno), formatoChileno, CultureInfo.InvariantCulture);
        Fecha_Modificacion = DateTime.ParseExact(Fecha_Modificacion.ToString(formatoChileno), formatoChileno, CultureInfo.InvariantCulture);
    }
}