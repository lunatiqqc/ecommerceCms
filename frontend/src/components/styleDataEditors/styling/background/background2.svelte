<script lang="ts">
	import type { Writable } from 'svelte/store';

	import ContentStylingConfigurator from '@/components/contentStylingConfigurator.svelte';

	export let styleContent: CmsClient.ImageStyle;
	export let pageStore: Writable<CmsClient.Page>;

	//if (!styleContent.background) {
	//	styleContent.background = CmsClient.BackgroundFromJSON({});
	//}


	styleContent.backgroundImage = 'test4';
</script>

background

<!-- svelte-ignore a11y-click-events-have-key-events -->

<!-- <ContentStylingConfigurator
	rootFolder="background"
	styleContent={styleContent.background}
	{pageStore}
/> -->
