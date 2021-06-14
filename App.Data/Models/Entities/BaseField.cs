using System;

namespace App.Data.Models
{
    public class Active
    {
        public bool IsActive { get; set; } = true;
    }

    public class Delete
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }

    public class ActiveDelete
    {
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }

    public class ActiveCreate
    {
        public bool IsActive { get; set; } = true;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class ActiveCreateDelete
    {
        public bool IsActive { get; set; } = true;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }

    public class UserActiveCreateDeleteUpdate
    {
        public Guid Uid { get; set; }
        public Guid UserUid { get; set; }
        public Guid CustomerUid { get; set; }
        public Guid ProductUid { get; set; }
        public bool IsActive { get; set; } = true;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }

    public class ActiveCreateDeleteUpdate
    {
        public Guid Uid { get; set; }
        public bool IsActive { get; set; } = true;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }
}
