<script lang="ts">
	import { page } from '$app/stores';
	import GridContentVisualEditor from '@/components/gridContentVisualEditor.svelte';
	import Icon from '@/components/icon.svelte';

	export let data;

	$: console.log('pageUpdated', data.page);

	let exited: boolean = false;
</script>

<div class="w-full flex flex-col absolute inset-0 pl-8 bg-slate-400 h-screen {exited && 'hidden'}">
	<GridContentVisualEditor bind:gridContent={data.page.gridContent} />

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
				console.log(data.page);
				const pagesClient = new CmsClient.PagesClient();
				pagesClient.put({ id: data.page.id, page: data.page });
			}}
		>
			Put
		</button>
	</div>
</div>
