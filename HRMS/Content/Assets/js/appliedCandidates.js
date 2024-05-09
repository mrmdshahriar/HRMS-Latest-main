$(document).ready(function () {
    $("#addtask").show();
    $(".updatetask").hide();
    loadingjobmasterdetails();
    getjobtypeddl();
    getdepartmentddl();
    getdesignationddl();
    gethrmskillsddl();
    var count = 0;
    $('#ddljobtype').select2({
        selectonclose: true
    });

    $('#ddldepartment').select2({
        selectonclose: true
    });
    $('#ddldesignation').select2({
        selectonclose: true
    });

    $('#ddlskills').select2({
        selectonclose: true
    });

    //$("#txtcnic").keypress(function (e) {
      
    //    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
          
    //         $("#errmsg".html("digits only".show().fadeout("slow");
    //        return false;
    //    }
    //});


    //$("#txtcontactnumber").keypress(function (e) {
    //    //if the letter is not digit then display error and don't type anything
    //    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
    //        //display error message
    //         $("#errmsg".html("digits only".show().fadeout("slow";
    //        return false;
    //    }
    //});
    //$("#txtage").keypress(function (e) {
    //    //if the letter is not digit then display error and don't type anything
    //    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
    //        //display error message
    //         $("#errmsg".html("digits only".show().fadeout("slow";
    //        return false;
    //    }
    //});

    //$("#txtminexpereince").keypress(function (e) {
    //   // if the letter is not digit then display error and don't type anything
    //    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
    //       // display error message
    //         $("#errmsg".html("digits only".show().fadeout("slow";
    //        return false;
    //    }
    //});
    //$("#txtmaxexpereince").keypress(function (e) {
    //    //if the letter is not digit then display error and don't type anything
    //    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
    //        //display error message
    //         $("#errmsg".html("digits only".show().fadeout("slow";
    //        return false;
    //    }
    //});
    //$("#txtsalaryrange").keypress(function (e) {
    //   // if the letter is not digit then display error and don't type anything
    //    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
    //        //display error message
    //         $("#errmsg".html("digits only".show().fadeout("slow";
    //        return false;
    //    }
    //});
});

          
function loadingjobmasterdetails() {


    $.ajax({
        type: "get",
        contenttype: "application/json; charset=utf-8",
        url: "/Recruitment/AppliedCandidateList",

        success: onsuccessjobmasterdetails,
        error: function (error) {

        },
        cache: false
    });
}
function onsuccessjobmasterdetails(data) {
                $("#AllSateList").empty();
            $("#AllSateList").append("<table id='allsatedatadetailtable' class='table u-table--v3 g-color-black'>");
                $("#allsatedatadetailtable").empty();
                $("#allsatedatadetailtable").append("<thead>" +
                    "<tr class='ob_gc'>" +
                "<th>sno</th>" +
                        "<th>candidate name</th>" +
                        "<th>job title</th>" +
                        "<th>department name</th>" +
                        "<th>designation name</th>" +
                        "<th>status</th>" +
                        "<th>apply date</th>" +
                        "<th>available date</th>" +
                        "<th class='last-td'>action</th> " +
                        "</tr>" +
                    "</thead>" +
                "<tbody id='values1'>");
            if (data.length > 0) {
                var val = '';

                    for (var i = 0; i < data.length; i++) {
                        var fulldatestart = new date(data[i].applydate);
                    var twodigitmonthstart = (fulldatestart.getmonth() + 1) + "";
                    if (twodigitmonthstart.length == 1)
                    twodigitmonthstart = "0" + twodigitmonthstart;
                    var twodigitdatestart = fulldatestart.getdate() + "";
                    if (twodigitdatestart.length == 1)
                    twodigitdatestart = "0" + twodigitdatestart;

                        var currentdatestart = twodigitdatestart + "/" + twodigitmonthstart + "/" + fulldatestart.getfullyear();

                        var fulldatestart1 = new date(data[i].availabledate);
                        var twodigitmonthstart1 = (fulldatestart1.getmonth() + 1) + "";
                        if (twodigitmonthstart1.length == 1)
                            twodigitmonthstart1 = "0" + twodigitmonthstart1;
                        var twodigitdatestart1 = fulldatestart1.getdate() + "";
                        if (twodigitdatestart1.length == 1)
                            twodigitdatestart1 = "0" + twodigitdatestart1;

                        var currentdatestart1 = twodigitdatestart1 + "/" + twodigitmonthstart1 + "/" + fulldatestart1.getfullyear();

                    val = "<tr>" +
                         "<td class='first-td'>" + data[i].id + "</td>" +
                        "<td>" + data[i].name + "</td>" +
                        "<td>" + data[i].jobtitle + "</td>" +
                        "<td>" + data[i].depertmentname + "</td>" +
                        "<td>" + data[i].designationname + "</td>" +
                        "<td>" + data[i].status + "</td>" +
                        "<td>" + currentdatestart + "</td>" +
                        "<td>" + currentdatestart1 + "</td>" +
                        "<td class='last-td'><a href='/Recruitment/AddAppliedCandidate/'" + data[i].id+"' class='btn btn-default btn-sm' title='edit' style='cursor: pointer;' onclick='loadingeditdata(" + data[i].id + ")' ><i class='fa fas fa-pencil'></i></a><a class='btn  btn-sm' title='delete' style='cursor: pointer;' onclick= delete(" + data[i].id + ") > <i class='fa fa-trash'></i></a > <a  href=" + data[i].attachment+" class='btn  btn-sm' title='view' style='cursor: pointer;'><i class='fa fa-eye'></i></a></td>"+
                         //"<td class='last-td'><label class='form-check-inline u-check g-mr-20 mx-0 mb-0'> <input class='g-hidden-xs-up g-pos-abs g-top-0 g-right-0' id='btnedit' href='https://localhost:44300/recruitment/editappliedcandidate/1' onclick='loadingeditdata(" + data.data[i].pk_metadataid + ")' name='radgroup1_1' data-toggle='modal' data-target='#mymodal'><i class='fa fa-pencil'></i></label ></td>" +
                            + "</tr></tbody>";


                    $('#values1').append(val);
                }
            }


                    $('#allsatedatadetailtable').datatable({
                        "bprocessing": true,
                    "bpaginate": true,
                    "spaginationtype": "full_numbers",
                    "responsive": true,
                    "ordering": true
            });

}



