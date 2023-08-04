<script lang="ts">
	import { allComponents } from '@/stores/allComponents.js';
	import { allComponentsEditors } from '@/stores/allComponentsEditors.js';
	import { getContext, onMount, createEventDispatcher, setContext } from 'svelte';
	import Icon from './icon.svelte';
	import { writable } from 'svelte/store';
	import { ContainerStylingFromJSON, TextContainerStylingFromJSON } from '@/cmsTypeScriptClient';

	export let modifiedGridColumn: { originalIndex: number; content: CmsClient.GridColumn };
	export let rowColumnIndex;
	export let componentDraggedDiscriminator;
	export let gridRows;

	const styleContent = getContext('styleContent');

	const dispatch = createEventDispatcher();

	let componentStore = writable(modifiedGridColumn.content.component);

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
		console.log('mouseover');

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

	onMount(() => {
		console.log('baseComponentMount');
	});

	let componentDiscriminator = $componentStore.discriminator;
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<!-- 
data-grid-column-original-index={modifiedGridColumn.originalIndex}
data-grid-column-index={rowColumnIndex} -->
<div
	bind:this={baseComponentRef}
	class="relative z-10
 bg-green-200
	h-full
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
		console.log('leave');

		mouseDown = false;
		hoveredSide = undefined;
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
		console.log(page);

		page = null;
	}}
>
	{#if false && hoveredSide}
		<div
			on:mousedown={(e) => {
				handleMouseDown(e, hoveredSide);
			}}
			class="absolute z-50 inset-0 h-full w-full {hoveredSide && borderHoverClassMap[hoveredSide]}"
		/>
	{/if}

	{#if true || hoveredSide !== undefined}
		<icon
			on:mouseover={() => {
				hoveredSide = null;
			}}
			on:click={() => {
				if (!$componentStore.textStyling) {
					$componentStore.textStyling = CmsClient.TextContainerStylingFromJSON({});
				}
				if (!$componentStore.styling) {
					$componentStore.styling = CmsClient.ContainerStylingFromJSON({});
				}
				//styleContent.set(componentStore);
				//styleContent4 = $componentStore;
				dispatch('setConfigurableContent', componentStore);

				console.log($componentStore);
			}}
			class="absolute flex items-center h-fit w-6 z-30 right-full"
		>
			<Icon class="mb-auto" width={20} icon="carbon:settings-view" />
		</icon>
	{/if}

	{#await getComponentEditor(componentDiscriminator) then module}
		{#if module}
			<svelte:component this={module} {...$componentStore}>
				{#await getComponent(componentDiscriminator) then module}
					<svelte:component this={module} {...$componentStore} />
				{/await}
			</svelte:component>
		{:else}
			{#await getComponent(componentDiscriminator) then module}
				<svelte:component this={module} {...$componentStore} />
			{/await}
		{/if}
	{/await}
</div>
