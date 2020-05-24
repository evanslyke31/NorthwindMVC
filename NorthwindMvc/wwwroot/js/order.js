$(function () {
    $("td[colspan=4]").find("table").hide();
    $("table").click(function (event) {
        event.stopPropagation();
        var $target = $(event.target);
        if ($target.closest("tr")[0].id == "order-row") {
            if ($target.closest("td").attr("colspan") > 1) {
                $target.slideUp();
            } else {
                $target.closest("tr").next().find("table").slideToggle();
            }
        }
    });
});

console.log("test");