using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _5032_MVC_News.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mail;
using System.Net.Mail;
using System.IO;

namespace _5032_MVC_News.Controllers
{
    public class AspNetUsersController : Controller
    {
        private NewsEntities db = new NewsEntities();

        public ActionResult Picture(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Picture([Bind(Include = "Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, FirstName, LastName")] AspNetUser aspNetUser, HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (Assignment.Helpers.Util.IsRecognisedImageFile(postedFile.FileName))
                {
                    String filePath = Assignment.Helpers.Util.GenerateUniqueString();
                    filePath = Assignment.Helpers.Util.CalculateMD5Hash(filePath);

                    String fileExtension = Path.GetExtension(postedFile.FileName);
                    String full = filePath + fileExtension;
                    postedFile.SaveAs(path + full);
                    aspNetUser.ImagePath = full;
                }
                else
                {
                    ViewBag.Message = "Please upload a valid image.";
                    return View(aspNetUser);
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
            return View(aspNetUser);
        }




        // GET: AspNetUsers
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            TempData["checkedEmail"] = collection["checkedEmail"];
            return RedirectToAction("Contact");

        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        [Authorize]
        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName")] AspNetUser aspNetUser, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUser);
        }

        [Authorize]
        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,ImagePath")] AspNetUser aspNetUser , HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }

        [Authorize]
        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AspNetUsers/Contact/5
        public ActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email To: {0} {1}</p><p>Message:</p><p></p>";
                var message = new System.Net.Mail.MailMessage();
                //changes
                var email = (string)TempData["checkedEmail"];
                string[] emails = email.Split(',');
                foreach (string name in emails)
                {
                    message.To.Add(new MailAddress(name));
                }

                string id = User.Identity.GetUserId();
                Models.ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
                string userEmail = user.Email;
                
                message.From = new MailAddress(userEmail);  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
               
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "sanyagaus@gmail.com",  // replace with valid value
                        Password = "password"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
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
