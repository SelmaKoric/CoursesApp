using ComfyLearn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Data
{
    public class ApplicationDbInitializer
    {
        public static void Seed(UserManager<User> userManager)
        {
 

            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                User admin = new User
                { 
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };


                IdentityResult admin_result = userManager.CreateAsync(admin, "admiN123!").Result;

                if (admin_result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                    
                }   
            }

            if (userManager.FindByEmailAsync("user@gmail.com").Result == null)
            {
                User user = new User
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                    EmailConfirmed = true
                };
                IdentityResult user_result = userManager.CreateAsync(user, "useR123!").Result;

                if (user_result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }


        }
    }
}
