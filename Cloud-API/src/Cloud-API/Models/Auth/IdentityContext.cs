using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Cloud_API.Models
{
    public class IdentityContext : IdentityDbContext<APIUser>
    {
    }
}
