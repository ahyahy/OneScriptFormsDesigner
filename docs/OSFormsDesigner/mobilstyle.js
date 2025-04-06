if (localStorage['keybt0hutbo'] == 'true') {
	document.body.setAttribute('style',
		'font-size: 35px !important;' +
		''
	);
	
	let elems = document.querySelectorAll("div#nsbanner");
	for (elem of elems) {
		elem.setAttribute('style',
			'height: 60px;' +
			''
		);
	}
}
else {
	document.body.setAttribute('style',
		'font-size: 16px !important' +
		''
	);
}

// === Начало для масштабирования ==========
function clickimg(e) {
	var img01 = e.target;
	let tail = img01.id.replace("myImg", "");
	var modal01 = document.getElementById('myModal' + tail);
	var modalImg01 = document.getElementById('img' + tail);

	modal01.style.display = "block";
	modalImg01.src = img01.src;

	var span01 = document.getElementById('close' + tail);
	span01.onclick = function () {
		modal01.style.display = "none";
	}
}
// === Конец для масштабирования ==========
