<script context="module" lang="ts">
	const keysToIgnore = ['imageFile', 'id', 'discriminator', 'html', 'editorState'];

	const nameToTypeMapper = {
		backgroundImage: 'ImageStyle',
		styling: 'ContainerStyling'
	};

	const orderMap = {
		textStyling: -1
	};
</script>

<script lang="ts">
	import { allStyleDataEditorComponents } from '@/stores/allStyleDataEditorComponents';
	import Background from './styleDataEditors/background/background2.svelte';
	import type { Writable } from 'svelte/store';

	export let styleContent: any;
	export let gridContentStore: Writable<CmsClient.GridContent>;
	export let rootFolder = '';
	export let firstLevelFolder = false;
	export let nestingLevel = 0;
	export let parentKey = '';

	console.log('styleContentUpdateNested', styleContent);

	let styleContentKeys = Object.keys(styleContent);

	$: for (let i = 0; i < styleContentKeys.length; i++) {
		const key = styleContentKeys[i];

		if (keysToIgnore.includes(key)) {
			continue;
		}

		if (styleContent[key] === undefined) {
			const dynamicFunctionName = capitalizeFirstLetter(nameToTypeMapper[key] || key) + 'FromJSON';

			const generateBaseJsonFunction = CmsClient[dynamicFunctionName];

			if (generateBaseJsonFunction) {
				styleContent[key] = CmsClient[dynamicFunctionName]({});
			}
		}
	}

	async function getDataStyleEditorComponent(name): unknown {
		const componentPath =
			'/src/components/styleDataEditors' + parentKey + '/' + name + '/' + name + '.svelte';

		const componentImportFunction = $allStyleDataEditorComponents[componentPath];

		if (componentImportFunction) {
			const test = componentImportFunction();

			return test;
		}
	}

	function capitalizeFirstLetter(string) {
		if (!string) {
			return string;
		}

		return string[0].toUpperCase() + string.slice(1);
	}

	styleContentKeys.sort((a, b) => {
		return (orderMap[a] || 0) - (orderMap[b] || 0);
	});

	console.log('styleContentKeysAfter', styleContentKeys);

	let backgroundComponent =
		$allStyleDataEditorComponents['/src/components/styleDataEditors/background/background.svelte'];
</script>

{#if styleContent}
	<ul class="pl-2 pb-6 border-b-2 border-b-black">
		{#each styleContentKeys as styleContentKey (nestingLevel + styleContentKey)}
			{#if !keysToIgnore.includes(styleContentKey)}
				<li class="py-2">
					<h1 class="text-xl capitalize">{styleContentKey}</h1>

					{#await getDataStyleEditorComponent(styleContentKey) then module}
						{#if module}
							<svelte:component
								this={module}
								{styleContentKey}
								bind:styleContent={styleContent[styleContentKey]}
								parentStyleContent={styleContent}
								{gridContentStore}
							>
								{#if false && styleContent[styleContentKey]}
									<svelte:self
										nestingLevel={nestingLevel + 1}
										styleContent={styleContent[styleContentKey]}
										parentKey={parentKey + '/' + styleContentKey}
										{gridContentStore}
									/>
								{/if}
							</svelte:component>
						{:else}
							{styleContentKey} editor not found

							{#if styleContent[styleContentKey]}
								<svelte:self
									nestingLevel={nestingLevel + 1}
									bind:styleContent={styleContent[styleContentKey]}
									parentKey={parentKey + '/' + styleContentKey}
									{gridContentStore}
								/>
							{/if}
						{/if}
					{/await}
				</li>
			{/if}
		{/each}
	</ul>
{/if}
