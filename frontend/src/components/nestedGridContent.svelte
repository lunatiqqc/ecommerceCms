<script lang="ts">
	import BaseComponent from '@/components/baseComponent.svelte';
	import BaseGridColumn from '@/components/baseComponent.svelte';
	import { dataset_dev } from 'svelte/internal';
	import type { Writable } from 'svelte/store';
	import GridRow from './visualEditor/gridRow.svelte';
	import Icon from './icon.svelte';

	export let gridRows: CmsClient.GridRow[];
	export let componentDraggedDiscriminator;
	export let gridRowNestingLevel = 0;
	export let parentGridRows;
	export let parentGridRowIndex;
	export let parentIndexOfColumnInGridRow;
	export let pageStore;

	let gridRowDragover: {
		location: string;
		index: number;
	};

	function handleInsertGridRow(gridRowIndex: number, columnIndex: number) {
		let offsetNewRow = 1;

		gridRows = [
			...gridRows.slice(0, gridRowIndex),

			CmsClient.GridRowFromJSON({
				columns: [
					CmsClient.GridColumnFromJSON({
						columnStart: columnIndex,
						width: 12 - columnIndex,
						component: CmsClient.ComponentFromJSON({
							discriminator: componentDraggedDiscriminator
						})
					})
				],
				styling: {
					height: 250
				}
			}),
			...gridRows.slice(gridRowIndex)
		];
	}

	function handleSortingGridRows(gridRowIndexDroppedOnto: number) {
		if (gridRowIndexDragged > gridRowIndexDroppedOnto) {
			gridRows = [
				...gridRows.slice(0, gridRowIndexDroppedOnto),
				gridRows[gridRowIndexDragged],
				...gridRows
					.slice(gridRowIndexDroppedOnto)
					.filter((item) => item !== gridRows[gridRowIndexDragged])
			];
		}

		if (gridRowIndexDragged < gridRowIndexDroppedOnto) {
			gridRows = [
				...gridRows
					.slice(0, gridRowIndexDroppedOnto + 1)
					.filter((item) => item !== gridRows[gridRowIndexDragged]),
				gridRows[gridRowIndexDragged],
				...gridRows.slice(gridRowIndexDroppedOnto + 1)
			];
		}

		gridRowIndexDraggedOver = undefined;
		gridRowIndexDragged = undefined;
	}

	let gridRowIndexDraggedOver: number | undefined;
	let gridRowIndexDragged: number | undefined;
</script>

{#if gridRows?.length}
	{#each gridRows as gridRow, i (gridRow)}
		{#if gridRowIndexDraggedOver === i && gridRowIndexDragged > i}
			<div
				on:dragover|preventDefault
				class="col-span-12 border-2 p-4 h-20"
				on:drop={() => {
					handleSortingGridRows(i);
				}}
			>
				<Icon strokeWidth="10" class=" w-full h-full " icon="ion:add" />
			</div>
		{/if}
		<GridRow
			{parentGridRows}
			bind:gridRows
			gridRowIndex={i}
			{componentDraggedDiscriminator}
			{gridRowNestingLevel}
			{parentGridRowIndex}
			{parentIndexOfColumnInGridRow}
			on:setConfigurableContent
			{pageStore}
			bind:gridRow
			on:insertgridrow={({ detail }) => {
				handleInsertGridRow(i, detail.columnIndex);
			}}
			on:dragging={() => {
				gridRowIndexDragged = i;
				//andleSortingGridRows(i);
			}}
			on:dragend={() => {
				console.log('dragend');
			}}
			on:dragover={() => {
				console.log('dragover');

				gridRowIndexDraggedOver = i;
			}}
		/>
		{#if gridRowIndexDraggedOver === i && gridRowIndexDragged < i}
			<div
				on:dragover|preventDefault
				class="col-span-12 border-2 p-4 h-20"
				on:drop={() => {
					handleSortingGridRows(i);
				}}
			>
				<Icon strokeWidth="10" class=" w-full h-full " icon="ion:add" />
			</div>
		{/if}
	{/each}
{/if}
