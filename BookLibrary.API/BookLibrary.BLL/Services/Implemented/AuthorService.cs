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

        public async Task<IEnumerable<AuthorDTO>> GetRangeAsync(uint offset, uint amount)
        {
            var entities = await _unitOfWork.AuthorRepository.GetRangeAsync(offset, amount);
            return _mapper.Map<IEnumerable<AuthorDTO>>(entities);
        }

        public async Task<IEnumerable<AuthorDTO>> SearchAsync(string search)
        {
            var Authors = await _unitOfWork.AuthorRepository.SearchAsync(search);

            return _mapper.Map<IEnumerable<AuthorDTO>>(await Authors.ToListAsync());
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

            _unitOfWork.AuthorRepository.Update(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<AuthorDTO>(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.AuthorRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}