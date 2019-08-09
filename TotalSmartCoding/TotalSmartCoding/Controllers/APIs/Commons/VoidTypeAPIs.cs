using System;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class VoidTypeAPIs
    {
        private readonly IVoidTypeRepository voidTypeRepository;

        public VoidTypeAPIs(IVoidTypeRepository voidTypeRepository)
        {
            this.voidTypeRepository = voidTypeRepository;
        }

        public IList<VoidType> SearchVoidTypes(string searchText)
        {
            return this.voidTypeRepository.SearchVoidTypes(searchText);
        }

        public IList<string> GetVoidTypeNames()
        {
            return this.voidTypeRepository.GetVoidTypeNames();
        }
    }
}