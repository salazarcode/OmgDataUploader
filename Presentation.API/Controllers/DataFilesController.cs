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
    public class DataFilesController : ControllerBase
    {
        private readonly ILogger<DataFilesController> _logger;
        private readonly IDataFileRepository _DataFileOneRepository;

        public DataFilesController(ILogger<DataFilesController> logger, IDataFileRepository DataFileOneRepository)
        {
            _logger = logger;
            _DataFileOneRepository = DataFileOneRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<DataFile>> All()
        {
            try
            {
                var res = await _DataFileOneRepository.All();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("{DataFileID}")]
        public async Task<IActionResult> Find([FromRoute] int DataFileID)
        {
            try
            {
                DataFile res = await _DataFileOneRepository.Find(DataFileID);
                return res == null ? NotFound("The entity doesn't exists") : Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<DataFile> Create([FromBody] DataFile DataFile)
        {
            try
            {
                DataFile.CreatedAt = DateTime.Now;  
                DataFile res = await _DataFileOneRepository.Create(DataFile);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPatch]
        public async Task<DataFile> Update([FromBody] DataFile DataFile)
        {
            try
            {
                DataFile res = await _DataFileOneRepository.Update(DataFile);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpDelete]
        [Route("{DataFileID}")]
        public async Task<IActionResult> Delete([FromRoute] int DataFileID)
        {
            try
            {
                int res = await _DataFileOneRepository.Delete(DataFileID);
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
