using System.Data.Entity;

namespace CadastroDocumentos.Models
{
    public class Contexto : DbContext
    {
        public Contexto()
                     : base("Contexto")
        {

        }

        public DbSet<Documento> Documento { get; set; }

    }
}