using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookLibrary.BLL.DTOs;
using BookLibrary.BLL.Services.Interfaces;
using BookLibrary.DAL.Models.Entities;
using BookLibrary.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<BookDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.BookRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<BookDTO>>(entities);
        }

        public async Task<IEnumerable<BookDTO>> SearchAsync(string search)
        {
            var Books = await _unitOfWork.BookRepository.SearchAsync(search);

            return _mapper.Map<IEnumerable<BookDTO>>(await Books.ToListAsync());
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