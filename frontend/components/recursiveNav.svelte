<script context="module" lang="ts">
	import type { CmsClient } from '@/cmsTypescriptClient/cmsClient';
	import NewPageMenu from '@/components/newPageMenu.svelte';
	import { store } from '@/stores/store';
	import type { SvelteComponent } from 'svelte/internal';
	let pageMenu: SvelteComponent;
	function handleContextMenu(e: MouseEvent, page: ClassProperties<CmsClient.Page>) {
		//page.children = [...page.children, { title: 'asloasdlasdasdasdasdasdasdasdadaasa' }];
		if (pageMenu) {
			pageMenu.$destroy();
		}
		page.children[0].pageMenu = new NewPageMenu({
			target: document.body,
			props: {
				rightClickCoordinates: [e.clientX, e.clientY],
				handleAddPage: addPage
			}
		});
		function addPage() {
			console.log('add page');

			if (page.children) {
				page.children.push({ title: 'work' });
			} else {
				page.children = [{ title: 'work' }];
			}

			store.update((old) => old);
		}
	}
</script>

<script lang="ts">
	export let items: ClassProperties<CmsClient.Page>[];
</script>

<ul>
	{#each items as item}
		<!-- <button
			on:click={() => {
				items = items;

				console.log(items);
			}}>asd2</button
		> -->
		<li class="relative" class:text-green-700={!item.isSystemPage}>
			<a
				rel="external"
				on:contextmenu|preventDefault|self={(item.id &&
					((e) => {
						//items = [];
						//item.children = [...item.children, { title: 'work' }];

						handleContextMenu(e, item);
					})) ||
					null}
				href={item.url}
				>{item.title || item.url}
			</a>
		</li>

		{#if item.children?.length}
			<svelte:self items={item.children} {handleContextMenu} />
		{/if}
	{/each}
</ul>
