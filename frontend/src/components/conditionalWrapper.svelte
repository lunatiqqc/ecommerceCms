<script lang="ts">
	export let tag = 'div';
	export let wrap = false;
</script>

{#if wrap}
	<svelte:element this={tag} {...$$restProps}>
		<slot />
	</svelte:element>
{:else}
	<slot />
{/if}
