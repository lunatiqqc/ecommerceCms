<script lang="ts">
	import { onMount } from 'svelte';

	const props: CmsClient.ImageComponent = $$props;

	onMount(() => {
		console.log('imageComponentMount');
	});
</script>

ImageComponent
