export function focus(node: HTMLElement) {
	node.focus();

	function moveCursorToEnd() {
		const selection = window.getSelection();

		// Check if the Selection object is not null before proceeding
		if (selection !== null) {
			// Set the caret position to the end of the text
			const range = document.createRange();
			range.selectNodeContents(node);
			range.collapse(false);
			selection.removeAllRanges();
			selection.addRange(range);
		}
	}

	// Call the moveCursorToEnd() function wherever needed
	moveCursorToEnd();

	// Call the moveCursorToEnd() function wherever needed
}
