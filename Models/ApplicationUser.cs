
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser<int>
    {
      
    }

    public class ApplicationRole : IdentityRole<int> { }
}
