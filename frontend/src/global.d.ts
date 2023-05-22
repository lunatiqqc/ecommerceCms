declare module globalThis {
	var CmsClient: typeof import('@/cmsTypescriptClient/cmsClient').CmsClient;
}

type NonFunctionPropertyNames<T> = {
	[K in keyof T]: T[K] extends (...args: any[]) => any
		? never
		: T[K] extends Array<infer U>
		? U extends T
			? never
			: K
		: K extends 'init' | 'fromJS' | 'toJSON'
		? never
		: K;
}[keyof T];

type ClassProperties<T> = Pick<T, NonFunctionPropertyNames<T>>;
