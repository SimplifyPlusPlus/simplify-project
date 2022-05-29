window.getElementCoordinatesById = (id) => {
	const el = document.getElementById(id);
	const position = el.getBoundingClientRect();
	let k = {
		x: position.x,
		y: position.y,
	};
	console.log(position.x, ' ', position.y);
	return k; 
}

window.registerDetailsCardEvents = (card_id) => {
	let card = document.getElementById(card_id);
	let otherCards = document.querySelectorAll('.details-card');
	// window.addEventListener('click', function (event) {
	// 	if (card.style.display === 'flex') {
	// 		otherCards.forEach((el) => {
	// 			if (!el.contains(event.target)) {
	// 				card.style.display = "none";
	// 			}
	// 		});
	// 	}
	// 	else {
	// 		console.log('not flex');
	// 	}
	// });
}
