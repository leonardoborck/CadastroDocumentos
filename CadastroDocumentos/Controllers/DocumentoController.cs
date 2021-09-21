using CadastroDocumentos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public JsonResult SelecionarPorProcesso(int id)
        {
            var categorias = Categorias.Where(x => x.Value == id).OrderBy(x => x.Key)
                .Select(x => new {categoria=x.Key,id = x.Key})
                .ToList();

            return Json(categorias, JsonRequestBehavior.AllowGet);
        }

        readonly IDictionary<int,string> Processos = new Dictionary<int, string>()
        {
            {1,"Processo 1"},
            {2, "Processo 2"},
            {3,"Processo 3"}
        };
        readonly IDictionary<string, int> Categorias = new Dictionary<string, int>()
        {
            {"Categoria A", 1},
            {"Categoria B", 1},
            {"Categoria C", 2},
            {"Categoria D", 3}
        };
        public ActionResult CadastrarNovoDocumento()
        {
            ViewBag.Processos = Processos;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarNovoDocumento(Documento documento)
        {
            ViewBag.Processos = Processos;

            if (ModelState.IsValid)
            {
                try
                {
                    var nomeDoArquivo = "";
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
            foreach (var documento in bancoDeDados.Documento)
            {
                if (documento.Codigo == codigo) return Content("false");
            }
            return Content("true");
        }
    }
}
