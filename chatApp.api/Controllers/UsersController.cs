using AutoMapper;
using chatApp.Dtos;
using chatApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace chatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
  // 1) dependencies
  private readonly UserManager<AppUser> userManager;
  private readonly IMapper mapper;

  // 2) ctor
  public UsersController(UserManager<AppUser> userManager, IMapper mapper)
  {
    this.userManager = userManager;
    this.mapper = mapper;
  }


  // 3) get all users
  [HttpGet]
  public async Task<IEnumerable<UserDto>> GetAllUsers()
  {
    // 1) get all users
    IEnumerable<AppUser> appUsers = await userManager.Users.ToListAsync();

    // 2) change to dto
    IEnumerable<UserDto> userDtos = mapper.Map<IEnumerable<UserDto>>(appUsers);

    // 3) return users
    return userDtos;
  }


  // 4) get user by id
  [HttpGet("{id}")]
  public async Task<ActionResult<UserDto>> GetUserById(Guid id)
  {
    // 1) get user
    AppUser appUser = await userManager.FindByIdAsync(id.ToString());
    // 2) if user is null
    if (appUser is null)
    {
      return Problem(
        detail: $"No user found with ID '{id}'.",
        statusCode: StatusCodes.Status404NotFound);
    }
    // 3) return userDto
    return mapper.Map<UserDto>(appUser);
  }


  // 5) create user
  // 6) update user


  // 7) delete user
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(string id)
  {
    // 1) get user
    AppUser appUser = await userManager.FindByIdAsync(id);
    // 2) if no user return 404
    if (appUser is null)
    {
      return Problem(
        detail: $"No user found with ID '{id}'.",
        statusCode: StatusCodes.Status404NotFound);
    }

    // 3) delete user
    IdentityResult result = await userManager.DeleteAsync(appUser);

    // 4) if there is an error
    if (!result.Succeeded)
    {
      List<string> errors = new List<string>();
      foreach (var err in result.Errors)
      {
        errors.Add(err.Description);
      }
      return BadRequest(error: new { errors = errors });
    }

    // 5) return success
    return Ok();
  }
}
