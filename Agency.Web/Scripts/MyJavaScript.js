var counter = 0;

$(document)
    .ready(function () {
        $('#BtAddVehicle').click(function () {

            counter = document.getElementById("FormContainer").rows.length-2;
            counter++;
            $('#WarningItemDelete').html("حذف آیتم");
            var newTr = $("#vehicleitem").clone(true,true);
            newTr.attr("id", "vehicleitem-" + counter);
            newTr.removeAttr('Hidden');
            newTr.find("span")
                .each(function () {
                    var validationname = $(this).attr("data-valmsg-for").replace("0", counter);
                    $(this).attr("data-valmsg-for", validationname);
                    if ($(this).attr("for") != null) {
                        var spanfor = $(this).attr("for").replace("0", counter);
                        $(this).attr("for", spanfor);
                    }
                });
            newTr.find("input").each(function() {
                $(this).attr("name", "VehicleList[" + counter + "].Count");
                $(this).val(1);
            });
            newTr.find("select").each(function () {
                $(this).attr("name", "VehicleList[" + counter + "].VehicleId");
                var e = $(this).find("option");
                for (var i = 1; i < e.length; i++) {
                    if (e[i].hasAttribute("selected")) {
                       // e[i].selectedIndex = -2;
                        //delete e[i].attributes[0];
                        //var f= e[i].attributes[0];
                    }
                    //if (e[i].attr("selected") != null) {
                    //    e[i].removeProp("selected");
                    //}

                }
                //$(this).children('option').each(function () {
                //    var x = $(this);
                //    $(this).attr("selected", "");
                //});
            });
                //شمارنده ی ردیف است
            //newTr.find("label:first").text(counter + 1);


            var btn = '<td class="col-md-1"><button class="removeBtn btn btn-sm btn-danger" type="button" >x</button></td>';
            $(newTr).append(btn);
            $('#FormContainer').append(newTr);

            //vase validate kardane ajza
            $("#CreateTourForm").data('validator', null);
            $.validator.unobtrusive.parse("#CreateTourForm");
        });
    });

////
var roomcounter = 0;

$(document)
    .ready(function () {
        $('#BtAddRoom').click(function () {

            roomcounter = document.getElementById("RoomTable").rows.length - 2;
            roomcounter++;
            $('#WarningItemDelete').html("حذف آیتم");
            var newTr = $("#roomitem").clone(true,true);
            newTr.attr("id", "roomitem-" + roomcounter);

            newTr.find("span")
                .each(function () {
                    var validationname = $(this).attr("data-valmsg-for").replace("0", roomcounter);
                    $(this).attr("data-valmsg-for", validationname);
                    if ($(this).attr("for") != null) {
                        var spanfor = $(this).attr("for").replace("0", roomcounter);
                        $(this).attr("for", spanfor);
                    }
                });
            newTr.find("input").each(function () {
                var t = $(this).attr("name").replace("0", roomcounter);
                var tt = $(this).attr("id").replace("0", roomcounter);
                $(this).attr("name", t);
                $(this).attr("id", tt);
                $(this).val("value", "");
                $(this).val("");
                $(this).removeClass("hasDatepicker");
                //  $("input").datepicker("option", "disabled", true);
                //   $("input").datepicker("clearDates");
                //  $("input").remove('datepicker');
                // $("input").datepicker('remove');
                //   $('input').val('').datepicker('update');
                //$("input").datepicker("disable");

            });
            newTr.find("textarea").each(function () {
                var t = $(this).attr("name").replace("0", roomcounter);
                $(this).attr("name", t);
                $(this).val("");
            });
            newTr.find("select").each(function () {
                $(this).attr("name", "RoomMainHotelList[" + roomcounter + "].RoomId");
            });
                //شمارنده ی ردیف است
            //newTr.find("label:first").text(counter + 1);


            var btn = '<td class="col-md-1"><button class="removeBtnRoom btn btn-sm btn-danger" type="button" >x</button></td>';
            $(newTr).append(btn);
            $('#RoomTable').append(newTr);

            //vase validate kardane ajza
            $("#CreateMainHotelForm").data('validator', null);
            $.validator.unobtrusive.parse("#CreateMainHotelForm");
        });

    });

