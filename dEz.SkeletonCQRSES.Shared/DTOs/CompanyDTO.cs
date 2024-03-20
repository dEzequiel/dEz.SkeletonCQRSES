using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Shared.DTOs
{
    /// <summary>
    /// DTO for retrieving Company.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="FullAddress"></param>
    public record CompanyForGet(Guid Id, string Name, string FullAddress);

    /// <summary>
    /// DTO for inserting Company.
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Address"></param>
    /// <param name="Country"></param>
    public record CompanyForAdd(Guid id, string Name, string Address, string CountrY);

    /// <summary>
    /// DTO for updating Company.
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Address"></param>
    /// <param name="Country"></param>
    public record CompanyForUpdate(string Name, string Address, string Country);
}
