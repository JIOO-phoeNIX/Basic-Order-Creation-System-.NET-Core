using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task2.DataAccess.Models
{   
    public class Task2DbContext : DbContext
    {
        public Task2DbContext(DbContextOptions<Task2DbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Customer table
        /// </summary>
        public DbSet<TABCUST> TABCUST { get; set; }

        /// <summary>
        /// User table
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Items table 
        /// </summary>
        public DbSet<TABITEM> TABITEM { get; set; }

        /// <summary>
        /// Sales order data table
        /// </summary>
        public DbSet<TABSODATA> TABSODATA { get; set; }

        /// <summary>
        /// Sales order table
        /// </summary>
        public DbSet<TABSORDER> TABSORDER { get; set; }
    }
}
