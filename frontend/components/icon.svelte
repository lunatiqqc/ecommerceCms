<script lang="ts">
	import Icon from '@iconify/svelte';
	import type { ComponentProps } from 'svelte/internal';

	import tailwindConfig from '@/tailwind.config.js';

	const test = tailwindConfig.theme?.spacing;

	console.log(test);

	type $$Props = Omit<ComponentProps<Icon>, 'width'>;

	let width = 6;

	if ($$props.class) {
		const regex = /w-(\d+)/;
		const match = $$props.class.match(regex);

		if (match) {
			const numberValue = parseInt(match[1]);
			width = numberValue;
		}
	}
</script>

<icon
	class="block relative"
	style="width:{width / 4}rem;
padding-bottom:{width / 4}rem
"
>
	<Icon {...$$props} class="w-full h-full absolute" />
</icon>
