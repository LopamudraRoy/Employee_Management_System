using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CompanyApplication.Models;

namespace CompanyApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private CompanydetailsEntities db = new CompanydetailsEntities();

        // GET: Employee
        public ViewResult Search(string q)
        {
            var persons = from p in db.Employees select p;
            if(!string.IsNullOrEmpty(q))
            {
                persons=persons.Where(p=>p.employee_name.Contains(q));
            }
            return View(persons);
        }
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Category).Include(e => e.Designation);
            return View(employees.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create

        //public ActionResult Index(string searching)
        //{
        //    //ViewData["Getemployeedetails"]=Empsearch;
        //    //var empquery = from x in db.Employees select x;
        //    //if(!string.IsNullOrEmpty(Empsearch))
        //    //{
        //    //    empquery=empquery.Where(x=>x.employee_name.Contains(Empsearch));
        //    //}

        //    //return View(await empquery.AsNoTracking().ToListAsync());
        //}

        public ActionResult Create()
        {
            ViewBag.cat_id = new SelectList(db.Categories, "id", "Category1");
            ViewBag.desig_id = new SelectList(db.Designations, "id", "Designation1");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,employee_name,cat_id,desig_id,address,salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cat_id = new SelectList(db.Categories, "id", "Category1", employee.cat_id);
            ViewBag.desig_id = new SelectList(db.Designations, "id", "Designation1", employee.desig_id);
            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_id = new SelectList(db.Categories, "id", "Category1", employee.cat_id);
            ViewBag.desig_id = new SelectList(db.Designations, "id", "Designation1", employee.desig_id);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,employee_name,cat_id,desig_id,address,salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_id = new SelectList(db.Categories, "id", "Category1", employee.cat_id);
            ViewBag.desig_id = new SelectList(db.Designations, "id", "Designation1", employee.desig_id);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
