<script lang="ts">
	import {
		PagesClient,
		type Component,
		type GridColumnComponent
	} from '@/cmsTypeScriptClient/index.js';
	import TextComponent from '@/components/cmsComponents/TextComponent.svelte';
	import EditorNewRow from '@/components/editorNewRow.svelte';
	import NestedGridContent from '@/components/nestedGridContent.svelte';
	import { SvelteComponent, each, onMount, setContext } from 'svelte/internal';
	import type { ComponentType, SvelteComponentTyped } from 'svelte/types/runtime/internal/dev.js';
	import { type Writable, writable } from 'svelte/store';

	import { allComponents } from '@/stores/allComponents.js';
	import { allStyleDataEditorComponents } from '@/stores/allStyleDataEditorComponents.js';
	import { stringify } from 'postcss';
	import Upload from '@/components/upload.svelte';
	import SelectableImageGallery from '@/components/selectableImageGallery.svelte';
	import Icon from '@/components/icon.svelte';
	import ContentStylingConfigurator from '@/components/contentStylingConfigurator.svelte';

	export let gridContent: CmsClient.GridContent | undefined;

	if (!gridContent) {
		gridContent = {};
	}
	if (!gridContent.gridRows) {
		gridContent.gridRows = [];
	}

	let styleContent = null;

	const gridContentStore = writable(gridContent);

	gridContentStore.subscribe((value) => {
		console.log('gridContentUpdated', value);
	});

	//gridContentStore.update((n) => n);
	console.log('orig gridContent', $gridContentStore);

	let componentDraggedDiscriminator;

	function handleComponentDrop(columnIndex) {
		if (!componentDraggedDiscriminator) {
			return;
		}
		const column = CmsClient.GridColumnFromJSON({
			component: CmsClient.ComponentFromJSON({ discriminator: componentDraggedDiscriminator }),
			columnStart: columnIndex,
			width: 1
		});

		$gridContentStore.gridRows = [
			...$gridContentStore.gridRows,
			CmsClient.GridRowFromJSON({
				columns: [column],
				styling: CmsClient.ContainerStylingFromJSON({ height: 250 })
			})
		];
	}

	let unique = {};

	function handleSetConfigurableContent({
		detail
	}: {
		detail: {
			styleContent: Writable<any>;
		};
	}) {
		console.log('handleSetConfigurableContent', detail);
		styleContent = detail;
		//
		//styleContent = 'dddd';

		//detail.styleContent = 'nope';

		//$detail.Background = 'test';

		//detail.styling = { test3: true };
		//
		//detail = detail;

		//detail = { test2: true };

		//detail.subscribe((o) => {
		//	styleContent2.update((prev) => {
		//		return prev + 1;
		//	});
		//
		//	styleContent.set(o);
		//});

		hideContentStylingConfiguration = false;

		unique = {};

		return;

		//
		//console.log('contentStylingConfiguration', contentStylingConfiguration);
		//
		//hideContentStylingConfiguration = false;

		//{contentStore : gridRowStore, stylingOptions : CmsClient.ContainerStylingFromJSON({})}

		//detail.columns = [];
		//
		//console.log(detail);

		//detail.update((gridRow) => {
		//	gridRow.columns = [];
		//	return gridRow;
		//});

		//detail.contentCallback(null);

		//const test = detail.content;
		//
		//test.columns = [];
		//
		//console.log(test === $gridContentStore?.gridRows[0]);
		//console.log(test, $gridContentStore?.gridRows[0]);

		//detail.$content.columns = [];

		//stylingConfiguration = detail.stylingConfiguration;
		//configuredContent = detail.content;

		//detail.content.columns = [];

		//const test = detail.content;
		//
		//console.log('test', test);

		//
		//intermediate = detail.content;
	}

	let gridContentChangeHistory = [$gridContentStore];

	let hideContentStylingConfiguration = false;
</script>

<!-- {#if mounted}
	<GrapesJs />
{/if} -->

<div class="flex pb-12">
	<!-- 	<button on:click={() => {}}>log gridContent</button>

	<button class="p-4 rounded-md bg-red-400 disabled:opacity-50" on:click={() => {}}>undo</button>
	<button class="p-4 rounded-md bg-green-400 disabled:opacity-50" on:click={() => {}}>redo</button> -->

	<div class="w-full">
		{#if $gridContentStore}
			<div class="w-full bg-slate-700 px-12">
				<div class="bg-white w-full grid grid-cols-12 relative">
					<NestedGridContent
						{gridContentStore}
						{componentDraggedDiscriminator}
						bind:gridRows={$gridContentStore.gridRows}
						on:setConfigurableContent={(e) => {
							handleSetConfigurableContent(e);
						}}
					/>
					{#each new Array(12) as number, i}
						<div
							data-grid-column-index-new={i}
							class="aspect-square outline-1 outline"
							on:dragover|preventDefault|stopPropagation
							on:drop={() => {
								handleComponentDrop(i);
							}}
						>
							{#if true}
								<icon>
									<Icon strokeWidth="10" class=" w-full h-full " icon="ion:add" />
								</icon>
							{/if}
						</div>
					{/each}
				</div>
			</div>
		{:else}
			no content
		{/if}

		<ul>
			{#each Object.keys($allComponents) as key}
				{@const discriminator = key.substring(key.lastIndexOf('/') + 1, key.lastIndexOf('.'))}
				<li
					draggable="true"
					on:dragend={(e) => {
						componentDraggedDiscriminator = null;
					}}
					on:drag={(e) => {
						componentDraggedDiscriminator = discriminator;
					}}
				>
					{discriminator}
				</li>
			{/each}
		</ul>
		<button
			on:click={() => {
				//$gridContentStore = $gridContentStore;
				console.log($gridContentStore);
			}}>Log orig gridContent</button
		>
		<button
			class="ml-8"
			on:click={() => {
				$gridContentStore.gridRows[0].columns = [];
			}}
			>update nested gridRow from gridContent
		</button>
	</div>

	{#if $styleContent && !hideContentStylingConfiguration}
		<div class="right-0 h-screen bg-slate-200 overflow-y-auto px-2 sticky top-0 z-40 w-72">
			<button
				on:click={() => {
					$styleContent.discriminator = 'tasdadasd';
				}}>test</button
			>
			<button
				on:click={() => {
					hideContentStylingConfiguration = true;
				}}>close</button
			>

			{#key unique}
				<ContentStylingConfigurator
					bind:styleContent={$styleContent}
					{gridContentStore}
					firstLevelFolder={true}
				/>
			{/key}
		</div>
	{/if}
</div>
