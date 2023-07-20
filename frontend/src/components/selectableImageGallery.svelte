<script lang="ts">
	import { createEventDispatcher, onMount } from 'svelte';
	import Upload from './upload.svelte';
	import Image from './image.svelte';
	import Icon from './icon.svelte';

	export let fileFolders: CmsClient.FileFolder[] = [
		CmsClient.FileFolderFromJSON({ name: 'Files', id: -1 })
	];

	export let folderNestingLevel = 0;

	export let dispatch = createEventDispatcher();

	async function getFileFolders() {
		const fileClient = new CmsClient.FileClient();
		try {
			const _fileFolders = await fileClient.getFileFolders();
			console.log('fileFolders from fetch', fileFolders);

			fileFolders[0].subfolders = _fileFolders;
		} catch (error) {
			console.error('An error occurred during folder fetch:', error);
		}
	}

	// Fetch fileFolders when the component is mounted
	onMount(() => {
		if (folderNestingLevel === 0) {
			getFileFolders();
		}
	});

	let selectedImageFile;

	function setSelectedImageFile(file) {
		selectedImageFile = file;

		dispatch('imageselect', selectedImageFile);
	}

	console.log('fileFolder', fileFolders, folderNestingLevel);

	let indeciesOfClosedFolder = [];
</script>

{#if fileFolders}
	<ul class="">
		{#each fileFolders as folder, i}
			{@const folderIsClosed = indeciesOfClosedFolder.includes(i)}
			<li>
				<div class="flex items-center gap-4">
					{#if folderIsClosed}
						<icon
							on:click={() => {
								indeciesOfClosedFolder = indeciesOfClosedFolder.filter((item) => {
									return item !== i;
								});
							}}
						>
							<Icon width="32" icon="flat-color-icons:folder" />
						</icon>
					{:else}
						<icon
							on:click={() => {
								indeciesOfClosedFolder = [...indeciesOfClosedFolder, i];
							}}
						>
							<Icon width="32" icon="flat-color-icons:opened-folder" />
						</icon>
					{/if}

					<div class="text-lg">{folder.name}</div>
					<div class="flex">
						<Upload fileFolder={folder} />
					</div>
				</div>

				{#if !folderIsClosed}
					<svelte:self
						folderNestingLevel={folderNestingLevel + 1}
						fileFolders={folder.subfolders || null}
						{dispatch}
					/>
				{/if}
			</li>
			{#if !folderIsClosed}
				{#if folder.files?.length}
					<ul class="flex flex-wrap mt-4">
						{#each folder.files as file}
							<li
								class={selectedImageFile?.id === file.id && 'outline outline-green-600 z-10'}
								on:click={() => {
									setSelectedImageFile(file);
								}}
							>
								<h1 class="hidden">{file.fileName + file.id}</h1>

								<Image height={200} image={file} />
							</li>
						{/each}
					</ul>
				{:else if !folder.subfolders?.length}
					<ul><li>Empty folder</li></ul>
				{/if}
			{/if}
		{/each}
	</ul>
{/if}
