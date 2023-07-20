<script lang="ts">
	import { allStyleDataEditorComponents } from '@/stores/allStyleDataEditorComponents';
	import type { Writable } from 'svelte/store';

	export let styleContent: any;
	export let gridContentStore: Writable<CmsClient.GridContent>;
	export let rootFolder = '';
	export let firstLevelFolder = false;
	export let nestingLevel = 0;
	export let parentKey = '';

	let styleContentKeys = Object.keys(styleContent);

	$: console.log(styleContent);

	const keysToIgnore = ['imageFile'];

	const nameToTypeMapper = {
		backgroundImage: 'ImageStyle'
	};

	function generateObjectFields(styleContentKeys) {
		for (let i = 0; i < styleContentKeys.length; i++) {
			const key = styleContentKeys[i];

			if (key === 'id' || keysToIgnore.includes(key)) {
				continue;
			}

			if (styleContent[key] === undefined) {
				const dynamicFunctionName =
					capitalizeFirstLetter(nameToTypeMapper[key] || key) + 'FromJSON';

				const generateBaseJsonFunction = CmsClient[dynamicFunctionName];

				if (generateBaseJsonFunction) {
					styleContent[key] = CmsClient[dynamicFunctionName]({});
				}
			}
			console.log('yo', key);
		}
	}

	$: for (let i = 0; i < styleContentKeys.length; i++) {
		const key = styleContentKeys[i];

		if (key === 'id' || keysToIgnore.includes(key)) {
			continue;
		}

		if (styleContent[key] === undefined) {
			const dynamicFunctionName = capitalizeFirstLetter(nameToTypeMapper[key] || key) + 'FromJSON';

			const generateBaseJsonFunction = CmsClient[dynamicFunctionName];

			if (generateBaseJsonFunction) {
				styleContent[key] = CmsClient[dynamicFunctionName]({});
			}
		}
		console.log('yo', key, nestingLevel);
	}

	async function getDataStyleEditorComponent(name): unknown {
		const componentPath =
			'/src/components/styleDataEditors' + parentKey + '/' + name + '/' + name + '.svelte';

		console.log(componentPath, parentKey);

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
</script>

{#if styleContent}
	<ul class="pl-2 pb-6 border-b-2 border-b-black">
		{#each Object.keys(styleContent) as styleContentKey (nestingLevel + styleContentKey)}
			{#if styleContentKey !== 'id' && !keysToIgnore.includes(styleContentKey)}
				<li class="py-2">
					<h1 class="text-xl capitalize">{styleContentKey}</h1>
					{#await getDataStyleEditorComponent(styleContentKey) then module}
						{#if module}
							<svelte:component
								this={module}
								{styleContentKey}
								styleContent={styleContent[styleContentKey]}
								{gridContentStore}
							>
								{#if styleContent[styleContentKey]}
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
									styleContent={styleContent[styleContentKey]}
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
