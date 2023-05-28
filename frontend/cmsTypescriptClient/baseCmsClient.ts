let fakeWindowInitialized = false;
function initFakeWindow() {
	if (typeof window === 'undefined') {
		globalThis.window = { fetch: fetch };
		fakeWindowInitialized = true;
	}
}
function cleanupFakeWindow() {
	if (fakeWindowInitialized) {
		delete globalThis.window;
		fakeWindowInitialized = false;
	}
}

export class BaseCmsClient {
	constructor() {
		initFakeWindow();
	}
	async transformResult(
		url: string,
		response: Response,
		transform: (response: Response) => unknown
	): Promise<any> {
		cleanupFakeWindow();
		if (response.status !== 204) {
			return JSON.parse(JSON.stringify(await transform(response)));
		}
		return Promise.resolve();
	}
	transformOptions(options: RequestInit): Promise<any> {
		return new Promise((resolve) => {
			resolve(options);
		});
	}
}

export class SwaggerResponse<TResult> {
	status: number;
	headers: { [key: string]: any };
	result: TResult;

	constructor(status: number, headers: { [key: string]: any }, result: TResult) {
		this.status = status;
		this.headers = headers;
		this.result = result;
	}
}
