<script lang="ts">
	import type { HTMLButtonAttributes } from 'svelte/elements';
	import RecursiveMenuPublic from '../recursiveMenu_public.svelte';
	import TextEditor from '../textEditor.svelte';

	export let textStyling: CmsClient.TextContainerStyling;

	const pagesClient = new CmsClient.PagesClient();
	const dynamicPages = pagesClient.getAll().then((pages) => {
		return pages[0]?.children;
	});

	let props: CmsClient.MenuComponent = $$props;

	let classes;

	$: if (textStyling) {
		let arrayOfClassNames = [];

		if (textStyling.textFormats) {
			arrayOfClassNames = Object.values(textStyling.textFormats);
		}

		if (textStyling.elementFormats) {
			arrayOfClassNames = Object.values(textStyling.elementFormats);
		}
		if (textStyling.fontSizeClassOptions) {
			arrayOfClassNames = Object.values(textStyling.fontSizeClassOptions);
		}

		classes = arrayOfClassNames.join(' ');
	}
</script>

<div class={classes}>
	{#await dynamicPages then pages}
		<RecursiveMenuPublic {textStyling} {pages} />
	{/await}
</div>
