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
        private readonly Contexto bancoDeDados = new Contexto();

        public ActionResult Listar()
        {
            return View(bancoDeDados.Documento.ToList().OrderBy(x => x.Titulo));
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Id,Codigo,Titulo,Processo,Categoria,ArquivoNome,Arquivo")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    string nomeArquivo = "";
                    if (documento.Arquivo.ContentLength > 0)
                    {
                        nomeArquivo = Path.GetFileName(documento.Arquivo.FileName);
                        var caminho = Path.Combine(Server.MapPath("~/Arquivos"), nomeArquivo);
                        documento.Arquivo.SaveAs(caminho);
                        documento.ArquivoNome = Path.Combine(nomeArquivo);
                    }
                    ViewBag.Mensagem = "Cadastro efetuado com sucesso.";
                    bancoDeDados.Documento.Add(documento);
                    bancoDeDados.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = "Erro : " + e.Message;
                }
            }
            return View(documento);
        }

        public ActionResult AcessarArquivo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = bancoDeDados.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }
        public ActionResult VerificarSeOCodigoDoDocumentoJaExiste(int codigo)
        {
            Documento documento = bancoDeDados.Documento.Find(codigo);
            if (documento == null) 
            {
                return Content("true");
            }
            else return Content("false");
        }
    }
}
