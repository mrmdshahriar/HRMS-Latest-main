using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class KeyResultsController : Controller
    {
        private HRMS.Models.HRMS db = new HRMS.Models.HRMS();

        // GET: KeyResults
        public ActionResult Index()
        {
            var keyResults = db.KeyResults.Include(k => k.HrmEmployee).Include(k => k.KeyObjective);
            return View(keyResults.ToList());
        }

        // GET: KeyResults/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyResult keyResult = db.KeyResults.Find(id);
            if (keyResult == null)
            {
                return HttpNotFound();
            }
            return View(keyResult);
        }

        // GET: KeyResults/Create
        public ActionResult Create()
        {
            ViewBag.AssignedTo = new SelectList(db.HrmEmployees.Where(x => x.Active == true), "Id", "FirstName");
            ViewBag.KeyObjectiveId = new SelectList(db.KeyObjectives.Where(x => x.Active == true), "Id", "DefineObjective");
            return PartialView("CreatePartialView");
        }

        // POST: KeyResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DefineKeyResult,KeyObjectiveId,AssignedTo,AssignDate,EndDate,Priority,Status,AssignedPercentage,CompletedPercentage,Remarks,CompletionDate,PendingDays,Active,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,Deleted")] KeyResult keyResult)
        {
            if (ModelState.IsValid)
            {
                db.KeyResults.Add(keyResult);
                db.SaveChanges();
                TempData["Message"] = "Saved Successfully";
                return RedirectToAction("KeyResults", "Performance", new { deptName = Session["DeptName"] });
            }

            ViewBag.AssignedTo = new SelectList(db.HrmEmployees.Where(x => x.Active == true), "Id", "FirstName", keyResult.AssignedTo);
            ViewBag.KeyObjectiveId = new SelectList(db.KeyObjectives.Where(x => x.Active == true), "Id", "DefineObjective", keyResult.KeyObjectiveId);
            return View(keyResult);
        }

        // GET: KeyResults/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyResult keyResult = db.KeyResults.Find(id);
            if (keyResult == null)
            {
                return HttpNotFound();
            }

          var list = new List<SelectListItem>()
                {
                   new SelectListItem{ Text="Select Priority", Value = "" },
                   new SelectListItem{ Text="Low", Value = "Low" },
                   new SelectListItem{ Text="Medium", Value = "Medium" },
                   new SelectListItem{ Text="High", Value = "High" },
                };

            ViewBag.Priority = new SelectList(list,"Value","Text", keyResult.Priority);

            var list2 = new List<SelectListItem>()
                {
                   new SelectListItem{ Text="Select Status", Value = "" },
                   new SelectListItem{ Text="InProgress", Value = "InProgress" },
                   new SelectListItem{ Text="Stuck", Value = "Stuck" },
                   new SelectListItem{ Text="Completed", Value = "Completed" },
                   new SelectListItem{ Text="NotDone", Value = "NotDone" }
                };


            ViewBag.Status = new SelectList(list2, "Value", "Text", keyResult.Status);

            ViewBag.AssignedTo = new SelectList(db.HrmEmployees.Where(x => x.Active == true), "Id", "FirstName", keyResult.AssignedTo);
            ViewBag.KeyObjectiveId = new SelectList(db.KeyObjectives.Where(x => x.Active == true), "Id", "DefineObjective", keyResult.KeyObjectiveId);
            return PartialView("EditPartialView", keyResult);
        }

        // POST: KeyResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DefineKeyResult,KeyObjectiveId,AssignedTo,AssignDate,EndDate,Priority,Status,AssignedPercentage,CompletedPercentage,Remarks,CompletionDate,PendingDays,Active,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,Deleted")] KeyResult keyResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyResult).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Updated Successfully";
                return RedirectToAction("KeyResults", "Performance", new { deptName = Session["DeptName"] });
            }
            ViewBag.AssignedTo = new SelectList(db.HrmEmployees.Where(x => x.Active == true), "Id", "FirstName", keyResult.AssignedTo);
            ViewBag.KeyObjectiveId = new SelectList(db.KeyObjectives.Where(x => x.Active == true), "Id", "DefineObjective", keyResult.KeyObjectiveId);
            return View(keyResult);
        }

        // GET: KeyResults/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyResult keyResult = db.KeyResults.Find(id);
            if (keyResult == null)
            {
                return HttpNotFound();
            }
            return PartialView("DeletePartialView", keyResult);
        }

        // POST: KeyResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            KeyResult keyResult = db.KeyResults.Find(id);
            db.KeyResults.Remove(keyResult);
            db.SaveChanges();

            TempData["Message"] = "Deleted Successfully";
            return RedirectToAction("KeyResults", "Performance", new { deptName = Session["DeptName"] });
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
