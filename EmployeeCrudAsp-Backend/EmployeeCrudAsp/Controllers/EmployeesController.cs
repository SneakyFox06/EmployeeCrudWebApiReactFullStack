using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeCrudAsp.Data;
using AutoMapper;
using EmployeeCrudAsp.Models.Employee;
using AutoMapper.QueryableExtensions;

namespace EmployeeCrudAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmsDbContext _context;
        private readonly IMapper mapper;

        public EmployeesController(EmsDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadOnlyDto>>> GetEmployees()
        {
            var employeeDtos = await _context.Employees
                .Include(q => q.Department)
                .ProjectTo<EmployeeReadOnlyDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(employeeDtos);

        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetailsDto>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employeeDtos = await _context.Employees
                .Include(q => q.Department)
                .ProjectTo<EmployeeDetailsDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.EId == id);

            if (employeeDtos == null)
            {
                return NotFound();
            }

            return Ok(employeeDtos);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeUpdateDto employeeDto)
        {
            if (id != employeeDto.EId)
            {
                return BadRequest();
            }

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            mapper.Map(employeeDto, employee);
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeCreateDto>> PostEmployee(EmployeeCreateDto employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EId }, employee);

        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> EmployeeExists(int id)
        {
            return await _context.Employees.AnyAsync(e => e.EId == id);
        }
    }
}
