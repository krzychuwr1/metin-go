using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Login;
using MetinGo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetinGo.Server.Controllers
{
	[Route("api/"+Endpoints.Login)]
	public class LoginController : Controller
    {
	    private readonly IUserService _userService;

	    public LoginController(IUserService userService)
	    {
		    _userService = userService;
	    }

	    [HttpPost]
	    public async Task<IActionResult> Post([FromBody]LoginRequest request)
	    {
		    var user = await _userService.LoginUser(request.Username, request.Password);
		    return Ok(new LoginResponse{Username = user.Name, UserId = user.Id});
	    }
    }
}
