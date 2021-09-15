using CadastroDocumentos.Models;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CadastroDocumentos.Controllers
{
    public class DocumentoController : Controller
    {
        private readonly Contexto db = new Contexto();

        //pagina inicial
        //retorna lista documento que foram enviados e ordena pelo titulo em ordem alfabetica
        public ActionResult Listar()
        {
            return View(db.Documento.ToList().OrderBy(x => x.Titulo));
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost] // quando for utilizado metodo post
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Id,Codigo,Titulo,Processo,Categoria,ArquivoURL,Arquivo")] Documento documento)
        {
            if (ModelState.IsValid) //apenas se todas condicoes do formulario estiverem ok
            {
                try //tenta enviar o arquivo para o servidor e salvar o nome no bd
                {
                    string nomeArquivo = "";
                    if (documento.Arquivo.ContentLength > 0) //verifica se o tamanho do arquivo nao e nulo
                    {
                        nomeArquivo = Path.GetFileName(documento.Arquivo.FileName);
                        var caminho = Path.Combine(Server.MapPath("~/Arquivos"), nomeArquivo);
                        documento.Arquivo.SaveAs(caminho); //salva o arquivo no caminho do servidor
                        documento.ArquivoNome = Path.Combine(nomeArquivo); //guarda o nome do arquivo no bd
                    }
                    ViewBag.Mensagem = "Cadastro efetuado com sucesso.";
                    db.Documento.Add(documento); //cria um novo item no bd
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = "Erro : " + e.Message;
                }
            }
            return View(documento);
        }

        //acessa um documento especifico
        public ActionResult AcessarArquivo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documento.Find(id); //procura o documento no bd pelo id
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }
        //verificacao que garante que o codigo do documento seja unico
        public ActionResult CodigoUnico(int codigo)
        {
            Documento documento = db.Documento.Find(codigo); //procura no banco de dados pelo codigo passado por parametro
            if (documento == null) //se nao encontrar um item e retornar nulo o usuario podera inserir o codigo
            {
                return Content("true");
            }
            else return Content("false");
        }
    }
}
