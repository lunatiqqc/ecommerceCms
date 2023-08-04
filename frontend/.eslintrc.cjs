module.exports = {
	root: true,
	extends: [
		'eslint:recommended',
		'plugin:@typescript-eslint/recommended',
		'plugin:svelte/recommended',
		'prettier'
	],
	parser: '@typescript-eslint/parser',
	plugins: ['@typescript-eslint'],
	parserOptions: {
		sourceType: 'module',
		ecmaVersion: 2020,
		extraFileExtensions: ['.svelte']
	},
	env: {
		browser: true,
		es2017: true,
		node: true
	},
	overrides: [
		{
			files: ['*.svelte'],
			parser: 'svelte-eslint-parser',
			parserOptions: {
				parser: '@typescript-eslint/parser'
			}
		}
	],
	settings: {
		'svelte3/ignore-warnings': 'svelte(a11y-click-events-have-key-events)',
		'svelte3/ignore-warnings': 'svelte(a11y-mouse-events-have-key-events)'
	},
	rules: {
		'a11y-click-events-have-key-events': 'off',
		'a11y-mouse-events-have-key-events': 'off'
	}
};
