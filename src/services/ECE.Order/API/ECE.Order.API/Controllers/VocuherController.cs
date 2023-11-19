using ECE.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECE.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VocuherController : MainController
    {
    }
}
