using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ADO.Data.Access.Interfaces;

namespace ADO.Data.Access.Conctretes
{
    public class DataAccessAdapter<T> : IParameter<T> where T : class
    {
        private Dictionary<string, dynamic> paramDictionary;

        public DataAccessAdapter()
        {
            paramDictionary = new Dictionary<string, dynamic>();
        }
        public SqlParameter[] GetParameters()
        {
            return Parameters;
        }

        public void SetParameters(T transferObject)
        {
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propInfo in propertyInfos)
            {
                try
                {
                    propInfo.GetValue(transferObject,null);
                    paramDictionary.Add("@"+propInfo.Name, propInfo.GetValue(transferObject, null));
                }
                catch (Exception e)
                {
                    
                }
            }
        }

        public SqlParameter[] Parameters
        {
            get
            {
                var parameters = from paramKey in paramDictionary.Keys
                                 select new SqlParameter
                                 {
                                     ParameterName = paramKey,
                                     Value = paramDictionary[paramKey]
                                 };
                return parameters.ToArray();
            }
        }
    }
}
