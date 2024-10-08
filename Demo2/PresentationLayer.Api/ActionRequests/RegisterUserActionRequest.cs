﻿using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Api.ActionRequests
{
    public class RegisterUserActionRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
