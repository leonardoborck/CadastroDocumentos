using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

public class ArquivoAttribute : RequiredAttribute
{
    public override bool IsValid(object value)
    {
        var file = value as HttpPostedFileBase;
        if (file == null)
        {
            return false;
        }

        var extension = Path.GetExtension(file.FileName);
        if (extension == ".xls" || extension == ".xlsx" || extension == ".doc" || extension == ".docx" || extension == ".pdf") return true;
        else return false;
    }
}