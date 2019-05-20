// Функцмя для нахождения первого дочернего элемента в списке и добавления атрибута hidden
function AddAttributeHiddenToFirstChildList(NameList) {
    var selector = "#" + NameList + ">option:first-child";
    var result = document.querySelector(selector);
    result.setAttribute("hidden", "");
};
AddAttributeHiddenToFirstChildList("NameVehicle");
// Добавления на страницу дополнительных опций для выбора пользователем
function FormAjaxQuery(id, Url, key, selectorRespond, AddAttribute, IdDiv) {
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
                $("#spiner").show();
            },
            complete: function () {
                $("#spiner").hide();
            },
            success: function (respond, status, jqXHR) {
                if (typeof respond.error === 'undefined') {
                    if (IdDiv != null) {
                        div = document.createElement("div");
                        div.id = IdDiv;
                        $(selectorRespond).append(div);
                        $("#" + IdDiv).html(respond);
                    }
                    else {
                        $(selectorRespond).html(respond);
                    }
                    if (AddAttribute != null) {
                        AddAttributeHiddenToFirstChildList(AddAttribute);
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
    var IdTypeOfVehicle;                  // Id текущего объявления
    var newArrayOfFiles = [];             // Новый массив файлов
    // Событие при изменении типа кузова
    $("#NameVehicle").on("change", function (event) {
        var result;
        IdTypeOfVehicle = event.target.value;
        result = document.querySelector("#NameModel>option:first-child");
        // Выбрать значение по умолчанию
        if (result != undefined) {
            result.setAttribute("selected", "");
        }
        FormAjaxQuery(IdTypeOfVehicle, "/Advert/CreateAdvert", "IdTypeOfVehicle", "#Markka", "NameMarkka", null);
    });
    // Событие при изменении марки авто
    $(document).on("change", "#NameMarkka", function (event) {
        var IdMarkka = event.target.value;
        FormAjaxQuery(IdMarkka, "/Advert/CreateAdvert", "IdMarkka", "#Model", "NameModel", null);
    });
    // Событие при изменении модели авто
    $(document).on("change", "#NameModel", function (event) {
        // Проверки на наличие существования опций, чтобы при изменении Модели авто не обращаться лишний раз на сервер
        var IsExistExtraAttributes;
        var IsExistOptionTruck;
        var IsExistCheckBox;
        IsExistExtraAttributes = $("#ExtraAttributes>:first-child").length;
        IsExistOptionTruck = $("#OptionTruck");
        IsExistCheckBox = $("#CheckBox");

        if (IdTypeOfVehicle == 1 && IsExistCheckBox.length == 0) {
            if (IsExistOptionTruck.length != 0) {
                IsExistOptionTruck.remove();
            }
            FormAjaxQuery(IdTypeOfVehicle, "/Advert/AddCheckBox", "IdTypeOfVehicle", "#ChangebleAttributes", null, "CheckBox");
        }
        else if (IdTypeOfVehicle == 2 && IsExistOptionTruck.length == 0) {
            if (IsExistCheckBox.length != 0) {
                IsExistCheckBox.remove();
            }
            FormAjaxQuery(IdTypeOfVehicle, "/Advert/AddCheckBox", "IdTypeOfVehicle", "#ChangebleAttributes", null, "OptionTruck");
        }
        else if (IdTypeOfVehicle == 3 && IsExistExtraAttributes != 0) {
            if (IsExistOptionTruck.length != 0) {
                IsExistOptionTruck.remove();
            }
            else if (IsExistCheckBox.length != 0) {
                IsExistCheckBox.remove();
            }
        }
        if (IsExistExtraAttributes == 0) {
            FormAjaxQuery(IdTypeOfVehicle, "/Advert/AddOptions", "IdTypeOfVehicle", "#ExtraAttributes", null, "Options");
        }
    });
    // Добавление выбранных фото на страницу
    $(document).on("change", "#Image", function (event) {
        const amountAvailablePhotos = 6; // Колличество допустимых для выбора фотографий
        var currentAmountPhotosOnPage;  // Текущее колличество фото на странице
        var amountChoosedFiles;         // Выбранное колличество файлов пользователем в input [type="file"]
        var container;                 // Контейнер для списка фотографий
        var reader;                     // Считывание файла
        var dataUri;                    // Массив байтов фото
        var imgBlock;                   // Контейнер для одной фотографии
        var comment;                    // Добавление комментариев
        var files;                      // Массив файлов

        files = event.target.files;
        container = $("#Photo");
        currentAmountPhotosOnPage = $("#Photo>.imgBlock>img").length;
        amountChoosedFiles = files.length;
        // Вставка фото на страницу
        if (currentAmountPhotosOnPage + amountChoosedFiles <= amountAvailablePhotos) {
            for (var i = 0; i < amountChoosedFiles; i++) {
                reader = new FileReader();
                reader.readAsDataURL(files[i]);
                reader.onload = function (event) {
                    dataUri = event.target.result;
                    div = document.createElement("div");
                    div.className = "imgBlock";
                    container.append(div);
                    var LastElement = $("#Photo>.imgBlock").last();
                    img = document.createElement("img");
                    img.src = dataUri;
                    LastElement.append(img);
                    div2 = document.createElement("button");
                    div2.className = "closeBlock";
                    div2.innerHTML = "x";
                    LastElement.append(div2);
                };
                reader.onerror = function (event) {
                    console.error("Файл не может быть прочитан! код " + event.target.error.code);
                };
                // Запись элементов в новый массив для дальнейшей передачи в контроллер
                newArrayOfFiles.push(files[i]);
            }
            if (files.length != 0) {
                // Добавление комментариев об успешном добавлении фото на страницу
                comment = document.createElement("div");
                comment.className = "loadSuccess";
                comment.innerHTML = "Фотографии успешно загружены";
                $("#comments").append(comment);
            }
        }
        else {
            // Добавление комментариев если превышен лимит допустимого колличества фото
            comment = document.createElement("div");
            comment.className = "loadCanceled";
            comment.innerHTML = "Больше 6 фотографий нельзя";
            $("#comments").append(comment);
        }

    });
    // Отображение блока для удаления фото
    $(document).on("mouseover", ".imgBlock", function (event) {
        $(this).find(">.closeBlock").css("display", "block");
    });
    // Скрывание блока для удаления фото
    $(document).on("mouseout", ".imgBlock", function (event) {
        $(this).find(">.closeBlock").css("display", "none");
    });
    // Удаление фото
    $(document).on("click", ".closeBlock", function (event) {
        event.preventDefault();
        var closestParent = $(this).closest(".imgBlock");
        var index = closestParent.index(".imgBlock");
        closestParent.remove();
        // Удаление файла из массива
        newArrayOfFiles.splice(index, 1);
        // Добавление комментариев
        comment = document.createElement("div");
        comment.className = "removePhoto";
        comment.innerHTML = "Фото удалено";
        $("#comments").append(comment);
    });
    // Отправка выбранных фото на сервер, если они имеются
    $(document).on("click", "input[type=submit]", function (event) {
        // event.preventDefault();
        // Проверка валидации формы
        if (!($('form')[0].checkValidity())) {
            return;
        }
        else {
            var formdata = new FormData();
            $.each(newArrayOfFiles, function (key, value) {
                formdata.append("Images", value);
            });
            $.ajax
                ({
                    url: "/Advert/ProcessImages",
                    type: 'POST',
                    data: formdata,
                    cache: false,
                    processData: false,
                    contentType: false,
                    contentType: false,
                    beforeSend: function () {
                        console.log("load start");
                    },
                    complete: function () {
                        console.log("load finished");
                    }
                });
        }
    });
});