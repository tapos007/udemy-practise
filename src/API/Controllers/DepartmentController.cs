using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("get all student");
        }

        [HttpGet("{code}")]
        public IActionResult GetA(string code)
        {
            return Ok("get this" + code + " department data");
        }
        
        
        [HttpPost]
        public IActionResult Insert()
        {
            return Ok("insert new department");
        }
        
        [HttpPut("{code}")]
        public IActionResult Update(string code)
        {
            return Ok("update this" + code + " department data");
        }
        
        [HttpDelete("{code}")]
        public IActionResult Delete(string code)
        {
            return Ok("delete this" + code + " department data");
        }
        
    }
}