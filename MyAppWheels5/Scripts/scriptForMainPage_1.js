// Добавление
function FormAjaxQuery(id, Url, key, selectorRespond) {
    var formdata = new FormData();
    formdata.append(key, id);
    $.ajax
        ({
            url: Url,
            type: 'POST',
            data: formdata,
            cache: false,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#spinner").show();
            },
            complete: function () {
                $("#spinner").hide();
            },
            success: function (respond, status, jqXHR) {
                if (typeof respond.error === 'undefined') {
                    $(selectorRespond).append(respond);
                    console.log("button   ",$("#Send>button"));
                    if($("#Send>button").length==0)
                    {
                        button = document.createElement("button");
                        button.className = "btn btn-success";
                        button.id = "btn";
                        button.innerHTML = "Найти";
                        $("#Send").append(button);
                    }
                }
                else {
                    console.log('ОШИБКА: ' + respond.error);
                }
            },
            error: function (jqXHR, status, errorThrown) {
                console.log('ОШИБКА AJAX запроса: ' + status, jqXHR);
            }
        });
}
$(document).ready(function () {
    var IdTypeOfVehicle=0;
    var IdCity=0;
    var IdMarkka=0;
    var IdModel = 0;
    // Действия при клике на тип кузова
    $(document).on("click", "#IdTypeOfVehicle>ul>li", function (event) {
        IdCity = 0;
        IdMarkka = 0;
        IdModel = 0;
        if (IdTypeOfVehicle != event.target.value) {
          
            IdTypeOfVehicle = event.target.value;
            $("#IdTypeOfVehicle>ul>li").removeClass("pick-out");
           
            $(this).addClass("pick-out");
            console.log("IdTypeOfVehicle    ", IdTypeOfVehicle);
            if ($("#IdCity") != undefined)
            {
                $("#IdCity").remove();
                $("#IdMarkka").remove();
                $("#IdModel").remove();
                console.log("remove");
            }
           
            FormAjaxQuery(IdTypeOfVehicle, "/Home/AddCityAndMarkka", "IdTypeOfVehicle", "#Navigation")
        }
       
        else {
            $("#IdCity>ul>li").removeClass("pick-out");
        $("#IdMarkka>ul>li").removeClass("pick-out");
        $("#IdModel>ul>li").removeClass("pick-out");
        }
        console.log("IdTypeOfVehicle    ", IdTypeOfVehicle);
        console.log("IdCity    ", IdCity);
        console.log("IdMarkka    ", IdMarkka);
        console.log("IdModel    ", IdModel);
    });
    // Действия при клике на город
    $(document).on("click", "#IdCity>ul>li", function (event) {
        if (IdCity == event.target.value) {
            $(this).removeClass("pick-out");
            IdCity =0;
        }
        else {
            $("#IdCity>ul>li").removeClass("pick-out");
            $(this).addClass("pick-out");
            IdCity = event.target.value;
        }
        console.log("IdTypeOfVehicle    ", IdTypeOfVehicle);
        console.log("IdCity    ", IdCity);
        console.log("IdMarkka    ", IdMarkka);
        console.log("IdModel    ", IdModel);
      
    });
    // Действия при клике на марку
    $(document).on("click", "#IdMarkka>ul>li", function (event) {
        if (IdMarkka != event.target.value) {
            $("#IdMarkka>ul>li").removeClass("pick-out");
            $(this).addClass("pick-out");
            IdMarkka = event.target.value;
           
            if ($("#IdModel") != undefined) {
                $("#IdModel").remove();
              
                console.log("remove");
            }
            IdModel = 0;
            console.log("ajax");
            FormAjaxQuery(IdMarkka, "/Home/AddCityAndMarkka", "IdMarkka", "#Navigation")
        }
        else {
            $(this).removeClass("pick-out");
            $("#IdModel").remove();
            IdMarkka = 0;
            IdModel = 0;
        }
        console.log("IdTypeOfVehicle    ", IdTypeOfVehicle);
        console.log("IdCity    ", IdCity);
        console.log("IdMarkka    ", IdMarkka);
        console.log("IdModel    ", IdModel);
    });
    // Действия при клике на модель
    $(document).on("click", "#IdModel>ul>li", function (event) {
        if (IdModel == event.target.value) {
            $(this).removeClass("pick-out");
            IdModel =0;
        }
        else {
            $("#IdModel>ul>li").removeClass("pick-out");
            $(this).addClass("pick-out");
            IdModel = event.target.value;
        }
        console.log("IdTypeOfVehicle    ", IdTypeOfVehicle);
        console.log("IdCity    ", IdCity);
        console.log("IdMarkka    ", IdMarkka);
        console.log("IdModel    ", IdModel);
       
    });
    // Действия при клике на кнопку отправки
    $(document).on("click", "#btn", function (event) {
        console.log("btn");
        var formdata = new FormData();
        formdata.append("IdTypeOfVehicle", IdTypeOfVehicle);
        formdata.append("IdMarkka", IdMarkka);
        formdata.append("IdModel", IdModel);
        formdata.append("IdCity", IdCity);
        $.ajax
            ({
                url: "/Home/SelectListOfVehicle",
                type: 'POST',
                data: formdata,
                cache: false,
                processData: false,
                contentType: false,
                beforeSend: function () {
                    $("#spinner2").show();
                },
                complete: function () {
                    $("#spinner2").hide();
                },
                success: function (respond, status, jqXHR) {
                    if (typeof respond.error === 'undefined') {
                        console.log("List   ", $("#List").length);
                        if ($("#List").length == 0)
                        {
                            div = document.createElement("div");
                            div.id = "List";
                            $("#ListOfVehicle").append(div);
                            console.log("List div   ", $("#List").length);
                        }
                        else
                        {
                            $("#List").remove();
                            div = document.createElement("div");
                            div.id = "List";
                            $("#ListOfVehicle").append(div);
                        }
                        $("#List").append(respond);
                      
                    }
                    else {
                        console.log('ОШИБКА: ' + respond.error);
                    }
                },
                error: function (jqXHR, status, errorThrown) {
                    console.log('ОШИБКА AJAX запроса: ' + status, jqXHR);
                }
            });

    });
});