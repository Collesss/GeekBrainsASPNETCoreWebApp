﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Api.Models.Dto.User
{
    public class DeleteUserDto
    {
        [Required]
        public int Id { get; set; }
    }
}
