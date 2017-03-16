using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace d3lfg.Models
{
  public class Group
  {
    public int Id { get; set; }
    public string Activity { get; set; } //rifts, bounties, etc.
    public string Description { get; set; }
    public string Region { get; set; }
    public int Paragon { get; set; }
    public string Difficulty { get; set; }
    public string GRLevel { get; set; }
    public string BattleTag { get; set; }
    public string Username { get; set; }
  }
}
