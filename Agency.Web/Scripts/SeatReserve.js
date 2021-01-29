function ReserveSeat(seatid, reserveid) {
    var personId = $('#SeatPerson').val();
    $.ajax({
        url: '/Reserve/ReserveSeat',
        type: "Get",
        dataType: "JSON",
        async:  false,
        data: {
            personid: personId,
            seatid: seatid,
            reserveid: reserveid
        },
        success: function (data) {
            if (data.IsAuthenticate === false) {
                dangerNoty("لطفا وارد سیستم شوید و ادامه دهید");
                //setTimeout(function () {  //Beginning of code that should run AFTER the timeout
                //    window.open("http://localhost:4147/Account/Login?ReturnUrl=%2F", '_blank');
                //    //lots more code
                //}, 2000);
            }
            else if (data.result === 2) {
                dangerNoty("این صندلی لحظاتی پیش رزرو شد، لطفا صفحه را بارگزاری مجدد کنید");
                //location.reload();
            }
            else if (data.result === 3) {
                dangerNoty("این صندلی غیر فعال شده است");
            }
            else if (data.Success) {
                infoNoty("رزرو صندلی با موفقیت انجام شد");
                location.reload();
            }
            //$("#DropDownHotel").html(""); // clear before appending new list 

            //$("#DropDownHotel").append('<option value >' + "انتخاب هتل" + '</option>');
            //for (var j = 0; j < cities.length; j++) {
            //    $("#DropDownHotel").append('<option value=' + cities[j].Value + '>' + cities[j].Text + '</option>');
            //}
        }
    });
}

var isValidEvent = true;
var count = 0;

$(function () {
    $(".empty-seat")
        .click(function () {
            if (isValidEvent == true) {
                if ($(this).hasClass("select-seat")) {
                    $('.total-count').text(--count);
                    $('.total-price').text(count * 40000 + ' تومان');
                    $(this).removeClass("select-seat");
                }
                $(this).find(".popover-a span").html($(this).attr("value")).removeClass();
            }
            isValidEvent = true;
        });
    $('[data-toggle="popover"]').popover();
    $('body').on('click', function (e) {
        $('[data-toggle=popover]').each(function () {
            // hide any open popovers when the anywhere else in the body is clicked
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('hide');
            }
        });
    });
    $('[title][title!=""]').tooltipster();
});
function DeactiveSeat(tourvehicleId, seatNumber) {
    $.ajax({
        url: '/Tour/DeactiveSeat',
        type: "Get",
        dataType: "JSON",
        async: false,
        data: {
            tourvehicleid: tourvehicleId,
            seatNumber: seatNumber
        },
        success: function () {
            location.reload();
            //if (data.IsAuthenticate === false) {
            //    dangerNoty("لطفا وارد سیستم شوید و ادامه دهید");
            //    //setTimeout(function () {  //Beginning of code that should run AFTER the timeout
            //    //    window.open("http://localhost:4147/Account/Login?ReturnUrl=%2F", '_blank');
            //    //    //lots more code
            //    //}, 2000);
            //}
            //else if (data.Success) {
            //    infoNoty("رزرو صندلی با موفقیت انجام شد");
            //    location.reload();
            //}
            //$("#DropDownHotel").html(""); // clear before appending new list 

            //$("#DropDownHotel").append('<option value >' + "انتخاب هتل" + '</option>');
            //for (var j = 0; j < cities.length; j++) {
            //    $("#DropDownHotel").append('<option value=' + cities[j].Value + '>' + cities[j].Text + '</option>');
            //}
        }
    });
}

function activeSeat(tourvehicleId, seatNumber) {
    $.ajax({
        url: '/Tour/activeSeat',
        type: "Get",
        dataType: "JSON",
        async: false,
        data: {
            tourvehicleid: tourvehicleId,
            seatNumber: seatNumber
        },
        success: function () {
            location.reload();
            //if (data.IsAuthenticate === false) {
            //    dangerNoty("لطفا وارد سیستم شوید و ادامه دهید");
            //    //setTimeout(function () {  //Beginning of code that should run AFTER the timeout
            //    //    window.open("http://localhost:4147/Account/Login?ReturnUrl=%2F", '_blank');
            //    //    //lots more code
            //    //}, 2000);
            //}
            //else if (data.Success) {
            //    infoNoty("رزرو صندلی با موفقیت انجام شد");
            //    location.reload();
            //}
            //$("#DropDownHotel").html(""); // clear before appending new list 

            //$("#DropDownHotel").append('<option value >' + "انتخاب هتل" + '</option>');
            //for (var j = 0; j < cities.length; j++) {
            //    $("#DropDownHotel").append('<option value=' + cities[j].Value + '>' + cities[j].Text + '</option>');
            //}
        }
    });
}















