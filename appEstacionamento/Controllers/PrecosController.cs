using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using appEstacionamento.Models;

namespace appEstacionamento.Controllers
{
    public class PrecosController : Controller
    {
        private appEstacionamentoContext db = new appEstacionamentoContext();

        // GET: Precos
        public ActionResult Index()
        {
            return View(db.Precos.ToList());
        }

        // GET: Precos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Precos precos = db.Precos.Find(id);
            if (precos == null)
            {
                return HttpNotFound();
            }
            return View(precos);
        }

        // GET: Precos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Precos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,dataIncial,dataFinal,valorHora,valorHoraAdicional")] Precos precos)
        {
            if (ModelState.IsValid)
            {
                db.Precos.Add(precos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(precos);
        }

        // GET: Precos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Precos precos = db.Precos.Find(id);
            if (precos == null)
            {
                return HttpNotFound();
            }
            return View(precos);
        }

        // POST: Precos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dataIncial,dataFinal,valorHora,valorHoraAdicional")] Precos precos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(precos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(precos);
        }

        // GET: Precos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Precos precos = db.Precos.Find(id);
            if (precos == null)
            {
                return HttpNotFound();
            }
            return View(precos);
        }

        // POST: Precos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Precos precos = db.Precos.Find(id);
            db.Precos.Remove(precos);
            db.SaveChanges();
            return RedirectToAction("Index");
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
