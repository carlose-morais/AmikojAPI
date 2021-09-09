using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmikojApi.Models;

namespace AmikojApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        //Post: /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser([FromBody] ApplicationUserModel model)
        {

            try
            {
                var applicationUser = _mapper.Map<ApplicationUser>(model);
                var result = await _userManager.CreateAsync(applicationUser, model.Password);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    return Ok(user);
                }
                else
                {
                    return Ok(result);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("Login")]
        //Post: /api/ApplicationUser/Login
        public async Task<Object> PostAuthenticationUser([FromBody] AuthenticationUserModel model)
        {

            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var canSign = await _userManager.CheckPasswordAsync(user, model.Password);

                    if (canSign)
                    {
                        return Ok(user);

                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NoContent();
                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
