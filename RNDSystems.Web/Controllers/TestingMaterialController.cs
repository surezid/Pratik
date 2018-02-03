﻿using Newtonsoft.Json;
using RNDSystems.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using RNDSystems.Models.ViewModels;
using RNDSystems.Web.ViewModels;

namespace RNDSystems.Web.Controllers
{
    public class TestingMaterialController : BaseController
    {
        
        public ActionResult TestingMaterialList(int RecID, string workStudyID)
        {
            _logger.Debug("TestingMaterialList");

            List<SelectListItem> ddlTestType = null;
            List<SelectListItem> ddlLotID = null;

          //  List<SelectListItem> ddlSubTestType = null;
          //  List<SelectListItem> ddAvailableTestType = null;

            //List<SelectListItem> TestLabs = null;
            //List<SelectListItem> Orientations = null;
            //List<SelectListItem> Location1s = null;  

            RNDTesting testing = null;
            try 
            {
                ddlTestType = new List<SelectListItem>();
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/Testing?RecID=0").ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        RNDTesting rndTesting = JsonConvert.DeserializeObject<RNDTesting>(res.Result.Content.ReadAsStringAsync().Result);
                        if (rndTesting != null)
                        {
                            //in workStudyController 

                            ddlTestType = rndTesting.ddTestType;
                           ddlLotID = rndTesting.ddLotID;


                           // ddlSubTestType = rndTesting.ddSubTestType;
                            //  ddAvailableTestType = rndTesting.ddAvailableTestType;
                            // TestLabs = rndTesting.TestLabs;
                            // Orientations = rndTesting.Orientations;
                            //Location1s = rndTesting.Location1s;
                        }
                    }
                });
                task.Wait();
                testing = new RNDTesting
                {
                    WorkStudyID = workStudyID
                };
                ViewBag.ddlTestType = ddlTestType;
                ViewBag.ddlAvailableTT = ddlTestType;

                ViewBag.ddlLotID = ddlLotID;

