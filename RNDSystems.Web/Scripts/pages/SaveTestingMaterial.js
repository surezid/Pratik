﻿$(document).ready(function () {
 //   debugger;
    var selectedLotID;
    //var selectedTestType; 
    var selectedTT;

    if ($('#RecId').val() !== '0') {
        $('#WorkStudyID').prop("readonly", true);    
    }  

    $('#LotID').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();

    $('#Orientation').attr('data-live-search', 'true');
    $('#Orientation').selectpicker();

    $('#Location1').attr('data-live-search', 'true');
    $('#Location1').selectpicker();

    $('#Replica').attr('data-live-search', 'true');
    $('#Replica').selectpicker();

    $('#ddTestLab').attr('data-live-search', 'true');
    $('#ddTestLab').selectpicker();

    //$('#AvailableTestType').attr('data-live-search', 'true');
    //$('#AvailableTestType').selectpicker();

    $('#ddTestType').attr('data-live-search', 'true');
    $('#ddTestType').selectpicker();

    $('#Hole').attr('data-live-search', 'true');
    $('#Hole').selectpicker();

    $('#PieceNo').attr('data-live-search', 'true');
    $('#PieceNo').selectpicker();

    $('#Location2').attr('data-live-search', 'true');
    $('#Location2').selectpicker();

    //$('#AvailableTestType').attr('data-live-search', 'true');
    //$('#AvailableTestType').selectpicker();

    $('#ddSubTestType').attr('data-live-search', 'true');
    $('#ddSubTestType').selectpicker();
     
    $('#btnSaveTest').on('click', function () {
        var form = $('#SaveTestingMaterial');
        //debugger;
        form.bootstrapValidator({
            message: 'This value is not valid',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                
                Location2: {
                    enabled: true,
                    validators: {
                        callback: {
                            message: 'Location2 is required.',
                            callback: function (value, validator, $field) {
                                /* Get the selected options */
                                //form.bootstrapValidator('enableFieldValidators', 'Location2', true);
                                var options = validator.getFieldElements('Location2').val();
                                var checkTestType = (validator.getFieldElements('TestType').val()).trim();
                                var returnvalue = false;
                                //debugger;
                                if (checkTestType == 'Macro Etch')
                                    return true;
                                else
                                    return ((options !== 'Select LotID') && (options !== '-1') && (options !== ' ') && (options !== ''));
                            }
                        }
                    }
                },
                Orientation: {
                    validators: {
                        callback: {
                            message: 'Orientation is required.',
                            callback: function (value, validator, $field) {
                                /* Get the selected options */
                                //var options = validator.getFieldElements('ddlLotID').val();
                                var options = validator.getFieldElements('Orientation').val();
                                // return (options !== '-1');
                                return ((options !== 'Select LotID') && (options !== '-1') && (options !== ' ') && (options !== ''));
                            }
                        }
                    }
                },
                // ddlLotID: {
                LotID: {
                    validators: {
                        callback: {
                            message: 'LotID is required.',
                            callback: function (value, validator, $field) {
                                /* Get the selected options */
                                //var options = validator.getFieldElements('ddlLotID').val();
                                var options = validator.getFieldElements('LotID').val();
                                return (options !== '-1');
                            }
                        }
                    }
                },
                TestType: {
                    validators: {
                        notEmpty: {
                            message: 'TestType is required.'
                        }
                    }
               },
            },
        });
    });
    //start here

    $("#TestLab").val('Canton');

    $('#ddTestLab').change(function () {
        var TestLab = ($(this).find("option:selected").val()).trim();
        //var checkValue = $("#TestLab").val();
      
        //if (checkValue != '')
            $("#TestLab").val(TestLab);
       // debugger;
    });

    $('#ddSubTestType').change(function () {
        var SubTestType =($(this).find("option:selected").val()).trim();
        $("#SubTestType").val(SubTestType);
            // debugger;
        });

    $('#ddTestType').change(function() {
        var TestType = ($(this).find("option:selected").val()).trim();
        $("#TestType").val(TestType);
            // debugger;
            });

    //  $('#AvailableTestType').change(function () {     
    $('#ddTestType').change(function () {
        selectedTT = ($(this).find("option:selected").val()).trim();      

    var RecID = $('#RecId').val();
    var flag = 0;
      
    if ((RecID == "0" || RecID == undefined) && selectedTT != "Please Select") {         
        var options = {
            flag: flag,
            RecID: 0,
            TestType: selectedTT,     
        };
        
        $.ajax({
            url: Api + "api/Testing/",
            headers: {
                Token: GetToken()
            },
            type: 'Get',
            data: options,
            async: false,
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data) {
                   
                   // $('#GageThickness').val(data.GageThickness);

                    //SubTestType
                    var outputSubTT = data.ddSubTestType;
                    var option1SubTT = '<option value="' +
                            0 + '">' + "--Select State--" + '</option>';
                    $("#ddSubTestType").append(option1SubTT);
                    var SubTTValue;
                    var SubTTText;
                    var optionSubTT;
                    $.each(outputSubTT, function (i) {
                        SubTTValue = outputSubTT[i].Value;
                        SubTTText = outputSubTT[i].Text;

                        optionSubTT += '<option value="' +
                            outputSubTT[i].Value + '">' + outputSubTT[i].Text + '</option>';
                    });
                    $("#ddSubTestType").empty();
                    $("#ddSubTestType").append(optionSubTT);
                    $("#ddSubTestType").selectpicker('refresh');
                }
            },
            error: function (x, y, z) { }
        });
    }
});
//ends here

    $('#LotID').change(function () {
        var RecID = $('#RecId').val();     
        selectedLotID = $(this).find("option:selected").val();    
        if ((RecID == "0" || RecID == undefined) && selectedLotID != "Please Select") {         
            var options = {
                //recId for EDIT
                WorkStudyID: $('#WorkStudyID').val(),
                LotID: selectedLotID,
                //TestType: selectedTestType,
                RecID: 0,                
            };
            
            $.ajax({
                url: Api + "api/Testing/",
                headers: {
                    Token: GetToken()
                },
                type: 'Get',
                data: options,
                async: false,
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data) {
                        
                       // $("#MillLotNo").val(data.MillLotNo);
                       // $("#SoNum").val(data.SoNum);
                       //// $("#Hole").val(data.Hole);
                       //// $("#PieceNo").val(data.PieceNo);
                       // $("#CustPart").val(data.CustPart);
                       // $("#UACPart").val(data.UACPart);
                       // $("#Alloy").val(data.Alloy);
                       // $("#Temper").val(data.Temper);

                     //   $('#GageThickness').val(data.GageThickness);
                                              
                       // Hole 
                        var outputHole = data.ddHole;
                        var optionHole1 = '<option value="' +
                                0 + '">' + "--Select State--" + '</option>';
                        $("#Hole").append(optionHole1);
                        var HoleValue;
                        var HoleText;                        
                        var optionHole;
                        $.each(outputHole, function (i) {
                            HoleValue = outputHole[i].Value;
                            HoleText = outputHole[i].Text;

                            optionHole += '<option value="' +
                                outputHole[i].Value + '">' + outputHole[i].Text + '</option>';
                        });

                        $("#Hole").empty();
                        $("#Hole").append(optionHole);
                        $("#Hole").selectpicker('refresh');


                        //PieceNo
                        var outputPieceNo = data.ddPieceNo;
                        var optionPieceNo1 = '<option value="' +
                                0 + '">' + "--Select State--" + '</option>';
                        $("#PieceNo").append(optionPieceNo1);
                        var PieceNoValue;
                        var PieceNoText;
                        var optionPieceNo;
                        $.each(outputPieceNo, function (i) {
                            PieceNoValue = outputPieceNo[i].Value;
                            PieceNoText = outputPieceNo[i].Text;

                            optionPieceNo += '<option value="' +
                                outputPieceNo[i].Value + '">' + outputPieceNo[i].Text + '</option>';
                        });

                        $("#PieceNo").empty();
                        $("#PieceNo").append(optionPieceNo);
                        $("#PieceNo").selectpicker('refresh');

                        //Location2
                        var outputLoc2 = data.ddLocation2;
                        var option1Loc2 = '<option value="' +
                                0 + '">' + "--Select State--" + '</option>';
                        $("#Location2").append(option1Loc2);
                        var Loc2Value;
                        var Loc2Text;
                        var optionLoc2;
                        $.each(outputLoc2, function (i) {
                            Loc2Value = outputLoc2[i].Value;
                            Loc2Text = outputLoc2[i].Text;

                            optionLoc2 += '<option value="' +
                                outputLoc2[i].Value + '">' + outputLoc2[i].Text + '</option>';
                        });

                        $("#Location2").empty();
                        $("#Location2").append(optionLoc2);
                        $("#Location2").selectpicker('refresh');
                    }
                },
                error: function (x, y, z) { }
            });
        }
    });

    $('#Location2').change(function () {
        var RecID = $('#RecId').val();
        selectedLoc2 = $(this).find("option:selected").val();
        var LotId = $('#LotID').val();
        if ((RecID == "0" || RecID == undefined) && selectedLoc2 != "Please Select") {
            var options = {
                //recId for EDIT
                WorkStudyID: $('#WorkStudyID').val(),
                Loc2: selectedLoc2,
                LotId: LotId,
                //TestType: selectedTestType,               
            };
            //debugger;
            $.ajax({  
                url: Api + "api/Testing/",
                headers: {
                    Token: GetToken()
                },
                type: 'Get',
                data: options,
                async: false,
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data) {                       
                        $("#GageThickness").val(data.GageThickness);
                        $('#GageThickness').prop('disabled', true);
                    }
                },
                error: function (x, y, z) { }
            });
        }
    });
  
    JqueryFunction = {
        ReadOnly: function () {

            //var inp = $("#ddlLotID").val();
            var inp = $("#LotID").val();

            if (jQuery.trim(inp).length > 0) {
                // $('#ddlLotID').prop("readonly", true);
                $('#LotID').prop("readonly", true);
            }
            else {
                //$('#ddlLotID').prop("readonly", false);
                $('#LotID').prop("readonly", false);
            }

            //inp = $("#SoNum").val();

            //if (jQuery.trim(inp).length > 0) {
            //    $('#SoNum').prop("readonly", true);
            //}
            //else {
            //    $('#SoNum').prop("readonly", false);
            //}

            //inp = $("#Hole").val();

            //if (jQuery.trim(inp).length > 0) {
            //    $('#Hole').prop("readonly", true);
            //}
            //else {
            //    $('#Hole').prop("readonly", false);
            //}

            //inp = $("#PieceNo").val();

            //if (jQuery.trim(inp).length > 0) {
            //    $('#PieceNo').prop("readonly", true);
            //}
            //else {
            //    $('#PieceNo').prop("readonly", false);
            //}            
        }
    }
    //$('#ddlLotID').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();     

});


