using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IAdRepository AdRepository { get; set;  }
        IBasketRepository BasketRepository { get; set;  }

        Task CompleteAsync();
    }
}
