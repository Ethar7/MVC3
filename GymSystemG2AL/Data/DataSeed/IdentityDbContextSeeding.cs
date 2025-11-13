// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using GymSystemG2AL.Entities;
// using Microsoft.AspNetCore.Identity;

// namespace GymSystemG2AL.Data.DataSeed
// {
//     public class IdentityDbContextSeeding
//     {
//         public static bool SeedData(RoleManager<IdentityRole> roleManager , UserManager<IdentityUser> userManager)
//         // public static async void SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
//         {
//             try
//             {
//                 var HasUsers = userManager.Users.Any();
//                 var HasRoles = roleManager.Roles.Any();
//                 if (HasUsers && HasRoles) return false;

//                 if (!HasRoles)
//                 {
//                     var Roles = new List<IdentityRole>()
//                     {
//                         new() {Name = "SuperAdmin"},
//                         new() {Name = "Admin"},
//                     };
//                     foreach (var role in Roles)
//                     {
//                         if (!roleManager.RoleExistsAsync(role.Name).Result)
//                         {
//                             roleManager.CreateAsync(role).Wait();
//                         }
//                     }

//                 }
            
//                 if (!HasUsers)
//                 {
//                     var MainAdmin = new ApplicationUser()
//                     {
//                         Firstname = "Ethar",
//                         LastName = "Osama",
//                         UserName = "EtharOsama",
//                         Email = "nabihethar@yahoo.com",
//                         PhoneNumber = "01065721210",
//                     };
//                     userManager.CreateAsync(MainAdmin, "P@ssw0rd").Wait();
//                     userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

//                     var Admin = new ApplicationUser()
//                     {
//                         Firstname = "Esar",
//                         LastName = "Osman",
//                         UserName = "EsarOsman",
//                         Email = "nweethar@yahoo.com",
//                         PhoneNumber = "01094721210",
//                     };
//                     userManager.CreateAsync(MainAdmin, "P@ssw0rd").Wait();
//                     userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();
//                 }
//                 return true;
//             }
//             catch (Exception ex)
//             {
                
//                 Console.WriteLine($"Seeding failed : {ex}");
//                 return false;
//             }
//         }
//     }
// }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymSystemG2AL.Data.DataSeed
{
    public static class IdentityDbContextSeeding
    {
        public static async Task<bool> SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var hasUsers = userManager.Users.Any();
                var hasRoles = roleManager.Roles.Any();

                if (hasUsers && hasRoles)
                    return false;

                // Seed Roles
                if (!hasRoles)
                {
                    var roles = new List<string> { "SuperAdmin", "Admin" };

                    foreach (var roleName in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(roleName))
                        {
                            await roleManager.CreateAsync(new IdentityRole(roleName));
                        }
                    }
                }

                // Seed Users
                if (!hasUsers)
                {
                    var mainAdmin = new ApplicationUser
                    {
                        Firstname = "Ethar",
                        LastName = "Osama",
                        UserName = "EtharOsama",
                        Email = "nabihethar@yahoo.com",
                        PhoneNumber = "01065721210"
                    };
                    await userManager.CreateAsync(mainAdmin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(mainAdmin, "SuperAdmin");

                    var admin = new ApplicationUser
                    {
                        Firstname = "Esar",
                        LastName = "Osman",
                        UserName = "EsarOsman",
                        Email = "nweethar@yahoo.com",
                        PhoneNumber = "01094721210"
                    };
                    await userManager.CreateAsync(admin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding failed: {ex.Message}");
                return false;
            }
        }
    }
}
