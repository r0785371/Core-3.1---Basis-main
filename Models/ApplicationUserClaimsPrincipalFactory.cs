using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Models
{
    //claims toevoegen aan ClaimsPrincipal
    public class ApplicationUserClaimsPrincipalFactory : 
        UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {

        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options
            ): base (userManager, roleManager,options)
        {

        }

        //toevoegen van nieuwe claims buiten identity
        protected override async Task<ClaimsIdentity>
            GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("CareerStartedDate", user.CareerStartedDate.ToShortDateString()));
            identity.AddClaim(new Claim("Department", user.Department));
            identity.AddClaim(new Claim("FullName", user.FullName));

            return identity;
        }
    }
}
