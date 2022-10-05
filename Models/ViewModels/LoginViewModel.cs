using Microsoft.Build.Framework;

namespace DotNet6WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? Acc { get; set; }
        public string? Pass { get; set; }
        public string? Message { get; set; }
    }
}
