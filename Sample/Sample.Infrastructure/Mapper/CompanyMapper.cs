using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class CompanyMapper
    {
        public static entityframework.Company MapToEntityFramework(entities.Company entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Company entity cannot be null.");

            var mapped = new entityframework.Company
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                CompanyCode = entity.CompanyCode ?? string.Empty,
                CompanyName = entity.CompanyName,
                Address = entity.Address,
                ContactNo = entity.ContactNo,
                Email = entity.Email,
                IsActive = entity.IsActive,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
                //Branches
            };

            return mapped;
        }

        public static entities.Company MapToEntity(entityframework.Company company)
        {
            if (company == null) return null!;

            return new entities.Company
            {
                id = company.Id,         // default = 0 kung int
                CompanyCode = company.CompanyCode ?? string.Empty,
                CompanyName = company.CompanyName,
                Address = company.Address,
                ContactNo = company.ContactNo,
                Email = company.Email,
                IsActive = company.IsActive,
                CreatedDateTime = company.CreatedDateTime,
                ModifiedDateTime = company.ModifiedDateTime,
                //Branches
            };
        }
    }
}
