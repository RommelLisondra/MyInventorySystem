using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class BranchMapper
    {
        public static entityframework.Branch MapToEntityFramework(entities.Branch entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Branch entity cannot be null.");

            var mapped = new entityframework.Branch
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                CompanyId = entity.CompanyId,
                BranchCode = entity.BranchCode,
                BranchName = entity.BranchName,
                Address = entity.Address,
                ContactNo = entity.ContactNo,
                Email = entity.Email,
                IsActive = entity.IsActive,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime
            };

            return mapped;
        }

        public static entities.Branch MapToEntity(entityframework.Branch branch)
        {
            if (branch == null) return null!;

            return new entities.Branch
            {
                id = branch.Id,         // default = 0 kung int
                CompanyId = branch.CompanyId,
                BranchCode = branch.BranchCode,
                BranchName = branch.BranchName,
                Address = branch.Address,
                ContactNo = branch.ContactNo,
                Email = branch.Email,
                IsActive = branch.IsActive,
                CreatedDateTime = branch.CreatedDateTime,
                ModifiedDateTime = branch.ModifiedDateTime,

                //Company
                //Warehouses
                //Employees
                //ItemWarehouseMappings
            };
        }
    }
}
