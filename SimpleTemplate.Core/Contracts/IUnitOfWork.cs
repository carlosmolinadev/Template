using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTemplate.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        DbConnection Connection { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}
