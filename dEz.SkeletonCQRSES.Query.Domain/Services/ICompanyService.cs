﻿using dEz.SkeletonCQRSES.Query.Domain.Entities;
using dEz.SkeletonCQRSES.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Query.Domain.Services
{
    /// <summary>
    /// Service interface for company-related operations.
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Gets all companies asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning an enumerable collection of
        /// <see cref="Company"/>.</returns>
        Task<IEnumerable<Company>> GetAllAsync();


        /// <summary>
        /// Gets a company by its ID asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the company.</param>
        /// <returns>A task representing the asynchronous operation, returning the <see cref="Company"/>
        /// object or null if not found.</returns>
        Task<Company> GetByIdAsync(Guid id);

        /// <summary>
        /// Add new company asynchronously.
        /// </summary>
        /// <param name="companyForAdd"></param>
        /// <returns>A task representing the asynchronous operation, returning the <see cref="Company"/>
        /// object.</returns>
        Task<Company> AddAsync(CompanyForAdd companyForAdd);

        /// <summary>
        /// Delete company asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A trask representing the asynchronous operation.</returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Update company asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyForUpdate"></param>
        /// <param name="trackChanges"></param>
        /// <returns>A trask representing the asynchronous operation.</returns>
        Task UpdateAsync(Guid id, CompanyForUpdate companyForUpdate);

    }
}
