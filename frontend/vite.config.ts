import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';
import Icons from 'unplugin-icons/vite';

export default defineConfig({
	resolve: {
		alias: {
			'@/*': './'
		}
	},
	plugins: [
		sveltekit(),
		Icons({
			compiler: 'svelte',
			autoInstall: true
		})
	],
	server: {
		fs: {
			allow: ['.']
		}
	},
	optimizeDeps: {
		include: ['lucide-svelte']
	}
});
