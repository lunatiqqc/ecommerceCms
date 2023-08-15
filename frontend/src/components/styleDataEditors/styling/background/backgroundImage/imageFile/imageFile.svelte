<script lang="ts">
	import Icon from '@/components/icon.svelte';
	import SelectableImageGallery from '@/components/selectableImageGallery.svelte';
	import { clickOutside } from '@/lib/actions/clickOutside';
	import Image from '@/components/image.svelte';

	export let styleContent: CmsClient.ImageFile;

	type $$props = CmsClient.Background;

	let selectImage: boolean = false;

	let selectedImage: CmsClient.ImageFile;

	let chosenBackgroundImage: CmsClient.ImageFile;

	function handleImageSelected({ detail }: { detail: CmsClient.ImageFile }) {
		selectedImage = detail;
	}

	function handleSubmitBackgroundImage() {


		styleContent = selectedImage;

		selectImage = false;
	}


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

	<div class="relative mt-4">
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
		<button
			on:click={() => {
				styleContent = null;
			}}
		>
			<icon class="absolute bottom-full right-0">
				<Icon class="border-2 w-8 h-8 border-black" width="100" icon="carbon:reset" />
			</icon>
		</button>
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

				selectImage = false;
			}}
			class="relative w-5/6 h-5/6 p-4 bg-slate-700 rounded"
		>
			<div class="overflow-auto h-full">
				<SelectableImageGallery on:imageselect={handleImageSelected} />
			</div>
			{#if selectedImage}
				<button on:click={handleSubmitBackgroundImage} class="p-4 border-2 absolute right-0"
					>Select</button
				>
			{/if}
		</div>
	</div>
{/if}
