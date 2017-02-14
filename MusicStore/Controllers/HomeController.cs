using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using MusicStore.DataAccessLayer;
using MusicStore.Models.View_Models;
using MusicStore.Models.View_Models.About;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private MusicContext db = new MusicContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var aboutViewModel = new AboutViewModel();

            // Label by album count
            IQueryable<LabelAlbumGroup> laData = from album in db.Albums
                group album by album.RecordLabel
                into labelGroup
                select new LabelAlbumGroup()
                {
                    RecordLabel = labelGroup.Key,
                    AlbumCount = labelGroup.Count()
                };

            // Composer by number of pieces
            IQueryable<ComposerPieceGroup> cpData = from piece in db.Pieces
                group piece by piece.Composer
                into composerGroup
                select new ComposerPieceGroup()
                {
                    Composer = composerGroup.Key,
                    PieceCount = composerGroup.Count()
                };

            // Composer by aalbum count
            var caData = new List<ComposerAlbumGroup>();
            foreach (var composer in db.Composers)
            {
                var albumCount = composer.Pieces.SelectMany(p => p.Recordings).Select(r => r.Album).Distinct().Count();
                var composerGroup = new ComposerAlbumGroup()
                {
                    Composer = composer,
                    AlbumCount = albumCount
                };

                caData.Add(composerGroup);
            }

            // Performer by album count
            var paData = new List<PerformerAlbumGroup>();
            foreach (var performer in db.Performers)
            {
                var albumCount = performer.Credits.Select(c => c.Recording).Select(r => r.Album).Distinct().Count();
                var performerGroup = new PerformerAlbumGroup()
                {
                    Performer = performer,
                    AlbumCount = albumCount
                };

                paData.Add(performerGroup);
            }

            aboutViewModel.LabelAlbumData = laData.OrderBy(l => l.AlbumCount).ToList();
            aboutViewModel.ComposerPieceData = cpData.OrderBy(c => c.PieceCount).ToList();
            aboutViewModel.ComposerAlbumData = caData.OrderByDescending(c => c.AlbumCount).ToList();
            aboutViewModel.PerformerAlbumData = paData.OrderByDescending(p => p.AlbumCount).ToList();

            return View(aboutViewModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}