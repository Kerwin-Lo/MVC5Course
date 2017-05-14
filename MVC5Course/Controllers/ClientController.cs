﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ClientController : BaseController
    {
        // GET: Client
        public ActionResult BatchUpdate()
        {
            GetClients();
            return View();
        }

        private void GetClients()
        {
            var data = db.Client.OrderByDescending(c => c.ClientId).Take(10);
            ViewData.Model = data;
        }

        [HttpPost]
        public ActionResult BatchUpdate(ClientBatchUpdateVM[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)

                {
                    var c = db.Client.Find(item.ClientId);
                    c.FirstName = item.FirstName;
                    c.MiddleName = item.MiddleName;
                    c.LastName = item.LastName;
                }
                db.SaveChanges();
                return RedirectToAction("BatchUpdate");
            }
            GetClients();
            return View();
        }
    }
}