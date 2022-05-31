using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDevAssignmnet2.Models;

namespace WebDevAssignmnet2.Controllers
{
    public class SellerController : Controller
    {
        private StoreDatabaseEntities1 sdb = new StoreDatabaseEntities1();

        // GET: Seller
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Seller seller)
        {
            if(ModelState.IsValidField("Email") && ModelState.IsValidField("Password"))
            {
                var user = sdb.Sellers.Where(s => s.Email.Equals(seller.Email) && s.Password.Equals(seller.Password)).FirstOrDefault();
                if(user != null)
                {
                    Session["SellerID"] = user.SId.ToString();
                    Session["Username"] = user.Username.ToString();
                    return Content(String.Format("<script language='javascript' type='text/javascript'>alert('Logged in successfully as {0}!');window.location.href='/';</script>", user.Username));
                }
                return View("Login");
            }
            return View(seller);
        }

        public ActionResult Logout()
        {
            Session["SellerID"] = null;
            return View("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Seller seller)
        {
            if (ModelState.IsValid)
            {
                var check = sdb.Sellers.FirstOrDefault(s => s.Email == seller.Email);
                if (check == null)
                {
                    sdb.Sellers.Add(seller);
                    sdb.SaveChanges();
                    return Content("<script language='javascript' type='text/javascript'>alert('Congratulations, your account has been successfully created.');window.location.href='/Seller/Login';</script>");

                }
                else
                {
                    TempData["Message"] = "Email already exists";
                    return View();
                }
            }
            return View(seller);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = sdb.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(int? SID, Seller seller)
        {

            if (SID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValidField("Username") && ModelState.IsValidField("Password"))
            {
                Seller sellerToUpdate = sdb.Sellers.Find(SID);
                var user = sdb.Sellers.Where(s => s.Username.Equals(seller.Username) && s.Password.Equals(seller.Password)).FirstOrDefault();
                if(user == null)
                {
                 TempData["Message"] = "Username or Password is incorrect";
                }
                else if(user.Password == sellerToUpdate.Password && user.Username == sellerToUpdate.Username)
                {
                    if (TryUpdateModel(sellerToUpdate, "", new string[] { "Name", "Address", "Phone", "Email" }))
                    {
                        try
                        {
                            sdb.SaveChanges();
                            TempData["Message"] = "Successfully Updated!";
                            return RedirectToAction("Edit");

                        }
                        catch (DataMisalignedException)
                        {
                            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                        }
                    }
                }
                else
                {
                    TempData["Message"] = "Username or Password is incorrect";
                }
                 return View(sellerToUpdate);
            }
            else
            {
              TempData["Message"] = "Incorrect Password";
            }
            return View();
        }
    }
}