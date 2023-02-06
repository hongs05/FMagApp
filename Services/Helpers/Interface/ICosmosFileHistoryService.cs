using System;
using System.Runtime.ConstrainedExecution;
using mvc.Models;

namespace mvc.Services.Helpers.Interface
{
	public interface ICosmosFileHistoryService
	{
        Task<List<HistoryFile>> Get(string sqlCosmosQuery);
        Task<HistoryFile> AddAsync(HistoryFile newFile);
        Task<List<HistoryFile>> GetByEmail(string email);
    }
}

