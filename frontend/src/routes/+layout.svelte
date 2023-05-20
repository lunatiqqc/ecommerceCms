<script lang="ts">
	import RecursiveNav from '@/components/recursiveNav.svelte';
	import NewPageMenu from '@/components/newPageMenu.svelte';
	import '@/src/app.scss';
	import { writable } from 'svelte/store';
	export let data;

	let dynamicMenuComponent = null;

	async function handleContextMenu(e: MouseEvent, page) {
		if (dynamicMenuComponent) {
			dynamicMenuComponent.$destroy();
		}
		dynamicMenuComponent = new NewPageMenu({
			target: document.body,
			props: {
				rightClickCoordinates: [e.clientX, e.clientY],
				pageItem: page,
				handleAddPage: addPage
			}
		});
	}

	function addPage(parent) {
		const parentRef = recursiveSearch(data.endpoints, parent.id);

		parent.children = [...parent.children, { title: 'asloasdlasd' }];

		data.endpoints = data.endpoints;

		function recursiveSearch<T>(array: T[], target: string): T | null {
			for (let i = 0; i < array.length; i++) {
				const element = array[i];
				for (const key in element) {
					if (element[key] === target) {
						return element;
					} else if (Array.isArray(element[key])) {
						const result = recursiveSearch(element[key] as T[], target);
						if (result) {
							return result;
						}
					}
				}
			}
			return null; // Element not found
		}
	}
</script>

<div class="flex">
	{#if data.endpoints}
		<RecursiveNav items={data.endpoints} {handleContextMenu} />
	{/if}

	<slot />
</div>
