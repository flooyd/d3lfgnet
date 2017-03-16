using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3lfg.Models
{
  public class MessageRepository : IMessageRepository
  {
    private List<Message> _messages = new List<Message>();

    public void AddMessage(Message message)
    {
      _messages.Add(message);
    }

    public List<Message> GetAll()
    {
      return _messages;
    }
  }
}
