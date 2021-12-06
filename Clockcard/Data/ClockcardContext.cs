using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clockcard.Models;

namespace Clockcard.Data
{
    public class ClockcardContext : DbContext //Link database Data to the program
    {
        public ClockcardContext (DbContextOptions<ClockcardContext> options)
            : base(options)
        {
        }

        public DbSet<Clockcard.Models.EmpDetails> EmpDetails { get; set; }

        public DbSet<Clockcard.Models.Clock> Clock { get; set; }

        public DbSet<Clockcard.Models.Timeline> Timeline { get; set; }


    }
}
