<script context="module" lang="ts">
	import { clickOutside } from '@/lib/actions/clickOutside';
	import { onMount, tick } from 'svelte/internal';
	import { focus } from '@/lib/actions/focus';
	import Icon from '@/components/icon.svelte';
	import ButtonIcon from '@/components/buttonIcon.svelte';
	import { store } from '@/stores/store';

	function moveElementByOffset(el: HTMLElement, options: { x: number; y: number }) {
		const matrix = new DOMMatrix();
		matrix.translateSelf(options.x, options.y);
		el.style.transform = matrix;
	}
	let editPageMenu: HTMLElement | null;
</script>

<script lang="ts">
	export let items: CmsClient.IPage[];

	export let parentItem: CmsClient.IPage;

	let showNewPage: boolean = false;
	let rightClickCoordinates: { x: number; y: number } | null = null;

	let newPageName = '';

	let currentIndex;

	let currentPageMenuItem: CmsClient.IPage | null;
</script>

<ul class="border-l-2">
	{#each items as item, i (item)}
		<li
			class="relative"
			class:text-green-700={!item.isSystemPage}
			class:text-green-900={item.isDynamicPagesRoot}
			class:text-bold={item.isDynamicPagesRoot}
		>
			<a
				rel="external"
				on:contextmenu|preventDefault|self={async (e) => {
					if (!item.isSystemPage || item.isDynamicPagesRoot) {
						currentPageMenuItem = item;
						showNewPage = false;
						await tick();
						if (editPageMenu) {
							editPageMenu.parentElement?.removeChild(editPageMenu);
						}

						rightClickCoordinates = { x: e.clientX, y: e.clientY };

						currentIndex = i;
					}
				}}
				href={item.url}>{item.title}</a
			>
			{#if item.children?.length}
				<svelte:self items={item.children} parentItem={item} />
			{/if}
			{#if showNewPage && currentIndex === i}
				<ul
					on:outclick={() => {
						showNewPage = false;
						newPageName = '';
					}}
					use:clickOutside
				>
					<li class="flex">
						<a
							class="min-w-[48px]"
							use:focus
							bind:textContent={newPageName}
							contenteditable
							href=""
						/>
						<div class="ml-4 flex gap-3">
							<ButtonIcon
								icon="carbon:close-outline"
								on:click={() => {
									showNewPage = false;
									newPageName = '';
								}}
							/>
							<ButtonIcon
								on:click={async () => {
									const pagesClient = new CmsClient.PagesClient();

									const newPage = await pagesClient.post(newPageName, item.id);

									newPage.children = [];

									item.children = [...item.children, newPage];

									showNewPage = false;
									newPageName = '';
								}}
								icon="carbon:checkmark"
							/>
						</div>
					</li>
				</ul>
			{/if}
		</li>
	{/each}
</ul>

{#key rightClickCoordinates}
	{#if rightClickCoordinates}
		<div
			bind:this={editPageMenu}
			use:moveElementByOffset={{ x: rightClickCoordinates.x, y: rightClickCoordinates.y }}
			class="fixed inset-0 bg-red-700 px-4 py-4 w-fit h-fit z-10"
			use:clickOutside
			on:outclick={() => (rightClickCoordinates = null)}
		>
			<ul class="p-0">
				<li class="list-none">
					<button
						on:click={() => {
							showNewPage = true;
							rightClickCoordinates = null;
						}}
						class="whitespace-nowrap text-black">Add page</button
					>
				</li>
				<li class="list-none">
					<button
						on:click={async () => {
							const pagesClient = new CmsClient.PagesClient();

							const test = await pagesClient.delete(currentPageMenuItem.id);

							parentItem.children = parentItem.children?.filter(
								(item) => item.id !== currentPageMenuItem.id
							);

							store.update((old) => old);

							currentPageMenuItem = null;
							rightClickCoordinates = null;
						}}
						class="whitespace-nowrap text-black">Remove page</button
					>
				</li>
			</ul>
		</div>
	{/if}
{/key}
