using AutoMapper;
using dEz.SkeletonCQRSES.Query.Domain.Entities;
using dEz.SkeletonCQRSES.Query.Domain.Repositories;
using dEz.SkeletonCQRSES.Query.Domain.Services;
using dEz.SkeletonCQRSES.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Query.Infrastructure.Services
{
    public sealed class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        ///<inheritdoc cref="ICompanyService"/>
        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return companies;
        }

        ///<inheritdoc cref="ICompanyService"/>
        public async Task<Company> GetByIdAsync(Guid id)
        {
            var company = await _companyRepository.GetAsync(id);
            return company;
        }

        ///<inheritdoc cref="ICompanyService"/>
        public async Task<Company> AddAsync(CompanyForAdd companyForAdd)
        {
            var companyEntity = _mapper.Map<Company>(companyForAdd);
            await _companyRepository.AddAsync(companyEntity);
            return companyEntity;
        }

        ///<inheritdoc cref="ICompanyService"/>
        public async Task DeleteAsync(Guid id)
        {
            var company = await _companyRepository.GetAsync(id);
            await _companyRepository.DeleteAsync(company);
        }

        ///<inheritdoc cref="ICompanyService"/>
        public async Task UpdateAsync(Guid id, CompanyForUpdate companyForUpdate)
        {
            var company = await _companyRepository.GetAsync(id);
            _mapper.Map(companyForUpdate, company);
            await _companyRepository.UpdateAsync(company);
        }
    }
}
