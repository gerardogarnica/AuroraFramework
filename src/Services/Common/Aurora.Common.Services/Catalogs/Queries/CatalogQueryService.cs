using Aurora.Common.Domain.Catalogs;
using Aurora.Common.Domain.Catalogs.Repositories;
using Aurora.Framework.Collections;
using Aurora.Framework.Logic;
using AutoMapper;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Catalogs.Queries
{
    public interface ICatalogQueryService
    {
        Task<Catalog> GetByCodeAsync(string code, bool onlyGetActiveItems);
        Task<PagedCollection<Catalog>> GetListAsync(
            PagedViewRequest viewRequest, bool onlyGetVisibles, bool onlyGetEditables);
    }

    public class CatalogQueryService : ICatalogQueryService
    {
        #region Miembros privados de la clase

        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public CatalogQueryService(
            ICatalogRepository catalogRepository,
            IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface ICatalogQueryService

        async Task<Catalog> ICatalogQueryService.GetByCodeAsync(string code, bool onlyGetActiveItems)
        {
            var catalogData = await _catalogRepository.GetByCodeAsync(code);

            if (catalogData == null) return null;

            if (onlyGetActiveItems)
            {
                catalogData.Items.ToList().RemoveAll(x => x.IsActive.Equals(false));
            }

            return _mapper.Map<Catalog>(catalogData);
        }

        async Task<PagedCollection<Catalog>> ICatalogQueryService.GetListAsync(
            PagedViewRequest viewRequest, bool onlyGetVisibles, bool onlyGetEditables)
        {
            // Adición de filtros
            Expression<Func<Domain.Catalogs.Models.CatalogData, bool>> filter = x => x.Equals(x);

            if (onlyGetVisibles)
                filter = filter.And(x => x.IsVisible.Equals(true));

            if (onlyGetEditables)
                filter = filter.And(x => x.IsEditable.Equals(true));

            var catalogsData = await _catalogRepository
                .GetPagedCollectionAsync(viewRequest, filter, x => x.Code);

            return _mapper.Map<PagedCollection<Catalog>>(catalogsData);
        }

        #endregion

        #region Métodos privados de la clase

        #endregion
    }
}