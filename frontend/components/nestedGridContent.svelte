<script lang="ts">
	import BaseGridColumn from '@/components/baseGridColumn.svelte';
	import { dataset_dev } from 'svelte/internal';
	import type { Writable } from 'svelte/store';

	export let page: CmsClient.Page;
	export let allComponents;
	export let rowRefs;
	export let componentDraggedDiscriminator;

	function getColumnStart(columns, currentIndex) {
		const prevColumn = columns[currentIndex - 1];
		if (prevColumn) {
			return prevColumn.width + currentIndex;
		}

		return currentIndex + 1;
	}

	$: modifiedGridColumns = page.gridRows?.map((row) => {
		let modifiedGridColumns = [];

		for (let i = 0; i < 12; i++) {
			console.log('i221312321', i);

			let orignalIndex;

			const columnContentAtIndex = row.columns?.find((column, j) => {
				orignalIndex = j;

				return column && i >= column.columnStart && i < column.columnStart + column.width;
			});

			if (columnContentAtIndex) {
				modifiedGridColumns[i] = { orignalIndex: orignalIndex, content: columnContentAtIndex };

				for (let j = 0; j < columnContentAtIndex.width - 1; j++) {
					modifiedGridColumns[i + 1 + j] = 'skip';
				}
				i = i + columnContentAtIndex.width - 1;
			} else {
				modifiedGridColumns[i] = null;
			}
		}

		return modifiedGridColumns;
	});

	$: {
		console.log('test', page);
		console.log('modifiedGridColumns', modifiedGridColumns);
	}
</script>

{#if page.gridRows}
	{#each page.gridRows as row, i}
		<div
			data-grid-row-index={i}
			bind:this={rowRefs[i]}
			class="grid col-span-12 grid-cols-12 relative p-4 border-2"
		>
			<!-- <div class="absolute">{i}</div> -->
			<!-- 			<div class="grid grid-cols-12 absolute -z-10 w-full h-full bg-opacity-30 bg-green-950">
				{#each new Array(12) as undefined, j}
					
				{/each}
			</div> -->
			{#if modifiedGridColumns[i]}
				{@const columns = modifiedGridColumns[i]}
				{#each columns as column, j}
					{#if column !== 'skip'}
						{#if column && column.content.component}
							<BaseGridColumn
								{componentDraggedDiscriminator}
								{allComponents}
								{column}
								rowColumnIndex={j}
								rowIndex={i}
								{page}
							/>

							<!-- {#if column.gridRows?.length}
						<svelte:self {page} {getComponent} />
					{/if} -->
						{:else if !column}
							<div
								data-grid-column-index={j}
								class="border-2 bg-green-950 col-start-{getColumnStart(row.columns, j)}"
							/>
						{/if}
					{/if}
				{/each}
			{/if}
		</div>
	{/each}
{/if}
