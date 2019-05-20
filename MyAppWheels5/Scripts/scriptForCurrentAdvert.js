$(document).ready(function () {
    $(document).on("click", ".block>img", function (event) {
        var data = $(this).attr("src");
        $(".big-block>img").attr("src", data);
    });
});