//function loadingeditdata(id) {
//    debugger
//    mode = "update";
//    $("#addtask").hide();
//    $(".updatetask").hide();
//    $("#divaddtask").append(" <button type='button' class='btn btn-inverse-primary  my-auto ml-auto updatetask' id='updatetask' onclick='updatestate(" + id + ");'>update</button>");

//    $.ajax({
//        type: "post",
//        contenttype: "application/json; charset=utf-8",
//        url: "/recruitment/addappliedcandidate/" + id,
//        success: onsuccesseditdetails,
//        error: function (error) {
//            alert("error", error);
//        },
//        cache: false
//    });

//}

//function onsuccesseditdetails(data) {
//    debugger;
//    window.location.href = "https://localhost:44300/recruitment/addappliedcandidate"

//    var date = new date(data.closingdate);
//      populatestepsform($.parsejson(json.stringify(data)));
//    var newdate = date.tostring('yy-mm-dd');

//    $("#txtcandidatename").val(data.candidatename);
//    $("#txtfathername").val(data.fathername);
//    $("#txtemail").val(data.email);
//    $("#txtcnic").val(data.cnic);
//    $("#txtcontactnumber").val(data.contactnumber);
//    $("#txtjobtitle").val(data.jobtitle);
//    $("#txtaddvertiseno").val(data.addvertiseno);
//    $("#ddljobtype").val(data.jobtype).select2();
//    $("#ddlshift").val(data.shiftid).select2();
//    $("#ddldepartment").val(data.departmentid).select2();
//    $("#ddldesignation").val(data.designationid).select2();
//    $("#ddlskills").val(data.skills).select2();
//    $("#txtjoblocation").val(data.location);
//    $("#ddlgender").val(data.gender).select2();
//    $("#txtage").val(data.age);
//    $("#txtminexpereince").val(data.minexpereince);
//    $("#txtmaxexpereince").val(data.maxexpereince);
//    $("#txtminqualification").val(data.minqualification);
//    $("#txtsalaryrange").val(data.salaryrange);
//    $("#ddlcurrency").val(data.currency).select2();
//    $("#ddlstatus").val(data.status).select2();
//    $("#txtappliedfrom").val(data.appliedfrom);
//    $('#applydate').datepicker({ dateformat: "yy-mm-dd" }).find('input').val(data.applydate);
//    $('#availabledate').datepicker({ dateformat: "yy-mm-dd" }).find('input').val(data.availabledate);

//    $("#activechk").prop('checked', data.active);
//}


