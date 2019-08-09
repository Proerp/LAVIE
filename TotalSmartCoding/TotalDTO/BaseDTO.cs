using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using TotalModel;
using TotalBase.Enums;
using TotalBase;

namespace TotalDTO
{
    public class BaseDTO : BaseModel, IAccessControlAttribute
    {
        public BaseDTO()
        {
            // apply any DefaultValueAttribute settings to their properties
            var propertyInfos = this.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var attributes = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                if (attributes.Any())
                {
                    var attribute = (DefaultValueAttribute)attributes[0];
                    propertyInfo.SetValue(this, attribute.Value, null);
                }
            }
        }

        public virtual GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.UnKnown; } }

        public virtual int GetID() { return 0; }
        public int LastID { get; set; } //THIS KEEP THE LAST ID WHEN CREATE NEW Entity. HOWEVER, EACH TIME TO EDIT THE CURRENT Entity, THIS ALSO KEEP CURRENT EDITED ID

        private string reference;
        [Display(Name = "Số phiếu")]
        [DefaultValue(null)]
        public virtual string Reference
        {
            get { return this.reference; }
            set { ApplyPropertyChange<BaseDTO, string>(ref this.reference, o => o.Reference, value); }
        }




        /// <summary>
        /// AT NOW: INTERFACE IAccessControlAttribute PROPERTY: PreparedPersonID: CHỈ CÒN Get, WE COMMENT OUT (REMOVE) Set
        /// HERE: WE USE IMPLEMENT: public virtual int PreparedPersonID { get { return ContextAttributes.User.UserID; } }
        /// IT MEANS THAT: THE PreparedPersonID IS THE LOGON UserID
        /// LET SEE: AT THE CONTRUCTOR OF BaseController, WE SET: this.baseService.UserID = ContextAttributes.User.UserID;
        /// ===> SO, NOW, WE IMPLEMENT: PreparedPersonID = this.baseService.UserID = ContextAttributes.User.UserID: THE LOGON USER
        /// ------------WHY WE NEED PreparedPersonID WHILE WE ALSO HAVE UserID?
        /// ------------AT THE VERY SOON, FAR AWAY IN THE PAST: WE THINK THAT: UserID IS THE LOGON USER, THE PreparedPersonID: IS THE PERSON WHO SAVE DATA
        /// ------------THE UserID CREATE AND SAVE DATA FOR PreparedPersonID
        /// ------------NHƯNG MÀ: THIS CASE RẤT ÍT XẢY RA TRONG THỰC TẾ, VÌ ÍT KHI UseID SAVE DATA DÙM PreparedPersonID
        /// ------------ĐẶT BIỆT: HIỆN TẠI, PHẦN MỀM KHÔNG CHO CHỌN PreparedPersonID
        /// ------------NGOÀI RA, TRONG QUÁ TRÌNH XỬ LÝ DỮ LIỆU, PHẦN MỀM LẠI CĂN CỨ UserID ĐỂ XỬ LÝ DATA, CŨNG NHƯ PERMISSION, VÍ DỤ NHƯ: CĂN CỨ UserID ĐỂ XÁC ĐỊNH LocationID => TỪ ĐÓ XÁC ĐỊNH WarehouseID, ...
        /// ------------VÌ NHỮNG LÝ LẼ ĐÓ, TẠM THỜI, CHÚNG TÔI: HỢP NHẤT PreparedPersonID VÀ UserID CHÍNH LÀ ContextAttributes.User.UserID: USER ĐANG LOGON VÀO PHẦN MỀM
        /// ------------CÓ NGHĨA LÀ: CHÚNG TA BỎ Ý TƯỞNG: UserID MAKE & SAVE DATA FOR PreparedPersonID (LÀM DÙM, THAY THẾ)---Ý TƯỞNG NÀY CÓ RẤT LÂU RỒI, TỪ THỜI VB6. CŨNG HAY, NHƯNG CÓ VẺ KHÔNG CÓ CŨNG KHÔNG SAO!!!! PHẢI BẮT USER LOGOUT -> LOGIN FOR APPROPRIATE USER IF NEEDED
        /// ------------THỰC TẾ: HIỆN GIƠ VẪN CHƯA ĐƯA PreparedPersonID VÀO VIEW ĐỂ CHỌN
        /// </summary>
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Người lập")]        
        public virtual int PreparedPersonID { get { return ContextAttributes.User.UserID; } }
        public int OrganizationalUnitID { get; set; }


        [Display(Name = "Người duyệt")]
        public int ApproverID { get; set; }


        private string description;
        [Display(Name = "Diễn giải")]
        [DefaultValue(null)]
        public string Description
        {
            get { return this.description; }
            set { ApplyPropertyChange<BaseDTO, string>(ref this.description, o => o.Description, value); }
        }


        public bool GlobalLocked { get; set; }


        public virtual bool AllowDataInput { get { return true; } }
        public bool Newable { get; set; }
        public bool Editable { get; set; }

        //private bool editable;
        //[Display(Name = "Số phiếu")]
        //public bool Editable
        //{
        //    get { return this.editable; }
        //    set { ApplyPropertyChange<BaseDTO, bool>(ref this.editable, o => o.Editable, value); }
        //}

        public virtual bool Importable { get { return false; } }
        public virtual bool Exportable { get { return false; } }

        public bool Deletable { get; set; }


        public virtual bool Printable { get; set; }
        public virtual bool PrintVisible { get { return false; } }

        public bool Approvable { get; set; }
        public bool UnApprovable { get; set; }
        public virtual bool NoApprovable { get { return false; } }

        public bool Lockable { get; set; }
        public bool UnLockable { get; set; }
        public virtual bool NoLockable { get { return false; } }

        
        public bool Voidable { get; set; }
        public bool UnVoidable { get; set; }
        public virtual bool NoVoidable { get { return true; } }

        public bool ShowDiscount { get; set; }


        //These properties are used as an implementation preservation of ISimpleViewModel for these ________ViewModel class (Those class ________ViewModel which is BOTH inheritance from this BaseDTO AND implementation of ISimpleViewModel)
        public virtual bool PrintAfterClosedSubmit { get; set; }
        public GlobalEnums.SubmitTypeOption SubmitTypeOption { get; set; }

        public virtual int PrintOptionID { get; set; }



        public virtual void PerformPresaveRule() { }

        public virtual void PrepareVoidDetail(int? detailID) { }
    }
}

