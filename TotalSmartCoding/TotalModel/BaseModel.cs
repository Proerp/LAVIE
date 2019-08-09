using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using TotalModel.Helpers;

namespace TotalModel
{
    public interface IBaseModel : IValidatableObject
    {
        DateTime? EntryDate { get; set; }
        int LocationID { get; set; }


        bool Approved { get; set; }
        Nullable<System.DateTime> ApprovedDate { get; set; }

        bool InActive { get; set; }
        Nullable<System.DateTime> InActiveDate { get; set; }

        bool InActivePartial { get; set; }
        Nullable<System.DateTime> InActivePartialDate { get; set; }

        Nullable<int> VoidTypeID { get; set; }

    }

    public abstract class BaseModel : NotifyPropertyChangeObject, IBaseModel
    {
        protected BaseModel() { this.EntryDate = DateTime.Now; }
        public virtual void Init() { this.EntryDate = DateTime.Now; }

        private DateTime? entryDate;
        [UIHint("DateTimeReadonly")]
        [Display(Name = "Ngày lập")]
        [Required(ErrorMessage = "Vui lòng nhập ngày lập")]
        public DateTime? EntryDate
        {
            get { return this.entryDate; }
            set { ApplyPropertyChange<BaseModel, DateTime?>(ref this.entryDate, o => o.EntryDate, value); }
        }



        public int LocationID { get; set; }

        public virtual string Caption { get { return ""; } }

        private string remarks;
        [Display(Name = "Ghi chú")]
        [DefaultValue(null)]
        public string Remarks
        {
            get { return this.remarks; }
            set { ApplyPropertyChange<BaseModel, string>(ref this.remarks, o => o.Remarks, value); }
        }

        private bool approved;
        [DefaultValue(false)]
        public bool Approved
        {
            get { return this.approved; }
            set { ApplyPropertyChange<BaseModel, bool>(ref this.approved, o => o.Approved, value); }
        }
        public Nullable<System.DateTime> ApprovedDate { get; set; }

        private bool locked;
        [DefaultValue(false)]
        public bool Locked
        {
            get { return this.locked; }
            set { ApplyPropertyChange<BaseModel, bool>(ref this.locked, o => o.Locked, value); }
        }

        private bool inActive;
        [DefaultValue(false)]
        public bool InActive
        {
            get { return this.inActive; }
            set { ApplyPropertyChange<BaseModel, bool>(ref this.inActive, o => o.InActive, value); }
        }
        public Nullable<System.DateTime> InActiveDate { get; set; }

        public bool InActivePartial { get; set; }
        public Nullable<System.DateTime> InActivePartialDate { get; set; }

        public virtual Nullable<int> VoidTypeID { get; set; }

        #region Implementation of IValidatableObject

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (false) yield return new ValidationResult("", new[] { "" });
        }

        #endregion
    }
}
