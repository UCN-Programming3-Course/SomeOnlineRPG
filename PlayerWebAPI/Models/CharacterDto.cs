﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayerWebAPI.Models
{
    public class CharacterDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}