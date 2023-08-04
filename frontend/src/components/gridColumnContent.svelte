<script lang="ts">
	import Image from './image.svelte';

	export let node = undefined;
	export let styling: CmsClient.ContainerStyling | undefined;

	if (!styling) {
		styling = CmsClient.ContainerStylingFromJSON({});
	}

	if (!styling.margin) {
		styling.margin = CmsClient.MarginFromJSON({});
	}
</script>

<article
	{...$$restProps}
	class={$$restProps.class + ' '}
	bind:this={node}
	style="height:{styling.height}px; margin-top:{styling.margin.top}px"
>
	{#if styling.background?.backgroudImage?.imageFile}
		<div class="absolute w-full h-full">
			<Image
				objectFit={styling.background.backgroudImage.objectFit}
				class="w-full h-full"
				preventLayoutShift={false}
				image={styling.background.backgroudImage.imageFile}
			/>
		</div>
	{/if}
	<slot />
</article>
