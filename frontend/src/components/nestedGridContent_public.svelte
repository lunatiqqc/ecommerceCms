<script lang="ts">
	import GridRowPublic from './gridRow_public.svelte';
	export let gridRows: CmsClient.GridRow[];
</script>

{#each gridRows as gridRow}
	<GridRowPublic {gridRow} />
{/each}
