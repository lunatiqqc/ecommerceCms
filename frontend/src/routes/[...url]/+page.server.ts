import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async ({ params }) => {
	const pagesClient = new CmsClient.PagesClient();


	const page = await pagesClient.get({ url: params.url || '/' });

	const GlobalContentClient = new CmsClient.GlobalContentClient();

	const headerResponse = await GlobalContentClient.getHeaderRaw();

	let header;

	if (headerResponse.raw.status === 204) {
		header = undefined;
	} else {
		header = await headerResponse.value();
	}
	const footerResponse = await GlobalContentClient.getFooterRaw();

	let footer;
	if (footerResponse.raw.status === 204) {
		footer = undefined;
	} else {
		footer = await footerResponse.value();
	}

	return {
		page: page,
		header: header,
		footer: footer
	};
};
