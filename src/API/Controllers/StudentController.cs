using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using DLL.Model;
using DLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class StudentController : MainApiController
    {
        private readonly IStudentService _studentService;


        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _studentService.GetAllAsync());
        }

        [HttpGet("{email}")]
        public async Task<ActionResult> GetA(string email)
        {
            return Ok(await  _studentService.GetAAsync(email));
        }
        
        
        [HttpPost]
        public async Task<ActionResult> Insert(Student student)
        {
            return Ok(await _studentService.InsertAsync(student));
        }
        
        [HttpPut("{email}")]
        public async Task<ActionResult> Update(string email,Student student)
        {
            return Ok(await _studentService.UpdateAsync(email,student));
        }
        
        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            return Ok(await _studentService.DeleteAsync(email));
        }
    }
    
    
    
}