function populatestepsform(data) {
    $.each(data, function (key, value) {
        var $el = $(`[name="${key}"]`),
            type = $el.attr('type');
        if ($el.length > 0)
            switch (type) {
                case 'checkbox':
                    $el.attr('checked', 'checked');
                    break;
                case 'radio':
                    $el.filter('[value="' + val + '"]').attr('checked', 'checked');
                    break;
                default:
                    $el.val(value);
            }
    });
}




function savedata() {

    var formdata = new formdata();
    var candidatename = $("#txtcandidatename").val();
    var fathername = $("#txtfathername").val();
    var email = $("#txtemail").val();
    var cnic = $("#txtcnic").val();
    var contactnumber = $("#txtcontactnumber").val();
    var jobtitle = $("#txtjobtitle").val();
    var addvertiseno = $("#txtaddvertiseno").val();
    var jobtype = $("#ddljobtype").val();
    var shift = $("#ddlshift").val();
    var department = $("#ddldepartment").val();
    var designation = $("#ddldesignation").val();
    var skills = $("#ddlskills").val();
    var joblocation = $("#txtjoblocation").val();
    var gender = $("#ddlgender").val();
    var currency = $("#ddlcurrency").val();
    var status = $("#ddlstatus").val();
    var appliedfrom = $("#txtappliedfrom").val();
    var age = $("#txtage").val();
    var minexpereince = $("#txtminexpereince").val();
    var maxexpereince = $("#txtmaxexpereince").val();
    var minqualification = $("#txtminqualification").val();
    var salaryrange = $("#txtsalaryrange").val();
    var applydate = $('#applydate').datepicker({ dateformat: "yy-mm-dd" }).find('input').val();
    var availabledate = $('#availabledate').datepicker({ dateformat: "yy-mm-dd" }).find('input').val();
    var active = $('#activechk').is(":checked");


            if (jobtitle == null || jobtitle == 'string' || !jobtitle.trim()) {

                swal.fire({
                    icon: 'warning',
                    dangermode: true,
                    text: 'please fill job title!'
                });
            return false;
             }
            if (jobtype == null || jobtype == -1 || jobtype == "") {
                swal.fire({
                    icon: 'warning',
                    text: 'please select jobtype!'
                });
            return false;
    }

            if (department == null || department == -1 || department == "") {
                swal.fire({
                    icon: 'warning',
                    text: 'please select department!'
                });
            return false;
    }

            if (designation == null || designation == -1 || designation == "") {
                swal.fire({
                    icon: 'warning',
                    text: 'please select designation!'
                });
            return false;
    }

            if (shift == null || shift == -1 || shift == "") {
                swal.fire({
                    icon: 'warning',
                    text: 'please select shift!'
                });
            return false;
    }

            if (skills == null || skills == -1 || skills == "") {
                swal.fire({
                    icon: 'warning',
                    text: 'please select skills!'
                });
            return false;
    }

    var files = $("#uploadimg").get(0).files;
    if (files.length > 0) {
        formdata.append("photo", files[0]);
    }

    var uploadfile = $("#uploadfile").get(0).files;
    if (uploadfile.length > 0) {
        formdata.append("attachment", uploadfile[0]);
    }



    formdata.append("candidatename", candidatename);
    formdata.append("fathername", fathername);
    formdata.append("cnic", cnic);
    formdata.append("email", email);
    formdata.append("contactnumber", contactnumber);
    formdata.append("jobtitle", jobtitle);
    formdata.append("jobtype", jobtype);
    formdata.append("shiftid", shift);
    formdata.append("departmentid", department);
    formdata.append("designationid", designation);
    formdata.append("minexpereince", minexpereince);
    formdata.append("maxexpereince", maxexpereince);
    formdata.append("minqualification", minqualification);
    formdata.append("addvertiseno", addvertiseno);
    formdata.append("addvertiseno", addvertiseno);
    formdata.append("location", joblocation);
    formdata.append("gender", gender);
    formdata.append("currency", currency);
    formdata.append("status", status);
    formdata.append("age", age);
    formdata.append("skills", skills);
    formdata.append("expectedsalary", salaryrange);
    formdata.append("appliedfrom", appliedfrom);
    formdata.append("applydate", applydate);
    formdata.append("availabledate", availabledate);
    formdata.append("active", active);

             var data = json.stringify(
            {
                candidatename: candidatename,
                fathername: fathername,
                cnic: cnic,
                email: email,
                contactnumber: contactnumber,
                jobtitle: jobtitle,
                jobtype: jobtype,
                shiftid: shift,
               departmentid: department,
               designationid: designation,
               minexpereince: minexpereince,
               maxexpereince: maxexpereince,
               minqualification: minqualification,
                addvertiseno: addvertiseno,
               location: joblocation,
                gender: gender,
                currency: currency,
                status: status,
                 age: age,
                skills: skills,
                expectedsalary: salaryrange,
                appliedfrom: appliedfrom,
                applydate: applydate,
                availabledate: availabledate,
               active: active
    });

    $.ajax({
        type: "post",
        data: formdata,
        contenttype: false, // not to set any content header
        processdata: false, // not to process data
            contenttype: "application/json; charset=utf-8",
        datatype: "json",
        url: "/recruitment/addappliedcandidate",
        success: onsuccessaddmanagement,
        error: function (error) {
            swal.fire({
                icon: 'error',
                title: 'oops...',
                text: 'something went wrong to insert new applied candidate!',
            });
        },
        cache: false
    });

}

