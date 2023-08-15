<script lang="ts">
	import ImageFile from './imageFile/imageFile.svelte';

	export let styleContent: CmsClient.Background['backgroundImage'];


</script>

<ImageFile bind:styleContent={styleContent.imageFile} />

{#if styleContent.imageFile}
	
<slot />

{/if}