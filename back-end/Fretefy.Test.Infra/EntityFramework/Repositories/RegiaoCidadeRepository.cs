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
            return _dbSet.AsQueryable();
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

        public bool alterarRegiaocidade(List<Guid> cidades, Guid regiao)
        {
            try
            {
                // Busca todas as cidades atualmente associadas à região
                var cidadesAtuais = _dbSet.Where(rc => rc.RegiaoID == regiao).ToList();

                if (cidades == null || cidades.Count == 0)
                {
                    // Remove todas as cidades da região
                    if (cidadesAtuais.Any())
                    {
                        _dbSet.RemoveRange(cidadesAtuais);
                        _dbContext.SaveChanges();
                    }
                    return true;
                }

                // IDs das cidades atualmente associadas
                var cidadesAtuaisIds = cidadesAtuais.Select(rc => rc.CidadeID).ToHashSet();

                // IDs recebidos na nova lista
                var novasCidadesIds = cidades.ToHashSet();

                // Cidades para remover (estão na base mas não na nova lista)
                var cidadesParaRemover = cidadesAtuais
              .Where(rc => !novasCidadesIds.Contains(rc.CidadeID))
              .ToList();

                // Cidades para adicionar (estão na nova lista mas não na base)
                var cidadesParaAdicionar = novasCidadesIds
              .Where(cidadeId => !cidadesAtuaisIds.Contains(cidadeId))
              .Select(cidadeId => new RegiaoCidade(cidadeId, regiao))
              .ToList();

                if (cidadesParaRemover.Any())
                    _dbSet.RemoveRange(cidadesParaRemover);

                if (cidadesParaAdicionar.Any())
                    _dbSet.AddRange(cidadesParaAdicionar);

                if (cidadesParaRemover.Any() || cidadesParaAdicionar.Any())
                    _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
