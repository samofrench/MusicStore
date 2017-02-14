using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using MusicStore.Enums;
using MusicStore.Models;

namespace MusicStore.DataAccessLayer
{
    public class MusicInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MusicContext>
    {

        protected override void Seed(MusicContext context)
        {
            const string lvb = "Beethoven";
            const string wam = "Mozart";
            const string jsb = "Bach";
            const string fc = "Chopin";
            const string gm = "Mahler";
            const string bs5 = "Symphony no. 5 in C Minor Op. 67";
            const string mpc25 = "Piano Concerto no. 25 in C K. 503";
            const string mpc27 = "Piano Concerto no. 27 in B-flat K. 595";
            const string bsq14 = "String Quartet no. 14 in C-Sharp Minor Op. 131";
            const string cb1 = "Ballade no. 1 in G Minor, op. 23";
            const string cb2 = "Ballade no. 2 in F, op. 38";
            const string cb3 = "Ballade no. 3 in A-Flat, op. 47";
            const string cb4 = "Ballade no. 4 in F Minor, op. 52";
            const string vs1 = "Sonata no. 1 in G Minor for Violin Solo BWV 1001";
            const string vp1 = "Partita no. 1 in B Minor for Violin Solo BWV 1002";
            const string vs2 = "Sonata no. 2 in A Minor for Violin Solo BWV 1003";
            const string vp2 = "Partita no. 2 in D Minor for Violin Solo BWV 1004";
            const string vs3 = "Sonata no. 3 in C for Violin Solo BWV 1005";
            const string vp3 = "Partita no. 3 in E for Violin Solo BWV 1006";
            const string rl1 = "Columbia";
            const string rl2 = "Deutsche Grammophon";
            const string rl3 = "EMI";
            const string rl4 = "RCA";
            const string rl5 = "Musical Heritage Society";
            const string rl6 = "Philips";
            const string cat1 = "MS 6468";
            const string cat2 = "2530 642";
            const string cat3 = "14-3664-1";
            const string cat4 = "LM-2370";
            const string cat5 = "MHS 1460/1/2";
            const string per1 = "Leonard Bernstein";
            const string per2 = "New York Philharmonic";
            const string per3 = "Claudio Abbado";
            const string per4 = "Friedrich Gulda";
            const string per5 = "Vienna Philharmonic";
            const string per6 = "Alban Berg Quartet";
            const string per7 = "Artur Rubinstein";
            const string per8 = "Roman Totenberg";
            const string role1 = "Conductor";
            const string role2 = "Orchestra";
            const string role3 = "Piano";
            const string role4 = "String Quartet";
            const string role5 = "Violin";

            // Composers
            var composersList = new List<Composer>
            {
                new Composer("Ludwig van", lvb, 1770, 1827),
                new Composer("Wolfgang Amadeus", wam, 1756, 1791),
                new Composer("Johann Sebastian", jsb, 1685, 1750),
                new Composer("Frederic", fc, 1810, 1849),
                new Composer("Gustav", gm, 1860, 1911)
            };

            context.Composers.AddRange(composersList);
            context.SaveChanges();

            // Get composer IDs
            var beethoven = composersList.First(c => c.LastName == lvb);
            var mozart = composersList.First(c => c.LastName == wam);
            var bach = composersList.First(c => c.LastName == jsb);
            var chopin = composersList.First(c => c.LastName == fc);
            var mahler = composersList.First(c => c.LastName == gm);

            // Pieces
            var piecesList = new List<Piece>
            {
                new Piece (beethoven, beethoven.Id, bs5),
                new Piece (mozart, mozart.Id, mpc25),
                new Piece (mozart, mozart.Id, mpc27),
                new Piece (beethoven, beethoven.Id, bsq14),
                new Piece (chopin, chopin.Id, cb1),
                new Piece (chopin, chopin.Id, cb2),
                new Piece (chopin, chopin.Id, cb3),
                new Piece (chopin, chopin.Id, cb4),
                new Piece (bach, bach.Id, vs1),
                new Piece (bach, bach.Id, vp1),
                new Piece (bach, bach.Id, vs2),
                new Piece (bach, bach.Id, vp2),
                new Piece (bach, bach.Id, vs3),
                new Piece (bach, bach.Id, vp3),
            };

            context.Pieces.AddRange(piecesList);
            context.SaveChanges();

            // Get piece IDs
            var p1 = context.Pieces.First(p => p.Name == bs5);
            var p2 = context.Pieces.First(p => p.Name == mpc25);
            var p3 = context.Pieces.First(p => p.Name == mpc27);
            var p4 = context.Pieces.First(p => p.Name == bsq14);
            var p5 = context.Pieces.First(p => p.Name == cb1);
            var p6 = context.Pieces.First(p => p.Name == cb2);
            var p7 = context.Pieces.First(p => p.Name == cb3);
            var p8 = context.Pieces.First(p => p.Name == cb4);
            var p9 = context.Pieces.First(p => p.Name == vs1);
            var p10 = context.Pieces.First(p => p.Name == vp1);
            var p11 = context.Pieces.First(p => p.Name == vs2);
            var p12 = context.Pieces.First(p => p.Name == vp2);
            var p13 = context.Pieces.First(p => p.Name == vs3);
            var p14 = context.Pieces.First(p => p.Name == vp3);

            // Labels
            var recordLabelsList = new List<RecordLabel>
            {
                new RecordLabel {Name = rl1, Country = Country.US},
                new RecordLabel {Name = rl2, Country = Country.Germany},
                new RecordLabel {Name = rl3, Country = Country.UK},
                new RecordLabel {Name = rl4, Country = Country.US},
                new RecordLabel {Name = rl5, Country = Country.US},
                new RecordLabel {Name = rl6, Country = Country.Netherlands}
            };

            context.Labels.AddRange(recordLabelsList);
            context.SaveChanges();

            // Labels
            var cbs = recordLabelsList.First(l => l.Name == rl1);
            var dgg = recordLabelsList.First(l => l.Name == rl2);
            var emi = recordLabelsList.First(l => l.Name == rl3);
            var rca = recordLabelsList.First(l => l.Name == rl4);
            var mhs = recordLabelsList.First(l => l.Name == rl5);
            var pps = recordLabelsList.First(l => l.Name == rl6);


            // Albums
            var albumsList = new List<Album>
            {
                new Album
                {
                    Name = "Beethoven Symphony no. 5 in C Minor op. 67",
                    RecordLabel = cbs,
                    CatNo = cat1,
                    ArtworkUrl =
                        "https://img.discogs.com/-68710UPDWkBpYt0vui9eJd98HY=/fit-in/536x537/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-3809255-1345264879-7966.jpeg.jpg",
                    Notes = string.Empty,
                    Country = Country.US,
                    Year = 1963,
                    Discs = 1,
                    Audio = Audio.Stereo
                },
                new Album
                {
                    Name = "Mozart Piano Concertos nr. 25 & 27",
                    RecordLabel = dgg,
                    CatNo = cat2,
                    ArtworkUrl =
                        "https://img.discogs.com/JPbWCcr_Rc86iNhFEjfBV1Ectvs=/fit-in/600x595/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-3424580-1329867292.jpeg.jpg",
                    Notes = string.Empty,
                    Country = Country.Germany,
                    Year = 1976,
                    Discs = 1,
                    Audio = Audio.Stereo
                },
                new Album
                {
                    Name = "Beethoven String Quartet Op. 131",
                    RecordLabel = emi,
                    CatNo = cat3,
                    ArtworkUrl =
                        "https://img.discogs.com/FEe5CW0PP79WHwpY1EqZ3uGvPus=/fit-in/600x581/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-8380010-1460548682-7107.jpeg.jpg",
                    Notes = string.Empty,
                    Country = Country.Germany,
                    Year = 1984,
                    Discs = 1,
                    Audio = Audio.Stereo
                },
                new Album
                {
                    Name = "The Chopin Ballades",
                    RecordLabel = rca,
                    CatNo = cat4,
                    ArtworkUrl =
                        "https://img.discogs.com/IOMVH8u4xNjCW7ALT5xxfrnEcpI=/fit-in/600x598/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-1672133-1300642927.jpeg.jpg",
                    Notes = string.Empty,
                    Country = Country.US,
                    Year = 1960,
                    Discs = 1,
                    Audio = Audio.Mono
                },
                new Album
                {
                    Name = "The Six Sonatas and Partitas for Solo Violin",
                    RecordLabel = mhs,
                    CatNo = cat5,
                    ArtworkUrl =
                        "https://img.discogs.com/u1UOBr9y8mQ_tZIH38I8-FSsnvo=/fit-in/600x591/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-8354348-1459966099-2208.jpeg.jpg",
                    Notes = "Performed on the \"Ames\" Stradivarius",
                    Country = Country.US,
                    Discs = 3,
                    Audio = Audio.Stereo
                }
            };

            context.Albums.AddRange(albumsList);
            context.SaveChanges();

            // Get album IDs
            var album1 = albumsList.First(a => a.CatNo == cat1);
            var album2 = albumsList.First(a => a.CatNo == cat2);
            var album3 = albumsList.First(a => a.CatNo == cat3);
            var album4 = albumsList.First(a => a.CatNo == cat4);
            var album5 = albumsList.First(a => a.CatNo == cat5);

            // Performers
            var performersList = new List<Performer>
            {
                new Performer(per1),
                new Performer(per2),
                new Performer(per3),
                new Performer(per4),
                new Performer(per5),
                new Performer(per6),
                new Performer(per7),
                new Performer(per8)
            };

            context.Performers.AddRange(performersList);
            context.SaveChanges();

            // Get performer IDs
            var Per1 = context.Performers.First(p => p.Name == per1);
            var Per2 = context.Performers.First(p => p.Name == per2);
            var Per3 = context.Performers.First(p => p.Name == per3);
            var Per4 = context.Performers.First(p => p.Name == per4);
            var Per5 = context.Performers.First(p => p.Name == per5);
            var Per6 = context.Performers.First(p => p.Name == per6);
            var Per7 = context.Performers.First(p => p.Name == per7);
            var Per8 = context.Performers.First(p => p.Name == per8);

            // Recordings
            var recordingsList = new List<Recording>
            {
                new Recording(album1.Id, album1, p1.Id, p1),
                new Recording(album2.Id, album2, p2.Id, p2),
                new Recording(album2.Id, album2, p3.Id, p3),
                new Recording(album3.Id, album3, p4.Id, p4),
                new Recording(album4.Id, album4, p5.Id, p5),
                new Recording(album4.Id, album4, p6.Id, p6),
                new Recording(album4.Id, album4, p7.Id, p7),
                new Recording(album4.Id, album4, p8.Id, p8),
                new Recording(album5.Id, album5, p9.Id, p9),
                new Recording(album5.Id, album5, p10.Id, p10),
                new Recording(album5.Id, album5, p11.Id, p11),
                new Recording(album5.Id, album5, p12.Id, p12),
                new Recording(album5.Id, album5, p13.Id, p13),
                new Recording(album5.Id, album5, p14.Id, p14)
            };

            context.Recordings.AddRange(recordingsList);
            context.SaveChanges();

            // Get recordings IDs
            var r1 = context.Recordings.First(r => r.AlbumId == album1.Id && r.PieceId == p1.Id);
            var r2 = context.Recordings.First(r => r.AlbumId == album2.Id && r.PieceId == p2.Id);
            var r3 = context.Recordings.First(r => r.AlbumId == album2.Id && r.PieceId == p3.Id);
            var r4 = context.Recordings.First(r => r.AlbumId == album3.Id && r.PieceId == p4.Id);
            var r5 = context.Recordings.First(r => r.AlbumId == album4.Id && r.PieceId == p5.Id);
            var r6 = context.Recordings.First(r => r.AlbumId == album4.Id && r.PieceId == p6.Id);
            var r7 = context.Recordings.First(r => r.AlbumId == album4.Id && r.PieceId == p7.Id);
            var r8 = context.Recordings.First(r => r.AlbumId == album4.Id && r.PieceId == p8.Id);
            var r9 = context.Recordings.First(r => r.AlbumId == album5.Id && r.PieceId == p9.Id);
            var r10 = context.Recordings.First(r => r.AlbumId == album5.Id && r.PieceId == p10.Id);
            var r11 = context.Recordings.First(r => r.AlbumId == album5.Id && r.PieceId == p11.Id);
            var r12 = context.Recordings.First(r => r.AlbumId == album5.Id && r.PieceId == p12.Id);
            var r13 = context.Recordings.First(r => r.AlbumId == album5.Id && r.PieceId == p13.Id);
            var r14 = context.Recordings.First(r => r.AlbumId == album5.Id && r.PieceId == p14.Id);

            // Credits
            var creditsList = new List<Credit>
            {
                new Credit(Per1.Id, Per1, r1.RecordingId, r1, role1),
                new Credit(Per2.Id, Per2, r1.RecordingId, r1, role2),

                new Credit(Per3.Id, Per3, r2.RecordingId, r2, role1),
                new Credit(Per4.Id, Per4, r2.RecordingId, r2, role3),
                new Credit(Per5.Id, Per5, r2.RecordingId, r2, role2),

                new Credit(Per3.Id, Per3, r3.RecordingId, r3, role1),
                new Credit(Per4.Id, Per4, r3.RecordingId, r3, role3),
                new Credit(Per5.Id, Per5, r3.RecordingId, r3, role2),

                new Credit(Per6.Id, Per6, r4.RecordingId, r4, role4),

                new Credit(Per7.Id, Per7, r5.RecordingId, r5, role3),
                new Credit(Per7.Id, Per7, r6.RecordingId, r6, role3),
                new Credit(Per7.Id, Per7, r7.RecordingId, r7, role3),
                new Credit(Per7.Id, Per7, r8.RecordingId, r8, role3),

                new Credit(Per8.Id, Per8, r9.RecordingId, r9, role5),
                new Credit(Per8.Id, Per8, r10.RecordingId, r10, role5),
                new Credit(Per8.Id, Per8, r11.RecordingId, r11, role5),
                new Credit(Per8.Id, Per8, r12.RecordingId, r12, role5),
                new Credit(Per8.Id, Per8, r13.RecordingId, r13, role5),
                new Credit(Per8.Id, Per8, r14.RecordingId, r14, role5)
            };
        }
    }
}