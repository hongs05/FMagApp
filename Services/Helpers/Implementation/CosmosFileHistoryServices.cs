using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using mvc.Models;
using mvc.Services.Helpers.Interface;
using mvc.Services.Helpers.Options;

namespace mvc.Services.Helpers.Implementation
{
	public class CosmosFileHistoryServices : ICosmosFileHistoryService
	{
		private readonly Container _container;
		private readonly CosmosOptions _options;

        public CosmosFileHistoryServices(CosmosClient cosmosClient,
											string databaseName,
											string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<HistoryFile> AddAsync(HistoryFile newFile)
		{
            var item = await _container.CreateItemAsync<HistoryFile>(newFile);
            return item;
        }

        public async Task<List<HistoryFile>> Get(string sqlCosmosQuery)
        {
			var query = _container
				.GetItemQueryIterator<HistoryFile>(new QueryDefinition(sqlCosmosQuery));

			List<HistoryFile> result = new List<HistoryFile>();
			while (query.HasMoreResults)
			{
				var response = await query.ReadNextAsync();
				result.AddRange(response);

			}
			return result;
            
        }
        public async Task<List<HistoryFile>> GetByEmail(string email)
        {
            var request = $"select * from c where c.UserId = '{email}'";
            var query = _container
                .GetItemQueryIterator<HistoryFile>(new QueryDefinition(request));

            List<HistoryFile> result = new List<HistoryFile>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response);

            }
            return result;

        }

    }
}

