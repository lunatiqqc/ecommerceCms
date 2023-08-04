<script lang="ts">
	import type { Writable } from 'svelte/store';
	import Icon from '@/components/icon.svelte';
	import SelectableImageGallery from '@/components/selectableImageGallery.svelte';
	import { clickOutside } from '@/lib/actions/clickOutside';
	import Image from '@/components/image.svelte';

	export let styleContent: CmsClient.ImageFile;
	export let gridContentStore: Writable<CmsClient.GridContent>;
	export let styleContentKey: 'imageFile';
	export let parentStyleContent: CmsClient.ImageStyle;

	type $$props = CmsClient.Background;

	let selectImage: boolean = false;

	let selectedImage: CmsClient.ImageFile;

	let chosenBackgroundImage: CmsClient.ImageFile;

	function handleImageSelected({ detail }: { detail: CmsClient.ImageFile }) {
		selectedImage = detail;
	}

	function handleSubmitBackgroundImage() {
		console.log('handleSubmitBackgroundImage');

		console.log('imageFileStyleContent', styleContent);

		console.log('parentStyleContent', parentStyleContent);

		styleContent = selectedImage;

		//gridContentStore.update((self) => self);

		selectImage = false;
	}

	function handleSetObjectFit() {
		styleContent.objectFit = '';
	}

	console.log('styleContentKey', styleContentKey);
	console.log('styleContent', styleContent);

	//styleContent = {
	//	discriminator: 'ImageFile',
	//	id: 144,
	//	fileName: 'b (21).jpg',
	//	uploadDate: '2023-07-15T21:06:07.403Z',
	//	fileFolderId: 5,
	//	aspectRatio: 1.3091364,
	//	sizes: [120, 240, 480, 768, 1024, 1920, 3840]
	//};
</script>

<div>
	<div class="flex items-center">
		<span>Image</span>
		<icon class="relative">
			<Icon icon="carbon:image" />
		</icon>
	</div>

	<div class="relative">
		{#if styleContent}
			<Image
				class="hover:opacity-70"
				imageStyle={CmsClient.ImageStyleFromJSON({ imageFile: styleContent })}
			/>
		{/if}
		<icon
			class="{styleContent &&
				' w-full h-full inset-0 absolute opacity-0 hover:opacity-100'} cursor-pointer"
			on:click={() => {
				selectImage = true;
			}}
		>
			<Icon class="border-2 w-full h-full border-black" width="100" icon="carbon:add" />
		</icon>
	</div>
</div>

{#if false && selectedImage}
	<slot />
{/if}

{#if selectImage}
	<div
		class="fixed inset-0 w-screen h-screen bg-slate-700 bg-opacity-70 flex items-center justify-center text-white"
	>
		<div
			use:clickOutside
			on:outclick={() => {
				console.log('outClick');

				selectImage = false;
			}}
			class="relative w-5/6 h-5/6 p-4 bg-slate-700 rounded"
		>
			<div class="overflow-auto h-full">
				<SelectableImageGallery on:imageselect={handleImageSelected} />
			</div>
			{#if selectedImage}
				<button on:click={handleSubmitBackgroundImage} class="p-4 border-2 absolute right-0"
					>Select2</button
				>
			{/if}
		</div>
	</div>
{/if}
