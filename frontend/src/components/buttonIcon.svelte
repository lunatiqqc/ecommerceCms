<script lang="ts">
	import Icon from '@/components/icon.svelte';
	import type { ComponentProps } from 'svelte/internal';

	type $$Props = ComponentProps<Icon>;
</script>

<button on:click>
	<Icon {...$$props} />
</button>
