export async function register() {
    global.CmsClient = (
        await import("@/cmsTypescriptClient/cmsClient")
    ).CmsClient;
}
