using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class RoleService : IRoleService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(RoleService));

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAll()
        {
            try
            {
                var roleList = await _unitOfWork.RoleRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Role>, IEnumerable<RoleDto>>(roleList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<RoleDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.RoleRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<RoleDto>>(result.Data);

                return new PagedResponse<IEnumerable<RoleDto>>
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

        public async Task<RoleDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Role, RoleDto>(await _unitOfWork.RoleRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateRole(RoleDto roleDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var roleEntity = Domain.Entities.Role.Create(
                    _mapper.Map<RoleDto, Domain.Entities.Role>(roleDto), createdBy);

                await _unitOfWork.RoleRepository.AddAsync(roleEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateRole", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateRole(RoleDto roleDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var roleEntity = Domain.Entities.Role.Update(
                    _mapper.Map<RoleDto, Domain.Entities.Role>(roleDto), editedBy);

                await _unitOfWork.RoleRepository.UpdateAsync(roleEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateRole", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<RoleDto>> SearchRolesByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var roles = await _unitOfWork.RoleRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.RoleName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<RoleDto>>(roles);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchRolesByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
