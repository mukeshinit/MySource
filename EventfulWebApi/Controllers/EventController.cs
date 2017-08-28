using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventfulWebApi.Service.Service;
using EventfulWebApi.Service.Interface;
using EventfulWebApi.Models;
using log4net;
using log4net.Config;
using log4net.Core;
using System.Reflection;
namespace EventfulWebApi.Controllers
{
    public class EventController : Controller
    {
        private EventService eventService = new EventService();
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Event
        public ActionResult Index()
        {
            try
            {
                DateTime st = new DateTime(2017, 8, 18);
                DateTime et = new DateTime(2017, 8, 26);
                EventSearchResults result = eventService.eventSearchResults("49.227684,-122.993332", 10, 
                    st, et, "Concerts & Tour Dates");
                if (result == null)
                {
                    return View();
                }                 
            }
            catch (Exception ex)
            {
                string errorMeaasge = "Error occured while retriving the Event Search. Error Message : " + ex.Message;
                Logger.Error(errorMeaasge);
              
            }
            return View();
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
