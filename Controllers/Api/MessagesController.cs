using d3lfg.Hubs;
using d3lfg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3lfg.Controllers.Api
{
  public class MessagesController : Controller
  {
    private IConnectionManager _connectionManager;
    private IMessageRepository _messageRepo;

    public MessagesController(IConnectionManager connectionManager, IMessageRepository messageRepository)
    {
      _connectionManager = connectionManager;
      _messageRepo = messageRepository;
    }

    [HttpGet]
    public List<Message> GetMessages()
    {
      return _messageRepo.GetAll();
    }

    [HttpPost]
    public void AddMessage(Message message)
    {
      _messageRepo.AddMessage(message);
      _connectionManager.GetHubContext<GroupHub>().Clients.All.publishMessage(message);
      
    }
  }
}
