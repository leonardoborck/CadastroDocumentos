using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace CadastroDocumentos.Models
{
    public class Documento
    {
        [Key] //primary key do bd
        public int Id { get; set; }

        //codigo do documento unico e obrigatorio
        [Remote("CodigoUnico", "Documento", ErrorMessage = "Ja existe um documento com esse código")]
        [Required(ErrorMessage = "O código é Obrigatório")]
        public int Codigo { get; set; }

        //titulo do documento obrigatorio, texto
        [Required(ErrorMessage = "O título é Obrigatório")]
        public string Titulo { get; set; }

        //processo campo de selecao definido no front-end
        [Required(ErrorMessage = "O processo é Obrigatório")]
        public string Processo { get; set; }

        //categoria do documento, texto
        [Required(ErrorMessage = "A categoria é Obrigatório")]
        public string Categoria { get; set; }

        //arquivo obrigatorio e somente extensoes especificadas
        [Required(ErrorMessage = "O arquivo é Obrigatório")]
        [NotMapped] //nao sera considerado no banco de dados
        [Arquivo(ErrorMessage = "Apenas as extensões [PDF, DOC, XLS, DOCX e XLSX]")]
        public HttpPostedFileBase Arquivo { get; set; }

        public string ArquivoNome { get; set; }
        
    }
}