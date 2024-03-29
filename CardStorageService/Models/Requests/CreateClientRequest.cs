﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CardStorageService.Models.Requests
{
    public class CreateClientRequest
    {
        public string? SurName { get; set; }

        public string? FirstName { get; set; }

        public string? Patronumic { get; set; }
    }
}
