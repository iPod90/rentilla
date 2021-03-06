﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rentilla.Models
{
    public class Object
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public Categorie categorie { get; set; }
    }
    public class ObjectDbContext : DbContext
    {
        public DbSet<Object> Objects { get; set; }
    }

}