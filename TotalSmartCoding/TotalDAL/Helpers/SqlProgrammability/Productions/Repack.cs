using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Productions
{
    public class Repack
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Repack(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.RepackPostSaveValidate();

            this.RepackEditable();

            this.RepackRollback();
        }

        private void RepackPostSaveValidate()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("RepackPostSaveValidate", queryArray);
        }

        private void RepackEditable()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("RepackEditable", queryArray);
        }

        private void RepackRollback()
        {
            string queryString = " @BatchID int, @RepackID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       DELETE FROM Repacks WHERE BatchID = @BatchID AND RepackID >= @RepackID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("RepackRollback", queryString);
        }

    }
}
