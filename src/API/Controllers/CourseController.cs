using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using DLL.Model;
using DLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class CourseController : MainApiController
    {
        private readonly ICourseService _courseService;


        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult>  GetAll()
        {
            
            return Ok( await _courseService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetA(string code)
        {
            return Ok(await _courseService.GetAAsync(code));
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Insert(CourseInsertRequestViewModel request)
        {
            return Ok(await _courseService.InsertAsync(request));
        }
        
        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code,Course course)
        {
            return Ok(await _courseService.UpdateAsync(code,course));
        }
        
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await _courseService.DeleteAsync(code));
        }
        
    }

    
}