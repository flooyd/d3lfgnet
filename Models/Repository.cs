using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using d3lfg.ViewModels;
using AutoMapper;

namespace d3lfg.Models
{
  public class Repository : IRepository
  {
    private Context _context;

    public Repository(Context context)
    {
      _context = context;
    }

    public void AddGroup(Group group)
    {
      _context.Add(group);
    }

    public IEnumerable<Group> GetAllGroups()
    {
      return _context.Groups.OrderByDescending(g => g.Id).ToList();
    }

    public Group GetExistingGroup(string battleTag)
    {
      return _context.Groups.OrderByDescending(g => g.Id).Where(g => g.BattleTag == battleTag).First();
    }

    public void EditExistingGroup(Group group)
    {
      //_context.Groups.Update(group);
      _context.Entry(group).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      //var entry = _context.Entry(group);
      //entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }
  }
}
