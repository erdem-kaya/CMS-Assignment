using System.ComponentModel.DataAnnotations;

namespace UmbracoProject.ViewModels;

public class ContactCardViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email Address")]
    [RegularExpression(@"^[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?$", ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;
}