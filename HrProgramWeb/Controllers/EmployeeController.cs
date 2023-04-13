using HrProgramWeb.Data;
using HrProgramWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HrProgramWeb.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IEnumerable<Employee> employees = new List<Employee>();

            employees = _db.Employees.Where(x => x.ApplicationUserId == userId);

            return View(employees);
        }


        //GET
        public IActionResult Create()
        {
            
            return View();
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            //Note: if you check the ModelState.IsValid, it will return false, because there is no ApplicationID and PizzaID,
            //you can create a view model to enter the new value, then, convert it to PizzaModel
            //validate the model
            //if (ModelState.IsValid)
            //{
            //get current user id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                //based on the userid to find current user and get its pizzas.
                var currentuser = _db.ApplicationUsers.Include(c => c.Employees).First(c => c.Id == userId);
                List<Employee> employes = new List<Employee>();
                employes = currentuser.Employees.ToList();

                //add the new item to employ list
                employes.Add(new Employee()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    BirthDate = model.BirthDate,
                    PersonalIdentityNumber=  model.PersonalIdentityNumber,
                    Gender = model.Gender,
                    Position = model.Position,
                    Status = model.Status,
                    ReleaseDate = model.ReleaseDate,
                    MobileNumber = model.MobileNumber


                });
                //update the Employes for current user.
                currentuser.Employees = employes;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
            //}
            //else
            //{
            //    return View();
            //}
        }

        public IActionResult Edit(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            var employeeFromDb = _db.Employees.FirstOrDefault(x => x.Id == Id);
            if(employeeFromDb == null)
            {
                return NotFound();
            }
            return View(employeeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee obj)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var employeeFromDb = _db.Employees.FirstOrDefault(x => x.Id == Id);
            if (employeeFromDb == null)
            {
                return NotFound();
            }
            return View(employeeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Employees.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
           

                _db.Employees.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
           
            
        }

    }
}
