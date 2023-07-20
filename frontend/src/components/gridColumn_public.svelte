<script lang="ts">
	import BaseComponentPublic from './baseComponent_public.svelte';
	import GridColumnContent from './gridColumnContent.svelte';
	import NestedGridContentPublic from './nestedGridContent_public.svelte';

	export let gridColumn: CmsClient.GridColumn;
</script>

<GridColumnContent
	class="
col-span-{gridColumn.width}
col-start-{gridColumn.columnStart + 1}"
	styling={gridColumn.styling}
>
	{#if gridColumn.component}
		<BaseComponentPublic component={gridColumn.component} />
	{:else if false && gridColumn.gridContent}
		<NestedGridContentPublic gridRows={gridColumn.gridContent.gridRows} />
	{/if}
</GridColumnContent>
