using Proyecto_PorSalud.Models;
using Proyecto_PorSalud.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PorSalud.Controllers
{
    public class ClientesController : Controller
    {
        private IClienteService Svc => (IClienteService)System.Web.HttpContext.Current.Application["ClienteService"];

        // VENTANA 1: Lista paginada (con carga asíncrona)
        public ActionResult Index() => View(); // Carga el cascarón, la tabla llega por AJAX

        [HttpGet]
        public async Task<ActionResult> Lista(int page = 1, int pageSize = 20, string search = "")
        {
            var result = await Svc.GetPagedAsync(page, pageSize, search);
            return PartialView("_Tabla", result);
        }

        // VENTANA 2: Crear cliente
        [HttpGet]
        public ActionResult Create() => View(new Cliente());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cliente model)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(400, "Datos inválidos");
            var id = await Svc.CreateAsync(model);
            return Json(new { ok = true, id });
        }

        // VENTANA 3: Detalle + Editar + Eliminar + Subir PDF (AJAX)
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var c = await Svc.GetByIdAsync(id);
            if (c == null) return HttpNotFound();
            return View(c);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Cliente model)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(400, "Datos inválidos");
            await Svc.UpdateAsync(model);
            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await Svc.DeleteAsync(id);
            return Json(new { ok = true });
        }

        // Subida de PDF (solo PDF, asíncrono)
        [HttpPost]
        public async Task<ActionResult> UploadPdf(int clienteId, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
                return new HttpStatusCodeResult(400, "Archivo requerido");

            if (file.ContentType != "application/pdf" && !file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                return new HttpStatusCodeResult(415, "Solo PDF");

            var carpeta = Server.MapPath("~/App_Data/Uploads");
            Directory.CreateDirectory(carpeta);
            var nombre = $"{Guid.NewGuid():N}.pdf";
            var ruta = Path.Combine(carpeta, nombre);
            file.SaveAs(ruta);

            using (var db = new AppDbContext())
            {
                db.Documentos.Add(new Documento
                {
                    ClienteId = clienteId,
                    Titulo = Path.GetFileName(file.FileName),
                    RutaRelativa = $"~/App_Data/Uploads/{nombre}",
                    ContentType = "application/pdf",
                    Length = file.ContentLength
                });
                await db.SaveChangesAsync();
            }
            return Json(new { ok = true });
        }

        // VENTANA 4: Ver documentos (visor PDF)
        [HttpGet]
        public async Task<ActionResult> Documentos(int id) // id = clienteId
        {
            using (var db = new AppDbContext())
            {
                var c = await db.Clientes.Include("Documentos").FirstOrDefaultAsync(x => x.Id == id);
                if (c == null) return HttpNotFound();
                return View(c);
            }
        }

        [HttpGet]
        public ActionResult VerPdf(int docId)
        {
            using (var db = new AppDbContext())
            {
                var d = db.Documentos.Find(docId);
                if (d == null) return HttpNotFound();
                var rutaFisica = Server.MapPath(d.RutaRelativa);
                return File(rutaFisica, d.ContentType); // El navegador lo muestra (iframe/object)
            }
        }
    }
}