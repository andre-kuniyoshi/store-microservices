﻿using Core.Common;

namespace Register.Application.Domain.Entities
{
    public class Address : BaseControlEntity
    {
        public Guid UserId { get; set; }
        public string Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public string City { get; set; }
        public string UF { get; set; }

        public User User { get; set; }
    }
}
