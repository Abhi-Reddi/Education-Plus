using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduPlus.WebUI.Controllers
{
    public class NotFoundController : Controller
    {
        public ActionResult Index(string entity, string backUrl)
        {
            ViewBag.Entity = entity;
            ViewBag.BackUrl = backUrl;
            return View();
        }
    }
}