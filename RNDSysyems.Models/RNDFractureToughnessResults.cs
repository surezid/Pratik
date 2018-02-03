﻿using System;

namespace RNDSystems.Models
{
    /// <summary>
    /// Fracture toughness results column details
    /// </summary>
    public class RNDFractureToughnessResults : IRNDModel
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

        ////[StringLength(10)]
        public string Validity { get; set; }
        //check numeric(4,1)
        public decimal KKsiIn { get; set; }
        //check numeric(4,1)
        public decimal KmaxKsiIn { get; set; }
        //check numeric(7,0)
        public decimal PqLBS { get; set; }
        //check numeric(7,0)
        public decimal PmaxLBS { get; set; }
        //check numeric(7,0)
        public decimal aOIn { get; set; }
        //check numeric(7,0)
        public decimal WIn { get; set; }
        //check numeric(7,0)
        public decimal BIn { get; set; }
        //check numeric(7,0)
        public decimal BnIn { get; set; }
        //check numeric(7,0)
        public decimal AvgFinalPreCrack { get; set; }
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
        #endregion

        public int total { get; set; }
    }
}
