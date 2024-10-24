using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Customer;

public class CreateCustomerDto : CreateOrUpdateCustomerDto
{
    [Required (ErrorMessage = "The Username field is required.")]
    [MaxLength(20, ErrorMessage = "Maximum length for Customer the Username field is 20 characters.")]
    public string Username { get; set; }

    [Required (ErrorMessage = "The EmailAddress field is required.")]
    public string EmailAddress { get; set; }
}