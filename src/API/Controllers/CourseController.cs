using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using DLL.Model;
using DLL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

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
        public async Task<IActionResult> Insert([FromForm] CourseInsertRequestViewModel request)
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
        
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("testing")]
        public async Task<IActionResult> Testing()
        {
            var loginUser = new RequestMaker()
            {
                Principal = User
            };
            await _courseService.Testing(loginUser);
            return Ok("test");
        }
        
        
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,Roles = "admin")]
        [HttpGet("testing1")]
        public async Task<IActionResult> Testing1()
        {
            var loginUser = new RequestMaker()
            {
                Principal = User
            };
            
            return Ok("test1");
        }
        
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,Roles = "manager")]
        [HttpGet("testing2")]
        public async Task<IActionResult> Testing2()
        {
            var loginUser = new RequestMaker()
            {
                Principal = User
            };
            
            return Ok("test2");
        }
        
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,Roles = "supervisor")]
        [HttpGet("testing3")]
        public async Task<IActionResult> Testing3()
        {
            var loginUser = new RequestMaker()
            {
                Principal = User
            };
            
            return Ok("test3");
        }
        
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,Roles = "admin,manager")]
        [HttpGet("testing4")]
        public async Task<IActionResult> Testing4()
        {
            var loginUser = new RequestMaker()
            {
                Principal = User
            };
            
            return Ok("test4");
        }
    }

    
}