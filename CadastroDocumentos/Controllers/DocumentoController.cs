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

        public ActionResult ListarDocumentosCadastrados()
        {
            return View(bancoDeDados.Documento.ToList().OrderBy(x => x.Titulo));
        }

        public ActionResult CadastrarNovoDocumento()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarNovoDocumento([Bind(Include = "Id,Codigo,Titulo,Processo,Categoria,ArquivoNome,Arquivo")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    string nomeDoArquivo = "";
                    if (documento.Arquivo.ContentLength > 0)
                    {
                        nomeDoArquivo = Path.GetFileName(documento.Arquivo.FileName);
                        var caminhoDoArquivo = Path.Combine(Server.MapPath("~/Arquivos"), nomeDoArquivo);
                        documento.Arquivo.SaveAs(caminhoDoArquivo);
                        documento.ArquivoNome = nomeDoArquivo;
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

        public ActionResult AcessarArquivoDoDocumento(int? id)
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
            foreach (Documento documento in bancoDeDados.Documento)
            {
                if (documento.Codigo == codigo) return Content("false");
            }
            return Content("true");
        }
    }
}