$(document).on("click", ".removeBtnRoom", function () {
    //$(this).parent('div').remove();
    if ($("#RoomTable tr").length === 1) {
        $('#WarningItemDelete').html(" ");
    }

    var table = document.getElementById("RoomTable");
    var count = table.rows.length;
    var thisId = $(this).closest('tr').attr("id");
    for (var i = 1 ; i < count; i++) {
        if (table.rows[i].id > thisId) {
            var number = table.rows[i].id;
            number = number.substring(9);
            number--;
            table.rows[i].id = "roomitem-" + number;

            var myrow = table.rows[i];
            $(myrow).find("input").each(function () {

                if ($(this).attr("id") != null) {
                    var newId = $(this).attr("id").replace(number + 1, number);
                    $(this).attr("id", newId);
                }
                if ($(this).attr("name") != null) {
                    var newName = $(this).attr("name").replace(number + 1, number);
                    $(this).attr("name", newName);
                }

            });
            $(myrow).find("textarea").each(function () {

                if ($(this).attr("id") != null) {
                    var newId = $(this).attr("id").replace(number + 1, number);
                    $(this).attr("id", newId);
                }
                if ($(this).attr("name") != null) {
                    var newName = $(this).attr("name").replace(number + 1, number);
                    $(this).attr("name", newName);
                }

            });
            $(myrow).find("select").each(function () {      

                if ($(this).attr("id") != null) {
                    var newId = $(this).attr("id").replace(number + 1, number);
                    $(this).attr("id", newId);
                }
                if ($(this).attr("name") != null) {
                    var newName = $(this).attr("name").replace(number + 1, number);
                    $(this).attr("name", newName);
                }

            });
        }
    }
    $(this).closest('tr').remove();
    roomcounter--;
});
////

$(document).on("click", ".removeBtn", function () {
    //$(this).parent('div').remove();
    if ($("#FormContainer tr").length === 3) {
        $('#WarningItemDelete').html(" ");
    }

    var table = document.getElementById("FormContainer");
    var count = table.rows.length;
    var thisId = $(this).closest('tr').attr("id");
    for (var i = 1 ; i < count; i++) {
        if (table.rows[i].id > thisId) {
            var number = table.rows[i].id;
            number = number.substring(12);
            number--;
            table.rows[i].id = "vehicleitem-" + number;

            var myrow = table.rows[i];
            $(myrow).find("input,select").each(function () {

                    if ($(this).attr("id") != null) {
                        var newId = $(this).attr("id").replace(number+1, number);
                        $(this).attr("id", newId);
                    }
                if ($(this).attr("name") != null) {
                    var newName = $(this).attr("name").replace(number+1, number);
                    $(this).attr("name", newName);
                }

            });
        }
    }
    $(this).closest('tr').remove();
    counter--;
});




var optioncounter = 0;
$(document)
    .ready(function () {
        $('#BtAddOption').click(function () {
            optioncounter = document.getElementById("OptionTable").rows.length - 2;
            optioncounter++;
            $('#OptionItemDeleteColumn').html("حذف آپشن");
            var newTr = $("#optionitem-0").clone();
            newTr.attr("id", "optionitem-" + optioncounter);
            newTr.removeAttr('Hidden');

            newTr.find("span")
                .each(function () {
                    var validationname = $(this).attr("data-valmsg-for").replace("0", optioncounter);
                    $(this).attr("data-valmsg-for", validationname);

                    if ($(this).attr("for") != null) {
                        var spanfor = $(this).attr("for").replace("0", optioncounter);
                        $(this).attr("for", spanfor);
                    }
                });
            newTr.find("input").each(function () {
                if ($(this).attr("name").includes("Title")) {
                    $(this).attr("name", "OptionList[" + optioncounter + "].Title");
                }
                else if ($(this).attr("name").includes("Cost")) {

                    $(this).attr("name", "OptionList[" + optioncounter + "].Cost");
                }
                $(this).val("");
            });
            //شمارنده ی ردیف است
            //newTr.find("label:first").text(counter + 1);


            $('#OptionTable').append(newTr);

            //vase validate kardane ajza
            $("#CreateTourForm").data('validator', null);
            $.validator.unobtrusive.parse("#CreateTourForm");
        });
    });

