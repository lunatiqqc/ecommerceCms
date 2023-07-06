<script lang="ts">
	import type { Component, GridColumnComponent } from '@/cmsTypescriptClient/index.js';
	import TextComponent from '@/components/cmsComponents/TextComponent.svelte';
	import EditorNewRow from '@/components/editorNewRow.svelte';
	import NestedGridContent from '@/components/nestedGridContent.svelte';
	import { SvelteComponent, each, onMount } from 'svelte/internal';
	import type { ComponentType, SvelteComponentTyped } from 'svelte/types/runtime/internal/dev.js';
	import { writable } from 'svelte/store';

	import { allComponents } from '@/stores/allComponents.js';
	import { stringify } from 'postcss';

	export let data;

	console.log('orig page2', data.page);

	let componentDraggedDiscriminator;

	function handleComponentDrop(columnIndex) {
		console.log('handleComponentDrop');

		if (!componentDraggedDiscriminator) {
			return;
		}
		const column = CmsClient.GridColumnFromJSON({
			width: 3,
			columnStart: columnIndex,
			gridRows: [
				CmsClient.GridRowFromJSONTyped(
					{
						columns: [
							CmsClient.GridColumnFromJSONTyped(
								{
									component: CmsClient.ComponentFromJSONTyped(
										{ discriminator: componentDraggedDiscriminator },
										false
									),
									columnStart: 0,
									width: 12
								},
								true
							)
						]
					},
					true
				)
			]
		});

		if (!pageChangeHistory[pageChangeHistory.length - 1 + pageHistoryOffset].gridRows) {
			pageChangeHistory[pageChangeHistory.length - 1 + pageHistoryOffset].gridRows = [];
		}

		pageChangeHistory[pageChangeHistory.length - 1 + pageHistoryOffset].gridRows = [
			...pageChangeHistory[pageChangeHistory.length - 1 + pageHistoryOffset].gridRows,
			CmsClient.GridRowFromJSONTyped({ columns: [column] }, true)
		];
	}

	let pageHistoryOffset = 0;

	let indexOfPageChangeHistory = 0;

	let pageChangeHistory = [data.page];

	function updatePageHistory() {
		//console.log('pageHistoryWithinUpdate: ', pageChangeHistory, indexOfPageChangeHistory);

		let cloneOfCurrentPage;

		if (indexOfPageChangeHistory == 0) {
			cloneOfCurrentPage = data.page;
		} else {
			cloneOfCurrentPage = structuredClone(pageChangeHistory[pageChangeHistory.length - 1]);
		}
		indexOfPageChangeHistory++;
		pageChangeHistory[indexOfPageChangeHistory] = cloneOfCurrentPage;
	}

	$: updatePageHistory(pageChangeHistory);
</script>

<!-- {#if mounted}
	<GrapesJs />
{/if} -->

<div class="w-full">
	<button
		on:click={() => {
			console.log(data.page);
		}}>log page</button
	>

	<button
		class="p-4 rounded-md bg-red-400 disabled:opacity-50"
		disabled={!(pageChangeHistory.length - 1 + pageHistoryOffset > 0)}
		on:click={() => {
			pageHistoryOffset--;
		}}>undo</button
	>
	{pageHistoryOffset}
	<button
		class="p-4 rounded-md bg-green-400 disabled:opacity-50"
		disabled={pageHistoryOffset === 0}
		on:click={() => {
			pageHistoryOffset++;
		}}>redo</button
	>

	<div class="w-full grid grid-cols-12 relative">
		<NestedGridContent
			{componentDraggedDiscriminator}
			bind:gridRows={pageChangeHistory[pageChangeHistory.length - 1 + pageHistoryOffset].gridRows}
		/>
		<div class="grid col-span-12 grid-cols-12">
			{#each new Array(12) as number, i}
				<div
					data-grid-column-index-new={i}
					class="aspect-square border-2"
					on:dragover|preventDefault
					on:drop={() => {
						handleComponentDrop(i);
					}}
				/>
			{/each}
		</div>

		<button
			on:click={() => {
				const pagesClient = new CmsClient.PagesClient();
				pagesClient.put({ page: data.page, id: data.page.id });
			}}>put</button
		>
	</div>
</div>

<ul>
	{#each Object.keys($allComponents) as key}
		{@const discriminator = key.substring(key.lastIndexOf('/') + 1, key.lastIndexOf('.'))}
		<li
			draggable="true"
			on:dragend={(e) => {
				componentDraggedDiscriminator = null;
			}}
			on:drag={(e) => {
				componentDraggedDiscriminator = discriminator;
			}}
		>
			{discriminator}
		</li>
	{/each}
</ul>
