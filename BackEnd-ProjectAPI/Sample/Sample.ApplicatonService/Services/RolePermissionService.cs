using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class RolePermissionService : IRolePermissionService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(RolePermissionService));

        public RolePermissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RolePermissionDto>> GetAll()
        {
            try
            {
                var rolePermissionList = await _unitOfWork.RolePermissionRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.RolePermission>, IEnumerable<RolePermissionDto>>(rolePermissionList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<RolePermissionDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.RolePermissionRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<RolePermissionDto>>(result.Data);

                return new PagedResponse<IEnumerable<RolePermissionDto>>
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

        public async Task<RolePermissionDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.RolePermission, RolePermissionDto>(await _unitOfWork.RolePermissionRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateRolePermission(RolePermissionDto RolePermissionDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var rolePermissionEntity = Domain.Entities.RolePermission.Create(
                    _mapper.Map<RolePermissionDto, Domain.Entities.RolePermission>(RolePermissionDto), createdBy);

                await _unitOfWork.RolePermissionRepository.AddAsync(rolePermissionEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateRolePermission", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateRolePermission(RolePermissionDto RolePermissionDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var rolePermissionEntity = Domain.Entities.RolePermission.Update(
                    _mapper.Map<RolePermissionDto, Domain.Entities.RolePermission>(RolePermissionDto), editedBy);

                await _unitOfWork.RolePermissionRepository.UpdateAsync(rolePermissionEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateRolePermission", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<RolePermissionDto>> SearchRolePermissionsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var rolePermissions = await _unitOfWork.RolePermissionRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like(e.RoleId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.PermissionName ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<RolePermissionDto>>(rolePermissions);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchRolePermissionsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
