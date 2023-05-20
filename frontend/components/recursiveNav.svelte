<script lang="ts">
	type Item = {
		title?: string;
		url?: string;
		parent?: Item;
		children?: Item[];
	};
	export let items: Item[];
	export let handleContextMenu;
</script>

<ul>
	{#each items as item}
		<li class="relative" class:text-green-700={item.id}>
			<a
				rel="external"
				on:contextmenu|preventDefault|self={item.id && ((e) => handleContextMenu(e, item))}
				href={item.url}
				>{item.title || item.url}
			</a>
		</li>

		{#if item.children}
			<svelte:self items={item.children} {handleContextMenu} />
		{/if}
	{/each}
</ul>
