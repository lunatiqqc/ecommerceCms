export const csr = true;
export const ssr = true;

import test from '@';
export async function load() {
	const routes = Object.keys(import.meta.glob('@/routes/**/**.svelte'))
		.map((route) => {
			return route.split('/+').shift()?.replace('/src/routes', '') || '/';
		})
		.filter((route, i, arr) => {
			return arr.indexOf(route) === i;
		});
	type Item = CmsClient.IPage;
	//var item: Item = getProperties(CmsClient.Page);
	async function generateItems(dirs: string[]): Promise<Item[]> {
		const result: Item[] = [];

		async function processDir(
			dir: string,
			currentLevel: Item[],
			parent: Item | undefined
		): Promise<void> {
			const dirPath = dir.split('/').filter(Boolean);
			let currentUrl = '';

			for (const subDir of dirPath) {
				currentUrl += '/' + subDir;

				if (subDir.startsWith('[')) {
					const pagesClient = new CmsClient.PagesClient();
					const dynamicPages = await pagesClient.get();

					if (dynamicPages && dynamicPages.length > 0) {
						const nonDynamicUrl = currentUrl.replace(/\/\[.*?\]/g, '');
						updateUrlsRecursively(dynamicPages);

						currentLevel.push(...dynamicPages);
						function updateUrlsRecursively(dynamicPages: Item[]) {
							for (const page of dynamicPages) {
								page.url = nonDynamicUrl + '/' + page.url;

								if (page.children && page.children.length) {
									updateUrlsRecursively(page.children);
								}
							}
						}
					}
				} else {
					let item = currentLevel.find((child) => child.url === currentUrl);

					if (!item) {
						item = {
							title: subDir,
							url: currentUrl,
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

					if (item.url == '/dashboard/content/pages') {
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

	const items = await generateItems(routes);

	return {
		endpoints: items
	};
}
