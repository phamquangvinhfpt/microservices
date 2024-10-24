using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Customer;

public abstract class CreateOrUpdateCustomerDto
{
    [Required (ErrorMessage = "The FirstName field is required.")]
    public string FirstName { get; set; }
    [Required (ErrorMessage = "The LastName field is required.")]
    public string LastName { get; set; }
}