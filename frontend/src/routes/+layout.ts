export const csr = true;
import { browser } from '$app/environment';
import { CmsClient } from '@/cmsTypescriptClient/cmsClient';

export async function load() {
	if (browser) {
		window.CmsClient = CmsClient;
		console.log('b');
	}

	const start = performance.now();

	const routes = Object.keys(import.meta.glob('@/src/routes/**/**.svelte'))
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

		async function processDir(dir: string, currentLevel: Item[]): Promise<void> {
			const dirPath = dir.split('/').filter(Boolean);
			let currentUrl = '';

			for (const subDir of dirPath) {
				currentUrl += '/' + subDir;

				if (subDir.startsWith('[')) {
					const pagesClient = new CmsClient.PagesClient();
					const dynamicPages = await pagesClient.get();

					if (dynamicPages && dynamicPages.length > 0) {
						updateUrlsRecursively(dynamicPages, currentUrl.replace(/\/\[.*?\]/g, ''));

						currentLevel.push(...dynamicPages);
					}

					function updateUrlsRecursively(dynamicPages: Item[], parentUrl: string) {
						for (const page of dynamicPages) {
							page.url = parentUrl + '/' + page.url;

							if (page.children && page.children.length) {
								updateUrlsRecursively(page.children, page.url);
							}
						}
					}
				} else {
					let item = currentLevel.find((child) => child.url === currentUrl);

					if (!item) {
						item = {
							id: 21,
							title: subDir,
							url: currentUrl,
							children: [],
							visibleInMenu: false,
							isSystemPage: true
						};
						currentLevel.push(item);
					}

					currentLevel = item.children || [];
				}
			}
		}

		for (const dir of dirs) {
			await processDir(dir, result);
		}

		return result;
	}

	const items = await generateItems(routes);

	console.log(performance.now() - start);

	return {
		endpoints: items
	};
}
