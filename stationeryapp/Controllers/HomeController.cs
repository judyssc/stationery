﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using stationeryapp.Models;

namespace stationeryapp.Controllers
{
    public class HomeController : Controller
    {
        private StoreClerkDBContext db = new StoreClerkDBContext();
        private RequisitionFormsDBContext db1 = new RequisitionFormsDBContext();
        public ActionResult Index(string sessionId)
        { 
            StoreClerk storeclerk = db.StoreClerks.Where(p => p.SessionId == sessionId).First();
            if (storeclerk != null && sessionId != null)
            {
                ViewData["num"] = db1.RequisitionForms.Where(x => x.Status == "Pending").Count();
                ViewData["sessionId"] = storeclerk.SessionId;
                ViewData["username"] = storeclerk.UserName;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}