$("td[colspan=3]").find("div").hide();
$("table").click(function (event) {
	var $target = $(event.target);
	if ($target.closest("tr")[0].id == "customer-row") {
		if ($target.closest("td").attr("colspan") > 1) {
			$target.slideUp();
		} else {
			$target.closest("tr").next().find("div").slideToggle();
		}
	}
});