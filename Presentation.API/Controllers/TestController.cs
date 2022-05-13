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
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IDataProviderOneRepository _dataProviderOneRepository;

        public TestController(ILogger<TestController> logger, IDataProviderOneRepository dataProviderOneRepository)
        {
            _logger = logger;
            _dataProviderOneRepository = dataProviderOneRepository;
        }

        [HttpPost]
        [Route("DataProviderOne")]
        public async Task<int> Create([FromForm] DataProviderOneVM input)
        {
            try
            {
                int res = await _dataProviderOneRepository.Create(new DataProviderOneEntity { 
                    vid = input.vid
                });
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
