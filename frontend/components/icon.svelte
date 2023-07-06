<script lang="ts">
	import Icon, { buildIcon, getIcon, _api } from '@iconify/svelte';
	import type IconifyIcon from '@iconify/svelte';
	import { onMount, type ComponentProps } from 'svelte/internal';
	import icon2 from '@/assets/icons/brackets-curly-bold-edited.svg?raw';
	import tailwindConfig from '@/tailwind.config.js';

	export let viewBox: SVGRect | null;

	console.log($$props.icon);

	const icon = getIcon($$props.icon);
	if (icon && viewBox) {
		icon.width = viewBox.width;
		icon.height = viewBox.height;
		icon.left = viewBox.left;
		icon.top = viewBox.top;
	}

	console.log('icon1', icon);

	const test = tailwindConfig.theme?.spacing;

	//console.log(test);

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

	let realIcon;

	let iconBody;

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
					console.log('res', res);

					//realIcon = buildIcon({
					//	body: res.icons[$$props.icon.split(':')[1]].body,
					//	width: viewBox?.width || 32,
					//	height: viewBox?.height || 32,
					//	left: viewBox?.left || 0,
					//	top: viewBox?.top || 0
					//});

					(iconBody = res.icons[$$props.icon.split(':')[1]].body),
						console.log('realIcon', realIcon);
				}
			);

			console.log('test4', test4);
		}
	});

	function svgPathFull() {}

	let test2;

	let iconHtml;
</script>

<!-- 	{@html icon2} -->
<Icon
	{...$$props}
	icon={{
		body: iconBody,
		width: viewBox?.width || 32,
		height: viewBox?.height || 32,
		left: viewBox?.left || 0,
		top: viewBox?.top || 0
	}}
/>
