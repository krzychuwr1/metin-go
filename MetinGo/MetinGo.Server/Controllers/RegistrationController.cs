using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Registration;
using MetinGo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetinGo.Server.Controllers
{
	[Route("api/"+Endpoints.Registration)]
	public class RegistrationController : Controller
	{
	    private readonly IUserService _userService;

	    public RegistrationController(IUserService userService)
	    {
		    _userService = userService;
	    }

	    [HttpPost]
	    public async Task<IActionResult> Post([FromBody]RegistrationRequest request)
	    {
		    await _userService.CreateUser(request.Username, request.Password);
	        return Ok();
	    }
    }
}
