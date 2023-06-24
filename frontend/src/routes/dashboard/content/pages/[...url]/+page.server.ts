import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async ({ params }) => {
	const pagesClient = new CmsClient.PagesClient();

	const page = await pagesClient.get({ url: params.url });

	return {
		page: page
	};
};
