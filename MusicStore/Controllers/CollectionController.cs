using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MusicStore.Models;
using MusicStore.Models.View_Models.Collection;

namespace MusicStore.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collection
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var albums = db.Users.First(u => u.Id == currentUserId).Albums;

            var collectionViewModel = new CollectionViewModel()
            {
                Albums = albums.ToList()
            };

            return View(collectionViewModel);
        }

        // GET: Collection/Details/id
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}