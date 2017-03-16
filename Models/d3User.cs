using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace d3lfg.Models
{
  public class d3User : IdentityUser
  {
    public string Battletag { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
  }
}