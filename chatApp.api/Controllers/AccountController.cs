using AutoMapper;
using chatApp.Dtos;
using chatApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace chatApp.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AccountController(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    SignInManager<AppUser> signInManager,
    IMapper mapper) : ControllerBase
{

  // 1) sign up new user
  [HttpPost("signup")]
  public async Task<IActionResult> SignUp(RegisterDto registerDto)
  {
    AppUser newUser = new()
    {
      FirstName = registerDto.FirstName,
      LastName = registerDto.LastName,
      Email = registerDto.Email,
      Created_at = DateTime.UtcNow,
      UserName = registerDto.Email
    };

    var result = await userManager.CreateAsync(newUser, registerDto.Password);

    if (!result.Succeeded)
    {
      var errors = new List<string>();
      foreach (var err in result.Errors)
      {
        errors.Add(err.Description);
      }
      return BadRequest(error: new { errors });
    }


    // add a role of member to all new users 
    // if role 'member' does not exist create it
    try
    {
      await userManager.AddToRoleAsync(newUser, "member");
    }
    catch (System.InvalidOperationException)
    {
      IdentityRole role = new() { Name = "member" };
      await roleManager.CreateAsync(role);
      await userManager.AddToRoleAsync(newUser, "member");
    }

    return CreatedAtAction(
      "signup",
      new { detail = "thank you for signning up." });

  }

  // login
  // 1) create login dto
  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
  {
    // 1) check if user exist
    AppUser user = await userManager.FindByEmailAsync(loginDto.Email);

    if (user is null)
    {
      return Problem(
        detail: "Invalid email or password.",
        statusCode: StatusCodes.Status400BadRequest);
    }

    // 2) if password correct sign in
    var signingInResult = await signInManager.PasswordSignInAsync(user, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
    // 3) if password incorret return error

    if (!signingInResult.Succeeded)
    {
      return Problem(
       detail: "Invalid email or password.",
       statusCode: StatusCodes.Status400BadRequest);
    }

    // 4) return ok or user info
    return mapper.Map<UserDto>(user);
  }

  // logout
  // 1) check if the user is logged in first
  [Authorize]
  [HttpPost("logout")]
  public async Task<IActionResult> Logout()
  {
    await signInManager.SignOutAsync();
    return NoContent();
  }


  // not logged in user
  [HttpGet("notloggedin")]
  public IActionResult NotLoggedIn()
  {
    return Problem(
      detail: "You are not logged in. Please log in to get access.",
      statusCode: StatusCodes.Status401Unauthorized);
  }


  [HttpGet("isAuthenticted")]
  public async Task<ActionResult<UserDto>> IsAuthenticated()
  {
    if (User.Identity.IsAuthenticated)
    {
      AppUser user = await userManager.FindByEmailAsync(User.Identity.Name);
      UserDto userDto = mapper.Map<UserDto>(user);
      return userDto;
    }
    return Unauthorized();
  }

  [HttpGet("isEmailUsed")]
  public async Task<Boolean> IsEmailUsed(string email)
  {
    var user = await userManager.FindByEmailAsync(email);

    if (user is null)
    {
      return false;
    }
    return true;
  }

}