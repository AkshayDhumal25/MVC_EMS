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
        public async Task<IActionResult> Index()
        {
            var employees = await _context.EmployeeMasters
                .Where(e => !e.IsDeleted)
                .Include(e => e.Country)
                .Include(e => e.State)
                .Include(e => e.City)
                .ToListAsync();
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
    }
}
