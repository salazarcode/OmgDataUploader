using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using PostgreSQLCopyHelper;
using Npgsql;

namespace Data.Repository
{
    public class GenericMasterRepository : IGenericMasterRepository
    {
        private readonly IDbConnection _connection;
        private readonly PostgreSQLCopyHelper.PostgreSQLCopyHelper<GenericMasterEntity> _copyHelper;
        public GenericMasterRepository(IDataProvider dataProvider)
        {
            _connection = dataProvider.GetConnection();
            _copyHelper = new PostgreSQLCopyHelper<GenericMasterEntity>("GenericMaster")
                                    .MapInteger("dataproviderid", x => x.DataProvider.DataProviderID)
                                    .MapVarchar("address", x => x.address)
                                    .MapVarchar("body_style", x => x.body_style)
                                    .MapVarchar("city", x => x.body_style)
                                    .MapVarchar("country", x => x.body_style)
                                    .MapVarchar("days_in_lot", x => x.body_style)
                                    .MapVarchar("dealer_name", x => x.body_style)
                                    .MapVarchar("drivetrain", x => x.body_style)
                                    .MapVarchar("exterior_color", x => x.body_style)
                                    .MapVarchar("final_url", x => x.body_style)
                                    .MapVarchar("fuel_type", x => x.body_style)
                                    .MapVarchar("hs_company_id", x => x.body_style)
                                    .MapVarchar("in_stock_date", x => x.body_style)
                                    .MapVarchar("latitude", x => x.body_style)
                                    .MapVarchar("longitude", x => x.body_style)

                                    .MapVarchar("make", x => x.body_style)
                                    .MapVarchar("mileage", x => x.body_style)
                                    .MapVarchar("model", x => x.body_style)
                                    .MapVarchar("msrp", x => x.body_style)
                                    .MapVarchar("phone_number", x => x.body_style)
                                    .MapVarchar("platform_sold_date", x => x.body_style)
                                    .MapVarchar("platform_sold_day_counter", x => x.body_style)
                                    .MapVarchar("price", x => x.body_style)

                                    .MapVarchar("make", x => x.body_style)
                                    .MapVarchar("mileage", x => x.body_style)
                                    .MapVarchar("model", x => x.body_style)
                                    .MapVarchar("msrp", x => x.body_style)
                                    .MapVarchar("phone_number", x => x.body_style)
                                    .MapVarchar("platform_sold_date", x => x.body_style)
                                    .MapVarchar("platform_sold_day_counter", x => x.body_style)
                                    .MapVarchar("price", x => x.body_style);
        }

        public Task<IEnumerable<GenericMasterEntity>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<int> BatchInsertion(IEnumerable<GenericMasterEntity> entities)
        {
            try
            {
                ulong res = await _copyHelper.SaveAllAsync((Npgsql.NpgsqlConnection)_connection, entities);

                return 1;
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public Task<GenericMasterEntity> Create(GenericMasterEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int EntityID)
        {
            throw new NotImplementedException();
        }

        public Task<GenericMasterEntity> Find(int EntityID)
        {
            throw new NotImplementedException();
        }

        public Task<GenericMasterEntity> Update(GenericMasterEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
