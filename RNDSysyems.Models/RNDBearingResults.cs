﻿using System;


namespace RNDSystems.Models
{
    /// <summary>
    /// Bearing Results column details
    /// </summary>
    public class RNDBearingResults : IRNDModel
    {

        public int RecId { get; set; }

        //check fk
        ////[StringLength(DataLengthConstant.LENGHT_ID)]
        public string WorkStudyID { get; set; }

        public int TestNo { get; set; }
        //check numeric(4,1)
        public decimal SubConduct { get; set; }

        //check numeric(4,1)
        public decimal SurfConduct { get; set; }
        //check numeric(4,1)
        public decimal eDCalc { get; set; }

        //check numeric(4,1)
        public decimal FbruKsi { get; set; }
        //check numeric(4,1)
        public decimal FbryKsi { get; set; }

        ////[StringLength(50)]
        public string SpeciComment { get; set; }

        ////[StringLength(20)]
        public string Operator { get; set; }
        public DateTime? TestDate { get; set; }
        ////[StringLength(15)]
        public string TestTime { get; set; }
        public char Completed { get; set; }
        #region IRNDModel

        ////[StringLength(25)]
        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }
        public int total { get; set; }

        #endregion
    }
}
