using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TestImageController : Controller
    {
        // GET: TestImage
        public ActionResult Index()
        {
            using (Model1 model1 = new Model1())
            {
                return View(model1.TBImgs.ToList());
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TBImg tBImge)
        {
            string fileName = Path.GetFileNameWithoutExtension(tBImge.ImageFile.FileName);
            string extension = Path.GetExtension(tBImge.ImageFile.FileName);
            string imagePath = "/Image/" + fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string serverPath = Path.Combine(Server.MapPath("~" + imagePath));

            tBImge.ImageFile.SaveAs(serverPath);
            tBImge.Image = imagePath;

            using (Model1 model1 = new Model1())
            {
                model1.TBImgs.Add(tBImge);
                model1.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }

        // GET: TestImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            using (Model1 model1 = new Model1())
            {
                TBImg tBImg = model1.TBImgs.Find(id);
                if (tBImg == null)
                {
                    return HttpNotFound();
                }
                return View(tBImg);
            }
        }

        // POST: TestImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (Model1 model1 = new Model1())
            {
                TBImg tBImg = model1.TBImgs.Find(id);
                if (tBImg != null)
                {
                    string fullPath = Request.MapPath("~" + tBImg.Image);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    model1.TBImgs.Remove(tBImg);
                    model1.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}