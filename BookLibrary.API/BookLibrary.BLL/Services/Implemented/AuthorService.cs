using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookLibrary.BLL.DTOs;
using BookLibrary.BLL.Paginating;
using BookLibrary.BLL.Services.Interfaces;
using BookLibrary.DAL.Models.Entities;
using BookLibrary.DAL.UnitOfWork;

namespace BookLibrary.BLL.Services.Implemented
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> GetAsync(int id)
        {
            return _mapper.Map<AuthorDTO>(await _unitOfWork.AuthorRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<AuthorDTO>> GetPageAsync(SampleFilterModel filter)
        {
            var search = filter.SearchQuery ?? String.Empty;
            var entities = await _unitOfWork.AuthorRepository.GetAllAsync(
                item => (item.Name.ToUpper().Contains(search.ToUpper())));

            var entitiesOnPage = entities.Skip((int)((filter.Page - 1) * filter.PageSize))
                .Take((int)filter.PageSize);

            return _mapper.Map<IEnumerable<AuthorDTO>>(entitiesOnPage);
        }

        public async Task<AuthorDTO> CreateAsync(AuthorDTO dto)
        {
            var model = _mapper.Map<Author>(dto);

            await _unitOfWork.AuthorRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<AuthorDTO>(model);
        }

        public async Task<AuthorDTO> UpdateAsync(AuthorDTO dto)
        {
            var model = _mapper.Map<Author>(dto);
            var originalAuthor = await _unitOfWork.AuthorRepository.GetByIdAsync((int) dto.Id);
            model.CreatedDate = originalAuthor.CreatedDate;
            _unitOfWork.AuthorRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<AuthorDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.AuthorRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<AuthorDTO> AttachBook(int authorId,int bookId)
        {
            await _unitOfWork.AuthorRepository.AttachBook(authorId, bookId);
            await _unitOfWork.SaveAsync();
            var updatedAuthor = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);
            return _mapper.Map<AuthorDTO>(updatedAuthor);
        }
    }
}