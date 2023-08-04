<script lang="ts">
	import Image from './image.svelte';

	export let node = undefined;
	export let styling: CmsClient.ContainerStyling | undefined;

	console.log('styling,', styling);

	if (!styling) {
		styling = CmsClient.ContainerStylingFromJSON({});
	}
	console.log($$restProps);
</script>

<section
	class={$$restProps.class + ' relative'}
	bind:this={node}
	style="min-height:{styling?.height}px"
>
	{#if styling.background?.backgroundImage?.imageFile}
		<div class="absolute w-full h-full -z-10">
			<Image
				class="w-full h-full"
				preventLayoutShift={false}
				backgroundImage={styling.background.backgroundImage}
				imageStyle={styling?.background.backgroundImage}
			/>
		</div>
	{/if}
	<slot />
</section>
