using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DB;
using WebApplication1.DM.Entities;

namespace WebApplication1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CoursesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MstCourses>>> GetAllCourses()
        {
            return await dbContext.MstCourses.ToListAsync();


        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<MstCourses>> CoursebyId(int id)
        {
            var student = await dbContext.MstCourses.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return BadRequest("invalid id");
            }

            return student;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MstCourses>> RemoveCourse(int id)
        {
            var student = await dbContext.MstCourses.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return BadRequest("invalid id");
            }
            dbContext.MstCourses.Remove(student);
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
        public async Task<ActionResult<MstCourses>> Addcourse(MstCourses course)
        {
            dbContext.MstCourses.Add(course);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("Get Result", new { id = course.Id }, course);
        }
    }

}

