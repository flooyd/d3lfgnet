using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace d3lfg.Models
{
  public class Context : IdentityDbContext<d3User>
  {
    private IConfigurationRoot _config;

    public Context(DbContextOptions options, IConfigurationRoot config) : base(options)
    {
      _config = config;
    }

    public DbSet<Group> Groups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuillder)
    {
      optionsBuillder.UseSqlServer(_config["ConnectionStrings:d3lfgContextConnection"]);
      
    }
  }
}
