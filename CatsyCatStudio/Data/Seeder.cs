using CatsyCatStudio.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatsyCatStudio.Data
{
    public class Seeder
    {
        private readonly AppDbContext _ctx;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;

        public Seeder(AppDbContext Context,IWebHostEnvironment Env, UserManager<StoreUser> UserManager)
        {
            _ctx = Context;
            _env = Env;
            _userManager = UserManager;
        }

        public async Task SeedAsync() {
            _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("rihan@gmail.com");

            if (user == null) {
                user = new StoreUser() { 
                UserName="rihan@gmail.com",
                FirstName="Rihan",
                LastName="Zamil",
                Email="rihan@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "Ahmed@123");
                if (result != IdentityResult.Success) {
                    throw new InvalidOperationException("User could not be created");
                }
            }

            if (!_ctx.Pies.Any()) {

                var path = Path.Combine(_env.ContentRootPath,@"wwwroot\Data\Pies.json");
                var json = File.ReadAllText(path);

                IEnumerable<Pie> Pies = JsonSerializer.Deserialize<IEnumerable<Pie>>(json);

                _ctx.Pies.AddRange(Pies);
                _ctx.SaveChanges();
            
            }
        }
    }
}
