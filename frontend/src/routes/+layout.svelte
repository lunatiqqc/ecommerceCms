<script lang="ts">
	import RecursiveNav from '@/components/recursiveNav.svelte';
	import '@/src/app.scss';
	import { store } from '@/stores/store';
	export let data;

	store.set(data);
</script>

<div class="flex">
	<!-- 	<button
		on:click={() => {
			data.endpoints = data.endpoints;
		}}>asd</button
	> -->

	{#if $store.endpoints}
		<RecursiveNav items={$store.endpoints} />
	{/if}

	<slot />
</div>
