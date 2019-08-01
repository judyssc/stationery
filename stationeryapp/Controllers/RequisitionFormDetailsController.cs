﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using stationeryapp.Models;

namespace stationeryapp.Controllers
{
    public class RequisitionFormDetailsController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        private RequisitionFormsDBContext db1 = new RequisitionFormsDBContext();
        private StoreClerkDBContext db2 = new StoreClerkDBContext();
        // GET: RequisitionFormDetails
        public ActionResult Index()
        {
            var requisitionFormDetails = db.RequisitionFormDetails.Include(r => r.RequisitionForm).Include(r => r.StationeryCatalog);
            return View(requisitionFormDetails.ToList());
        }
        // GET: RequisitionFormDetails/Details/5
        public ActionResult Details(string id, string sessionId)
        {
            StoreClerk storeclerk = db2.StoreClerks.Where(p => p.SessionId == sessionId).First();
            RequisitionForm request = db1.RequisitionForms.Where(p => p.FormNumber == id).First();
            RequisitionFormDetail detail = db.RequisitionFormDetails.Where(x => x.FormNumber == id).First();
            if (storeclerk != null && sessionId != null)
            {
                if (request.Status == "Pending")
                {
                    request.ReceivedBy = storeclerk.Id;
                }
                request.Status = "Received";
                db1.Entry(request).State = EntityState.Modified;
                db1.SaveChanges();
                ViewData["sessionId"] = storeclerk.SessionId;
                ViewData["username"] = storeclerk.UserName;
                return View(detail);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
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
