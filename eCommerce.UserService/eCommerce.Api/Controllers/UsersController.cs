﻿using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        this._usersService = usersService;
    }

    //GET /api/Users/{userID}
    [HttpGet("{userID}")]
    public async Task<IActionResult> GetUserByUserID(Guid userID)
    {
        if (userID == Guid.Empty)
            return BadRequest("Invalid User ID");
        
        UserDTO? response = await _usersService.GetUserByUserID(userID);

        if (response == null)
            return NotFound(response);
        

        return Ok(response);
    }
}