function onsuccessaddmanagement(data) {
    let timerinterval
    swal.fire({
        icon: 'success',
        title: 'your work has been saved',
        timer: 2500,
        timerprogressbar: true,
        didopen: () => {
            swal.showloading()
            timerinterval = setinterval(() => {
                const content = swal.gethtmlcontainer()
                if (content) {
                    const b = content.queryselector('b')
                    if (b) {
                        b.textcontent = swal.gettimerleft()
                    }
                }
            }, 2500)
        },
        willclose: () => {
            clearinterval(timerinterval)
        }
    }).then((result) => {
         
        if (result.dismiss === swal.dismissreason.timer) {

        }
    })

    mode = "";
    window.location.href = "https://localhost:44300/recruitment/hrmappliedcandidate"
    location.reload(true);
      clearfields();
    loadingjobmasterdetails();

}

function updatestate(id) {
    mode = "update";
    var candidatename = $("#txtcandidatename").val();
    var fathername = $("#txtfathername").val();
    var email = $("#txtemail").val();
    var cnic = $("#txtcnic").val();
    var contactnumber = $("#txtcontactnumber").val();
    var jobtitle = $("#txtjobtitle").val();
    var addvertiseno = $("#txtaddvertiseno").val();
    var jobtype = $("#ddljobtype").val();
    var shift = $("#ddlshift").val();
    var department = $("#ddldepartment").val();
    var designation = $("#ddldesignation").val();
    var skills = $("#ddlskills").val();
    var joblocation = $("#txtjoblocation").val();
    var gender = $("#ddlgender").val();
    var currency = $("#ddlcurrency").val();
    var status = $("#ddlstatus").val();
    var appliedfrom = $("#txtappliedfrom").val();
    var age = $("#txtage").val();
    var minexpereince = $("#txtminexpereince").val();
    var maxexpereince = $("#txtmaxexpereince").val();
    var minqualification = $("#txtminqualification").val();
    var salaryrange = $("#txtsalaryrange").val();
    var applydate = $('#applydate').datepicker({ dateformat: "yy-mm-dd" }).find('input').val();
    var availabledate = $('#availabledate').datepicker({ dateformat: "yy-mm-dd" }).find('input').val();
    var active = $('#activechk').is(":checked");

    var data = json.stringify(
        {
            appliedid: id,
            candidatename: candidatename,
            fathername: fathername,
            cnic: cnic,
            email: email,
            contactnumber: contactnumber,
            jobtitle: jobtitle,
            jobtype: jobtype,
            shiftid: shift,
            departmentid: department,
            designationid: designation,
            minexpereince: minexpereince,
            maxexpereince: maxexpereince,
            minqualification: minqualification,
            addvertiseno: addvertiseno,
            location: joblocation,
            gender: gender,
            currency: currency,
            status: status,
            age: age,
            skills: skills,
            expectedsalary: salaryrange,
            appliedfrom: appliedfrom,
            applydate: applydate,
            availabledate: availabledate,
            active: active
        });
    $.ajax({
        type: "post",
        data: data,
        contenttype: "application/json; charset=utf-8",
        datatype: "json",
        url: "/Rrecruitment/updateappliedcandidate",
        success: onsuccessupdatemanagement,
        error: function (error) {
            swal.fire({
                icon: 'error',
                title: 'oops...',
                text: 'something went wrong to insert new state!',
            });
        },
        cache: false,
    });
}
function onsuccessupdatemanagement(data) {
    let timerinterval
    swal.fire({
        icon: 'success',
        title: 'your work has been updated',
        timer: 2500,
        timerprogressbar: true,
        didopen: () => {
            swal.showloading()
            timerinterval = setinterval(() => {
                const content = swal.gethtmlcontainer()
                if (content) {
                    const b = content.queryselector('b')
                    if (b) {
                        b.textcontent = swal.gettimerleft()
                    }
                }
            }, 2500)
        },
        willclose: () => {
            clearinterval(timerinterval)
        }
    }).then((result) => {
         //read more about handling dismissals below 
        if (result.dismiss === swal.dismissreason.timer) {

        }
    })

    mode = "";
    globalimagespath = "";
     location.reload(true);
    clearfields();
    loadingjobmasterdetails();

}




