function setCartState() {
	if (localStorage.getItem("cart")) {
		setSession("cartHasItems", "Y");
	} else {
		setSession("cartHasItems", "clear");
	}
}

function setSession(sKey, sValue) {
	$.post('/SetSession/SetVariable',
		{ key: sKey, value: sValue }, function (data) {
			//alert("Success " + data.success);
		});
}

function pwdDisplay() {
	var $pwd = $(".pwd");
	if ($pwd.attr('type') === 'password') {
		$pwd.attr('type', 'text');
	} else {
		$pwd.attr('type', 'password');
	}
}