$(document).on("click", ".removeBtnOption", function () {
    //$(this).parent('div').remove();
    if ($("#OptionTable tr").length === 1) {
        $('#OptionItemDeleteColumn').html(" ");
    }

    var table = document.getElementById("OptionTable");
    var count = table.rows.length;
    var thisId = $(this).closest('tr').attr("id");
    for (var i = 1 ; i < count; i++) {
        if (table.rows[i].id > thisId) {
            var number = table.rows[i].id;
            number = number.substring(11);
            number--;
            table.rows[i].id = "optionitem-" + number;

            var myrow = table.rows[i];
            $(myrow).find("input").each(function () {

                if ($(this).attr("id") != null) {
                    var newId = $(this).attr("id").replace(number + 1, number);
                    $(this).attr("id", newId);
                }
                if ($(this).attr("name") != null) {
                    var newName = $(this).attr("name").replace(number + 1, number);
                    $(this).attr("name", newName);
                }

            });
        }
    }
    $(this).closest('tr').remove();
    optioncounter--;
});


var personcounter = 0;
$(document)
    .ready(function () {
        $('#BtAddPerson').click(function () {
            //tedade div haye persone mojud az gabl
            var prevpersoncount = document.getElementsByClassName("editpersonclass").length;

            var newTr = $("#personitem-0").clone(true);
            newTr.class = "personclass";
            newTr[0].style.display = 'block';
            //document.getElementById("personitem-0").style.display = 'block';

            //document.getElementById("H1")[0].setAttribute("class", "democlass");
            var btn = $(newTr).find("#buttonpersonitem");
            var buttonhtml = '<i class="fa fa-user"></i>' + "اطلاعات شخص " + (prevpersoncount );
            btn.html(buttonhtml);
            var p = "#show-search" + prevpersoncount;
            btn.attr("data-target", p);
            var div = $(newTr).find("#show-search0");
            p = p.substr(1);
            div.attr("id", p);
            newTr.attr("id", "personitem-" + (prevpersoncount));

            

            newTr.find("span")
                .each(function () {
                    var validationname = $(this).attr("data-valmsg-for").replace("0", personcounter).replace("ListEditPersonViewModel", "ListPersonViewModel");
                    $(this).attr("data-valmsg-for", validationname);
                    if ($(this).attr("for") != null) {
                        var spanfor = $(this).attr("for").replace("0", personcounter);
                        $(this).attr("for", spanfor);
                    }
                });

            newTr.find("input").each(function () {
                var name = $(this).attr("name").replace("0", personcounter).replace("ListEditPersonViewModel", "ListPersonViewModel");
                var id = $(this).attr("id").replace("0", personcounter).replace("ListEditPersonViewModel", "ListPersonViewModel");      
                $(this).attr("name", name);
                $(this).attr("id", id);
                $(this).attr("value", "");
                $(this).val("");
                if ($(this).hasClass("genderman")) {
                    $(this).val("false");
                }
                else if ($(this).hasClass("genderwoman")) {
                    $(this).val("true");
                }
            });
            newTr.find("select").each(function () {
                var name = $(this).attr("name").replace("0", personcounter).replace("ListEditPersonViewModel", "ListPersonViewModel");
                var id = $(this).attr("id").replace("0", personcounter).replace("ListEditPersonViewModel", "ListPersonViewModel");
                $(this).attr("name", name);
                $(this).attr("id", id);
                $(this).attr("value", "");
                $(this).val("");
            });

            
            //شمارنده ی ردیف است
            //newTr.find("label:first").text(counter + 1);


            $('#persondiv').append(newTr);
            personcounter++;

            //vase validate kardane ajza
            $("#CreateTourForm").data('validator', null);
            $.validator.unobtrusive.parse("#CreateTourForm");
        });
    });

$(document).on("click", ".removeBtnPerson", function () {
    //$(this).parent('div').remove();
    var id = $(this).parent().parent().attr("id");
    var index = id.split("-");
    index = index[1]-1;
    var divs = document.getElementsByClassName("personclass");


    for (var ii = 0; ii < divs.length; ii++) {
        var div = divs[ii];
        var id2 = div.id;
        if (id2 > id) {
            var inputs = document.getElementById(id2).getElementsByTagName('input');
            for (var j = 0; j < inputs.length; j++) {
                if (inputs[j].id != null) {
                    var newId = inputs[j].id.replace(index + 1, index);
                    inputs[j].id = newId;
                }
                if (inputs[j].name != null) {
                    var newName = inputs[j].name.replace(index + 1, index);
                    inputs[j].name = newName;
                }
                if (inputs[j].classList.contains("hasDatepicker")) {
                    inputs[j].classList.remove("hasDatepicker","valid");
                }
            }
            var selects = document.getElementById(id2).getElementsByTagName('select');
            for (var j = 0; j < selects.length; j++) {
                if (selects[j].id != null) {
                    var newId = selects[j].id.replace(index + 1, index);
                    selects[j].id = newId;
                }
                if (selects[j].name != null) {
                    var newName = selects[j].name.replace(index + 1, index);
                    selects[j].name = newName;
                }
            }
            var spans = document.getElementById(id2).getElementsByTagName('span');
            for (var j = 0; j < spans.length; j++) {
                var g = spans[j].attributes[1].value.replace(index + 1, index);
                spans[j].attributes[1].value = g;
            }
            
            index = index + 1;
            div.id = "personitem-" + index;
            var btn = div.childNodes[1].childNodes[3].childNodes[1];
            //var btn = div.find("#buttonpersonitem");
            btn.innerText = "اطلاعات شخص " + (index);
            btn.dataset.target = "#show-search" + index;
            div.childNodes[3].id = "show-search" + index;
            //var buttonhtml = '<i class="fa fa-user"></i>' + "اطلاعات شخص " + (index);
            //btn.html(buttonhtml);
        }
        
    }
    document.getElementById(id).remove();
    personcounter--;
});

