/// <reference path="../cmsTypescriptClient/cmsClient.ts" />

//declare type Test = import('@/cmsTypescriptClient/cmsClient.ts').CmsClient;

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
