﻿namespace eStoreClient.Models.Auth
{
    public class MemberDTO
    {
        public Guid Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string RoleName { get; set; } = "CUSTOMER";
    }
}
