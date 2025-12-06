using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ExpenseService : IExpenseService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ExpenseService));

        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseDto>> GetAll()
        {
            try
            {
                var expenseList = await _unitOfWork.ExpenseRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Expense>, IEnumerable<ExpenseDto>>(expenseList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<ExpenseDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ExpenseRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ExpenseDto>>(result.Data);

                return new PagedResponse<IEnumerable<ExpenseDto>>
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

        public async Task<ExpenseDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Expense, ExpenseDto>(await _unitOfWork.ExpenseRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateExpense(ExpenseDto expenseDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var expenseEntity = Domain.Entities.Expense.Create(
                    _mapper.Map<ExpenseDto, Domain.Entities.Expense>(expenseDto), createdBy);

                await _unitOfWork.ExpenseRepository.AddAsync(expenseEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateExpense", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateExpense(ExpenseDto expenseDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var expenseEntity = Domain.Entities.Expense.Update(
                    _mapper.Map<ExpenseDto, Domain.Entities.Expense>(expenseDto), editedBy);

                await _unitOfWork.ExpenseRepository.UpdateAsync(expenseEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateExpense", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ExpenseDto>> SearchExpensesByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var expenses = await _unitOfWork.ExpenseRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.ExpenseNo ?? string.Empty).ToLower(), $"%{keyword}%") //||
                    //EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchExpensesByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
