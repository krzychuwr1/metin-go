using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetinGo.Server.Controllers
{
	[Route("api/[controller]")]
	public class RegistrationController
    {
	    private readonly IUserService _userService;

	    public RegistrationController(IUserService userService)
	    {
		    _userService = userService;
	    }

	    [HttpPost]
	    public void Post([FromBody]RegistrationRequest request)
	    {
		    _userService.CreateUser(request.Username, request.Password);
	    }
    }
}
