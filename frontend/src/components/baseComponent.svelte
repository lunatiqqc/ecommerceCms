<script lang="ts">
	import { allComponents } from '@/stores/allComponents.js';
	import { allComponentsEditors } from '@/stores/allComponentsEditors.js';
	import { getContext, onMount, createEventDispatcher, setContext } from 'svelte';
	import Icon from './icon.svelte';
	import { writable } from 'svelte/store';
	import { ContainerStylingFromJSON, TextContainerStylingFromJSON } from '@/cmsTypeScriptClient';
	import Image from './image.svelte';

	export let gridColumn: CmsClient.GridColumn;
	export let rowColumnIndex;
	export let componentDraggedDiscriminator;
	export let gridRows;
	export let mousemoveGridrow;

	const dispatch = createEventDispatcher();

	let componentStore = writable(gridColumn.component);

	let hoveredSide;
	let hoveredSideWithComponent;

	async function getComponent(
		discriminator: CmsClient.Component['discriminator']
	): Promise<ComponentType> {
		const component = $allComponents['/src/components/cmsComponents/' + discriminator + '.svelte'];

		return component();
	}
	async function getComponentEditor(
		discriminator: CmsClient.Component['discriminator']
	): Promise<ComponentType> {
		const component =
			$allComponentsEditors[
				'/src/components/cmsComponentsEditors/' + discriminator + 'Editor' + '.svelte'
			];

		if (component) {
			return component();
		}
	}

	function getElementClientSide(event) {
		const { clientX, clientY, currentTarget } = event as MouseEvent & { target: HTMLElement };
		const threshold =
			15 + parseInt(window.getComputedStyle(currentTarget).getPropertyValue('border-width'));
		const { left, top, width, height } = currentTarget.getBoundingClientRect();

		//if (!this.originalWidth) {
		//	this.originalWidth = width;
		//}

		// Calculate the distance from the mouse coordinates to each side of the element
		const distanceLeft = clientX - left;
		const distanceTop = clientY - top;
		const distanceRight = left + width - clientX;
		const distanceBottom = top + height - clientY;

		// Check if the distance is within the threshold, including the space outside the element
		if (distanceLeft <= threshold || clientX < left) {
			return 'left';
		}
		if (distanceRight <= threshold || clientX > left + width) {
			return 'right';
		}
		if (distanceTop <= threshold || clientY < top) {
			return 'top';
		}
		if (distanceBottom <= threshold || clientY > top + height) {
			return 'bottom';
		}
	}

	function handleMouseMoveOver(event) {
		mousemove = true;

		const elementClientSideHovered = getElementClientSide(event);
		if (elementClientSideHovered) {
			hoveredSide = elementClientSideHovered;
			return;
		}
	}

	const borderHoverCursorClass = {
		top: 'cursor-n-resize',
		right: 'cursor-e-resize',
		bottom: 'cursor-s-resize',
		left: 'cursor-w-resize'
	};

	const borderHoverClassMap = {
		top: 'border-t-black border-t-8',
		right: 'border-r-black border-r-8',
		bottom: 'border-b-black border-b-8',
		left: 'border-l-black border-l-8'
	};

	let mouseDown;
	let mouseDragStartPosition;

	function handleMouseDown(e, hoveredSide) {
		baseComponentRectBeforeModification = baseComponentRef.getBoundingClientRect();
		const { clientX, clientY, currentTarget } = e;
		mouseDownPositionBeforeMoving = { initialX: clientX, initialY: clientY };

		function handleMouseMoveWhilePressed(e) {
			const { clientX, clientY, currentTarget } = e;

			if (hoveredSide === 'bottom') {
				const mouseyDelta = clientY - mouseDownPositionBeforeMoving.initialY;

				baseComponentRef.style.minHeight =
					baseComponentRectBeforeModification.height + mouseyDelta + 'px';
			}
		}
		function handleMouseUp() {
			document.removeEventListener('mousemove', handleMouseMoveWhilePressed);
			document.removeEventListener('mouseup', handleMouseUp);
		}
		document.addEventListener('mousemove', handleMouseMoveWhilePressed);
		document.addEventListener('mouseup', handleMouseUp);
	}

	let mouseDownPositionBeforeMoving: { initialX: number; initialY: number };
	let baseComponentRef: HTMLElement;
	let baseComponentRectBeforeModification: DOMRect;

	let page = getContext('test');

	onMount(() => {});

	let mousemove = false;
	let componentDiscriminator = $componentStore.discriminator;
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<!-- 
data-grid-column-original-index={modifiedGridColumn.originalIndex}
data-grid-column-index={rowColumnIndex} -->
<div
	bind:this={baseComponentRef}
	class="relative z-10
	h-full
	pt-{$componentStore?.styling?.padding?.top}
	pr-{$componentStore?.styling?.padding?.right}
	pl-{$componentStore?.styling?.padding?.left}
	pb-{$componentStore?.styling?.padding?.bottom}
	mt-{$componentStore?.styling?.margin?.top}
	mr-{$componentStore?.styling?.margin?.right}
	ml-{$componentStore?.styling?.margin?.left}
	mb-{$componentStore?.styling?.margin?.bottom}

	{mousemove && 'bg-slate-300 bg-opacity-30 outline-dashed outline-1'}
	"
	on:mousedown={(e) => {
		mouseDragStartPosition = e.clientX;
		mouseDown = true;
	}}
	on:mouseup={() => {
		mouseDragStartPosition = null;
		mouseDown = false;
	}}
	on:mouseover|stopPropagation={(e) => {
		handleMouseMoveOver(e);
	}}
	on:mouseleave={() => {
		mouseDown = false;
		hoveredSide = undefined;
		mousemove = false;
	}}
	on:dragover={(e) => {
		//handleMouseMoveOver.call(
		//	{},
		//	e,
		//	modifiedGridColumn.content.columnStart,
		//	modifiedGridColumn.content.width
		//);
	}}
	on:dragleave={() => {
		hoveredSideWithComponent = null;
	}}
	on:click|stopPropagation={() => {
		page = null;
	}}
