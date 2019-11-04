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
    public class BookService: IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookDTO> GetAsync(int id)
        {
            return _mapper.Map<BookDTO>(await _unitOfWork.BookRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<BookDTO>> GetPageAsync(SampleFilterModel filter)
        {
            var search = filter.SearchQuery ?? String.Empty;
            var entities = await  _unitOfWork.BookRepository.GetAllAsync(
                item => (item.AuthorBooks.Any(a=>a.Author.Name.ToUpper().Contains(search.ToUpper()))));

            var entitiesOnPage = entities.Skip((int) ((filter.Page-1) * filter.PageSize))
                .Take((int) filter.PageSize);
            
            return _mapper.Map<IEnumerable<BookDTO>>(entitiesOnPage);
        }


        public async Task<BookDTO> CreateAsync(BookDTO dto)
        {
            var model = _mapper.Map<Book>(dto);

            await _unitOfWork.BookRepository.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<BookDTO>(model);
        }

        public async Task<BookDTO> UpdateAsync(BookDTO dto)
        {
            var model = _mapper.Map<Book>(dto);
            var originalBook = await _unitOfWork.BookRepository.GetByIdAsync((int)dto.Id);
            model.CreatedDate = originalBook.CreatedDate;
            _unitOfWork.BookRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<BookDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.BookRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}