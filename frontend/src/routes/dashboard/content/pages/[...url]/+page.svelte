<script lang="ts">
	import { page } from '$app/stores';
	import GridContentVisualEditor from '@/components/visualEditor/gridContentVisualEditor.svelte';
	import Icon from '@/components/icon.svelte';

	export let data;

	$: console.log('pageUpdated', data.page);

	let exited: boolean = false;

	let showSettings = false;
	let showResponsiveDevices = false;

	let responsiveSize;
</script>

<div class="w-full flex flex-col absolute inset-0 pl-8 bg-slate-400 h-screen {exited && 'hidden'}">
	<GridContentVisualEditor
		bind:responsiveSize
		{showSettings}
		bind:gridContent={data.page.gridContent}
	/>

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
				pagesClient.put({ id: data.page.id, pagesPutRequest: data.page });
			}}
		>
			Put
		</button>
		<button
			on:click={() => {
				showSettings = true;
			}}
		>
			<Icon width="24" icon="carbon:settings" />
		</button>
		<div class="relative flex items-center justify-center">
			<button
				on:click={() => {
					showResponsiveDevices = !showResponsiveDevices;
				}}
			>
				<Icon width="24" icon="mdi:responsive" />
			</button>
			{#if showResponsiveDevices}
				<div class="flex flex-col absolute bottom-full bg-slate-100 rounded p-2 gap-2 text-black">
					<button
						on:click={() => {
							responsiveSize = 320;
							showResponsiveDevices = !showResponsiveDevices;
						}}
					>
						<Icon width="24" icon="mdi:cellphone" />
					</button>
					<button
						on:click={() => {
							responsiveSize = 720;
							showResponsiveDevices = !showResponsiveDevices;
						}}
					>
						<Icon width="24" icon="mdi:tablet" />
					</button>
					<button
						on:click={() => {
							responsiveSize = 1440;
							showResponsiveDevices = !showResponsiveDevices;
						}}
					>
						<Icon width="24" icon="mdi:monitor" />
					</button>
				</div>
			{/if}
		</div>
	</div>
</div>
