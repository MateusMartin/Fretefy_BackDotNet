using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private DbSet<Cidade> _dbSet;
        private readonly DbContext _dbContext;

        public CidadeRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<Cidade>();
            _dbContext = dbContext;
        }

        public IQueryable<Cidade> List()
        {
            return _dbSet.AsQueryable();
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _dbSet.Where(w => EF.Functions.Like(w.UF, $"%{uf}%"));
        }

        public IEnumerable<Cidade> Query(string terms)
        {

            return _dbSet.Where(w => EF.Functions.Like(w.Nome, $"%{terms}%") || EF.Functions.Like(w.UF, $"%{terms}%"));
        }


        public async Task CreateRangeAsync(IEnumerable<Cidade> cidades)
        {
            //Busca Cidades Existentes
            var existentes = await _dbSet
                .Select(c => new { c.Nome, c.UF })
                .ToListAsync();

            //Deixa Apenas as novas cidades
            var cidadesNovas = cidades
                .Where(c => !existentes.Any(e => e.Nome == c.Nome && e.UF == c.UF))
                .ToList();

            if (cidadesNovas.Any())
            {
                //Inser Cidades Na base
                await _dbSet.AddRangeAsync(cidadesNovas);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
