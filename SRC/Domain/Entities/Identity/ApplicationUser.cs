using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class ApplicationUser : IdentityUser<Guid>
{
}
public static class ApplicationUserExtensions
{
    public static void ConfigureApplicationUser(this ModelBuilder modelBuilder)
    {
    }
}