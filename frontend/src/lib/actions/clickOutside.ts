export function clickOutside(node: HTMLElement) {
	const handleClick = (event: MouseEvent) => {
		const { clientX, clientY } = event;

		const nodeBoundingBox: DOMRect = node.getBoundingClientRect();
		//console.log(clientX, clientY, nodeBoundingBox);

		//if (
		//	clientX > nodeBoundingBox.right ||
		//	clientY > nodeBoundingBox.bottom ||
		//	clientX < nodeBoundingBox.left ||
		//	clientY < nodeBoundingBox.top
		//) {
		//	const newEvent = new CustomEvent('outclick');
		//	node.dispatchEvent(newEvent);
		//
		//	//event.stopPropagation();
		//	return;
		//}

		if (!node.contains(event.target) && node !== event.target) {
			node.dispatchEvent(new CustomEvent('outclick'));
			//event.stopPropagation();
		}
	};

	document.addEventListener('click', handleClick, true);
	document.addEventListener('contextmenu', handleClick, true);

	return {
		destroy() {
			document.removeEventListener('click', handleClick, true);
		}
	};
}
