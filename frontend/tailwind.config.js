/** @type {import('tailwindcss').Config} */

const maxValueRegex = "([1-9]|1[0-2])"
export default {
	mode: 'jit',
	content: [
		'./src/**/*.{html,js,svelte,ts}',
		'./components/**/*.{html,js,svelte,ts}',
		'./svelte-lexical/packages/svelte-lexical/src/**/*.{html,js,svelte,ts}'
	],
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
		},
		{
			pattern: new RegExp("pt-" + maxValueRegex)
		},
		{
			pattern: new RegExp("pr-" + maxValueRegex)
		},
		{
			pattern: new RegExp("pb-" + maxValueRegex)
		},
		{
			pattern: new RegExp("pl-" + maxValueRegex)
		},
		{
			pattern: new RegExp("mt-" + maxValueRegex)
		},
		{
			pattern: new RegExp("mr-" + maxValueRegex)
		},
		{
			pattern: new RegExp("mb-" + maxValueRegex)
		},
		{
			pattern: new RegExp("ml-" + maxValueRegex)
		},
	],
	darkMode: ['class', '[class="light"]']
	// These paths are just examples, customize them to match your project structure
};
