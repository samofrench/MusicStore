using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MusicStore.Models;
using MusicStore.Models.View_Models.Performer;
using PagedList;

namespace MusicStore.Controllers
{
    [Authorize(Roles = "canEditUsers")]
    public class PerformerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const int page_size = 10;

        // GET: Performer
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, int page = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.RoleSortParam = sortOrder == "Role" ? "role_desc" : "Role";
            var performersVm = new List<PerformerViewModel>();
            var performers = from p in db.Performers select p;

            switch (sortOrder)
            {
                case "name_desc":
                    performers = performers.OrderByDescending(p => p.Name);
                    break;
                case "Role":
                    performers = performers.OrderBy(p => p.Credits.FirstOrDefault().Role);
                    break;
                case "role_desc":
                    performers = performers.OrderByDescending(p => p.Credits.FirstOrDefault().Role);
                    break;
                default:
                    performers = performers.OrderBy(p => p.Name);
                    break;
            }

            foreach (var p in performers)
            {
                var roles = p.Credits.Select(c => c.Role).Distinct().ToList();
                performersVm.Add(new PerformerViewModel
                {
                    PerformerId = p.Id,
                    Name = p.Name,
                    Roles = roles
                });
            }

            int pageSize = page_size;
            int pageNumber = page;

            return View(performersVm.ToPagedList(pageNumber, pageSize));
        }

        // GET: Performer/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var perfDetailVm = new PerformerViewModel();
            Performer performer = db.Performers.Find(id);
            if (performer == null)
            {
                return HttpNotFound();
            }

            // todo mapper?
            perfDetailVm.PerformerId = performer.Id;
            perfDetailVm.Name = performer.Name;

            // roles
            perfDetailVm.Roles = performer.Credits.Select(c => c.Role).Distinct().ToList();

            // albums
            var albumsData = performer.Credits.Select(c => c.Recording).Select(r => r.Album).Distinct().ToList();
            perfDetailVm.Albums = albumsData;
            perfDetailVm.AlbumsCount = albumsData.Count();

            // composers

            var composersData = performer.Credits.Select(c => c.Recording).Select(r => r.Piece).Select(p => p.Composer).Distinct().ToList();
            perfDetailVm.Composers = composersData;
            perfDetailVm.ComposersCount = composersData.Count();

            return View(perfDetailVm);
        }

        // GET: Performer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Performer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Performer performer)
        {
            if (ModelState.IsValid)
            {
                db.Performers.Add(performer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(performer);
        }

        // GET: Performer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performer performer = db.Performers.Find(id);
            if (performer == null)
            {
                return HttpNotFound();
            }
            return View(performer);
        }

        // POST: Performer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Performer performer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(performer);
        }

        // GET: Performer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performer performer = db.Performers.Find(id);
            if (performer == null)
            {
                return HttpNotFound();
            }
            return View(performer);
        }

        // POST: Performer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Performer performer = db.Performers.Find(id);
            db.Performers.Remove(performer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
