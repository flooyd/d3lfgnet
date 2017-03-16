using d3lfg.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3lfg.Models
{
  public interface IRepository
  {
    IEnumerable<Group> GetAllGroups();

    Group GetExistingGroup(string userName);
    void AddGroup(Group group);
    void EditExistingGroup(Group group);
    Task<bool> SaveChangesAsync();
  }
}
