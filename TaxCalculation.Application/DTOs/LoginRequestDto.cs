using System.ComponentModel.DataAnnotations;

namespace TaxCalculation.Application.DTOs;

public class LoginRequestDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(200, MinimumLength = 8)]
    public string Password { get; set; } = null!;
}
