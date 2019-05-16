using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _5032_MVC_News.Models;

namespace _5032_MVC_News.Controllers
{
    public class ArticleDetailsController : Controller
    {
        private NewsEntities db = new NewsEntities();

        // GET: ArticleDetails
        public ActionResult Index()
        {
            var articleDetails = db.ArticleDetails.Include(a => a.AspNetUser);
            return View(articleDetails.ToList());
        }

        // GET: ArticleDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleDetail articleDetail = db.ArticleDetails.Find(id);
            if (articleDetail == null)
            {
                return HttpNotFound();
            }
            return View(articleDetail);
        }

        [Authorize]
        // GET: ArticleDetails/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: ArticleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleId,ArticleTitle,PublishDate,ArticleContent,AuthorId")] ArticleDetail articleDetail)
        {
            if (ModelState.IsValid)
            {
                db.ArticleDetails.Add(articleDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.AspNetUsers, "Id", "Email", articleDetail.AuthorId);
            return View(articleDetail);
        }

        [Authorize]
        // GET: ArticleDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleDetail articleDetail = db.ArticleDetails.Find(id);
            if (articleDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.AspNetUsers, "Id", "Email", articleDetail.AuthorId);
            return View(articleDetail);
        }

        // POST: ArticleDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleId,ArticleTitle,PublishDate,ArticleContent,AuthorId")] ArticleDetail articleDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articleDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.AspNetUsers, "Id", "Email", articleDetail.AuthorId);
            return View(articleDetail);
        }

        [Authorize]
        // GET: ArticleDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleDetail articleDetail = db.ArticleDetails.Find(id);
            if (articleDetail == null)
            {
                return HttpNotFound();
            }
            return View(articleDetail);
        }

        // POST: ArticleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticleDetail articleDetail = db.ArticleDetails.Find(id);
            db.ArticleDetails.Remove(articleDetail);
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
