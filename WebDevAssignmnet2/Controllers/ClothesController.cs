using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebDevAssignmnet2.Models;
using PagedList;
using System.Data.SqlClient;
using System.Net;

namespace WebDevAssignmnet2.Controllers
{
    public class ClothesController : Controller
    {
        private StoreDatabaseEntities1 sdb = new StoreDatabaseEntities1();

        // GET: Clothes
        public ActionResult Clothing(string searchString)
        {
            var clothes = from s in sdb.Clothes select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clothes = clothes.Where(s => s.Category.Contains(searchString) || s.Gender.Contains(searchString));
            }

            return View(clothes);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cloth clothes)
        {
            TempData["Message1"] = "Successfully Added";
            if (ModelState.IsValid)
            {
                TempData["Message1"] = "Successfully Added";
                Cloth cloth = new Cloth()
                {
                    Gender = clothes.Gender,
                    Category = clothes.Category,
                    Condition = clothes.Condition,
                    Color = clothes.Color,
                    Size = clothes.Size,
                    Brand = clothes.Brand,
                    Location = clothes.Location,
                    Price = clothes.Price,
                };
                    sdb.Clothes.Add(cloth);
                    sdb.SaveChanges();
                TempData["Message1"] = "Successfully Added";
            }
            TempData["Message1"] = "Successfully Added";
            return View(clothes);
        }

        // GET: Clothes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth cloth = sdb.Clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            return View(cloth);
        }
    }
}