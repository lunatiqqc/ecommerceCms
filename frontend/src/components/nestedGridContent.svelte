<script lang="ts">
	import BaseComponent from '@/components/baseComponent.svelte';
	import BaseGridColumn from '@/components/baseComponent.svelte';
	import { dataset_dev } from 'svelte/internal';
	import type { Writable } from 'svelte/store';
	import GridRow from './gridRow.svelte';

	export let gridRows: [CmsClient.GridRow];
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
</script>

{#if gridRows?.length}
	{#each gridRows as gridRow, i}
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
		/>
	{/each}
{/if}
