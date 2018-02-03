/// <reference path="SaveAssignMaterial.js" />
$(document).ready(function () {
    //debugger;
    
    if ($('#WorkStudyID').val() !== '0') {
        $('#WorkStudyID').prop("readonly", true);
    }
    else {
        $('#MillLotNo').selectpicker();
    }

  
  

    $('#MillLotNo').attr('data-live-search', 'true');
   
    //Edit
    if (($("#RecId").val() != "0"))
    {
       
        $('#MillLotNo').attr("disable", true);
    
    }

    $("#MillLotNo").change(function () {
        //debugger;
        var RecID = $("#RecId").val();
        var MillLotNo = $('#MillLotNo').val();
        //  if ((RecID == "0" || RecID == undefined)){ //&& MillLotNo!="-1") {
        {
           // debugger;
            var LotNo = $('#MillLotNo').val();
            
            var options = {                
                recID: 0,
                MillLotNo: $('#MillLotNo').val(),
                WorkStudyId: $('#WorkStudyID').val(),
            };
            $.ajax({
                url: Api + "api/Processing/",
                headers: {
                    Token: GetToken()
                },
                type: 'Get',
                data: options,
                async: false,
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    //changed here start
                        var outputHole = data.ddHole;
                        var optionHole1 = '<option value="' +
                                0 + '">' + "--Select State--" + '</option>';
                        $("#ddlHole").append(optionHole1);
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
                        //changed here end                  
                       
                },
                error: function(x,y,z){}
            });
        }
    });

    //$('#StudyStatus').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    //$('#StudyType').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    //$('#Plant').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    //$('#StartDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    //$('#StartDate').datepicker("update", new Date());
    //$('#DueDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    //$('#CompleteDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });

    //$('#ddlHours').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#SHTStartHrs').attr('data-live-search', 'true');
    $('#SHTStartHrs').selectpicker();

    $('#SHTStartMns').attr('data-live-search', 'true');
    $('#SHTStartMns').selectpicker();

    $('#ArtStartHrs').attr('data-live-search', 'true');
    $('#ArtStartHrs').selectpicker();

    $('#ArtStartMns').attr('data-live-search', 'true');
    $('#ArtStartMns').selectpicker();


    //changed here start
    $('#Hole').attr('data-live-search', 'true');
    $('#Hole').selectpicker();

    $('#PieceNo').attr('data-live-search', 'true');
    $('#PieceNo').selectpicker();
    
  
    //changed here start
    //if ($('#ddlMillLotNo').val() === '0') {
    //    $('#ddlMillLotNo').val('');
    //}

    //if ($('#ProcessNo').val() === '0') {
    //    $('#ProcessNo').val('');
    //}

    //if ($('#AgeLotNo').val() === '0') {
    //    $('#AgeLotNo').val('');
    //}

    //if ($('#HTLogNo').val() === '0') {
    //    $('#HTLogNo').val('');
    //}

    //$('#btnAdd').on('click', function () {
    //    location.href = '/ProcessingMaterial/SaveProcessingMaterial/0'
    //});
    //$('#btnPM').on('click', function () {
    //    location.href = '/ProcessingMaterial/SaveProcessingMaterial?id=0&workStudyId=' + $('#WorkStudyID').val()
    //});


    //$('#StartDate')
    //    .on('change', function (e) {
    //        console.log('value is : ' + this.value);
    //});

    $('#SHTDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#ArtAgeDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });

    var form = $('#SaveProcessingMaterial');
    form.bootstrapValidator({        
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            MillLotNo: {
                        validators: {
                            callback: {
                                //message: 'Study Type is required.',
                                callback: function (value, validator, $field) {
                                    /* Get the selected options */
                                    var options = validator.getFieldElements('MillLotNo').val();
                                    return (options !== '-1');
                                }
                            }
                        }
                    },
        }

    });
});