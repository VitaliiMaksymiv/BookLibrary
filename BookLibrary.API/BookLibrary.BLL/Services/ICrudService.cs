using System.Collections.Generic;
using System.Threading.Tasks;
using BookLibrary.BLL.Paginating;

namespace BookLibrary.BLL.Services
{
    public interface ICrudService<TEntityDTO> where TEntityDTO : class, new()
    {
        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity</returns>
        Task<TEntityDTO> GetAsync(int id);

        /// <summary>
        /// Gets entities with pagination
        /// </summary>
        /// <param name="offset">Amount to skip</param>
        /// <param name="amount">Amount to take</param>
        /// <returns>Entities</returns>
        Task<IEnumerable<TEntityDTO>> GetPageAsync(SampleFilterModel sampleFilterModel);

        /// Add methods to service
        /// <summary>
        /// Registers a new entity
        /// </summary>
        /// <param name="value">New entity</param>
        /// <returns>Created entity</returns>
        Task<TEntityDTO> CreateAsync(TEntityDTO dto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="value">Entity model to update</param>
        /// <returns>Updated entity</returns>
        Task<TEntityDTO> UpdateAsync(TEntityDTO dto);

        /// <summary>
        /// Removes entity with this id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>void</returns>
        Task DeleteAsync(int id);
    }
}