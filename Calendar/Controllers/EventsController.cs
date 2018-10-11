using Calendar.Helper;
using Calendar.ViewModel;
using MyMVCWebsite.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Calendar.Models;
using System.Web;

namespace Calendar.Controllers
{
    [RoutePrefix("Events")]
    [AuthenticationAttribute]
    public class EventsController : Controller
    {
        // GET: Events
        [OutputCache(Duration = 5)]
        public async Task<ActionResult> Index()
        {
            // Retreiving all events
            IList<EventModels> events = await HttpHelper.getEvents();

            // here checking admin role to show/hide manage roles link
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.isAdmin = "No";

                if (UserHelper.isAdminUser(user.GetUserId()))
                {
                    ViewBag.isAdmin = "Yes";
                }

            }

            IList<EventViewModel> lstEventViewModel = EventHelper.modelToViewModel(events);

            return View(lstEventViewModel);

        }

        // GET: Events/Details/5
        [ErrorAttribute]
        [OutputCache(Duration = 5)]
        [Route("Details/{id:regex(\\d{4}):min(1)}")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "HTTP/1.1 400 Bad Request");
            }
            // Getting an event by Id
            EventModels eventEntity = await HttpHelper.getEvent_Id(Convert.ToInt32(id));

            if (eventEntity.Id == 0)
            {
                throw new HttpException(400, "HTTP/1.1 400 Bad Request");
            }
            else
            {
                EventViewModel eventViewModelEntity = EventHelper.modelToViewModel(eventEntity);

                return PartialView(eventViewModelEntity);
            }
        }

        [OutputCache(Duration = 5)]
        [Authorize(Roles = "Admin,Adding")]
        // GET: Events/CustomCreat
        public async Task<ActionResult> CustomCreat()
        {
            IList<EventModels> events = await HttpHelper.getEvents();

            CustomEventViewModel eventViewModel = EventHelper.composeCustomEvent(events);
            // returning custom entity for displaying the events also while creating new one
            return View(eventViewModel);

        }


        // POST: Events/CustomCreat
        [HttpPost]
        [ErrorAttribute]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CustomCreat([Bind(Include = "Title,Description,StartDate,EndDate")] EventViewModel pEvent)
        {

            if (ModelState.IsValid)
            {
                EventModels eventEnity = EventHelper.viewmodelToModel(pEvent);
                // Sending an event entity to be added through the API
                eventEnity = await HttpHelper.Create(eventEnity);
                // If not added then new enity will be received with Id = 0
                if (eventEnity.Id != 0)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    throw new HttpException(500, "Internal Server Error");
                }
            }
            else
            {
                return RedirectToAction("CustomCreat");
            }


        }

        [HttpGet]
        [Route("AllEvents_DescriptionFilter/{pTitle?}")]
        public async Task<PartialViewResult> AllEvents_TitleFilter(string pTitle)
        {
            // getting all events that has title contains the pTitle, when pTitle is null,all events will be retreived
            IList<EventModels> events = await HttpHelper.getEvents(pTitle);
            IList<EventViewModel> eventViewModels = EventHelper.modelToViewModel(events);

            return PartialView("Listing", eventViewModels);

        }
    }
}