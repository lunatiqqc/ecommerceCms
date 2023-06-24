//declare module globalThis {
//	var CmsClient: typeof import('@/cmsTypescriptClient/cmsClient.ts');
//}

import * as cmsClient from '@/cmsTypescriptClient/index';

declare global {
	declare module CmsClient {
		export = cmsClient;
	}
}

declare namespace svelteHTML {
	// enhance elements
	interface IntrinsicElements {
		'my-custom-element': { someattribute: string; 'on:event': (e: CustomEvent<any>) => void };
	}
	// enhance attributes
	interface HTMLAttributes<T> {
		// If you want to use on:beforeinstallprompt
		'on:outclick'?: (event: CustomEvent<unknown>) => any;
	}
}
