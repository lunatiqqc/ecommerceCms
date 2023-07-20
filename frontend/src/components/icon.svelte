<script lang="ts">
	import Icon, { buildIcon, getIcon, _api } from '@iconify/svelte';
	import type IconifyIcon from '@iconify/svelte';
	import { onMount, type ComponentProps } from 'svelte/internal';

	export let viewBox: SVGRect | null;

	//const icon = getIcon($$props.icon);
	//if (icon && viewBox) {
	//	icon.width = viewBox.width;
	//	icon.height = viewBox.height;
	//	icon.left = viewBox.left;
	//	icon.top = viewBox.top;
	//}

	let width = 6;

	if ($$props.class) {
		const regex = /w-(\d+)/;
		const match = $$props.class.match(regex);

		if (match) {
			const numberValue = parseInt(match[1]);
			width = numberValue;
		}
	}

	let icon;

	onMount(async () => {
		if ($$props.icon) {
			const test4 = await _api.sendAPIQuery(
				_api.getAPIConfig('') || '',
				{
					type: 'icons',
					provider: '',
					prefix: $$props.icon.split(':')[0],
					icons: [$$props.icon.split(':')[1]]
				},
				(res) => {
					//realIcon = buildIcon({
					//	body: res.icons[$$props.icon.split(':')[1]].body,
					//	width: viewBox?.width || 32,
					//	height: viewBox?.height || 32,
					//	left: viewBox?.left || 0,
					//	top: viewBox?.top || 0
					//});

					icon = {
						width: res.width,
						height: res.height,
						body: res.icons[$$props.icon.split(':')[1]].body
					};

					if ($$props.strokeWidth) {
						const newBody = replaceStrokeWidth(icon.body, $$props.strokeWidth);

						icon.body = newBody;
					}
				}
			);
		}
	});

	function replaceStrokeWidth(text, newWidth) {
		return text.replace(/(stroke-width=")\d+(")/, '$1' + newWidth + '$2');
	}

	let test;
</script>

<!-- 	{@html icon2} -->

{#if icon}
	<Icon
		bind:this={test}
		{...$$props}
		icon={{
			body: icon.body,
			width: viewBox?.width || icon.width || 32,
			height: viewBox?.height || icon.height || 32,
			left: viewBox?.left || 0,
			top: viewBox?.top || 0
		}}
	/>
{/if}
