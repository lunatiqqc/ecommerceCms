import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';
import dynamicImport from 'vite-plugin-dynamic-import';

import path from 'path';

export default defineConfig({
	plugins: [sveltekit(), dynamicImport(/* options */)],
	server: {
		fs: {
			allow: ['.']
		}
	},
	resolve: {
		alias: {
			'@': path.resolve(__dirname)
		},
		extensions: ['.mjs', '.js', '.mts', '.ts', '.jsx', '.tsx', '.json', '.svelte']
	}
});
