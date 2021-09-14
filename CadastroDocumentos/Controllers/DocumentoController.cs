using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CadastroDocumentos.Models;

namespace CadastroDocumentos.Controllers
{
    public class DocumentoController : Controller
    {
        private Contexto db = new Contexto();

        /*        // GET: Documento
                public ActionResult Index()
                {
                    return View(db.Documento.ToList().OrderBy(x => x.Titulo));
                }*/

        // GET: Documento/Create

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Documento arq)
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
                            arq.ArquivoURL = caminho;
                        }
                        ViewBag.Mensagem = "Arquivo : " + nomeArquivo + " , enviado com sucesso.";
                        db.Documento.Add(arq);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Mensagem = "Erro : " + ex.Message;
                    }
                }
            return View(arq);
        }

      /*  public ActionResult Create()
        {
            return View();
        }

        // POST: Documento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,Titulo,Processo,Categoria,ArquivoURL")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string nomeArquivo = "";
                    if (documento.Arquivo.ContentLength > 0)
                    {
                        nomeArquivo = Path.GetFileName(documento.Arquivo.FileName);
                        var caminho = Path.Combine(Server.MapPath("~/Imagens"), nomeArquivo);
                        documento.Arquivo.SaveAs(caminho);
                        documento.ArquivoURL = caminho;
                    }
                    ViewBag.Mensagem = "Arquivo : " + nomeArquivo + " , enviado com sucesso.";
                    db.Documento.Add(documento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Mensagem = "Erro : " + ex.Message;
                }
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
        }*/
    }
}
