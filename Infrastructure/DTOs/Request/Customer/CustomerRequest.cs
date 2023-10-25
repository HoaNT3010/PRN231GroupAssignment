﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Request.Customer
{
    public class CustomerRequest
    {
        
            public int Id { get; }
            public string FirstName { get; set; }
            public string LastName { get; set; } 
            public string PhoneNumber { get; set; }
            public DateTime? DateOfBirth { get; set; } 
            public Gender? Gender { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
    }

    public class UpdateCustomerRequest
    {
        public CustomerStatus status { get; set; }
    }
}