$(document).on("click", ".removeBtnPerson2", function () {
    //$(this).parent('div').remove();
    var id = $(this).attr("id");
    var index = id.split("-");
    index = index[1]-1;
    var divs = document.getElementsByClassName("editpersonclass");
    for (var ii = 0; ii < divs.length; ii++) {
        var div = divs[ii];
        var id2 = div.id;
        if (id2 > id) {
            var inputs = document.getElementById(id2).getElementsByTagName('input');
            for (var j = 0; j < inputs.length; j++) {
                if (inputs[j].id != null) {
                    var newId = inputs[j].id.replace(index + 1, index);
                    inputs[j].id = newId;
                }
                if (inputs[j].name != null) {
                    var newName = inputs[j].name.replace(index + 1, index);
                    inputs[j].name = newName;
                }
                if (inputs[j].classList.contains("hasDatepicker")) {
                    inputs[j].classList.remove("hasDatepicker","valid");
                }
            }
            var selects = document.getElementById(id2).getElementsByTagName('select');
            for (var j = 0; j < selects.length; j++) {
                if (selects[j].id != null) {
                    var newId = selects[j].id.replace(index + 1, index);
                    selects[j].id = newId;
                }
                if (selects[j].name != null) {
                    var newName = selects[j].name.replace(index + 1, index);
                    selects[j].name = newName;
                }
            }
            var spans = document.getElementById(id2).getElementsByTagName('span');
            for (var j = 0; j < spans.length; j++) {
                var g = spans[j].attributes[1].value.replace(index + 1, index);
                spans[j].attributes[1].value = g;
            }
            
            index = index + 1;
            div.id = "personitem-" + index;
            var btn = div.childNodes[1].childNodes[3].childNodes[1];
            //var btn = div.find("#buttonpersonitem");
            btn.innerText = "اطلاعات شخص " + (index);
            btn.dataset.target = "#show-search" + index;
            div.childNodes[3].id = "show-search" + index;
            //var buttonhtml = '<i class="fa fa-user"></i>' + "اطلاعات شخص " + (index);
            //btn.html(buttonhtml);
        }
        
    }
    document.getElementById(id).remove();
    personcounter--;
});

$(document).on("click", ".removeBtn11", function () {
    //$(this).parent('div').remove();
    if ($("#FormContainer tr").length === 3) {
        $('#WarningItemDelete').html(" ");
    }

    var divs = document.getElementById("persondiv");
    var p = divs.getElementsByClassName("editpersonclass");
    var count = divs.getElementsByClassName("editpersonclass").length;
    var thisId = $(this).parent().parent().parent().attr("id");
    for (var i = 1 ; i < count; i++) {
        if (p[i].id > thisId) {
            var number = p[i].id;
            number = number.substring(11);
            number--;
            p[i].id = "personitem-" + number;
            var btn = p[i].childNodes[1].childNodes[1].childNodes[1];
            p[i].childNodes[3].id = "#show-search" + number;
           btn.innerText = "اطلاعات شخص " + (number);;
           btn.dataset.target = "#show-search" + number;
          //div.childNodes[3].id = "show-search" + number;
            var myrow = p[i];
            $(myrow).find("input,select").each(function () {

                    if ($(this).attr("id") != null) {
                        var newId = $(this).attr("id").replace(number+1, number);
                        $(this).attr("id", newId);
                    }
                if ($(this).attr("name") != null) {
                    var newName = $(this).attr("name").replace(number+1, number);
                    $(this).attr("name", newName);
                }

            });
        }
    }
    $(this).parent().parent().parent().remove();
    counter--;
   // var btn = div.childNodes[1].childNodes[3].childNodes[1];
   
});