>
	{#if $componentStore?.styling?.background?.backgroundImage?.imageFile}
		<div class="absolute w-full h-full -z-10">
			<Image
				class="w-full h-full"
				preventLayoutShift={false}
				backgroundImage={$componentStore.styling.background.backgroundImage}
				imageStyle={$componentStore.styling.background.backgroundImage}
			/>
		</div>
	{/if}
	{#if false && hoveredSide}
		<div
			on:mousedown={(e) => {
				handleMouseDown(e, hoveredSide);
			}}
			class="absolute z-50 inset-0 h-full w-full {hoveredSide && borderHoverClassMap[hoveredSide]}"
		/>
	{/if}

	{#if mousemove}
		<icon
			on:mouseover={() => {
				hoveredSide = null;
			}}
			on:click={() => {
				//styleContent.set(componentStore);
				//styleContent4 = $componentStore;

				let styleContentKeys = Object.keys($componentStore);

				const keysToIgnore = [];

				const nameToTypeMapper = {
					backgroundImage: 'ImageStyle',
					styling: 'ContainerStyling',
					textStyling: 'TextContainerStyling'
				};

				for (let i = 0; i < styleContentKeys.length; i++) {
					const key = styleContentKeys[i];

					if (keysToIgnore.includes(key)) {
						continue;
					}

					if ($componentStore[key] === undefined) {
						const dynamicFunctionName =
							capitalizeFirstLetter(nameToTypeMapper[key] || key) + 'FromJSON';

						const generateBaseJsonFunction = CmsClient[dynamicFunctionName];

						console.log(dynamicFunctionName);

						if (generateBaseJsonFunction) {
							$componentStore[key] = CmsClient[dynamicFunctionName]({});
						}
					}

					function capitalizeFirstLetter(string) {
						if (!string) {
							return string;
						}

						return string[0].toUpperCase() + string.slice(1);
					}
				}

				dispatch('setConfigurableContent', componentStore);
			}}
			class="absolute flex items-center h-fit w-6 z-30 left-full pb-4"
		>
			<Icon class="mb-auto" width={20} height={20} icon="carbon:settings-view" />
		</icon>
	{/if}

	{#await getComponentEditor(componentDiscriminator) then module}
		{#if module}
			<svelte:component this={module} {...$componentStore}>
				{#await getComponent(componentDiscriminator) then module}
					<div
						class="
					pt-{$componentStore?.styling?.padding?.top}
					pr-{$componentStore?.styling?.padding?.right}
					pl-{$componentStore?.styling?.padding?.left}
					pb-{$componentStore?.styling?.padding?.bottom}
					mt-{$componentStore?.styling?.margin?.top}
					mr-{$componentStore?.styling?.margin?.right}
					ml-{$componentStore?.styling?.margin?.left}
					mb-{$componentStore?.styling?.margin?.bottom}"
					>
						<svelte:component this={module} {...$componentStore} />
					</div>
				{/await}
			</svelte:component>
		{:else}
			{#await getComponent(componentDiscriminator) then module}
				<svelte:component this={module} {...$componentStore} />
			{/await}
		{/if}
	{/await}
</div>
