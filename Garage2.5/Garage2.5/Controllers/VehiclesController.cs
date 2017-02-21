using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2._5.DataAccessLayer;
using Garage2._5.Models;
using Garage2._5.ViewModels;

namespace Garage2._5.Controllers
{
    public class VehiclesController : Controller
    {
        private Garage2_5Context db = new Garage2_5Context();

        // GET: Vehicles
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.RegNrSortParm = sortOrder == "RegNr" ? "RegNr_desc" : "RegNr";
            ViewBag.MemberNameSortParm = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.TypSortParm = sortOrder == "Typ" ? "Typ_desc" : "Typ";
            ViewBag.FärgSortParm = sortOrder == "Färg" ? "Färg_desc" : "Färg";
            ViewBag.TidSortParm = sortOrder == "Tid" ? "Tid_desc" : "Tid";
            
            IQueryable<Vehicle> fordon = db.Vehicles;

            if (!String.IsNullOrEmpty(searchString))
            {
                fordon = fordon.Where(s => s.RegistrationNumber.Contains(searchString)
                                        || s.Members.Name.Contains(searchString)
                                        || s.Color.Contains(searchString)
                                        || s.Model.Contains(searchString)
                                        || s.Make.Contains(searchString)
                                        || s.Wheels.ToString().Contains(searchString)
                                        || s.VehicleTypes.Type.Contains(searchString)
                                        || s.CheckIn.ToString().Contains(searchString)
                                        );
                return View(fordon);
            }

            switch (sortOrder)
            {
                case "RegNr":
                    fordon = fordon.OrderBy(f => f.RegistrationNumber);
                    break;
                case "RegNr_desc":
                    fordon = fordon.OrderByDescending(f => f.RegistrationNumber);
                    break;
                case "Typ":
                    fordon = fordon.OrderBy(f => f.VehicleTypes.Type);
                    break;
                case "Typ_desc":
                    
                    fordon = fordon.OrderByDescending(f => f.VehicleTypes.Type);
                    break;
                case "Färg":
                    fordon = fordon.OrderBy(f => f.Color);
                    break;
                case "Färg_desc":
                    fordon = fordon.OrderByDescending(f => f.Color);
                    break;
                case "Tid":
                    fordon = fordon.OrderBy(f => f.CheckIn);
                    break;
                case "Tid_desc":
                    fordon = fordon.OrderByDescending(f => f.CheckIn);
                    break;
                case "Name":
                    fordon = fordon.OrderBy(f => f.Members.Name);
                    break;
                case "Name_desc":
                    fordon = fordon.OrderByDescending(f => f.Members.Name);
                    break;
                default:
                    break;
            }

            return View(fordon.ToList());

            //var vehicles = db.Vehicles.Include(v => v.Members).Include(v => v.VehicleTypes);
            //return View(vehicles.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name");
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegistrationNumber,Color,Make,Model,Wheels,MemberId,VehicleTypeId")] Vehicle vehicle)
        {
            var findFordon = from m in db.Vehicles
                             where vehicle.RegistrationNumber == m.RegistrationNumber
                             select m.RegistrationNumber;
            if (findFordon.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    vehicle.CheckIn = DateTime.Now;
                    vehicle.RegistrationNumber = vehicle.RegistrationNumber.ToUpper();
                    vehicle.Color = vehicle.Color.ToLower();
                    vehicle.Color = vehicle.Color.First().ToString().ToUpper() + vehicle.Color.Substring(1); //Stor första bokstav.
                    vehicle.Make = vehicle.Make.ToLower();
                    vehicle.Make = vehicle.Make.First().ToString().ToUpper() + vehicle.Make.Substring(1); //Stor första bokstav.
                    vehicle.Model = vehicle.Model.ToUpper();

                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            else
            {
                ViewBag.error = "Registration Already Exist!";
            }

                ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vehicle.MemberId);
                ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vehicle.VehicleTypeId);
                return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegistrationNumber,Color,Make,Model,Wheels,MemberId,VehicleTypeId")] Vehicle vehicle)
        {   

            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", vehicle.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            
            Kvito kvito = new Kvito();
            kvito.RegNumber = vehicle.RegistrationNumber;
            kvito.CheckInDate = vehicle.CheckIn;
            kvito.CheckOutDate = DateTime.Now;
            kvito.MemberName = vehicle.Members.Name;
            kvito.VehicleTypeName = vehicle.VehicleTypes.Type;
            TimeSpan currenttime = kvito.CheckOutDate - kvito.CheckInDate;
            kvito.TotalPrice = (int)currenttime.TotalMinutes;

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Receipt",kvito);           

        }

        public ActionResult Receipt(Kvito kvito)
        {

            return View(kvito);
        }

        public ActionResult Stats()
        {
            ViewBag.car = 0;
            ViewBag.bus = 0;
            ViewBag.motorcycle = 0;
            ViewBag.boat = 0;
            ViewBag.airplane = 0;
            foreach (var item in db.Vehicles)
            {
                switch (item.VehicleTypes.Type)
                {
                    case "Car":
                        ViewBag.car += 1;
                        break;
                    case "Bus":
                        ViewBag.bus += 1;
                        break;
                    case "Motorcycle":
                        ViewBag.motorcycle += 1;
                        break;
                    case "Boat":
                        ViewBag.boat += 1;
                        break;
                    case "Airplane":
                        ViewBag.airplane += 1;
                        break;
                    default:
                        break;
                }

            }
            ViewBag.TotalHjul = 0;

            foreach (var item in db.Vehicles)
            {
                ViewBag.TotalHjul = ViewBag.TotalHjul + item.Wheels;
            }
            ViewBag.TotalTid = 0;


            double TotalMinutesOfParking = 0;
            foreach (var item in db.Vehicles)
            {

                TotalMinutesOfParking = Math.Round(TotalMinutesOfParking + (DateTime.Now - item.CheckIn).TotalMinutes);

            }
            @ViewBag.TotalVehicles = db.Vehicles.Count();
            ViewBag.count = TotalMinutesOfParking;
            ViewBag.TotalTid = TotalMinutesOfParking;
            return View();
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
