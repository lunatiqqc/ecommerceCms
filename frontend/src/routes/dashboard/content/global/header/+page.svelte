<script lang="ts">
	import GridContentVisualEditor from '@/components/gridContentVisualEditor.svelte';
	export let data;
</script>

<GridContentVisualEditor bind:gridContent={data.header.gridContent} />
