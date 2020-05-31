var order;
var products;
var total = 0;

$(document).ready(function () {
	var val = getCookie("order");
	if (val == "") {
		return
	}
	var splt = val.substring(0,val.length-1).split('-');
	for (var i = 0; i < splt.length; i++) {
		o = splt[i].split(':');
		$('#' + o[0] + '-amt').val(o[1]);
		order[o[0]] = o[1];
	}
	calcTotal();
});

$("button.amt-plus").click(function (event) {

	var $target = $(event.target);
	var cardID = $target[0].id.split('-')[0];
	if ($('#' + cardID + '-amt').val() < products[parseInt(cardID) - 1].unitsInStock) {
		$('#' + cardID + '-amt').val(parseInt($('#' + cardID + '-amt').val()) + 1);
		order[cardID] = parseInt($('#' + cardID + '-amt').val());
		calcTotal();
		bakeCookie();
	}
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
		var val = $('#' + cardID + '-amt').val();
		if (parseInt(val) > products[parseInt(cardID) - 1].unitsInStock) {
			$('#' + cardID + '-amt').val(products[parseInt(cardID) - 1].unitsInStock);
		} else if (val == "" || val == NaN) {
			$('#' + cardID + '-amt').val(0);
		} else if (parseInt(val) < 0) {
			$('#' + cardID + '-amt').val(0);
		}
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
	$('#order-total').text(total.toFixed(2));
}

function bakeCookie() {
	//var exdate = new Date();
	//exdate = new Date(exdate.getTime() + 30 * 60000);
	var orderStr = "";
	for (const id in order) {
		if (order[id] > 0) {
			orderStr += id + ":" + order[id] + "-";
		}
	}
	document.cookie = "order=" + orderStr + ";";//expires=" + exdate.toUTCString();
}

function getCookie(cname) {
	var name = cname + "=";
	var decodedCookie = decodeURIComponent(document.cookie);
	var ca = decodedCookie.split(';');
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i];
		while (c.charAt(0) == ' ') {
			c = c.substring(1);
		}
		if (c.indexOf(name) == 0) {
			return c.substring(name.length, c.length);
		}
	}
	return "";
}

function clearOrders() {
	for (const id in order) {
		order[id] = 0;
		$('.amt-text').val(0);
	}
	bakeCookie();
	calcTotal();
}