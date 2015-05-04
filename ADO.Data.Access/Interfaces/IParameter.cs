using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Data.Access.Interfaces
{
    public interface IParameter<T>
    {
        SqlParameter[] Parameters { get; }
        void SetParameters(T transferObject);
    }
}
