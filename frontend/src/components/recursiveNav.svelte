<script context="module" lang="ts">
	import { clickOutside } from '@/lib/actions/clickOutside';
	import { onMount, tick } from 'svelte/internal';
	import { focus } from '@/lib/actions/focus';
	import Icon from '@/components/icon.svelte';
	import ButtonIcon from '@/components/buttonIcon.svelte';
	import { page } from '$app/stores';

	function moveElementByOffset(el: HTMLElement, options: { x: number; y: number }) {
		const matrix = new DOMMatrix();
		matrix.translateSelf(options.x, options.y);
		el.style.transform = matrix;
	}
	let editPageMenu: HTMLElement | null;
</script>

<script lang="ts">
	import { PagesClient } from '@/cmsTypeScriptClient';

	export let items: CmsClient.Page[];

	export let parentItem: CmsClient.Page | null;

	export let parentUrl = '';
	export let showEditItemAtItem: CmsClient.Page | null = null;

	let showNewPage: boolean = false;
	let rightClickCoordinates: { x: number; y: number } | null = null;

	let newPageUrl = '';

	let currentIndex;

	let currentPageMenuItem: CmsClient.Page | null;

	let confirmDeleteAtIndex: number | null;
</script>

<ul class="border-l-2 pl-6 text-xl">
	{#each items as item, i (item)}
		<li
			class="relative my-4"
			class:text-green-700={!item.isSystemPage}
			class:text-green-900={item.isDynamicPagesRoot}
			class:text-bold={item.isDynamicPagesRoot}
		>
			<div
				class="flex items-center {!item.visibleInMenu &&
					!item.isSystemPage &&
					!item.isDynamicPagesRoot &&
					'opacity-50'} gap-2 h-6"
			>
				<a
					class="h-full"
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
					href={(() => {
						if (showEditItemAtItem !== null && showEditItemAtItem === item) {
							return null;
						}
						if (parentUrl) {
							if (item.url === '/') {
								return parentUrl + '/' + item.url;
							}
							return parentUrl + '/' + item.url;
						}
						return item.url;
					})()}
				>
					{#if showEditItemAtItem === item}
						<div
							class="pr-1 outline-2 outline-slate-500 focus:outline-black outline"
							use:focus
							contenteditable="true"
							bind:textContent={item.url}
						/>
					{:else}
						<div
							class="h-full flex items-center {!item.isSystemPage &&
								'border-r-slate-500 border-r-2 pr-4'}"
						>
							{item.url}
							{#if item.url === '/'}
								<icon class="ml-2 h-full" title="Homepage">
									<Icon
										height="100%"
										viewBox={{ width: 18, height: 16, left: 1.5, top: 1.5 }}
										icon="system-uicons:home-door"
									/>
								</icon>
							{/if}
						</div>
					{/if}
				</a>

				{#if showEditItemAtItem === item}
					<div class="ml-4 flex gap-3">
						<ButtonIcon
							icon="zondicons:close-outline"
							on:click={() => {
								showEditItemAtItem = null;
							}}
						/>
						<ButtonIcon
							on:click={async () => {
								const pagesClient = new CmsClient.PagesClient();

								const res = await pagesClient.put({ id: item.id, page: item });

								showEditItemAtItem = null;
							}}
							icon="zondicons:checkmark"
						/>
					</div>
				{/if}
				{#if !item.isSystemPage && showEditItemAtItem === null}
					{#if item.visibleInMenu}
						<icon
							class="ml-2 h-full"
							on:click={() => {
								item.visibleInMenu = false;
							}}
						>
							<Icon height="100%" icon="zondicons:view-hide" />
						</icon>
					{:else}
						<icon
							class="ml-2 h-full"
							on:click={() => {
								item.visibleInMenu = true;
							}}
						>
							<Icon height="100%" icon="zondicons:view-show" />
						</icon>
					{/if}

					<icon
						class="ml-2 h-full"
						on:click={() => {
							showNewPage = true;
							currentIndex = i;
						}}
					>
						<Icon height="100%" icon="zondicons:add-solid" />
					</icon>

					<icon
						class="ml-2 h-full"
						on:click={() => {
							showEditItemAtItem = item;
							currentIndex = i;
						}}
					>
						<Icon height="100%" icon="lucide:edit" />
					</icon>

					{#if item.url !== '/'}
						<icon
							class="ml-2 h-full"
							on:click={async () => {
								confirmDeleteAtIndex = i;
							}}
						>
							<Icon height="100%" icon="zondicons:trash" />
						</icon>
					{/if}
				{/if}
			</div>

			{#if confirmDeleteAtIndex === i}
				<div
					use:clickOutside
					on:outclick={() => {
						confirmDeleteAtIndex = null;
					}}
					class="absolute py-2 px-3 rounded bg-slate-600 text-slate-200 flex-col z-10"
				>
					<div class="text-xl mb-2">
						Sure?
						{#if item.children?.length}
							<br />
							<p class="text-sm text-opacity-80 whitespace-nowrap">
								Nested pages under this page will also be deleted
							</p>
						{/if}
					</div>
					<div class="flex gap-8">
						<button
							class="border-2 py-2 px-3 rounded"
							on:click={async () => {
								const pagesClient = new CmsClient.PagesClient();
								const test = await pagesClient.delete({ id: item.id });
								parentItem.children = parentItem.children?.filter((_item) => _item.id !== item.id);
								//store.update((old) => old);
								currentPageMenuItem = null;
								rightClickCoordinates = null;

								confirmDeleteAtIndex = null;
							}}>Yes</button
						><button
							class="border-2 py-2 px-3 rounded"
							on:click={() => {
								confirmDelete = false;
							}}>No</button
						>
					</div>
				</div>
			{/if}
			{#if item.children?.length}
				<svelte:self
					parentUrl={(() => {
						if (item.url === '/') {
							return parentUrl;
						} else {
							return parentUrl + '/' + item.url;
						}
					})()}
					bind:items={item.children}
					bind:parentItem={item}
					bind:showEditItemAtItem
				/>
			{/if}
			{#if showNewPage && currentIndex === i}
				<ul
					on:outclick={() => {
						showNewPage = false;
						newPageUrl = '';
					}}
					use:clickOutside
				>
					<li class="flex">
						<div
							class="min-w-[48px] border-2 ml-4"
							use:focus
							bind:textContent={newPageUrl}
							contenteditable
						/>

						<div class="ml-4 flex gap-3">
							<ButtonIcon
								icon="zondicons:close-outline"
								on:click={() => {
									showNewPage = false;
									newPageUrl = '';
								}}
							/>
							<ButtonIcon
								on:click={async () => {
									const pagesClient = new CmsClient.PagesClient();

									const newPage = await pagesClient.post({ url: newPageUrl, parentId: item.id });

									newPage.children = [];

									item.children = [...item.children, newPage];

									showNewPage = false;
									newPageUrl = '';
								}}
								icon="zondicons:checkmark"
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

							//store.update((old) => old);

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
