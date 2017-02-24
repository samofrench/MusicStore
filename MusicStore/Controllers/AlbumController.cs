using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MusicStore.Models;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MusicStore.Models.View_Models.Album;
using PagedList.EntityFramework;

namespace MusicStore.Controllers
{
    public class AlbumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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
        [Authorize]
        public ActionResult Create()
        {

            //PopulateLabelsDropdown();
            ViewBag.CurrentPage = "_Page1";




            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Page1(CreateAlbumViewModel viewModel)
        {
            ViewBag.CurrentPage = "_Page1";

            return View("Create", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Page2(CreateAlbumViewModel viewModel)
        {
            ViewBag.CurrentPage = "_Page2";

            return View("Create", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Page3(CreateAlbumViewModel viewModel)
        {
            ViewBag.CurrentPage = "_Page3";

            return View("Create", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAlbumViewModel viewModel)
        {
            return RedirectToAction("Index");

            //db.SaveChangesAsync();

            //return RedirectToAction("Details", new {id = album.Id});
        }

        // POST: Album/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(
        //    [Bind(Include = "Name,RecordLabelId,RecordLabel,CatNo,ArtworkUrl,Notes,Country,Year,Discs,Audio")] Album
        //        album)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Albums.Add(album);
        //            await db.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (RetryLimitExceededException exception)
        //    {
        //        ModelState.AddModelError("",
        //            "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //    }

        //    PopulateLabelsDropdown(album.RecordLabelId);
        //    return View(album);
        //}

        // GET: Album/Edit/5
        [Authorize(Roles = "canEditUsers")]
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
        [Authorize(Roles = "canEditUsers")]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id, byte[] rowVersion)
        {
            string[] fieldsToBind = new string[]
            {
                "Name", "RecordLabelId", "CatNo", "ArtworkUrl", "Notes", "Country", "Year", "Discs",
                "Audio", "RowVersion"
            };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var albumToUpdate = await db.Albums.FindAsync(id);
            if (albumToUpdate == null)
            {
                Album deletedAlbum = new Album();
                TryUpdateModel(deletedAlbum, fieldsToBind);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. This Album has been deleted by another user.");
                ViewBag.RecordLabelId = new SelectList(db.Labels, "Id", "Name", deletedAlbum.RecordLabelId);
                return View(deletedAlbum);
            }

            if (TryUpdateModel(albumToUpdate, fieldsToBind))
            {
                try
                {
                    db.Entry(albumToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Album) entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The album was already deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Album) databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                            ModelState.AddModelError("Name", string.Format("Current value: {0}", databaseValues.Name));
                        if (databaseValues.ArtworkUrl != clientValues.ArtworkUrl)
                            ModelState.AddModelError("ArtworkUrl",
                                string.Format("Current value: {0}", databaseValues.ArtworkUrl));
                        if (databaseValues.CatNo != clientValues.CatNo)
                            ModelState.AddModelError("CatNo", string.Format("Current value: {0}", databaseValues.CatNo));
                        if (databaseValues.Notes != clientValues.Notes)
                            ModelState.AddModelError("Notes", string.Format("Current value: {0}", databaseValues.Notes));
                        if (databaseValues.RecordLabelId != clientValues.RecordLabelId)
                            ModelState.AddModelError("RecordLabelId",
                                string.Format("Current value: {0}", databaseValues.RecordLabelId));
                        if (databaseValues.Audio != clientValues.Audio)
                            ModelState.AddModelError("Audio", string.Format("Current value: {0}", databaseValues.Audio));
                        if (databaseValues.Country != clientValues.Country)
                            ModelState.AddModelError("Country", string.Format("Current value: {0}", databaseValues.Country));
                        if (databaseValues.Discs != clientValues.Discs)
                            ModelState.AddModelError("Discs", string.Format("Current value: {0}", databaseValues.Discs));
                        if (databaseValues.Year != clientValues.Year)
                            ModelState.AddModelError("Year", string.Format("Current value: {0}", databaseValues.Year));
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                                               +
                                                               "was modified by another user after you got the original value. The "
                                                               +
                                                               "edit operation was canceled and the current values in the database "
                                                               +
                                                               "have been displayed. If you still want to edit this record, click "
                                                               +
                                                               "the Save button again. Otherwise click the Back to List hyperlink.");
                        albumToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException dex)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            PopulateLabelsDropdown(albumToUpdate.RecordLabelId);

            return View(albumToUpdate);
        }

        // GET: Album/Delete/5
        [Authorize(Roles = "canEditUsers")]
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = await db.Albums.FindAsync(id);

            if (album == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }

                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }
            return View(album);
        }

        // POST: Album/Delete/5
        [Authorize(Roles = "canEditUsers")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Album album)
        {
            try
            {
                db.Entry(album).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new {concurrencyError = true, id = album.Id});
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(album);
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

        private void PopulateLabelsDropdown(object selected = null)
        {
            var query = db.Labels.OrderByDescending(l => l.Albums.Count);

            ViewBag.RecordLabelId = new SelectList(query, "Id", "Name", selected);
        }

        private void PopulatePiecesDropdown(object selected = null)
        {
            var query = db.Pieces.OrderBy(p => p.Composer.LastName);
            ViewBag.PieceId = new SelectList(query, "Id", "Name", selected);
        }

        private void PopulateComposersDropdown(object selected = null)
        {
            var query = db.Composers.OrderBy(p => p.LastName);
            ViewBag.ComposerId = new SelectList(query, "Id", "AlphaName", selected);
        }

        private void PopulatePerformersDropdown(object selected = null)
        {
            var query = db.Performers.OrderBy(p => p.Name);
            ViewBag.PerfoermerId = new SelectList(query, "Id", "Name", selected);
        }

        public ActionResult _AddAlbum(Album album)
        {
            return PartialView("_AddAlbum", album);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _AddAlbum(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            bool hasAlbum = db.UserAlbums.Any(u => u.ApplicationUserId == currentUserId && u.AlbumId == id);
            Album album = await db.Albums.FirstAsync(a => a.Id == id);
            ApplicationUser owner = await db.Users.FirstAsync(u => u.Id == currentUserId);

            if (!hasAlbum)
            {
                db.UserAlbums.Add(new UserAlbum()
                {
                    AlbumId = id,
                    ApplicationUserId = User.Identity.GetUserId(),
                    Album = album,
                    Owner = owner,
                    Sound = string.Empty,
                    Condition = string.Empty,
                    Eq = string.Empty,
                    Notes = string.Empty,
                    Clean = true,
                    Donate = false,
                    Needs = false,
                    Sell = false
                });

                await db.SaveChangesAsync();
            }

            return RedirectToAction("Details", album);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _RemoveAlbum(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            UserAlbum toDelete = await db.UserAlbums.FirstAsync(u => u.ApplicationUserId == currentUserId && u.AlbumId == id);
            Album album = await db.Albums.FirstAsync(a => a.Id == id);

            if (toDelete != null)
            {
                db.UserAlbums.Remove(toDelete);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Details", album);
        }

    }

}
