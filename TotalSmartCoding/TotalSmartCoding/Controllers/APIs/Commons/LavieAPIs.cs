using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;

using TotalCore.Repositories.Commons;
using TotalBase;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class LavieAPIs
    {
        private readonly ILavieAPIRepository lavieAPIRepository;

        public LavieAPIs(ILavieAPIRepository lavieAPIRepository)
        {
            this.lavieAPIRepository = lavieAPIRepository;
        }


        public ICollection<LavieIndex> GetLavieIndexes()
        {
            return this.lavieAPIRepository.GetEntityIndexes<LavieIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public void LavieUpdate(int lavieID)
        {
            this.lavieAPIRepository.LavieUpdate(lavieID);
        }

        public void LavieDoEmpty()
        {
            this.lavieAPIRepository.LavieDoEmpty();
        }


        public bool SystemInfoValidate()
        {
            return lavieAPIRepository.SystemInfoValidate();
        }
    }
}
