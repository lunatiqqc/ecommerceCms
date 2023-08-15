<script lang="ts">
	import type { Writable } from 'svelte/store';
	import Image from './image.svelte';

	export let node = undefined;

	export let gridColumnStore: Writable<CmsClient.GridColumn>;

	export let styling: CmsClient.ContainerStyling;

	export let visualEditor = false;

	if (!styling) {
		styling = CmsClient.ContainerStylingFromJSON({});
	}

	if (!styling.margin) {
		styling.margin = CmsClient.MarginFromJSON({});
	}
</script>

<article
	{...$$restProps}
	class="{$$restProps.class} pt-{styling?.padding?.top}"
	bind:this={node}
	style="height:{styling.height}px; margin-top:{visualEditor ? 0 : styling.margin.top}px"
>
	{#if styling.background?.backgroundImage?.imageFile?.fileName}
		<div class="absolute w-full h-full">
			<Image
				class="w-full h-full"
				preventLayoutShift={false}
				imageStyle={{
					imageFile: styling.background.backgroundImage.imageFile,
					objectFit: styling.background.backgroundImage.objectFit
				}}
			/>
		</div>
	{/if}
	<slot />
</article>
