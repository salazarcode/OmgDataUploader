using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Presentation.API.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataFilesController : ControllerBase
    {
        private readonly ILogger<DataFilesController> _logger;
        private readonly IDataFileRepository _DataFileRepository;
        private readonly IDataProviderRepository _dataProviderRepository;
        private readonly BlobServiceClient _blobService;

        public DataFilesController(
                                    ILogger<DataFilesController> logger, 
                                    IDataFileRepository DataFileRepository, 
                                    IConfiguration conf, 
                                    IDataProviderRepository dataProviderOneRepository)
        {
            _logger = logger;
            _DataFileRepository = DataFileRepository;
            _dataProviderRepository = dataProviderOneRepository;
            _blobService = new BlobServiceClient(conf["azureStorageConnectionString"]);
        }

        [HttpGet]
        public async Task<IEnumerable<DataFile>> All()
        {
            try
            {
                var res = await _DataFileRepository.All();
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
                DataFile res = await _DataFileRepository.Find(DataFileID);
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
                DataFile res = await _DataFileRepository.Create(DataFile);
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
                DataFile res = await _DataFileRepository.Update(DataFile);
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
                int res = await _DataFileRepository.Delete(DataFileID);
                return res == 1 ? Ok("Deleted successfully") : NotFound("Unable to delete the entity");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("GetFilesProvidersFiles")]
        public async Task<dynamic> GetFilesProvidersFiles()
        {
            try
            {
                //The proccess 

                // Review every DataProvider checking for new files
                // Those new files must be updated






                // step 1: Read from DB which files have been processed 
                IEnumerable<DataProvider> providers = await _dataProviderRepository.All();
                foreach (var provider in providers)
                {
                    BlobContainerClient blobContainerClient = _blobService.GetBlobContainerClient(provider.AzureContainerName);
                    Azure.Pageable<BlobItem> blobs = blobContainerClient.GetBlobs();

                    provider.ActualFiles = blobs.Select(b => b.Name).ToList();
                    provider.ProcessedDataFiles = (await _DataFileRepository.GetByDataProviderID(provider.DataProviderID)).ToList();
                    List<string> processedFiles = provider.ProcessedDataFiles.Select(x => x.FileName).ToList();
                    provider.NewFiles = provider.ActualFiles.ToList().Except(processedFiles).ToList();

                    foreach (var file in provider.NewFiles)
                    {
                        switch (provider.DataProviderName.ToLower().Replace(" ", "_"))
                        {
                            case "test_data_provider":
                                //Get the file from BlobStorage Container
                                var blobClient = blobContainerClient.GetBlobClient(file);
                                BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
                                string downloadedData = downloadResult.Content.ToString();
                                List<string> lines = downloadedData.Split("\n").Select(x => x.Replace("\"", "").ToString()).ToList();
                                List<List<string>> matrix = lines.Select(x => x.Split(",").ToList()).ToList();
                                matrix.RemoveAt(0);

                                //The custom mapping for each line
                                List<GenericMasterEntity> list = new List<GenericMasterEntity>();
                                List<int> errorLines = new List<int>();
                                for (int i = 1; i < matrix.Count; i++)
                                {
                                    try
                                    {
                                        List<string> linea = matrix[i];
                                        GenericMasterEntity elem = new GenericMasterEntity();

                                        elem.vin = linea[1];
                                        elem.title = linea[2];
                                        elem.final_url = linea[4];

                                        elem.make = linea[5];
                                        elem.model= linea[4];
                                        elem.year = Convert.ToInt32(linea[7]);
                                        elem.mileage_value = Convert.ToDecimal(linea[8]);
                                        elem.mileage_unit = linea[9];


                                        elem.transmission = linea[4];
                                        elem.fuel_type = linea[31];
                                        elem.body_style = linea[32];
                                        elem.drivetrain = linea[33];


                                        elem.state_of_vehicle = linea[34];
                                        //elem. = Convert.ToDecimal(linea[8]);
                                        elem.mileage_unit = linea[9];


                                        elem.model = linea[4];
                                        elem.year = Convert.ToInt32(linea[7]);
                                        elem.mileage_value = Convert.ToDecimal(linea[8]);
                                        elem.mileage_unit = linea[9];


                                        elem.model = linea[4];
                                        elem.year = Convert.ToInt32(linea[7]);
                                        elem.mileage_value = Convert.ToDecimal(linea[8]);
                                        elem.mileage_unit = linea[9];


                                        list.Add(elem);
                                    }
                                    catch (Exception)
                                    {
                                        errorLines.Add(i);
                                        continue;
                                    }
                                }

                                break;
                            default:
                                break;
                        }
                    }
                }

                return providers;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
