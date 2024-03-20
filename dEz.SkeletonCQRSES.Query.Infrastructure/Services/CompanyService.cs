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
        public async Task<IEnumerable<CompanyForGet>> GetAllAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CompanyForGet>>(companies);
            return result;
        }

        ///<inheritdoc cref="ICompanyService"/>
        public async Task<CompanyForGet> GetByIdAsync(Guid id)
        {
            var company = await _companyRepository.GetAsync(id);
            var result = _mapper.Map<CompanyForGet>(company);
            return result;
        }

        ///<inheritdoc cref="ICompanyService"/>
        public async Task<CompanyForGet> AddAsync(CompanyForAdd companyForAdd)
        {
            var companyEntity = _mapper.Map<Company>(companyForAdd);
            await _companyRepository.AddAsync(companyEntity);
            var companyToReturn = _mapper.Map<CompanyForGet>(companyEntity);
            return companyToReturn;
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
