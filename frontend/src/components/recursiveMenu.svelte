<script lang="ts">
	import { text } from '@sveltejs/kit';
	import TextEditor from './textEditor.svelte';

	export let pages: CmsClient.Page[];
	export let nestingLevel = 0;
	export let isEditor: boolean;
	export let textStyling: CmsClient.TextContainerStyling;
</script>

{#if pages?.length}
	<ul class="pl-4 flex gap-4 justify-between">
		{#each pages as page, i}
			<li class="relative">
				{textStyling?.headingValue}
				<svelte:element this={textStyling?.headingValue || 'div'}>
					{#if isEditor}
						<TextEditor textContent={{ html: `<div>${page.title}</div>` }}>{page.title}</TextEditor>
					{:else}
						<a href={page.url}>{page.title}</a>
					{/if}
				</svelte:element>

				{#if page.children}
					<svelte:self pages={page.children} nestingLevel={nestingLevel + 1} />
				{/if}
			</li>
		{/each}
	</ul>
{/if}
