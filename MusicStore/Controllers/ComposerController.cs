using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MusicStore.DataAccessLayer;
using MusicStore.Models;
using MusicStore.Models.View_Models.Composer;
using PagedList;

namespace MusicStore.Controllers
{
    public class ComposerController : Controller
    {
        private MusicContext db = new MusicContext();
        private const int page_size = 10;

        // GET: Composer
        public ActionResult Index(string sortOrder, int page = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.BirthSortParam = sortOrder == "BYear" ? "byear_desc" : "BYear";
            ViewBag.AlbumSortParam = sortOrder == "Album" ? "album_desc" : "Album";

            var composersVm = new List<ComposerViewModel>();
            var composers = from c in db.Composers select c;

            switch (sortOrder)
            {
                case "name_desc":
                    composers = composers.OrderByDescending(c => c.LastName);
                    break;
                case "BYear":
                    composers = composers.OrderBy(c => c.BirthYear);
                    break;
                case "byear_desc":
                    composers = composers.OrderByDescending(c => c.BirthYear);
                    break;
                case "Album":
                    composers = composers.OrderByDescending(c => c.Pieces.SelectMany(p => p.Recordings).Select(r => r.AlbumId).Distinct().Count());
                    break;
                case "album_desc":
                    composers = composers.OrderBy(c => c.Pieces.SelectMany(p => p.Recordings).Select(r => r.AlbumId).Distinct().Count());
                    break;
                default:
                    composers = composers.OrderBy(c => c.LastName);
                    break;
            }

            foreach (var c in composers)
            {
                var albums = c.Pieces.SelectMany(p => p.Recordings).Select(r => r.Album).Distinct().ToList();
                var pieces = c.Pieces.ToList();
                var performers = c.Pieces.SelectMany(p => p.Recordings).SelectMany(r => r.Credits).Select(cr => cr.Performer).Distinct().ToList();
                composersVm.Add(new ComposerViewModel
                {
                    ComposerId = c.Id,
                    FullName = c.FullName,
                    BirthYear = c.BirthYear,
                    DeathYear = c.DeathYear,
                    Albums = albums,
                    AlbumsCount = albums.Count(),
                    Pieces = pieces,
                    PiecesCount = pieces.Count(),
                    Performers = performers,
                    PerformersCount = performers.Count(),
                });
            }

            int pageSize = page_size;
            int pageNumber = page;

            return View(composersVm.ToPagedList(pageNumber, pageSize));
        }

        // GET: Composer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var compDetailVm = new ComposerViewModel();
            Composer composer = db.Composers.Find(id);
            if (composer == null)
            {
                return HttpNotFound();
            }

            // todo mapper?
            compDetailVm.ComposerId = composer.Id;
            compDetailVm.FullName = composer.FullName;
            compDetailVm.BirthYear = composer.BirthYear;
            compDetailVm.DeathYear = composer.DeathYear;

            // albums
            var albumsData = composer.Pieces.SelectMany(p => p.Recordings).Select(r => r.Album).Distinct().ToList();
            compDetailVm.Albums = albumsData;
            compDetailVm.AlbumsCount = albumsData.Count();

            // pieces
            compDetailVm.Pieces = composer.Pieces.ToList();
            compDetailVm.PiecesCount = composer.Pieces.Count();

            // performers
            var performersData = composer.Pieces.SelectMany(p => p.Recordings).SelectMany(r => r.Credits).Select(c => c.Performer).Distinct().ToList();
            compDetailVm.Performers = performersData;
            compDetailVm.PerformersCount = performersData.Count();

            return View(compDetailVm);
        }

        // GET: Composer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Composer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,BirthYear,DeathYear")] Composer composer)
        {
            if (ModelState.IsValid)
            {
                db.Composers.Add(composer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(composer);
        }

        // GET: Composer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composer composer = db.Composers.Find(id);
            if (composer == null)
            {
                return HttpNotFound();
            }
            return View(composer);
        }

        // POST: Composer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,BirthYear,DeathYear")] Composer composer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(composer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(composer);
        }

        // GET: Composer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composer composer = db.Composers.Find(id);
            if (composer == null)
            {
                return HttpNotFound();
            }
            return View(composer);
        }

        // POST: Composer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Composer composer = db.Composers.Find(id);
            db.Composers.Remove(composer);
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
