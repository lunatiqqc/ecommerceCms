export const load: PageLoad = async () => {
	const GlobalContentClient = new CmsClient.GlobalContentClient();

	const headerResponse = await GlobalContentClient.getHeaderRaw();

	let header;

	if (headerResponse.raw.status === 204) {
		header = CmsClient.GlobalHeaderFromJSON({});
	} else {
		header = await headerResponse.value();
	}

	return {
		header: header
	};
};
