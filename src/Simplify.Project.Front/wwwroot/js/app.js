window.getElementCoordinatesById = (id) => {
	const el = document.getElementById(id);
	const position = el.getBoundingClientRect();
	let coords = {
		x: (position.x - position.width * 1.5 + 32),
		y: position.y,
	};
	if (coords.x < 70) {
		coords.x = (position.x - position.width * 1.25 + 32);
	}
	if (coords.x > 128) {
		coords.x *= 0.65;
	}
	return coords; 
}

window.registerDetailsCardEvents = (card_id) => {
	let card = document.getElementById(card_id);
	let otherCards = document.querySelectorAll('.details-card');
	window.addEventListener('click', function (event) {
		if (card.style.display === 'flex') {
			let haveContains = Array.from(otherCards).filter(x => x.contains(event.target)).length > 0;
			if (!haveContains) {
				card.style.display = 'none';
			}
		}
	});
}
