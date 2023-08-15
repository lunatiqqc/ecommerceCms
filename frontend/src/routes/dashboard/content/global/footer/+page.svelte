<script lang="ts">
	import GridContentVisualEditor from '@/components/visualEditor/gridContentVisualEditor.svelte';
	export let data;
</script>

<GridContentVisualEditor bind:gridContent={data.footer.gridContent} />