function OnSuccess(response) {
    if (response.success) {
        $("#message").html("ویرایش شد");
        alert(response.msg);
        setTimeout(
  function () {
      $("#myModal").modal('hide');
  }, 1500);


    } else {
        $("#message").html("خطا در ویرایش");
        alert("pppp");
        setTimeout(
function () {
    $("#myModal").modal('hide');
}, 1500);
    }
}


function reloadcurrentpage() {
    location.reload();
}

$(document).on("keydown", ".numbertxt", function (e) {
    if (e.keyCode === 8 || e.keyCode===9 ) {
        // let it happen, don't do anything
    }
    else if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});

$(document).on("keydown", ".currencytxt", function (e) {
    if (e.keyCode === 8 || e.keyCode === 9 || e.keyCode === 188) {
        // let it happen, don't do anything
    }
    else if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});

function FillCity() {
    var stateId = $('#DropDownState').val();
    $.ajax({
        url: '/Hotel/SelectCities',
        type: "Post",
        dataType: "JSON",
        data: { id: stateId },
        success: function (cities) {
            $("#DropDownCities").html(""); // clear before appending new list 

            $("#DropDownCities").append('<option value>' + "انتخاب شهر"  + '</option>');
            for (var j = 0; j < cities.length; j++) {
                $("#DropDownCities").append('<option value=' + cities[j].Value + '>' + cities[j].Text + '</option>');
            }
        }
    });
}

function FillCityDes() {
    var stateId = $('#DropDownStateDes').val();
    $.ajax({
        url: '/Hotel/SelectCities',
        type: "Post",
        dataType: "JSON",
        data: { id: stateId },
        success: function (cities) {
            $("#DropDownCitiesDes").html(""); // clear before appending new list 

            $("#DropDownCitiesDes").append('<option value>' + "انتخاب شهر" + '</option>');
            for (var j = 0; j < cities.length; j++) {
                $("#DropDownCitiesDes").append('<option value=' + cities[j].Value + '>' + cities[j].Text + '</option>');
            }
        }
    });
}

function FillHotel() {
    var cityId = $('#DropDownCitiesDes').val();
    $.ajax({
        url: '/Hotel/SelectHotels',
        type: "Post",
        dataType: "JSON",
        data: { id: cityId },
        success: function (cities) {
            $("#DropDownHotel").html(""); // clear before appending new list 

           //$("#DropDownHotel").append('<option value >' + "انتخاب هتل" + '</option>');
            for (var j = 0; j < cities.length; j++) { 
                $("#DropDownHotel").append('<option value=' + cities[j].Value + '>' + cities[j].Text + '</option>');
            }
        }
    });
}

var seatnumber = 1;
$(document).on("click", ".seatbuttonclick", function () {
    var Capacity = parseInt($('#vehicleCapacity').html());
    if (Capacity === seatnumber) {
        $("#createvehicleformatbutton").prop('disabled', false);
    }
    //|| $(this).attr("value") !== (seatnumber - 1).toString()
    if ($(this).attr("value") === undefined) {
        if (Capacity < seatnumber) {
            $("#createvehicleformatbutton").prop('disabled', false);
            warningNoty("ظرفیت وسیله نقلیه تکمیل شده است");
        } else {
            var id = $(this).attr("id");
            $(this).attr("value", seatnumber);
            id = id.replace("button", "#hidden");
            $(id).attr("value", seatnumber);
            seatnumber = seatnumber + 1;
        }

    }
    else if (parseInt($(this).attr("value")) < (seatnumber - 1)) {
        return;
    }
    else if ($(this).value !== undefined) {
        var number = $(this).attr("value");
        var table = $("#SeatTable");
        table.find("input").each(function () {
            if ($(this).attr("value") <= number) {
                $(this).removeAttr("value");
            }
        });
    }
    else if ($(this).attr("value") === (seatnumber - 1).toString()) {
        $("#createvehicleformatbutton").prop('disabled', true);
        $(this).removeAttr("value");
        var id2 = $(this).attr("id");
        id2 = id2.replace("button", "#hidden");
        $(id2).removeAttr("value");
        seatnumber = seatnumber - 1;
    }



});


