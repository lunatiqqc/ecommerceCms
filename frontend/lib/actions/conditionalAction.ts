export function conditionalAction(node, callback) {
	if (callback) {
		callback(node);
	}
}
