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
        Task<IEnumerable<Visualisation>> GetAll();
        Task<Visualisation> Get(string id);
        Task<Visualisation> Create(Visualisation vis);
        Task<Visualisation> Update(Visualisation vis);
        Task Delete(string id);
    }

    public class VisualisationRepository : RepositoryBase, IVisualisationRepository
    {
        public VisualisationRepository(DatabaseConfig databaseConfig) : base(databaseConfig)
        {
        }

        public async Task<Visualisation> Create(Visualisation vis)
        {
            vis.Id = Guid.NewGuid().ToString("N");
            await db.ExecuteAsync("INSERT into visualisation(id, name) values (@Id, @Name)", vis);

            return vis;
        }

        public Task Delete(string id)
        {
            return db.ExecuteAsync("DELETE FROM visualisation WHERE id = @id",new { id });
        }

        public Task<Visualisation> Get(string id)
        {
            return db.QueryFirstOrDefaultAsync<Visualisation>("SELECT * FROM visualisation WHERE id = @id", new { id });
        }

        public Task<IEnumerable<Visualisation>> GetAll()
        {
            return db.QueryAsync<Visualisation>("SELECT * FROM visualisation");
        }

        public async Task<Visualisation> Update(Visualisation vis)
        {
            await db.ExecuteAsync("UPDATE visualisation SET name = @Name WHERE id = @Id", vis);

            return vis;
        }
    }
}
