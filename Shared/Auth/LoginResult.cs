﻿namespace Shared.Auth
{
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }
    }
}
