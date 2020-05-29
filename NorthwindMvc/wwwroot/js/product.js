var order;
var products;
var total = 0;

$("button.amt-plus").click(function (event) {

	var $target = $(event.target);
	var cardID = $target[0].id.split('-')[0];
	$('#' + cardID + '-amt').val(parseInt($('#' + cardID + '-amt').val()) + 1); 
	order[cardID] = parseInt($('#' + cardID + '-amt').val());
	calcTotal();
	bakeCookie();
});

$("button.amt-minus").click(function (event) {
	var $target = $(event.target);
	var cardID = $target[0].id.split('-')[0];
	if ($('#' + cardID + '-amt').val() > 0) {
		$('#' + cardID + '-amt').val(parseInt($('#' + cardID + '-amt').val()) - 1);
	}
	order[cardID] = parseInt($('#' + cardID + '-amt').val());
	calcTotal();
	bakeCookie();
});

function isNumberKey(evt) {
	var charCode = (evt.which) ? evt.which : evt.keyCode
	if (charCode > 31 && (charCode < 48 || charCode > 57))
		return false;
	return true;
}

$('.amt-text').keydown(function (event) {
	var keypressed = event.keyCode || event.which;
	if (keypressed == 13) {
		var $target = $(event.target);
		var cardID = $target[0].id.split('-')[0];
		order[cardID] = parseInt($('#' + cardID + '-amt').val());
		calcTotal();
		bakeCookie();
	}
});

function calcTotal() {
	total = 0;
	for (const id in order) {
		total += products[id - 1].unitPrice * order[id];
	}
	$('#order-total').text(total);
}

function bakeCookie() {
	var orderStr = "";
	for (const id in order) {
		if (order[id] > 0) {
			orderStr += id + ":" + order[id] + "-";
		}
	}
	console.log(orderStr);
	document.cookie = "order=" + orderStr + ";";
	document.cookie = "tset=test;";
}