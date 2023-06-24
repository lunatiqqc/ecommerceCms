<script lang="ts">
	import type { Component, GridColumnComponent } from '@/cmsTypescriptClient/index.js';
	import TextComponent from '@/components/cmsComponents/TextComponent.svelte';
	import EditorNewRow from '@/components/editorNewRow.svelte';
	import NestedGridContent from '@/components/nestedGridContent.svelte';
	import { SvelteComponent, each, onMount } from 'svelte/internal';
	import type { ComponentType, SvelteComponentTyped } from 'svelte/types/runtime/internal/dev.js';
	import { writable } from 'svelte/store';
	import { page } from '$app/stores';

	export let data;

	console.log('orig page', data.page);

	//const page = writable(data.page);

	const allComponents = import.meta.glob('@/components/cmsComponents/*', {
		eager: false,
		import: 'default'
	});

	let mounted = false;

	onMount(() => {
		mounted = true;
	});

	let newRowRef: [Element?] = [];
	let rowRefs: [Element?] = [];

	console.log(data.page);

	function handleComponentDragEnd(e: MouseEvent, componentDiscriminator) {
		const elementDraggedTo = document.elementFromPoint(e.clientX, e.clientY);

		console.log('elementDraggedTo', elementDraggedTo);

		if (!elementDraggedTo) {
			return;
		}

		const columnIndexDataOfNewRow = elementDraggedTo.dataset.gridColumnIndexNew;

		if (columnIndexDataOfNewRow) {
			const columnIndexOfNewRow = parseInt(columnIndexDataOfNewRow);
			console.log(columnIndexOfNewRow);

			const column = CmsClient.GridColumnFromJSON({
				component: CmsClient.ComponentFromJSONTyped(
					{ $discriminator: componentDiscriminator },
					false
				),
				width: 1,
				columnStart: columnIndexOfNewRow
			});

			let columns = [];

			columns.push(column);

			if (!data.page.gridRows) {
				data.page.gridRows = [];
			}
			data.page.gridRows = [
				...data.page.gridRows,
				CmsClient.GridRowFromJSONTyped({ columns: columns }, true)
			];

			//return data.page;
			return;
		}

		const parentRow = elementDraggedTo.closest('[data-grid-row-index]');

		if (!parentRow) {
			return;
		}

		const columnIndexDataOfExistingRow = elementDraggedTo.dataset.gridColumnIndex;

		if (columnIndexDataOfExistingRow) {
			const columnIndexOfExistingRow = parseInt(columnIndexDataOfExistingRow);
			console.log('columnIndexOfExistingRow', columnIndexDataOfExistingRow);
			if (!data.page.gridRows) {
				data.page.gridRows = [];
			}

			const originalColumnIndexOfExistingRow = parseInt(
				elementDraggedTo.dataset.gridColumnOriginalIndex
			);

			let existingColumns = data.page.gridRows[parentRow.dataset.gridRowIndex].columns;

			let existingColumn = existingColumns[originalColumnIndexOfExistingRow];

			console.log('existingColumn', existingColumn);

			const column = CmsClient.GridColumnFromJSON({
				component: CmsClient.ComponentFromJSONTyped(
					{ $discriminator: componentDiscriminator },
					false
				),
				width: 1,
				columnStart: columnIndexOfExistingRow,
				id: existingColumn?.id
			});

			console.log('column', column);

			if (existingColumn) {
				data.page.gridRows[parentRow.dataset.gridRowIndex].columns[
					originalColumnIndexOfExistingRow
				] = column;
			} else {
				data.page.gridRows[parentRow.dataset.gridRowIndex].columns = [...existingColumns, column];
			}

			return;
		}
	}

	let componentDraggedDiscriminator;
</script>

<!-- {#if mounted}
	<GrapesJs />
{/if} -->

<div class="w-full">
	<div class="w-full grid grid-cols-12 relative">
		<NestedGridContent {componentDraggedDiscriminator} {rowRefs} page={data.page} {allComponents} />
		<div class="grid col-span-12 grid-cols-12">
			{#each new Array(12) as number, i}
				<div data-grid-column-index-new={i} class="aspect-square border-2" />
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
	{#each Object.keys(allComponents) as key}
		{@const discriminator = key.substring(key.lastIndexOf('/') + 1, key.lastIndexOf('.'))}
		<li
			draggable="true"
			on:dragend={(e) => {
				handleComponentDragEnd(e, discriminator);
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
