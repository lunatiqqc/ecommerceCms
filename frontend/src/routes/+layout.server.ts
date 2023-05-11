export const csr = true;

export async function load() {
	const routesBaseDir = '/src/routes';
	const filePaths = import.meta.glob('/src/routes/**'); // Include subfolder

	const pageDirectories = Object.keys(filePaths)
		.map((filePath) => {
			const routeDir = filePath.replace(routesBaseDir, '');
			const fileNameIndex = routeDir.lastIndexOf('+');

			return routeDir.slice(0, fileNameIndex);
		})
		.filter((directory, i, arr) => {
			return arr.indexOf(directory) === i;
		});

	return {
		endpoints: pageDirectories
	};
}
