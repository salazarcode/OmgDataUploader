using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace Data.Repository
{
    public class DataFileRepository : IDataFileRepository
    {
        private readonly IDbConnection _connection;
        public DataFileRepository(IDataProvider dataProvider)
        {
            _connection = dataProvider.GetConnection();                
        }

        public async Task<IEnumerable<DataFile>> All()
        {
            try
            {
                string query = $@"
                    select df.*, dp.*, dp.DataProviderID 
                    from DataFiles df 
                    inner join DataProviders dp on dp.DataProviderID = df.DataProviderID";

                IEnumerable<DataFile> res = await _connection.QueryAsync<DataFile, DataProvider, DataFile>(
                    query,
                    (file, provider) => {
                        file.DataProvider = provider;
                        return file;
                    },
                    param: null,
                    splitOn: "DataProviderName"
                );
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataFile> Create(DataFile entity)
        {
            try
            {
                string query = $@"  insert into DataFiles(
                                        DataProviderID, DataProvider, CreatedAt) 
                                    values(
                                        @DataProviderID, @DataProvider, @CreatedAt) returning DataFileID";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@DataProviderID", entity.DataProvider.DataProviderID);
                p.Add("@DataProvider", entity.FileName);
                p.Add("@CreatedAt", entity.CreatedAt);

                IEnumerable<int> res = await _connection.QueryAsync<int>(query, p);
                var element = await this.Find(res.First());
                return element;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Delete(int EntityID)
        {
            try
            {
                string query = $@"delete from DataFiles where DataFileID = @DataFileID";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@DataFileID", EntityID);

                int res = await _connection.ExecuteAsync(query, p);

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataFile> Find(int EntityID)
        {
            try
            {
                string query = $@"
                    select df.*, dp.*, dp.DataProviderID 
                    from DataFiles df 
                    inner join DataProviders dp on dp.DataProviderID = df.DataProviderID
                    where df.DataFileID = @DataFileID";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@DataFileID", EntityID);

                IEnumerable<DataFile> res = await _connection.QueryAsync<DataFile, DataProvider, DataFile>(
                    query,
                    (file, provider) => {
                        file.DataProvider = provider;
                        return file;
                    },
                    param: p,
                    splitOn: "DataProviderName"
                );

                return res.ToList().Count() != 0 ? res.First() : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DataFile>> GetByDataProviderID(int DataProviderID)
        {
            try
            {
                string query = $@"
                    select df.*, dp.*, dp.DataProviderID 
                    from DataFiles df 
                    inner join DataProviders dp on dp.DataProviderID = df.DataProviderID
                    where df.DataProviderID = @DataProviderID";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@DataProviderID", DataProviderID);

                IEnumerable<DataFile> res = await _connection.QueryAsync<DataFile, DataProvider, DataFile>(
                    query,
                    (file, provider) => {
                        file.DataProvider = provider;
                        return file;
                    },
                    param: p,
                    splitOn: "DataProviderName"
                );
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataFile> Update(DataFile entity)
        {
            try
            {
                string query = $@"
                            update DataFiles 
                            set 
                                DataProviderID = @FileName, AbsolutePath = @FileName
                            where 
                                DataFileID = @DataFileID";

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("@DataFileID", entity.DataFileID);
                p.Add("@DataProviderID", entity.DataProvider.DataProviderID);
                p.Add("@FileName", entity.FileName);

                int res = await _connection.ExecuteAsync(query, p);
                var element = await this.Find(entity.DataFileID);
                return element;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
