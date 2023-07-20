<script lang="ts">
	import Icon from '@/components/icon.svelte';
	import RecursiveNav from '@/components/recursiveNav.svelte';
	import '@/app.scss';
	import { onMount } from 'svelte';
	export let data;

	let hideMenu = false;

	onMount(() => {
		// On page load or when changing themes, best to add inline in `head` to avoid FOUC
		if (
			localStorage.theme === 'dark' ||
			(!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)
		) {
			document.documentElement.classList.add('light');
		} else {
			document.documentElement.classList.remove('dark');
		}

		// Whenever the user explicitly chooses light mode
		localStorage.theme = 'light';

		// Whenever the user explicitly chooses dark mode
		localStorage.theme = 'dark';

		// Whenever the user explicitly chooses to respect the OS preference
		localStorage.removeItem('theme');
	});
</script>

<div class="flex">
	<!-- 	<button
		on:click={() => {
			data.endpoints = data.endpoints;
		}}>asd</button
	> -->
	<div class={hideMenu && 'w-0'}>
		<icon
			on:click={() => {
				hideMenu = true;
			}}
			class="ml-auto block w-fit"
		>
			<Icon width={24} icon="carbon:exit" />
		</icon>

		{#if data.endpoints}
			<RecursiveNav bind:items={data.endpoints} />
		{/if}
	</div>

	{#if hideMenu}
		<icon
			class="absolute"
			on:click={() => {
				hideMenu = false;
			}}
		>
			<Icon
				rotate={90}
				on:click={() => {
					setHideMenu = true;
				}}
				width={24}
				icon="carbon:exit"
			/>
		</icon>
	{/if}

	<slot />
</div>
