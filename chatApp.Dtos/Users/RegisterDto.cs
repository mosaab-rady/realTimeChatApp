using System.ComponentModel.DataAnnotations;

namespace chatApp.Dtos;

public class RegisterDto
{

  [Required]
  public required string FirstName { get; set; }

  [Required]
  public required string LastName { get; set; }

  [Required]
  [EmailAddress]
  public required string Email { get; set; }

  [Required]
  public required string Password { get; set; }

  [Required]
  [Compare("Password", ErrorMessage = "Password and confirm password must be the same.")]
  public required string ConfirmPassword { get; set; }
}