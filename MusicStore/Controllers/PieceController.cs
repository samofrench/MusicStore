using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MusicStore.DataAccessLayer;
using MusicStore.Models;
using MusicStore.Models.View_Models.Piece;
using PagedList;

namespace MusicStore.Controllers
{
    [Authorize(Roles = "canEditUsers")]
    public class PieceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const int page_size = 10;

        // GET: Piece
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, int page = 1)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CompSortParam = string.IsNullOrEmpty(sortOrder) ? "comp_desc" : "";
            ViewBag.PieceSortParam = sortOrder == "Piece" ? "piece_desc" : "Piece";
            var pieces = from p in db.Pieces.Include(p => p.Composer) select p;

            switch (sortOrder)
            {
                case "comp_desc":
                    pieces = pieces.OrderByDescending(p => p.Composer.LastName);
                    break;
                case "Piece":
                    pieces = pieces.OrderBy(p => p.Name);
                    break;
                case "piece_desc":
                    pieces = pieces.OrderByDescending(p => p.Name);
                    break;
                default:
                    pieces = pieces.OrderBy(p => p.Composer.LastName);
                    break;
            }

            int pageSize = page_size;
            int pageNumber = page;

            return View(pieces.ToPagedList(pageNumber, pageSize));
        }

        // GET: Piece/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pieceDetailVm = new PieceDetailsViewModel();

            Piece piece = db.Pieces.Find(id);
            if (piece == null)
            {
                return HttpNotFound();
            }

            // todo mapper?
            pieceDetailVm.PieceId = piece.Id;
            pieceDetailVm.Name = piece.Name;
            pieceDetailVm.ComposerId = piece.ComposerId;
            pieceDetailVm.Composer = piece.Composer;
            
            // albums
            var albumsData = piece.Recordings.Select(r => r.Album).Distinct().ToList();
            pieceDetailVm.Albums = albumsData;
            pieceDetailVm.AlbumsCount = albumsData.Count();

            // performers

            var performersData = piece.Recordings.SelectMany(p => p.Credits).Select(c => c.Performer).Distinct().ToList();
            pieceDetailVm.Performers = performersData;
            pieceDetailVm.PerformersCount = performersData.Count();           

            return View(pieceDetailVm);
        }

        // GET: Piece/Create
        public ActionResult Create()
        {
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "FirstName");
            return View();
        }

        // POST: Piece/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ComposerId,Name")] Piece piece)
        {
            if (ModelState.IsValid)
            {
                db.Pieces.Add(piece);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "FirstName", piece.ComposerId);
            return View(piece);
        }

        // GET: Piece/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piece piece = db.Pieces.Find(id);
            if (piece == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "FirstName", piece.ComposerId);
            return View(piece);
        }

        // POST: Piece/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ComposerId,Name")] Piece piece)
        {
            if (ModelState.IsValid)
            {
                db.Entry(piece).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "FirstName", piece.ComposerId);
            return View(piece);
        }

        // GET: Piece/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piece piece = db.Pieces.Find(id);
            if (piece == null)
            {
                return HttpNotFound();
            }
            return View(piece);
        }

        // POST: Piece/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Piece piece = db.Pieces.Find(id);
            db.Pieces.Remove(piece);
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
