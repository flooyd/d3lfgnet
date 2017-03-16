using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3lfg.ViewModels
{
  public class GroupViewModel
  {
    public int Id { get; set; }
    public string Activity { get; set; }
    public string Description { get; set; }
    public string Region { get; set; }
    public int Paragon { get; set; }
    public string Difficulty { get; set; }
    public string GRLevel { get; set; }
    public string BattleTag { get; set; }
  }
}
