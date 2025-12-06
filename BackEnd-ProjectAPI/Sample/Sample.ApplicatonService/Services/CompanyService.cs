using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class CompanyService : ICompanyService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(CompanyService));

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            try
            {
                var companyList = await _unitOfWork.CompanyRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Company>, IEnumerable<CompanyDto>>(companyList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<CompanyDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.CompanyRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<CompanyDto>>(result.Data);

                return new PagedResponse<IEnumerable<CompanyDto>>
                {
                    Data = dtoData,
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalRecords = result.TotalRecords,
                    TotalPages = result.TotalPages
                };
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAllPaged", ex);
                throw;
            }
        }

        public async Task<CompanyDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Company, CompanyDto>(await _unitOfWork.CompanyRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateCompany(CompanyDto companyDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var companyEntity = Domain.Entities.Company.Create(
                    _mapper.Map<CompanyDto, Domain.Entities.Company>(companyDto), createdBy);

                await _unitOfWork.CompanyRepository.AddAsync(companyEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateCompany", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateCompany(CompanyDto CompanyDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var CompanyEntity = Domain.Entities.Company.Update(
                    _mapper.Map<CompanyDto, Domain.Entities.Company>(CompanyDto), editedBy);

                await _unitOfWork.CompanyRepository.UpdateAsync(CompanyEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateCompany", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<CompanyDto>> SearchCompanysByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var Companys = await _unitOfWork.CompanyRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.CompanyCode ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.CompanyName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Address ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<CompanyDto>>(Companys);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchCompanysByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
