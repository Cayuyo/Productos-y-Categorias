#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

public class Categoria
{
    [Key]
    public int CategoriaId { get; set;}
    [Required(ErrorMessage = "El campo \"Nombre\" es obligatorio.")]
    [StringLength(45, MinimumLength=3, ErrorMessage ="El Nombre debe tener mas de 3 y menos de 45 caracteres.")]
    public string Nombre {get;set;}
    public DateTime Fecha_Creacion { get; set; }
    public DateTime Fecha_Modificacion { get; set; }

    public List<Asociacion> AssocProducto { get; set; } = new List<Asociacion>();

    public Categoria()
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