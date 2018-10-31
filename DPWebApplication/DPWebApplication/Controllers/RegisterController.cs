using DPWebApplication.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DPWebApplication.Controllers
{
    public class RegisterController : Controller
    {

        // GET: Register
        [HttpGet]
        public ActionResult AddorEdit(int id = 0)
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]

        public ActionResult AddorEdit(User userModel)
        {
            using (AuthenticationEntities db = new AuthenticationEntities())
            {

                userModel.password = Crypto.Hash(userModel.password);
                userModel.confirmpassword = Crypto.Hash(userModel.confirmpassword);

                if (db.Users.Any(x => x.username == userModel.username))
                {
                    ViewBag.DuplicateMessage = "Username already exist.";
                    return View("AddorEdit", userModel);
                }

                db.Users.Add(userModel);
                //  db.StudentMajors = db.StudentMajors.ToList();
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("AddorEdit", new User());

        }




        // GET: /Register/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Register/Login

        [HttpPost]
        public ActionResult Login(User login)
        {
            using (AuthenticationEntities db = new AuthenticationEntities())
            {
                login.password = Crypto.Hash(login.password);
                var userDetails = db.Users.Where(x => x.username == login.username && x.password == login.password).FirstOrDefault();
                if (userDetails == null)
                {
                    //login.LoginErrorMessage = "Wrong username of password";
                    ViewBag.ErrorMessage = "Wrong username or password";
                    return View("Login", login);
                }
                else
                {
                    Session["UserId"] = userDetails.UserId;
                    return RedirectToAction("GetPosts", "Comments");
                }
            }

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Register");
        }


    }
}