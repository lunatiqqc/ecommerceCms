<script lang="ts">
	import { onMount } from 'svelte';
	import ObjectFit from './styleDataEditors/background/backgroundImage/objectFit/objectFit.svelte';

	export let height: number | null = null;
	export let width: number | null = null;
	export let preventLayoutShift = true;

	export let imageStyle: CmsClient.ImageStyle & { imageFile: CmsClient.ImageFile };

	console.log('incoming image', imageStyle);

	let responsiveSize;

	let imageContainer: HTMLElement;

	let observer;

	function handleResize(entries) {
		const { width: _width, height: _height } = entries[0].contentRect;
		// Handle the resize event for the observed element

		height = _height;
		width = _width;
	}

	$: if (height !== null || height !== undefined) {
		const responsiveSizesFiltered = imageStyle.imageFile.sizes?.filter((size) => {
			console.log('size', size);

			const sizeInt = size;

			if (width) {
				return sizeInt < width;
			}
			return sizeInt < height * imageStyle.imageFile.aspectRatio;
		});

		responsiveSize = imageStyle.imageFile.sizes[responsiveSizesFiltered.length];
	}

	let mounted = false;
	onMount(() => {
		observer = new ResizeObserver(handleResize);
		observer.observe(imageContainer);

		if (preventLayoutShift && !height && !width) {
			imageContainer.style.paddingBottom = (1 / imageStyle.imageFile.aspectRatio) * 100 + '%';
		}
		if (height) {
			imageContainer.style.width = Math.round(height * imageStyle.imageFile.aspectRatio) + 'px';
			imageContainer.style.height = height + 'px';
		}
		height = imageContainer.getBoundingClientRect().height;

		mounted = true;

		return () => {
			observer.unobserve(imageContainer);
			observer.disconnect();
		};
	});
	// src={CmsClient.BASE_PATH + '/' + imageStyle.imageFile.fileName}
</script>

<figure {...$$restProps} bind:this={imageContainer} class={`${$$restProps.class || ''} relative`}>
	{#if mounted}
		<img
			class="absolute w-full h-full {imageStyle.objectFit || 'object-contain'}"
			alt="alty"
			src={CmsClient.BASE_PATH +
				'/' +
				imageStyle.imageFile.fileName?.replace('.', '_w' + responsiveSize + '.')}
		/>
	{/if}
</figure>
