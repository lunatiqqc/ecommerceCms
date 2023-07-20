<script lang="ts">
	import { allComponents } from '@/stores/allComponents.js';
	import type { ComponentType } from 'svelte';
	export let component: CmsClient.Component;

	function getComponent(
		discriminator: CmsClient.Component['discriminator']
	): Promise<ComponentType> {
		console.log(discriminator);

		const componentGetterKey = '/src/components/cmsComponents/' + discriminator + '.svelte';

		const componentGetter = $allComponents[componentGetterKey];

		console.log(componentGetterKey, $allComponents, componentGetter());

		return componentGetter();
	}
</script>

<div>
	{#await getComponent(component.discriminator) then module}
		<svelte:component this={module} {...component} />
	{/await}
</div>
