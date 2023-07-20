export async function load() {
	const CmsClient = await import('@/cmsTypeScriptClient/index');

	globalThis.CmsClient = CmsClient;
}
