using EventfulWebApi.Models;
using EventfulWebApi.Service.Service;
using log4net;
using System;
using System.Reflection;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EventfulWebApi.Controllers
{
    public class EventController : Controller
    {
        private EventService eventService = new EventService();
        private EventCategoryService eventCategoryService = new EventCategoryService();
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Event
        [HttpGet]
        public ActionResult Index()
        {
            EventSearchResults result = null;
            Events events = null;
            try
            {
                DateTime st = new DateTime(2017, 8, 18);
                DateTime et = new DateTime(2017, 8, 26);
                GetEventCatetory();
               
                result = eventService.GetEventSearchResults("49.227684,-122.993332", 10,
                    st, et, "Concerts & Tour Dates");

                events = result.events;

            }
            catch (Exception ex)
            {
                string errorMeaasge = "Error occured while retriving the Event Search. Error Message : " + ex.Message;
                Logger.Error(errorMeaasge);
                return null;
              
            }
            return View("Index", events);
        }
         
        public void GetEventCatetory()
        {
            EventCategories eventCategory = eventCategoryService.GetEventCategory();
            var selectlist = eventCategory.category                
                .Select(x =>
                        new SelectListItem
                        {
                            Value = x.name,
                            Text = x.name
                        });        
            ViewBag.EventTypes = new SelectList(selectlist.OrderBy(x => x.Value), "Value", "Text");
        }
        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewD(string search)
        {
            DateTime st = new DateTime(2017, 8, 18);
            DateTime et = new DateTime(2017, 8, 26);
            EventSearchResults result = null;
            result = eventService.GetEventSearchResults("49.227684,-122.993332", 10,
                st, et, "Concerts & Tour Dates");
            return View("Index", result);
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
        /// <summary>  
        /// Override the Json Result with Max integer JSON lenght  
        /// </summary>  
        /// <param name="data">Data</param>  
        /// <param name="contentType">Content Type</param>  
        /// <param name="contentEncoding">Content Encoding</param>  
        /// <param name="behavior">Behavior</param>  
        /// <returns>As JsonResult</returns>  
        protected override JsonResult Json(object data, string contentType,Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}
