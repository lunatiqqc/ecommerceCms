<script lang="ts">
	import { ObjectFitOption } from '@/cmsTypeScriptClient';
	import type { Writable } from 'svelte/store';

	export let styleContent: CmsClient.ObjectFitOption;
	export let gridContentStore: Writable<CmsClient.GridContent>;
	export let styleContenta;
	export let styleContentKey;
	export let parentStyleContent;

	function handleSetObjectFit(val) {
		styleContent = val;
	}
</script>

<div class="flex flex-col">
	{#each Object.entries(CmsClient.ObjectFitOption) as [key, val]}
		<label>
			{key}
			<input
				on:click={() => {
					handleSetObjectFit(val);
				}}
				name="object-fit"
				type="radio"
				checked={styleContent === val}
			/>
		</label>
	{/each}
</div>
