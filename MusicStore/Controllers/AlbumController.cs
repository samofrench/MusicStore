using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MusicStore.DataAccessLayer;
using MusicStore.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using PagedList.EntityFramework;

namespace MusicStore.Controllers
{
    public class AlbumController : Controller
    {
        private MusicContext db = new MusicContext();
        private int pageSize = 10;

        // GET: Album
        public async Task<ViewResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;

            var albums = from a in db.Albums select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(a => a.Name.Contains(searchString) || a.RecordLabel.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    albums = albums.OrderByDescending(a => a.Name);
                    break;
                case "Date":
                    albums = albums.OrderBy(a => a.Year);
                    break;
                case "date_desc":
                    albums = albums.OrderByDescending(a => a.Year);
                    break;
                default:
                    albums = albums.OrderBy(a => a.Name);
                    break;
            }

            int pageNumber = (page ?? 1);

            return View(await albums.ToPagedListAsync(pageNumber, pageSize));
        }

        // GET: Album/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Album/Create
        public ActionResult Create()
        {
            PopulateLabelsDropdown();
            return View();
        }

        // POST: Album/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,RecordLabelId,RecordLabel,CatNo,ArtworkUrl,Notes,Country,Year,Discs,Audio")] Album album)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Albums.Add(album);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException exception)
            {
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateLabelsDropdown(album.RecordLabelId);
            return View(album);
        }

        // GET: Album/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = await db.Albums.FindAsync(id);

            if (album == null)
            {
                return HttpNotFound();
            }

            PopulateLabelsDropdown(album.RecordLabelId);

            return View(album);
        }

        // POST: Album/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var albumToUpdate = await db.Albums.FindAsync(id);
            if (TryUpdateModel(albumToUpdate, "",
                new string[] { "Name", "RecordLabelId", "RecordLabel", "CatNo", "ArtworkUrl", "Notes", "Country", "Year", "Discs", "Audio"}))
            {
                try
                {
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException exception)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            PopulateLabelsDropdown(albumToUpdate.RecordLabelId);

            return View(albumToUpdate);
        }

        // GET: Album/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage =
                    "Delete failed. Try again, and if the problem persists contact your system administrator.";
            }
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Album/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Album album = await db.Albums.FindAsync(id);
                db.Albums.Remove(album);
                await db.SaveChangesAsync();
            }
            catch (RetryLimitExceededException exception)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

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

        private void PopulateLabelsDropdown(object selectedLabel = null)
        {
            var labelsQuery = db.Labels.OrderByDescending(l => l.Albums.Count);

            ViewBag.RecordLabelId = new SelectList(labelsQuery, "Id", "Name", selectedLabel);
        }

    }
}
