using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3lfg.Models
{
  public interface IMessageRepository
  {
    List<Message> GetAll();
    void AddMessage(Message message);
  }
}
