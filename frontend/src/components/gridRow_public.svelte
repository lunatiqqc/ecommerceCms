<script lang="ts">
	import GridColumnPublic from './gridColumn_public.svelte';
	import GridRowContent from './gridRowContent.svelte';

	export let gridRow: CmsClient.GridRow;
</script>

<GridRowContent styling={gridRow.styling} class="grid grid-cols-12" {gridRow}>
	{#if gridRow.columns}
		{#each gridRow.columns as gridColumn}
			<GridColumnPublic {gridColumn} />
		{/each}
	{/if}
</GridRowContent>
