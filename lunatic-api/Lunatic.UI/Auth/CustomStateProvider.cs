﻿using Lunatic.UI.Contracts;
using Lunatic.UI.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Lunatic.UI.Auth
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationService authService;
        private readonly ITokenService tokenService;

        public CustomStateProvider(IAuthenticationService authService, ITokenService tokenService)
        {
            this.authService = authService;
            this.tokenService = tokenService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await tokenService.GetTokenAsync();
                if (userInfo!=null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, "user logged") };
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task Logout()
        {
            await authService.Logout();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Login(LoginModel loginParameters)
        {
            await authService.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Register(RegisterModel registerParameters)
        {
            await authService.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
