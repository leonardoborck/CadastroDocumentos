using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

public class ArquivoAttribute : RequiredAttribute
{
    public override bool IsValid(object value)
    {
        //verifica se o arquivo foi enviado
        var file = value as HttpPostedFileBase; 
        if (file == null)
        {
            return false;
        }

        //separa em uma variavel apenas a extensao do arquivo
        var extension = Path.GetExtension(file.FileName);
        //verifica se a extensao do arquivo enviado é igual as extensoes permitidas
        if (extension == ".xls" || extension == ".xlsx" || extension == ".doc" || extension == ".docx" || extension == ".pdf") return true;
        else return false;
    }
}