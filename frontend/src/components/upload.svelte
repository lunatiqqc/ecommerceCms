<script lang="ts">
	import type { ChangeEventHandler, EventHandler } from 'svelte/elements';
	import Icon from './icon.svelte';
	import { clickOutside } from '@/lib/actions/clickOutside';

	export let fileFolder: CmsClient.FileFolder;

	let files: FileList | null = null;

	async function handleFileUpload() {
		if (!files) {
			console.error('No file selected.');
			return;
		}

		//const blob = new Blob([file], { type: file.type });

		console.log(files);

		const fileClient = new CmsClient.FileClient();

		try {
			const response = await fileClient.uploadRaw({
				files: Array.from(files),
				fileFolderName: 'test'
			});

			if (response.raw.ok) {
				console.log('File uploaded successfully!');
				// Handle successful upload, such as displaying a success message or redirecting
			} else {
				console.error('File upload failed.');
				// Handle failed upload, such as displaying an error message
			}
		} catch (error) {
			console.error('An error occurred during file upload:', error);
			// Handle error, such as displaying an error message
		}
	}

	function handleFilesChange(e) {
		const selectedFiles = e.target.files;
		files = selectedFiles;
	}

	$: console.log(files);

	let showUploadInput = false;
</script>

<article>
	<icon
		on:click={() => {
			showUploadInput = !showUploadInput;
		}}
	>
		<Icon
			width={32}
			viewBox={{ left: 0, top: 0, width: 44, height: 44 }}
			icon="flat-color-icons:add-image"
		/>
	</icon>
	{#if showUploadInput}
		<form
			use:clickOutside
			on:outclick={() => {
				showUploadInput = false;
			}}
			class="absolute z-10 bg-slate-800 light:bg-slate-50"
		>
			<div class="p-4">
				{#if fileFolder.id === -1}
					<label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
						>New folder name:
						<input
							class="block w-full text-sm text-white border border-gray-300 rounded cursor-pointer bg-gray-50 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400"
							type="text"
							required
							on:change={(e) => {
								handleFilesChange(e);
							}}
						/>
					</label>
				{/if}
				<label
					class="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
					for="multiple_files">Upload multiple files</label
				>
				<input
					class="block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400"
					id="multiple_files"
					type="file"
					multiple
					on:change={(e) => {
						handleFilesChange(e);
					}}
				/>
			</div>
			{#if files}
				<div><button class="p-4 rounded border-2" on:click={handleFileUpload}>Upload</button></div>
			{/if}
		</form>
	{/if}
</article>

<style>
	/* Add your custom styles here */
</style>
