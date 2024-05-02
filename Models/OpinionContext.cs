using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TP1_API.Models
{
    public class OpinionContext:DbContext
    {
        public OpinionContext(DbContextOptions<OpinionContext> options)
        : base(options)
    {
    }

    public DbSet<Opinion> Opinions { get; set; } = null!;
    }
}