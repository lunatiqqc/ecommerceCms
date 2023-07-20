<script lang="ts">
	import type { Writable } from 'svelte/store';

	import ContentStylingConfigurator from '@/components/contentStylingConfigurator.svelte';
	import type { ContainerStyling } from '@/cmsTypeScriptClient';

	export let styleContent: ContainerStyling;
	export let pageStore: Writable<CmsClient.Page>;

	//if (!styleContent.background) {
	//	styleContent.background = CmsClient.BackgroundFromJSON({});
	//}
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->

<slot />

<!-- <ContentStylingConfigurator
	rootFolder="background"
	styleContent={styleContent.background}
	{pageStore}
/> -->
