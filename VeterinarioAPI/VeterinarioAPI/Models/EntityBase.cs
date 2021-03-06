﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class EntityBase
    {
        public EntityBase()
        {
            UpdatedAt = DateTime.Now;
            Deleted = false;
        }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public bool Deleted { get; set; }
    }
}