using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtClubActivityManagement;
using ArtClubActivityManagement.ViewModel;
using System.Data.Entity.Infrastructure;
using System.Linq.Dynamic; //sort
using System.Web.Helpers; //gridView

namespace ArtClubActivityManagement.Controllers
{
    [Authorize(Roles = "Membru,Non-Membru,Administrator")]
    public class EvenimentController : Controller
    {
        private ArtClubEntities21 db = new ArtClubEntities21();

        // GET: /Eveniment/
        public ActionResult Index(int page = 1, string sort = "ID_Data", string sortdir = "asc", string search = "")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetTables(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }

        public List<Eveniment> GetTables(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (ArtClubEntities21 dc = new ArtClubEntities21())
            {
                var v = (from a in dc.Eveniments
                         where
                                 a.ID_NumeEveniment.Contains(search) ||
                                 a.ID_NumeResursa.Contains(search) ||
                                 a.NumarZile.Contains(search) ||
                                 a.ID_Data.Contains(search) ||
                                 a.Ora.Contains(search) ||
                                 a.ID_TipPlata.Contains(search) 
                         select a
                               );
                totalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);   //linq.dynamic
                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }
                return v.ToList();
            }
        }

        private void PopulateAssignedMembriiDaa (Eveniment eveniment)
        {
            var allMembrii = db.Membriis;
            var evenimentMembru = new HashSet<string>(eveniment.Membriis.Select(c => c.ID_Username));
            var viewModel = new List<AssignedMembriiDaa>();
            foreach (var membru in allMembrii)
            {
                viewModel.Add(new AssignedMembriiDaa
                {
                    ID_Username = membru.ID_Username,
                    Assigned = evenimentMembru.Contains(membru.ID_Username)
                });
            }
            ViewBag.Membrii = viewModel;
        }

        // GET: /Eveniment/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eveniment eveniment = db.Eveniments.Find(id);
            if (eveniment == null)
            {
                return HttpNotFound();
            }
            return View(eveniment);
        }

        // GET: /Eveniment/Create
        public ActionResult Create()
        {
            var eveniment = new Eveniment();
            eveniment.Membriis = new List<Membrii>();
            PopulateAssignedMembriiDaa(eveniment);

            ViewBag.ID_TipPlata = new SelectList(db.Functies, "ID_NumeFunctie", "ID_NumeFunctie");        
            ViewBag.ID_NumeResursa = new SelectList(db.Resurses, "ID_NumeResursa", "ID_NumeResursa");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_NumeEveniment,ID_NumeResursa,NumarZile,ID_Data,Ora,ID_TipPlata")] Eveniment eveniment, string[] selectedMembrii)
        {
            //if (eveniment.ID_NumeResursa == "Disponibil")
            //  {
            //            if (ModelState.IsValid)
            //         {
            //              string test = eveniment.ID_NumeResursa;
            //            if (eveniment.Resurse.ID_Status == "Disponibil")
            //              {

            if (selectedMembrii != null)
            {
                eveniment.Membriis = new List<Membrii>();
                foreach (var membru in selectedMembrii)
                {
                    var membruToAdd = db.Membriis.Find(membru);
                    eveniment.Membriis.Add(membruToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Eveniments.Add(eveniment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_TipPlata = new SelectList(db.Functies, "ID_NumeFunctie", "ID_NumeFunctie", eveniment.ID_TipPlata);
            ViewBag.ID_NumeResursa = new SelectList(db.Resurses, "ID_NumeResursa", "ID_NumeResursa", eveniment.ID_NumeResursa);

            PopulateAssignedMembriiDaa(eveniment);
            return View(eveniment);
        }

        // GET: /Eveniment/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eveniment eveniment = db.Eveniments.Find(id);
            if (eveniment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_TipPlata = new SelectList(db.Functies, "ID_NumeFunctie", "ID_NumeFunctie", eveniment.ID_TipPlata);
            ViewBag.ID_NumeResursa = new SelectList(db.Resurses, "ID_NumeResursa", "ID_NumeResursa", eveniment.ID_NumeResursa);
            return View(eveniment);
        }

        // POST: /Eveniment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID_NumeEveniment,ID_NumeResursa,NumarZile,ID_Data,Ora,ID_TipPlata")] Eveniment eveniment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eveniment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TipPlata = new SelectList(db.Functies, "ID_NumeFunctie", "ID_NumeFunctie", eveniment.ID_TipPlata);
            ViewBag.ID_NumeResursa = new SelectList(db.Resurses, "ID_NumeResursa", "ID_NumeResursa", eveniment.ID_NumeResursa);
            return View(eveniment);
        }

        // GET: /Eveniment/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eveniment eveniment = db.Eveniments.Find(id);
            if (eveniment == null)
            {
                return HttpNotFound();
            }
            return View(eveniment);
        }

        // POST: /Eveniment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Eveniment eveniment = db.Eveniments.Find(id);
            db.Eveniments.Remove(eveniment);
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
