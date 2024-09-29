﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PresentationLayer.Api.Filters
{
    public class IPWhiteListAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _config;

        public IPWhiteListAuthorizationFilter(IConfiguration config)
        {
            _config = config;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string currentIp = context.HttpContext.Connection.RemoteIpAddress.ToString();

            // string allowedIp = context.HttpContext.User.FindFirst("Ip").Value; // from jwt


            string allowedIp = _config["AllowedIp"];

            if(currentIp != allowedIp)
            {
                context.Result = new ForbidResult(); // 🚩🚩 Short Cut
            }

        }
    }
}
