using Microsoft.AspNetCore.Identity;

namespace BlazorWasmChat.Authentication
{
    public class AppUser : IdentityUser
    {
        public string? Fullname { get; set; } = string.Empty;
    }
}