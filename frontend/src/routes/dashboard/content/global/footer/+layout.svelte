<script lang="ts">
	import Icon from '@/components/icon.svelte';

	export let data;

	let exited = false;

	import { page } from '$app/stores';
</script>

<div class="w-full flex flex-col absolute inset-0 bg-slate-400 h-screen {exited && 'hidden'}">
	<slot />

	<div class="flex fixed py-2 px-4 rounded bg-slate-600 text-slate-300 bottom-0 left-0 gap-4">
		<button
			class="flex gap-2 items-center"
			on:click={() => {
				exited = true;
			}}
		>
			<icon>
				<Icon rotate={90} icon="ion:exit-outline" />
			</icon>
			<h1>Exit</h1>
		</button>
		<button
			on:click={() => {
				const globalContentClient = new CmsClient.GlobalContentClient();
				globalContentClient.updateFooter({ globalFooter: $page.data.footer });
			}}
		>
			Put
		</button>
	</div>
</div>
