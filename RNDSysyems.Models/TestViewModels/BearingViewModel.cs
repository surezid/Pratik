﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RNDSystems.Models.TestViewModels
{
    public class BearingViewModel
    {
        public int RecID { get; set; }
        public string WorkStudyID { get; set; }
        public int TestNo { get; set; }
        public decimal SubConduct { get; set; }
        public decimal SurfConduct { get; set; }
        public decimal eDCalc { get; set; }
        public decimal FbruKsi { get; set; }
        public decimal FbryKsi { get; set; }
        public string SpeciComment { get; set; }
        public string Operator { get; set; }
        public string TestDate { get; set; }
        public string TestTime { get; set; }
        public string EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public char Completed { get; set; }
    }
}
