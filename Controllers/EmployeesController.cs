using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.Entities;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbcontext;

        public EmployeesController(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbcontext.Employees.ToList();

            return Ok(allEmployees);

        }


        [HttpGet]

        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbcontext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();

            }

            return Ok(employee);



        }


        [HttpPost]

        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {

                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,

            };

            dbcontext.Employees.Add(employeeEntity);
            dbcontext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbcontext.Employees.Find(id);
            if (employee == null)
            {
                return BadRequest();
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dbcontext.Employees.Update(employee); dbcontext.SaveChanges();

            return Ok(employee);

        }

        [HttpDelete]

        [Route("{id:guid}")]

        public IActionResult DeleteEmployee(Guid id)

        {
            var employee = dbcontext.Employees.Find(id);

            if (employee == null)

            {
                return BadRequest();
            }

            dbcontext.Employees.Remove(employee);
            dbcontext.SaveChanges();

            return Ok(employee);
        }
    }
}
