using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoCidadeRepository : IRegiaoCidadeRepository
    {
        private DbSet<RegiaoCidade> _dbSet;
        private readonly DbContext _dbContext;

        public RegiaoCidadeRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<RegiaoCidade>();
            _dbContext = dbContext;
        }

        public IQueryable<RegiaoCidade> List()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<RegiaoCidade> Query(string terms)
        {
            throw new System.NotImplementedException();
        }

        public bool insertRegiaoCidade(List<Guid> cidades, string regiao)
        {
            if (cidades == null || cidades.Count == 0)
                return false;

            if (!Guid.TryParse(regiao, out Guid regiaoGuid))
                return false;

            var entidades = new List<RegiaoCidade>();

            foreach (var cidadeId in cidades)
            {

                var entidade = new RegiaoCidade(cidadeId, regiaoGuid);
                entidades.Add(entidade);

            }

            _dbSet.AddRange(entidades);
            _dbContext.SaveChanges();

            return true;
        }

    }
}
