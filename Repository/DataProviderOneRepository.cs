using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DataProviderOneRepository : IDataProviderOneRepository
    {
        private readonly IDbConnection _connection;
        public DataProviderOneRepository(IDataProvider dataProvider)
        {
            _connection = dataProvider.GetConnection();                
        }
        public int Create()
        {
            try
            {

                return 1;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
