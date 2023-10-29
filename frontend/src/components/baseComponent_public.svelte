<script lang="ts">
	import { allComponents } from '@/stores/allComponents.js';
	import type { ComponentType } from 'svelte';
	export let component: CmsClient.Component;

	function getComponent(
		discriminator: CmsClient.Component['discriminator']
	): Promise<ComponentType> {
		const componentGetterKey = '/src/components/cmsComponents/' + discriminator + '_public.svelte';

		const componentGetter = $allComponents[componentGetterKey];

		return componentGetter();
	}
</script>

<div>
	{#await getComponent(component.discriminator) then module}
		<svelte:component this={module} {...component} />
	{/await}
</div>
