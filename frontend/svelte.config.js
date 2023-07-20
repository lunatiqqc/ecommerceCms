//import adapter from '@sveltejs/adapter-auto';
import { vitePreprocess } from '@sveltejs/kit/vite';
//import adapterStatic from '@sveltejs/adapter-static';
import adapterNode from '@sveltejs/adapter-node';
//import adapterAuto from '@sveltejs/adapter-auto';

import { resolve } from 'path';

/** @type {import('@sveltejs/kit').Config} */
const config = {
	onwarn: (warning, handler) => {
		const { code, frame } = warning;
		if (code.includes('a11y')) return;

		handler(warning);
	},
	// Consult https://kit.svelte.dev/docs/integrations#preprocessors
	// for more information about preprocessors
	preprocess: vitePreprocess({}),

	kit: {
		// adapter-auto only supports some environments, see https://kit.svelte.dev/docs/adapter-auto for a list.
		// If your environment is not supported or you settled on a specific environment, switch out the adapter.
		// See https://kit.svelte.dev/docs/adapters for more information about adapters.

		adapter: adapterNode({
			// default options are shown
			out: 'build',
			precompress: false,
			envPrefix: '',
			polyfill: true
		})
		//adapter: adapterStatic({
		//	// default options are shown. On some platforms
		//	// these options are set automatically — see below
		//	pages: 'build',
		//	assets: 'build',
		//	fallback: null,
		//	precompress: false,
		//	strict: true
		//})
		//adapter: adapterAuto()
	}
};
export default config;
