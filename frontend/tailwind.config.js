/** @type {import('tailwindcss').Config} */
export default {
	mode: 'jit',
	content: ['./src/**/*.{html,js,svelte,ts}', './components/**/*.{html,js,svelte,ts}'],
	theme: {
		extend: {}
	},
	plugins: [],
	safelist: [
		{
			pattern: /col-span-([1-9]|1[0-2])/
		},
		{
			pattern: /col-start-([1-9]|1[0-2])/
		}
	],
	darkMode: ['class', '[class="light"]']
	// These paths are just examples, customize them to match your project structure
};
