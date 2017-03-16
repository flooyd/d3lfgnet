using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using d3lfg.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using d3lfg.ViewModels;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using d3lfg.Hubs;
using System.Net;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace d3lfg.Controllers.Api
{

  [Route("api/groups")]
  [Authorize]
  public class GroupsController : Controller
  {
    private IRepository _repo;
    private IConnectionManager _connectionManager;

    public GroupsController(IRepository repo, IConnectionManager connectionManager)
    {
      _repo = repo;
      _connectionManager = connectionManager;
    }

    [AllowAnonymous]
    [HttpGet("")]
    public IActionResult Get()
    {
      Debug.WriteLine(Request.Host);
      var groups = _repo.GetAllGroups();
      //Group to GroupViewModel
      var mappedGroups = Mapper.Map<IEnumerable<GroupViewModel>>(groups);
      return Ok(mappedGroups);
    }

    [Authorize]
    [HttpGet("getusergroup")]
    public IActionResult GetUserGroup()
    {
      var group = _repo.GetExistingGroup(User.Identity.Name);
      var mappedGroup = Mapper.Map<GroupViewModel>(group);

      return Ok(mappedGroup);
    }

    [Authorize]
    [HttpPost("edit")] //not sure whether to use put or post
    public async Task<IActionResult> EditUserGroup([FromBody]Group group)
    {
      if (ModelState.IsValid)
      {
        if(User.Identity.Name == group.BattleTag)
        {
          var existingGroup = _repo.GetExistingGroup(group.BattleTag);
          existingGroup.Activity = group.Activity;
          existingGroup.Description = group.Description;
          existingGroup.Difficulty = group.Difficulty;
          existingGroup.GRLevel = group.GRLevel;
          existingGroup.Paragon = group.Paragon;
          existingGroup.Region = group.Region;
          _repo.EditExistingGroup(existingGroup);

          //existingGroup = group;
        }

        

        if (await _repo.SaveChangesAsync())
        {
          var mappedGroup = Mapper.Map<GroupViewModel>(group);
          _connectionManager.GetHubContext<GroupHub>().Clients.All.editGroup(mappedGroup);
          return StatusCode(200); //ok
        }
        else
        {
          return StatusCode(500); //internal server error
        }
      }

      return StatusCode(400);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddGroup([FromBody]Group group)
    {
      if (ModelState.IsValid)
      {
        string battleTag = User.Identity.Name;

        group.BattleTag = battleTag;

        _repo.AddGroup(group);

        if (await _repo.SaveChangesAsync())
        {
          var mappedGroup = Mapper.Map<GroupViewModel>(group);
          _connectionManager.GetHubContext<GroupHub>().Clients.All.publishGroup(mappedGroup);
          return StatusCode(200); //ok
        }
        else
        {
          return StatusCode(500); //internal server error
        }
      }

      return StatusCode(400);

    }

  }
}
