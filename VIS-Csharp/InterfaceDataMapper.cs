using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIS_Csharp
{
    public interface InterfaceDataMapper<T>
    {
        Task<T> Get(int id);
        Task<T> Create(T librarian);
        Task<T> Update(int id, T librarian); 
        Task<bool> Delete(int id);
    }
}
