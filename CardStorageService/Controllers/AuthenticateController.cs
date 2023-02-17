﻿using CardStorageService.Models.Requests;
using CardStorageService.Services;
using CardStorageService.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace CardStorageService.Controllers
{
    [Route("api/auth]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Services

        private readonly IAuthenticateService _authenticateService;

        #endregion

        #region Constructor

        public AuthenticateController (IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        #endregion

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticationRequest);
            if (authenticationResponse.Status == Models.AuthenticationStatus.Success)
            {
                Response.Headers.Add("X-Session-Token", authenticationResponse.SessionInfo.SessionToken);
            }
            return Ok(authenticationResponse);

        }

        //[HttpGet("session")]
        //public IActionResult GetSessionInfo ()
        //{
        //    //Authorization: Bearer XXXXXXXXXXXXXXXXXX

        //    var authorization = Request.Headers[HeaderNames.Authorization];

        //    AuthenticationHeaderValue
        //}


    }
}
