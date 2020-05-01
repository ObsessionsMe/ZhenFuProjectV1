using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class SqlParams
    {
        public Dictionary<string, object> Params = new Dictionary<string, object>();

        public SqlParameter [] GetSqlParameters()
        {
            var _params = new SqlParameter[Params.Keys.Count];
            int _i = 0;

            foreach (var key in Params.Keys)
            {
                _params[_i] = new SqlParameter(key, Params[key]);
                _i++;
            }
            return _params;
        }
    }
}
