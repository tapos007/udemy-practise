using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiVersion( "1.0" )]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MainApiController : ControllerBase
    {
        
        
    }
}