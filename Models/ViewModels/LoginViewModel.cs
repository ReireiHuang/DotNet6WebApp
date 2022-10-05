using Microsoft.Build.Framework;

namespace DotNet6WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Acc { get; set; }
        [Required]
        public string Pass { get; set; }
        public string? Message { get; set; }
    }
}
