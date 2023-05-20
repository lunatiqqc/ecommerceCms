export class BaseCmsClient {
	constructor() {}

	async transformResult<T>(
		url: string,
		response: Response,
		transform: (response: Response) => unknown
	): Promise<T> {
		return JSON.parse(JSON.stringify(await transform(response)));
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

export const window = { fetch: fetch };
