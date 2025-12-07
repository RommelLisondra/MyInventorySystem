using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IExpenseService : IDisposable
    {
        Task<IEnumerable<ExpenseDto>> GetAll();
        Task<PagedResponse<IEnumerable<ExpenseDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<ExpenseDto> GetById(int id);
        Task CreateExpense(ExpenseDto ExpenseDto, string createdBy);
        Task UpdateExpense(ExpenseDto ExpenseDto, string editedBy);
        Task<IEnumerable<ExpenseDto>> SearchExpensesByKeywordAsync(string? keyword);

    }
}