                // testing.GageThickness = "LotID";
                // 
                //ViewBag.ddlSubTestType = ddlSubTestType;
                //  ViewBag.ddAvailableTestType = ddAvailableTestType;
                // ViewBag.ddTestLabs = TestLabs;
                // ViewBag.ddOrientations = Orientations;
                //ViewBag.ddLocation1s = Location1s;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(testing);
        }

        //public ActionResult TestingMaterialList(List<SelectListItem> ddlTestType, int RecID, string workStudyID)
        //{
        //    _logger.Debug("TestingMaterialList");

        //    //List<SelectListItem> ddlTestType = null;
        //    //  List<SelectListItem> ddlLotID = null;
        //    //  List<SelectListItem> ddlSubTestType = null;
        //      List<SelectListItem> ddAvailableTestType = null;

        //    //List<SelectListItem> TestLabs = null;
        //    //List<SelectListItem> Orientations = null;
        //    //List<SelectListItem> Location1s = null;  

        //    RNDTesting testing = null;
        //    try
        //    {
        //        var client = GetHttpClient();
        //        // var task = client.GetAsync(Api + "api/TestingMaterial?TestingNo=0").ContinueWith((res) =>
        //        var task = client.GetAsync(Api + "api/Testing?RecID=0").ContinueWith((res) =>
        //        {
        //            if (res.Result.IsSuccessStatusCode)
        //            {
        //                RNDTesting rndTesting = JsonConvert.DeserializeObject<RNDTesting>(res.Result.Content.ReadAsStringAsync().Result);
        //                if (rndTesting != null)
        //                {
        //                    //in workStudyController 

        //                    ddlTestType = rndTesting.ddTestType;
        //                    //       ddlLotID = rndTesting.ddLotID;
        //                    // ddlSubTestType = rndTesting.ddSubTestType;
        //                    //  ddAvailableTestType = rndTesting.ddAvailableTestType;
        //                    // TestLabs = rndTesting.TestLabs;
        //                    // Orientations = rndTesting.Orientations;
        //                    //Location1s = rndTesting.Location1s;
        //                }
        //            }
        //        });
        //        task.Wait();
        //        testing = new RNDTesting
        //        {
        //            WorkStudyID = workStudyID
        //        };
        //        ViewBag.ddlTestType = ddlTestType;
        //        // testing.GageThickness = "LotID";
        //        //    ViewBag.ddlLotID = ddlLotID;
        //        //ViewBag.ddlSubTestType = ddlSubTestType;
        //        //  ViewBag.ddAvailableTestType = ddAvailableTestType;
        //        // ViewBag.ddTestLabs = TestLabs;
        //        // ViewBag.ddOrientations = Orientations;
        //        //ViewBag.ddLocation1s = Location1s;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex);
        //    }
        //    return View(testing);
        //}

        // public ActionResult SaveTestingMaterial(int id, string workStudyId)
        public ActionResult SaveTestingMaterial(int id, string workStudyId, List<string> avialableTT)
        {
            RNDTesting testing = null;

            //chnage this ddlTestType to ddlAvailableTestType
         //   List<SelectListItem> ddlAvailableTestType = null;

            List<SelectListItem> ddlLotID = null;
            List<SelectListItem> ddlOrientation = null;
            List<SelectListItem> ddlReplica = null;
            List<SelectListItem> ddlLocation1 = null;
            List<SelectListItem> ddlTestLab = null;
            List<SelectListItem> ddlHole = null;
            List<SelectListItem> ddlPieceNo = null;
            List<SelectListItem> ddlLocation2 = null;
            List<SelectListItem> ddlSubTestType = null;
              
            int intRowId;
            int intCount = 25;

            string[] strOrient = {" ","L","LT","L-T","L-S","T-L","T-S","S-L","S-T","Custom" };
            string[] strLocOne = { " ","Front", "Middle", "Back", "Low Con" };
            string[] strTesLab = { "Canton", "Anaheim","WMTR", "ATS"};

            //List<SelectListItem> ddlSubTestType = null;
            List<SelectListItem> ddlAvailableTestType = null;
            //List<SelectListItem> ddlMaterialTestType = null;

            try
            {
                var client = GetHttpClient();
                if (workStudyId==null)
                {
                    var task = client.GetAsync(Api + "api/Testing?recID=" + id).ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            testing = JsonConvert.DeserializeObject<RNDTesting>(res.Result.Content.ReadAsStringAsync().Result);
                            if (testing != null)
                            {
                                ddlAvailableTestType = testing.ddTestType;
                                ddlLotID = testing.ddLotID;
                                ddlSubTestType = testing.ddSubTestType;
                                ddlLocation2 = testing.ddLocation2;
                                ddlHole = testing.ddHole;
                                ddlPieceNo = testing.ddPieceNo;
                                //ddlSubTestType = testing.ddSubTestType;
                                //ddlAvailableTestType = testing.ddAvailableTestType;
                                //ddlMaterialTestType = testing.ddMaterialTestType;    
                            }
                        }
                    });
                    task.Wait();
                }
                else
                {
                    var task = client.GetAsync(Api + "api/Testing?recID=" + id + "&workStudyID=" + workStudyId).ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            testing = JsonConvert.DeserializeObject<RNDTesting>(res.Result.Content.ReadAsStringAsync().Result);
                            if (testing != null)
                            {
                                if (!string.IsNullOrEmpty(workStudyId))
                                {
                                    testing.WorkStudyID = workStudyId;

                                }
                               // ddlAvailableTestType = testing.ddTestType;
                                ddlLotID = testing.ddLotID;
                                //ddlSubTestType = testing.ddSubTestType;
                                //ddlAvailableTestType = testing.ddAvailableTestType;
                                //ddlMaterialTestType = testing.ddMaterialTestType;    
                            }
                        }
                    });
                    task.Wait();
                }

                ddlOrientation = new List<SelectListItem>();
                ddlLocation1 = new List<SelectListItem>();
                ddlTestLab = new List<SelectListItem>();
                ddlReplica = new List<SelectListItem>();
                ddlHole = new List<SelectListItem>();
                ddlPieceNo = new List<SelectListItem>();
                ddlLocation2 = new List<SelectListItem>();
                ddlSubTestType = new List<SelectListItem>();

                intRowId = 0;
                string strValue = string.Empty;
                while (intRowId < strOrient.Length)
                {
                    strValue = strOrient[intRowId];
                    if (testing!=null)
                    {
                        ddlOrientation.Add(new SelectListItem
                        {
                            Value = strValue,
                            Text = strValue,
                            Selected = (Convert.ToString(testing.Orientation) == Convert.ToString(strValue)) ? true : false,
                        });
                    }                   

                    intRowId += 1;
                }
                intRowId = 0;
                while (intRowId < strLocOne.Length)
                {
                    ddlLocation1.Add(new SelectListItem
                    {
                        Value = strLocOne[intRowId],
                        Text = strLocOne[intRowId],
                        Selected = (Convert.ToString(testing.Location1) == Convert.ToString(strLocOne[intRowId])) ? true : false,
                    });
                    intRowId += 1;
                }
                intRowId = 0;
                while (intRowId < strTesLab.Length)
                {
                    ddlTestLab.Add(new SelectListItem
                    {
                        Value = strTesLab[intRowId],
                        Text = strTesLab[intRowId],
                        Selected = (Convert.ToString(testing.TestLab) == Convert.ToString(strTesLab[intRowId])) ? true : false,
                    });
                    intRowId += 1;
                }
                intRowId = 0;
                while (intRowId < intCount)
                {
                    intRowId += 1;
                    ddlReplica.Add(new SelectListItem
                    {
                        Value = Convert.ToString(intRowId),
                        Text = Convert.ToString(intRowId),
                        Selected = (Convert.ToString(testing.Replica) == Convert.ToString(intRowId)) ? true : false,
                    });
                   
                }
                //ends here

                // for (int i = 0; i < strOrient.Length; i++)
                ddlHole.Add(new SelectListItem
                {
                    Value = "Select LotID",
                    Text = "Select LotID",
                    Selected = (Convert.ToString(testing.Hole) == Convert.ToString("Select LotID")) ? true : false,
                });
                ddlPieceNo.Add(new SelectListItem
                {
                    Value = "Select LotID",
                    Text = "Select LotID",
                    Selected = (Convert.ToString(testing.PieceNo) == Convert.ToString("Select LotID")) ? true : false,
                });
                ddlLocation2.Add(new SelectListItem
                {
                    Value = "Select LotID",
                    Text = "Select LotID",
                    Selected = (Convert.ToString(testing.Location2) == Convert.ToString("Select LotID")) ? true : false,
                });
                ddlSubTestType.Add(new SelectListItem
                {
                    Value = "Select Test Type",
                    Text = "Select Test Type",
                    Selected = (Convert.ToString(testing.SubTestType) == Convert.ToString ("Select Test Type")) ? true : false,
                });

                //ddlAvailableTestType.Add(new SelectListItem
                //{
                //    Value = "Select Test Type",
                //    Text = "Select Test Type",
                //    Selected = (Convert.ToString(testing.TestType) == Convert.ToString("Select Test Type")) ? true : false,
                //});

                //  ViewBag.ddlAvailableTestType = ddlAvailableTestType;

                //start here 

            //    List<SelectListItem> ddlAvailableTestType = null;

                ddlAvailableTestType = new List<SelectListItem>();
                
                //if (avialableTT.Count != 0)
                if ((avialableTT != null)&&(avialableTT.Count > 0))
                {
                    //  List<string> selectedTT = SeperateValues(avialableTT[0]);                  
                    string[] selectedTT = SeperateValues(avialableTT[0]);
                    //start here
                    foreach (string s in selectedTT)
                    {
                        if (s=="-1")
                        {
                            ddlAvailableTestType.Add(new SelectListItem
                            {
                                Value = "Please Select",
                                Text = "Please Select",
                                Selected = (Convert.ToString(testing.TestType) == Convert.ToString("Please Select")) ? true : false,
                            });
                        }
                        else
                        {
                            ddlAvailableTestType.Add(new SelectListItem
                            {
                                Value = s,
                                Text = s,
                                Selected = (Convert.ToString(testing.TestType) == Convert.ToString(s)) ? true : false,
                            });
                        }                        
                    }
                    //ends here                    
                }
                else
                {
                    ddlAvailableTestType.Add(new SelectListItem
                    {
                        Value = "Please Select",
                        Text = "Please Select",
                        Selected = (Convert.ToString(testing.TestType) == Convert.ToString("Please Select")) ? true : false,
                    });
                }

                ViewBag.ddlAvailableTestType = ddlAvailableTestType;
                ViewBag.ddlLotID = ddlLotID;
                ViewBag.ddlOrientation = ddlOrientation;
                ViewBag.ddlReplica = ddlReplica;
                ViewBag.ddlLocation1 = ddlLocation1;
                ViewBag.ddlTestLab = ddlTestLab;
                ViewBag.ddlHole = ddlHole;
                ViewBag.ddlPieceNo = ddlPieceNo;
                ViewBag.ddlLocation2 = ddlLocation2;
                ViewBag.ddlSubTestType = ddlSubTestType;
                //ViewBag.ddlMaterialTestType = ddlMaterialTestType;
               // testing.GageThickness = "LotID";
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(testing);
        }       
        
        [HttpPost]

        public ActionResult SaveTestingMaterial(RNDTesting model)
        {
            var client = GetHttpClient();
            //  var task = client.PostAsJsonAsync(Api + "api/TestingMaterial", model).ContinueWith((res) =>
            var task = client.PostAsJsonAsync(Api + "api/Testing", model).ContinueWith((res) =>
            {
                if (res.Result.IsSuccessStatusCode)
                {
                    RNDTesting rndTesting = JsonConvert.DeserializeObject<RNDTesting>(res.Result.Content.ReadAsStringAsync().Result);
                    if (rndTesting != null)
                    {
                    }
                }
            });
            task.Wait();
            return RedirectToAction("TestingMaterialList", new { RecID = model.TestingNo, workStudyID = model.WorkStudyID });            
        }

        //  public ActionResult TestingMaterial(string avialableTT)
        public ActionResult TestingMaterial(List<string> avialableTT)
        {
            List<SelectListItem> ddlAvailableTestType = null;

            bool isSuccess = false;
            ddlAvailableTestType = new List<SelectListItem>();
            try
            {
                if (avialableTT.Count != 0)
                {
                    int intRowId = 0;
                    while (intRowId < avialableTT.Count)
                    {
                        if (avialableTT[intRowId] != "-1")
                        {
                            ddlAvailableTestType.Add(new SelectListItem
                            {
                                Value = avialableTT[intRowId],
                                Text = avialableTT[intRowId],
                                Selected = true,
                            });
                        }
                        intRowId += 1;
                    }
                    // ViewBag.ddlAvailableTT = avialableTT;
                    isSuccess = true;
                }
                else
                    isSuccess = false;
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }            

            // return View();
            return Json(new { isSuccess = isSuccess, AvailableTestType = ddlAvailableTestType }, JsonRequestBehavior.AllowGet);
        }

        protected string[] SeperateValues(string avialableTT)
        {
            string[] separatingChars = { "," };
            string[] selectedTT = avialableTT.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            return selectedTT;
        }


        [HttpPost]
        public ActionResult PrintSelected(string SelectedTests)
        {
            _logger.Debug("PrintSelected");
            //DataGridoption ExportDataFilter = new DataGridoption();

            // ExportDataFilter.searchBy = TestingNos;
            //  ExportDataFilter.Screen = "PrintTest";

            // List<RNDTesting> lstExportTesting = new List<RNDTesting>();
            //DataSearch<RNDTesting> objTest = null;

            List<TestingViewModel> lstExportTesting = new List<TestingViewModel>();
            DataSearch<TestingViewModel> objTest = null;
                        
            bool isSuccess = false;
            try
            {
                var client = GetHttpClient();

               // var task = client.PostAsJsonAsync(Api + "api/Testing", SelectedTests).ContinueWith((res) =>
               var task = client.GetAsync(Api + "api/Testing?SelectedTests=" + SelectedTests).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        //  objTest = JsonConvert.DeserializeObject<DataSearch<RNDTesting>>(res.Result.Content.ReadAsStringAsync().Result);
                        objTest = JsonConvert.DeserializeObject<DataSearch<TestingViewModel>>(res.Result.Content.ReadAsStringAsync().Result);
                        if (objTest != null && objTest.items != null && objTest.items.Count > 0)
                        {
                            lstExportTesting = objTest.items;
                            if (lstExportTesting != null)
                            {
                                //set filename
                                string fileName = "PrintNew" + "_" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "");
                                // GetExcelFile<RNDTesting>(lstExportTesting, fileName);
                                GetExcelFile<TestingViewModel>(lstExportTesting, fileName);
                                isSuccess = true;
                            }                                
                        }
                    }
                });
                task.Wait();               
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
             // return RedirectToAction("TestingMaterialList");
            return Json(new { isSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
        }
    }    
}
