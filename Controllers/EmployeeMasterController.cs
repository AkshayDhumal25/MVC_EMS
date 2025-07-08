using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_EMS.Data;
using MVC_EMS.Models;

namespace MVC_EMS.Controllers
{
    public class EmployeeMasterController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeMasterController(AppDbContext context)
        {
            _context = context;
        }

        // List employees
        //public async Task<IActionResult> Index()
        //{
        //    var employees = await _context.EmployeeMasters
        //        .Where(e => !e.IsDeleted)
        //        .Include(e => e.Country)
        //        .Include(e => e.State)
        //        .Include(e => e.City)
        //        .ToListAsync();
        //    return View(employees);
        //}
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "EmployeeMaster") });
            }

            if (HttpContext.Session.GetString("IsAdmin") != "True")
            {
                return Content("Access Denied: Only admins allowed.");
            }

            var employees = _context.EmployeeMasters
                .Where(e => !e.IsDeleted)
                .Include(e => e.Country)
                .Include(e => e.State)
                .Include(e => e.City)
                .ToList();

            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var countries = _context.countries
                .Select(c => new SelectListItem
                {
                    Value = c.CountryId.ToString(),
                    Text = c.CountryName
                })
                .ToList();

            var model = new EmployeeFormViewModel
            {
                Employee = new EmployeeMaster(),
                Countries = countries
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns if validation fails
                model.Countries = _context.countries
                    .Select(c => new SelectListItem
                    {
                        Value = c.CountryId.ToString(),
                        Text = c.CountryName
                    })
                    .ToList();

                return View(model);
            }

            // Set extra fields
            model.Employee.CreatedDate = DateTime.Now;
            model.Employee.IsDeleted = false;
            model.Employee.PanNumber = model.Employee.PanNumber?.ToUpper();
            model.Employee.PassportNumber = model.Employee.PassportNumber?.ToUpper();

            _context.EmployeeMasters.Add(model.Employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        // JSON: Get States by CountryId
        [HttpGet]
        public JsonResult GetStates(int countryId)
        {
            var states = _context.states
                .Where(s => s.CountryId == countryId)
                .Select(s => new
                {
                    state_Id = s.StateId,
                    stateName = s.StateName
                })
                .ToList();

            return Json(states);
        }

        // JSON: Get Cities by StateId
        [HttpGet]
        public JsonResult GetCities(int stateId)
        {
            var cities = _context.cities
                .Where(c => c.StateId == stateId)
                .Select(c => new
                {
                    city_Id = c.CityId,
                    cityName = c.CityName
                })
                .ToList();

            return Json(cities);
        }

        public IActionResult Details(int id)
        {
            var employee = _context.EmployeeMasters
                .Include(e => e.Country)
                .Include(e => e.State)
                .Include(e => e.City)
                .FirstOrDefault(e => e.Emp_Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var employee = _context.EmployeeMasters.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.EmployeeMasters.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var employee = _context.EmployeeMasters.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Populate dropdowns
            ViewBag.Countries = _context.countries
                .Select(c => new SelectListItem { Value = c.CountryId.ToString(), Text = c.CountryName }).ToList();

            ViewBag.States = _context.states
                .Where(s => s.CountryId == employee.CountryId)
                .Select(s => new SelectListItem { Value = s.StateId.ToString(), Text = s.StateName }).ToList();

            ViewBag.Cities = _context.cities
                .Where(c => c.StateId == employee.StateId)
                .Select(c => new SelectListItem { Value = c.CityId.ToString(), Text = c.CityName }).ToList();

            return View(employee);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeMaster employee)
        {
            if (! ModelState.IsValid)
            {
                var existingEmployee = _context.EmployeeMasters.Find(employee.Emp_Id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Update properties
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.EmailAddress = employee.EmailAddress;
                existingEmployee.MobileNumber = employee.MobileNumber;
                existingEmployee.CountryId = employee.CountryId;
                existingEmployee.StateId = employee.StateId;
                existingEmployee.CityId = employee.CityId;
                existingEmployee.UpdatedDate = DateTime.Now;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            // If invalid, reload dropdowns
            ViewBag.Countries = _context.countries
                .Select(c => new SelectListItem { Value = c.CountryId.ToString(), Text = c.CountryName }).ToList();

            ViewBag.States = _context.states
                .Where(s => s.CountryId == employee.CountryId)
                .Select(s => new SelectListItem { Value = s.StateId.ToString(), Text = s.StateName }).ToList();

            ViewBag.Cities = _context.cities
                .Where(c => c.StateId == employee.StateId)
                .Select(c => new SelectListItem { Value = c.CityId.ToString(), Text = c.CityName }).ToList();

            return View(employee);
        }





    }
}
