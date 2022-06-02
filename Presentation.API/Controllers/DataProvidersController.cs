using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataProvidersController : ControllerBase
    {
        private readonly ILogger<DataProvidersController> _logger;
        private readonly IDataProviderRepository _dataProviderRepository;

        public DataProvidersController(ILogger<DataProvidersController> logger, IDataProviderRepository dataProviderRepository)
        {
            _logger = logger;
            _dataProviderRepository = dataProviderRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<DataProvider>> All()
        {
            try
            {
                var res = await _dataProviderRepository.All();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("{DataProviderID}")]
        public async Task<IActionResult> Find([FromRoute] int DataProviderID)
        {
            try
            {
                DataProvider res = await _dataProviderRepository.Find(DataProviderID);
                return res == null ? NotFound("The entity doesn't exists") : Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<DataProvider> Create([FromBody] DataProvider dataProvider)
        {
            try
            {
                dataProvider.CreatedAt = DateTime.Now;  
                DataProvider res = await _dataProviderRepository.Create(dataProvider);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPatch]
        public async Task<DataProvider> Update([FromBody] DataProvider dataProvider)
        {
            try
            {
                DataProvider res = await _dataProviderRepository.Update(dataProvider);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpDelete]
        [Route("{DataProviderID}")]
        public async Task<IActionResult> Delete([FromRoute] int DataProviderID)
        {
            try
            {
                int res = await _dataProviderRepository.Delete(DataProviderID);
                return res == 1 ? Ok("Deleted successfully") : NotFound("Unable to delete the entity");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
