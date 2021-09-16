using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

public class VerificarExtensoesPermitidasDoArquivoAttribute : RequiredAttribute
{
    public override bool IsValid(object valor)
    {
        var arquivo = valor as HttpPostedFileBase;
        if (arquivo == null)
        {
            return false;
        }
        var extensao = Path.GetExtension(arquivo.FileName);
        if (extensao == ".xls" || extensao == ".xlsx" || extensao == ".doc" || extensao == ".docx" || extensao == ".pdf") return true;
        else return false;
    }
}