using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoRepository : IRegiaoRepository
    {
        private DbSet<Regiao> _dbSet;
        private readonly DbContext _dbContext;

        public RegiaoRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<Regiao>();
            _dbContext = dbContext;
        }

        public IQueryable<Regiao> List()
        {
            throw new System.NotImplementedException();
        }




        public IEnumerable<Regiao> Query(string terms)
        {
            throw new System.NotImplementedException();
        }

        public bool exissteNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return false;

            string lowerNome = nome.ToLower();

            return _dbSet.Any(r => r.Nome.ToLower() == lowerNome);
        }

        public (bool, string) insertRegiao(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return (false, "Nome não informado");

            var novaRegiao = new Regiao(nome);
            _dbSet.Add(novaRegiao);
            _dbContext.SaveChanges();

            return (true, novaRegiao.Id.ToString());
        }
    }

}
