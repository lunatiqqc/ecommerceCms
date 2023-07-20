export const csr = true;
export const ssr = true;

export async function load() {
	const CmsClient = await import('@/cmsTypeScriptClient/index');

	globalThis.CmsClient = CmsClient;

	const routes = Object.keys(import.meta.glob('@/routes/**/**.svelte'))
		.map((route) => {
			return route.split('/+').shift()?.replace('/src/routes', '') || '/';
		})
		.filter((route, i, arr) => {
			return arr.indexOf(route) === i;
		});
	type Item = globalThis.CmsClient.Page;
	//var item: Item = getProperties(CmsClient.Page);
	async function generateMenuItems(dirs: string[]): Promise<Item[]> {
		const result: Item[] = [];

		async function processDir(
			dir: string,
			currentLevel: Item[],
			parent: Item | undefined
		): Promise<void> {
			const dirPath = dir.split('/').filter(Boolean);

			for (const subDir of dirPath) {
				if (subDir.startsWith('[')) {
					const pagesClient = new CmsClient.PagesClient();
					const dynamicPages = await pagesClient.getAll();

					currentLevel.push(...dynamicPages);
				} else {
					let item = currentLevel.find((child) => child.title === subDir);

					if (!item) {
						item = {
							title: subDir,
							url: subDir,
							visibleInMenu: false,
							isSystemPage: true
						};
						currentLevel.push(item);
					}
					if (item.children) {
						currentLevel = item.children;
					} else {
						item.children = [];
						currentLevel = item.children;
					}

					if (item.url == 'pages') {
						item.isDynamicPagesRoot = true;
					}
				}
			}
		}

		for (const dir of dirs) {
			await processDir(dir, result);
		}

		return result;
	}

	const items = await generateMenuItems(routes);

	return {
		endpoints: items
	};
}
