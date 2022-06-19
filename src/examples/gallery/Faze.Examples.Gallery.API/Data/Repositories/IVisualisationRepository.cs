using Dapper;
using Faze.Examples.Gallery.API.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.API.Data
{
    public interface IVisualisationRepository
    {
        Task<IEnumerable<Visualisation>> GetAllAsync();
    }

    public class VisualisationRepository : RepositoryBase, IVisualisationRepository
    {
        public VisualisationRepository(DatabaseConfig databaseConfig) : base(databaseConfig)
        {
        }

        public Task<IEnumerable<Visualisation>> GetAllAsync()
        {
            return db.QueryAsync<Visualisation>("SELECT * FROM visualisation");

        }
    }
}
