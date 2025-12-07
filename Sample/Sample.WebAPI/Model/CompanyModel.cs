namespace Sample.WebAPI.Model
{
    public class CompanyModel 
    {
        public virtual int id { get; set; }
        public virtual string CompanyCode { get; set; } = null!;
        public virtual string CompanyName { get; set; } = null!;
        public virtual string? Address { get; set; }
        public virtual string? ContactNo { get; set; }
        public virtual string? Email { get; set; }
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual bool IsActive { get; set; } = true;

        // Navigation property
        public virtual ICollection<BranchModel> Branches { get; set; } = new List<BranchModel>();
    }
}
