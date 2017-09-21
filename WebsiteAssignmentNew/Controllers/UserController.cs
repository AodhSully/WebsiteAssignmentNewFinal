using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteAssignmentNew.Models;

namespace WebsiteAssignmentNew.Controllers
{
    public class UserController : Controller
    {
        DAO dao = new DAO();
        // GET: User
        public ActionResult Index()
        {
            return View("Register");
        }

        [HttpPost]

        public ActionResult Register(UserModel user)
        {
            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.Insert(user);
                if (count == 1)
                {
                    ViewBag.Status = "New user created";
                }
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                return View("Status");
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            ModelState.Remove("Email");
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("ComparePassword");

            if (ModelState.IsValid)
            {
                if (user.UserRole == Role.Staff &&
                    user.UserName == "ultan" &&
                    user.Password == "ubadcat"
                    ||
                    user.UserRole == Role.Staff &&
                    user.UserName == "aodh" &&
                    user.Password == "abadcat"
                    ||
                    user.UserRole == Role.Staff &&
                    user.UserName == "graham" &&
                    user.Password == "gbadcat"
                    )
                {
                    Session["name"] = "Staff";
                    return RedirectToAction("ShowFeedback", "Contact"); //"Index", "ShowFeedback" "Contact Us", "Index", "Contact"
                }
                else if (user.UserRole == Role.Customer)
                {
                    user.FirstName = dao.CheckLogin(user);
                    if (user.FirstName != null)
                    {
                        //Session.Add("name",user.FirstName);
                        Session["name"] = user.FirstName;
                        Session["UserName"] = user.UserName;
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ViewBag.Status = "Error! " + dao.message;
                        return View("Status");
                    }

                }
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                    return View("Status");
                }

            }
            else return View(user);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return View("../Home/Index");
        }
    }
}