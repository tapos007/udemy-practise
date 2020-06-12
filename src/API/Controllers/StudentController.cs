using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.Model;
using DLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class StudentController : MainApiController
    {
        private readonly IStudentRepository _studentRepository;


        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] string rollNumber, [FromQuery] string nickName)
        {
            return Ok(await _studentRepository.GetAllAsync());
        }

        [HttpGet("{email}")]
        public async Task<ActionResult> GetA(string email)
        {
            return Ok(await  _studentRepository.GetAAsync(email));
        }
        
        
        [HttpPost]
        public async Task<ActionResult> Insert([FromForm] Student student)
        {
            return Ok(await _studentRepository.InsertAsync(student));
        }
        
        [HttpPut("{email}")]
        public async Task<ActionResult> Update(string email,Student student)
        {
            return Ok(await _studentRepository.UpdateAsync(email,student));
        }
        
        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            return Ok(await _studentRepository.DeleteAsync(email));
        }
    }
    
    
    
}