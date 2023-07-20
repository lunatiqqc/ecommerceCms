import type { PageServerLoad } from './$types';

export const trailingSlash = 'ignore';

export const load: PageServerLoad = async ({ params }) => {
	const pagesClient = new CmsClient.PagesClient();

	console.log('params3', params);

	const page = await pagesClient.get({ url: params.url || '/' });

	return {
		page: page
	};
};
