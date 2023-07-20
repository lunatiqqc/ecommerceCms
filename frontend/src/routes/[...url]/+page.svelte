<script lang="ts">
	import NestedGridContentPublic from '@/components/nestedGridContent_public.svelte';

	export let data;
</script>

<div id="public-root" class="w-full">
	{#if data.header?.gridContent?.gridRows?.length}
		<header>
			<NestedGridContentPublic gridRows={data.header.gridContent.gridRows} />
		</header>
	{/if}

	{#if data.page.gridContent?.gridRows}
		<main>
			<NestedGridContentPublic gridRows={data.page.gridContent.gridRows} />
		</main>
	{/if}

	{#if data.footer?.gridContent?.gridRows?.length}
		<footer>
			<NestedGridContentPublic gridRows={data.footer.gridContent.gridRows} />
		</footer>
	{/if}
</div>
