<script lang="ts">
	import Image from './image.svelte';

	export let node = undefined;
	export let styling: CmsClient.ContainerStyling | undefined;

	console.log('styling,', styling);

	if (!styling) {
		styling = CmsClient.ContainerStylingFromJSON({});
	}
</script>

<section {...$$restProps} bind:this={node} style="min-height:{styling?.height}px">
	{#if styling.background?.backgroundImage?.imageFile}
		<div class="absolute w-full h-full">
			<Image
				class="w-full h-full"
				preventLayoutShift={false}
				backgroundImage={styling.background.backgroundImage}
			/>
		</div>
	{/if}
	<slot />
</section>
