<script lang="ts">
	import { allComponents } from '@/stores/allComponents.js';
	import { getContext, onMount } from 'svelte';

	export let modifiedGridColumn: { originalIndex: number; content: CmsClient.GridColumn };
	export let rowColumnIndex;
	export let componentDraggedDiscriminator;
	export let gridRows;

	let hoveredSide;
	let hoveredSideWithComponent;

	async function getComponent(
		discriminator: CmsClient.Component['discriminator']
	): Promise<ComponentType> {
		const component = $allComponents['/src/components/cmsComponents/' + discriminator + '.svelte'];

		return component();
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

	function handleMouseMoveOver(event, rowIndex, columnStart, columnWidth) {
		return;
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

	let componentDiscriminator = modifiedGridColumn.content.component.discriminator;
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<!-- 
data-grid-column-original-index={modifiedGridColumn.originalIndex}
data-grid-column-index={rowColumnIndex} -->
<div
	bind:this={baseComponentRef}
	class="relative
	{borderHoverCursorClass[hoveredSide]}bg-green-200
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
	on:mousemove={(e) => {
		handleMouseMoveOver.call(
			{},
			e,
			modifiedGridColumn.content.columnStart,
			modifiedGridColumn.content.width
		);
	}}
	on:mouseleave={() => {
		mouseDown = false;
		hoveredSide = null;
	}}
	on:dragover={(e) => {
		handleMouseMoveOver.call(
			{},
			e,
			modifiedGridColumn.content.columnStart,
			modifiedGridColumn.content.width
		);
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
	{#await getComponent(componentDiscriminator) then module}
		<svelte:component this={module} {...modifiedGridColumn.content.component} />
	{/await}
</div>
