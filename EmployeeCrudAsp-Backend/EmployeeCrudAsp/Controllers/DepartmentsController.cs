using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeCrudAsp.Data;
using AutoMapper;
using EmployeeCrudAsp.Models.Department;

namespace EmployeeCrudAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly EmsDbContext _context;
        private readonly IMapper mapper;

        public DepartmentsController(EmsDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentReadOnlyDto>>> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            var departmentDtos = mapper.Map<IEnumerable<DepartmentReadOnlyDto>>(departments);
            return Ok(departmentDtos);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            var departmentDto = mapper.Map<DepartmentReadOnlyDto>(department);
            return Ok(department);
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, DepartmentUpdateDto departmentDtos)
        {
            if (id != departmentDtos.DId)
            {
                return BadRequest();
            }

            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            mapper.Map(departmentDtos, department);
            _context.Entry(department).State= EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DepartmentExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DepartmentCreateDto>> PostDepartment(DepartmentCreateDto departmentDto)
        {
            var department = mapper.Map<Department>(departmentDto);
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.DId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> DepartmentExists(int id)
        {
            return await _context.Departments.AnyAsync(e => e.DId == id);
        }
    }
}
