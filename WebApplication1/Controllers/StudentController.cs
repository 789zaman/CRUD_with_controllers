using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DB;
using WebApplication1.DM;
using WebApplication1.DM.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MstStudents>>> GetAllStudent()
        {
            return await dbContext.MstStudents.ToListAsync();


        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<MstStudents>> StudentbyId(int id)
        {
            var student = await dbContext.MstStudents.FirstOrDefaultAsync(x => x.Id==id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MstStudents>> RemoveStudent(int id)
        {
            var student = await dbContext.MstStudents.FirstOrDefaultAsync(x =>x.Id==id);

            if (student == null)
            {
                return NotFound();
            }
            dbContext.MstStudents.Remove(student);
            await dbContext.SaveChangesAsync();
            return student;

        }


        //public ActionResult<MstStudents> CreateStudent([FromBody] MstStudents obj) {
        //    if (!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }
        //    if (dbContext.MstStudents.FirstOrDefault(x => x.Name.ToLower() == obj.Name.ToLower()) != null) {
        //        ModelState.AddModelError("","Name Already Exists");
        //        return BadRequest(ModelState);
        //    }

        //  var result =  dbContext.MstStudents.Add(obj);
        //    return Ok(result);

        //}
        [HttpPost]
        public async Task<ActionResult<MstStudents>> Addstudent(MstStudents student)
        {
            dbContext.MstStudents.Add(student);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(student), new { id = student.Id }, student);
        }
    }
}