function getjobtypeddl() {

    $.ajax({
        type: "get",
        contenttype: "application/json; charset=utf-8",
        url: "/master/getjobtypelist",
        success: function (data, status) {
            ;
            if (data != "") {

                $('#ddljobtype').empty();
                $('#ddljobtype').append('<option value="-1" selected disabled>select jobtype</option>');
                $.each(data, function (i, tweet) {
                    $('#ddljobtype').append('<option value="' + data[i].id + '">' + data[i].name + '</option>');

                });

            }

        },
        error: function (error) {
            swal.fire({
                icon: 'error',
                title: 'oops...',
                text: 'something went wrong!',
            });
        },
        cache: false,

    });
}

function getdepartmentddl() {

    $.ajax({
        type: "get",
        contenttype: "application/json; charset=utf-8",
        url: "/master/getdepartmentlist",
        success: function (data, status) {
            
            if (data != "") {

                $('#ddldepartment').empty();
                $('#ddldepartment').append('<option value="-1" selected disabled>select department</option>');
                $.each(data, function (i, tweet) {
                    $('#ddldepartment').append('<option value="' + data[i].id + '">' + data[i].name + '</option>');

                });

            }

        },
        error: function (error) {
            swal.fire({
                icon: 'error',
                title: 'oops...',
                text: 'something went wrong!',
            });
        },
        cache: false,

    });
}

function getdesignationddl() {

    $.ajax({
        type: "get",
        contenttype: "application/json; charset=utf-8",
        url: "/master/getdesignationlist",
        success: function (data, status) {
            ;
            if (data != "") {

                $('#ddldesignation').empty();
                $('#ddldesignation').append('<option value="-1" selected disabled>select designation</option>');
                $.each(data, function (i, tweet) {
                    $('#ddldesignation').append('<option value="' + data[i].id + '">' + data[i].name + '</option>');

                });

            }

        },
        error: function (error) {
            swal.fire({
                icon: 'error',
                title: 'oops...',
                text: 'something went wrong!',
            });
        },
        cache: false,

    });
}

function gethrmskillsddl() {

    $.ajax({
        type: "get",
        contenttype: "application/json; charset=utf-8",
        url: "/master/gethrmskillslist",
        success: function (data, status) {
            ;
            if (data != "") {

                $('#ddlskills').empty();
                $('#ddlskills').append('<option value="-1" selected disabled>select skills</option>');
                $.each(data, function (i, tweet) {
                    $('#ddlskills').append('<option value="' + data[i].id + '">' + data[i].name + '</option>');

                });

            }

        },
        error: function (error) {
            swal.fire({
                icon: 'error',
                title: 'oops...',
                text: 'something went wrong!',
            });
        },
        cache: false,

    });
}

function clearfields() {
    $(".updatetask").hide();
    $("#addtask").show();

    $("#txtcandidatename").val("");
    $("#txtfathername").val("");
    $("#txtemail").val("");
    $("#txtcnic").val("");
    $("#txtcontactnumber").val("");
    $("#txtjobtitle").val("");
    $("#txtaddvertiseno").val("");
    $("#ddljobtype").val("-1").select2();
    $("#ddlshift").val("-1").select2();
    $("#ddldepartment").val("-1").select2();
    $("#ddldesignation").val("-1").select2();
    $("#ddlskills").val("-1").select2();
    $("#txtjoblocation").val("");
    $("#ddlcurrency").val();
    $("#ddlstatus").val();
    $("#txtappliedfrom").val();
    $("#txtage").val("");
    $("#txtminexpereince").val("");
    $("#txtmaxexpereince").val("");
    $("#txtminqualification").val("");
    $("#txtsalaryrange").val("");
    $("#txtjobdescription").val("");
    $("#applydate").val("");
    $("#availabledate").val("");

}


