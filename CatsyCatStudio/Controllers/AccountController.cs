﻿using CatsyCatStudio.Models;
using CatsyCatStudio.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatsyCatStudio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<StoreUser> UserManager,SignInManager<StoreUser> SignInManager, IConfiguration Configuration)
        {
            _userManager = UserManager;
            _signInManager = SignInManager;
            _configuration = Configuration;
        }

        public IActionResult Login() 
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("","Can not Find User");
                
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.UserName);
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    //create token
                    // create claims
                    //create keys
                    //create Signong Creds
                    //create token
                    //handle token
                    //response
                     
                    var claims = new[] { 
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Tokens:Issuer"], _configuration["Tokens:Audience"],
                        claims, expires: DateTime.UtcNow.AddMinutes(45), signingCredentials: creds);

                    var response = new { 
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration= token.ValidTo
                };

                    return Created("",response);
                }

            }
            

            return BadRequest();
        }
    }
}
