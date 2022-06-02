using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class GeneralMapper
    {
        private readonly List<MappingRecordVM> _mappingRecord;
        public GeneralMapper()
        {
            _mappingRecord = new List<MappingRecordVM>(){
                new MappingRecordVM()
                {
                    DataProviderSlug =  "test_data_provider",
                    DataProviderTable = "GenericMaster"

                }
            };
        }

        public void Map(string DataProviderSlug, string[] lines) 
        {
            switch (DataProviderSlug)
            {
                case "test_data_provider":
                    List<GenericMasterEntity> entities = new List<GenericMasterEntity>();   
                    foreach (var line in lines)
                    {
                        GenericMasterEntity entity = new GenericMasterEntity();
                        string[] splittedLine = line.Split(',');

                        //The hearth of the mapping itself
                        entity.vin = splittedLine[1];
                        entity.make = splittedLine[5];
                        entity.model = splittedLine[6];
                        entity.year = Convert.ToInt32(splittedLine[7]);

                        //Adding the guy to the list
                        entities.Add(entity);   
                    }
                    //Having the entities... I save them to DB, to the specific table
                    break;
                default:
                    break;
            }
        }
    }
}
