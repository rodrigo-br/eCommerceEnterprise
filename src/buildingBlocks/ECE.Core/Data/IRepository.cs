using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECE.Core.DomainObjects;

namespace ECE.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {

    }


}
