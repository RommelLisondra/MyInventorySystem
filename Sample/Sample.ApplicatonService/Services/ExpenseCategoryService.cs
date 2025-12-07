using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ExpenseCategoryService : IExpenseCategoryService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ExpenseCategoryService));

        public ExpenseCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseCategoryDto>> GetAll()
        {
            try
            {
                var expenseCategory = await _unitOfWork.ExpenseCategoryRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ExpenseCategory>, IEnumerable<ExpenseCategoryDto>>(expenseCategory);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<ExpenseCategoryDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ExpenseCategoryRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ExpenseCategoryDto>>(result.Data);

                return new PagedResponse<IEnumerable<ExpenseCategoryDto>>
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

        public async Task<ExpenseCategoryDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ExpenseCategory, ExpenseCategoryDto>(await _unitOfWork.ExpenseCategoryRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateExpenseCategory(ExpenseCategoryDto expenseCategoryDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var expenseCategory = Domain.Entities.ExpenseCategory.Create(
                    _mapper.Map<ExpenseCategoryDto, Domain.Entities.ExpenseCategory>(expenseCategoryDto), createdBy);

                await _unitOfWork.ExpenseCategoryRepository.AddAsync(expenseCategory);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateExpenseCategory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateExpenseCategory(ExpenseCategoryDto expenseCategoryDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var expenseCategory = Domain.Entities.ExpenseCategory.Update(
                    _mapper.Map<ExpenseCategoryDto, Domain.Entities.ExpenseCategory>(expenseCategoryDto), editedBy);

                await _unitOfWork.ExpenseCategoryRepository.UpdateAsync(expenseCategory);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateExpenseCategory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ExpenseCategoryDto>> SearchExpenseCategorysByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var expenseCategory = await _unitOfWork.ExpenseCategoryRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.Name ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ExpenseCategoryDto>>(expenseCategory);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchExpenseCategorysByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
