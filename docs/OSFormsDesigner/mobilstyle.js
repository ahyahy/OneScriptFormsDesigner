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

// ================================
function divrightClick(e) {
	if (e.target.nodeName == 'A') {
		localStorage["destination"] = '' + e.target;
	}
}

document.addEventListener('DOMContentLoaded', function (event) {
	localStorage["destination"] = '' + document.location;
});
// ================================
// Функции копирования примеров.
window.onload = function () {
	var a = document.getElementById('copy1');
	a.onclick = function () {
		window.getSelection().removeAllRanges();
		var range1 = document.createRange();
		range1.selectNode(document.getElementById('cont1'));
		try {
			navigator.clipboard.writeText(range1);
		} catch (err) { }
		window.getSelection().removeRange(range1);
		return false;
	}

	var b = document.getElementById('copy2');
	b.onclick = function () {
		window.getSelection().removeAllRanges();
		var range2 = document.createRange();
		range2.selectNode(document.getElementById('cont2'));
		try {
			navigator.clipboard.writeText(range2);
		} catch (err) { }
		window.getSelection().removeRange(range2);
		return false;
	}
}
// ================================

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
