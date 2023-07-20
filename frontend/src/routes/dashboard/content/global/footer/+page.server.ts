export const load: PageLoad = async () => {
	const GlobalContentClient = new CmsClient.GlobalContentClient();

	const footerResponse = await GlobalContentClient.getFooterRaw();

	let footer;

	if (footerResponse.raw.status === 204) {
		footer = CmsClient.GlobalFooterFromJSON({});
	} else {
		footer = await footerResponse.value();
	}

	return {
		footer: footer
	};
};
