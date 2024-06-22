﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormModel.DTO.Account
{
    public class ProfileRequestDTO
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public decimal Balance { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