var Hoteloptioncounter = 0;
$(document)
    .ready(function () {
        $('#BtAddHotelOption').click(function () {
            Hoteloptioncounter = document.getElementById("HotelOptionTable").rows.length - 2;
            Hoteloptioncounter++;
            $('#HotelOptionItemDeleteColumn').html("حذف آپشن");
            var newTr = $("#optionitem-0").clone();
            newTr.attr("id", "optionitem-" + Hoteloptioncounter);
            newTr.removeAttr('Hidden');

            newTr.find("span")
                .each(function () {
                    var validationname = $(this).attr("data-valmsg-for").replace("0", Hoteloptioncounter);
                    $(this).attr("data-valmsg-for", validationname);

                    if ($(this).attr("for") != null) {
                        var spanfor = $(this).attr("for").replace("0", Hoteloptioncounter);
                        $(this).attr("for", spanfor);
                    }
                });
            newTr.find("input").each(function () {
                if ($(this).attr("name").includes("Name")) {
                    $(this).attr("name", "OptionList[" + Hoteloptioncounter + "].Name");
                }
                else if ($(this).attr("name").includes("Price")) {

                    $(this).attr("name", "OptionList[" + Hoteloptioncounter + "].Price");
                }
                //else if ($(this).attr("name").includes("IsFree")) {

                //    $(this).attr("name", "OptionList[" + Hoteloptioncounter + "].IsFree");
                //}
                $(this).val("");
            });
            //شمارنده ی ردیف است
            //newTr.find("label:first").text(counter + 1);


            $('#HotelOptionTable').append(newTr);

            //vase validate kardane ajza
            $("#CreateMainHotelForm").data('validator', null);
            $.validator.unobtrusive.parse("#CreateMainHotelForm");
        });
    });

$(document).on("click", ".removeBtnHotelOption", function () {
    //$(this).parent('div').remove();
    if ($("#HotelOptionTable tr").length === 1) {
        $('#HotelOptionItemDeleteColumn').html(" ");
    }

    var table = document.getElementById("HotelOptionTable");
    var count = table.rows.length;
    var thisId = $(this).closest('tr').attr("id");
    for (var i = 1 ; i < count; i++) {
        if (table.rows[i].id > thisId) {
            var number = table.rows[i].id;
            number = number.substring(11);
            number--;
            table.rows[i].id = "optionitem-" + number;

            var myrow = table.rows[i];
            $(myrow).find("input").each(function () {

                if ($(this).attr("id") != null) {
                    var newId = $(this).attr("id").replace(number + 1, number);
                    $(this).attr("id", newId);
                }
                if ($(this).attr("name") != null) {
                    var newName = $(this).attr("name").replace(number + 1, number);
                    $(this).attr("name", newName);
                }

            });
        }
    }
    $(this).closest('tr').remove();
    Hoteloptioncounter--;
});

//function LoadSeats(reserveId,tourId2) {
//    var vehicleId = $('#dropdownselecttourvehicle').val();
//    window.location.href = '/reserve/CreateReserve3/?reserveid=' +
//        reserveId +
//        '&tourId=' +
//        tourId2 +
//        '&tourVehicleId=' +
//        vehicleId;
//    //$.ajax({
//    //    url: '/reserve/CreateReserve3',
//    //    type: "Get",
//    //    //dataType: "JSON",
//    //    data: {
//    //        reserveid: reserveId,
//    //        tourId: tourId2,
//    //        tourVehicleId: vehicleId
//    //    },
//    //    success: function (cities) {
//    //        aaaaa = cities;
//    //        //$("#DropDownCities").html(""); // clear before appending new list 

//    //        //$("#DropDownCities").append('<option value>' + "انتخاب شهر"  + '</option>');
//    //        //for (var j = 0; j < cities.length; j++) {
//    //        //    $("#DropDownCities").append('<option value=' + cities[j].Value + '>' + cities[j].Text + '</option>');
//    //        //}
//    //    }
//    //});
//}

//$("#butprint").on("click", function () {
//    //alert('d');
//    $.ajax({
//        dataType: "json",
//        url: '/Excel/Export',
//        type: 'Get'

//    }).done(function (a) {
//        if (a.success) {
//            infoNoty(a.msg);
//            window.location = a.url;
//            //alert(a.msg);
//        } else {
//            alert("مشکلی در اکسپورت فایل بوجود آمده است");
//        }

//    });

//});