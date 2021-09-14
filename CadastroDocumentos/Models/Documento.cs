using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CadastroDocumentos.Models
{
    public class Documento
    {
        [Key]
        public int Id { get; set; }

        //criar função
        //[Remote("CodigoUnico", "Documento", ErrorMessage = "Ja existe um documento com esse código")]
        [Required(ErrorMessage = "O código é Obrigatório")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O título é Obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O processo é Obrigatório")]
        public string Processo { get; set; }

        [Required(ErrorMessage = "A categoria é Obrigatório")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "O arquivo é Obrigatório")]
        [NotMapped]
        [Arquivo(ErrorMessage = "Apenas as extensões [PDF, DOC, XLS, DOCX e XLSX]")]
        public HttpPostedFileBase Arquivo { get; set; }

        public string ArquivoURL { get; set; }
        
    }
}