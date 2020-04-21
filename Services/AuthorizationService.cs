using Microsoft.AspNetCore.Http;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Services
{
    public class AuthorizationService
    {

        private readonly RegistryDbContext db = new RegistryDbContext();
        public Role Role { get; set; }

        public HttpContext HttpContext { get; set; }
        public AuthorizationService(RegistryDbContext context, HttpContext httpContext, Role role)
        {
            Role = Role;
            HttpContext = httpContext;
            db = context;
        }

        public  bool IsAuthorized()
        {
            var userName = HttpContext.Session.GetString("CurrentUser");
            if (userName == null)
            {
                return false;
            }
            var user = db.Accounts.FirstOrDefault(x => x.UserName == userName);

            if (user.Role == Role.Admin || user.Role == Role)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
