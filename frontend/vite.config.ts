import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	resolve: {
		alias: {
			'@/*': './'
		}
	},
	plugins: [sveltekit()],
	server: {
		fs: {
			allow: ['.']
		}
	},
	optimizeDeps: {
		include: ['lucide-svelte']
	}
});
