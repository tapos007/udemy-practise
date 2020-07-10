using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : MainApiController
    {
        private readonly ITestService _testService;
        private readonly ITransactionService _transactionService;

        public TestController(ITestService testService,ITransactionService transactionService)
        {
            _testService = testService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           // await _testService.AddNewRoles();
            // await _testService.AddNewUser();
            await _transactionService.FinancialTransaction();
            return Ok("hello world");
        }
    }
}