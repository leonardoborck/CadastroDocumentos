using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CadastroDocumentos.Models
{
    public class Documento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o código")]
        [RegularExpression(@"[0-9]")]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "Informe o título")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Informe o processo")]
        public string Processo { get; set; }
        [Required(ErrorMessage = "Informe a categoria")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "Insira o arquivo")]
        [RegularExpression(@"^.*\.(pdf | PDF | doc | DOC | docx | DOCX | xls | XLS |xlsx|XLSX)$", ErrorMessage ="Apenas formatos (DOC|DOCX|PDF|XLS|XLSX)")]
        public IFormFile Arquivo{get;set;}
        
    }
}