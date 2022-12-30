﻿namespace ManagementApi
{
    public class Auth0Configuration
    {
        public string Domain { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}