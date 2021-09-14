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
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            return View(db.Documento.ToList().OrderBy(x => x.Titulo));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Titulo,Processo,Categoria,ArquivoURL,Arquivo")] Documento arq)
        {
            if (ModelState.IsValid)
            {
                    try
                    {
                        string nomeArquivo = "";
                        if (arq.Arquivo.ContentLength > 0)
                        {
                            nomeArquivo = Path.GetFileName(arq.Arquivo.FileName);
                            var caminho = Path.Combine(Server.MapPath("~/Arquivos"), nomeArquivo);
                            arq.Arquivo.SaveAs(caminho);
                            arq.ArquivoNome = Path.Combine(nomeArquivo);
                        }
                        ViewBag.Mensagem = "Arquivo : " + nomeArquivo + " , enviado com sucesso.";
                        db.Documento.Add(arq);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                }
                    catch (Exception e)
                    {
                        ViewBag.Mensagem = "Erro : " + e.Message;
                    }
                }
            return View(arq);
        }

        // GET: Contato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
