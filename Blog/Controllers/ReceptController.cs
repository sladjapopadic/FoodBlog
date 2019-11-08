using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Data;
using Blog.Data.Models;
using Microsoft.Ajax.Utilities;

namespace Blog.Controllers
{
    public class ReceptController : Controller
    {
        private BlogContext db = new BlogContext();
        
        public ActionResult Index()
        {
            return View(db.Recepti.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recept recept = db.Recepti.Find(id);
            if (recept == null)
            {
                return HttpNotFound();
            }
            return View(recept);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Sastojci,Priprema,VremePripreme,BrojPorcija,VrstaRecepta")] Recept recept)
        {
            recept.Autor = Session["Username"].ToString();
            if (ModelState.IsValid)
            {
                db.Recepti.Add(recept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recept);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recept recept = db.Recepti.Find(id);
            if (recept == null)
            {
                return HttpNotFound();
            }
            return View(recept);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Sastojci,Priprema,VremePripreme,Autor,BrojPorcija,VrstaRecepta")] Recept recept)
        {
            recept.Autor = Session["Username"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(recept).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recept);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recept recept = db.Recepti.Find(id);
            if (recept == null)
            {
                return HttpNotFound();
            }
            db.Recepti.Remove(recept);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recept recept = db.Recepti.Find(id);
            db.Recepti.Remove(recept);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Filter(ReceptFilter filter)
        {
            var recipes = from s in db.Recepti
                           select s;            

            if (!filter.VrstaRecepta.IsNullOrWhiteSpace())
            {

                recipes = recipes.Where(p => p.VrstaRecepta.Contains(filter.VrstaRecepta));
            }

            return View(recipes.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult MojiRecepti()
        {
            return View();
        }
    }
}
