<script lang="ts">
	import { allComponents } from '@/stores/allComponents.js';

	export let modifiedGridColumn: { originalIndex: number; content: CmsClient.GridColumn };
	export let rowColumnIndex;
	export let componentDraggedDiscriminator;
	export let gridRows;

	let hoveredSide;
	let hoveredSideWithComponent;

	async function getComponent(
		discriminator: CmsClient.Component['discriminator']
	): Promise<ComponentType> {
		const component = $allComponents['/components/cmsComponents/' + discriminator + '.svelte'];

		return component();
	}

	function handleMouseMoveOver(event, rowIndex, columnStart, columnWidth) {
		return;
		console.log('Move');

		const { clientX, clientY, target } = event;
		const threshold =
			15 + parseInt(window.getComputedStyle(target).getPropertyValue('border-width'));
		const { left, top, width, height } = target.getBoundingClientRect();

		if (!this.originalWidth) {
			this.originalWidth = width;
		}

		// Calculate the distance from the mouse coordinates to each side of the element
		const distanceLeft = clientX - left;
		const distanceTop = clientY - top;
		const distanceRight = left + width - clientX;
		const distanceBottom = top + height - clientY;

		let mousePositionDeltaX = 0;

		// Check if the distance is within the threshold, including the space outside the element
		if (distanceLeft <= threshold || clientX < left) {
			if (
				columnStart - 1 > 0 &&
				!gridRows[rowIndex].columns?.find((column) => {
					return (
						columnStart - 1 >= column.columnStart &&
						columnStart - 1 <= column.columnStart + column.width - 1
					);
				})
			) {
				hoveredSide = 'left';
				console.log('Resize left possible');
			} else {
				console.log('hovered left side');

				if (componentDraggedDiscriminator) {
					hoveredSideWithComponent = 'left';
					console.log(componentDraggedDiscriminator);
				}
			}

			return;
		}
		if (distanceRight <= threshold || clientX > left + width) {
			if (
				columnStart + 1 + columnWidth - 1 <= 11 &&
				!gridRows[rowIndex].columns?.find((column) => {
					return column.columnStart === columnStart + columnWidth - 1 + 1;
				})
			) {
				hoveredSide = 'right';
				if (mouseDown) {
					mousePositionDeltaX = event.clientX - mouseDragStartPosition;

					event.target.style.width = clientX - left + 10 + 'px';

					console.log('dragging right');
				}
				console.log('Resize right possible');
			} else {
				if (componentDraggedDiscriminator) {
					hoveredSideWithComponent = 'right';
					console.log(componentDraggedDiscriminator);
				}
				console.log('hovered right side');
			}
			return;
		}
		if (distanceTop <= threshold || clientY < top) {
			if (rowIndex > 0) {
				hoveredSide = 'top';
				console.log('Resize top possible');
			} else {
				if (componentDraggedDiscriminator) {
					hoveredSideWithComponent = 'top';
					console.log(componentDraggedDiscriminator);
				}
				console.log('hovered top side');
			}
			return;
		}
		if (distanceBottom <= threshold || clientY > top + height) {
			hoveredSide = 'bottom';
			console.log('Resize bottom possible');

			if (componentDraggedDiscriminator) {
				hoveredSideWithComponent = 'bottom';
				console.log(componentDraggedDiscriminator);
			}
			console.log('hovered bottom side');
			return;
		}
		hoveredSideWithComponent = null;
		hoveredSide = null;
	}

	const borderHoverCursorClass = {
		top: 'cursor-n-resize',
		right: 'cursor-e-resize',
		bottom: 'cursor-s-resize',
		left: 'cursor-w-resize'
	};

	const borderHoverWithComponentClass = {
		top: 'border-t-black border-t-8',
		right: 'border-r-black border-r-8',
		bottom: 'border-b-black border-b-8',
		left: 'border-l-black border-l-8'
	};

	let mouseDown;
	let mouseDragStartPosition;
</script>

<div
	data-grid-column-original-index={modifiedGridColumn.originalIndex}
	data-grid-column-index={rowColumnIndex}
	class="grid__column relative col-span-{modifiedGridColumn.content.width} 
	col-start-{modifiedGridColumn.content.columnStart + 1} overflow-hidden
	{borderHoverCursorClass[hoveredSide]}
	{borderHoverWithComponentClass[hoveredSideWithComponent]}
	transition-[border-width] bg-green-200 p-4
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
>
	{#await getComponent(modifiedGridColumn.content.component.discriminator) then module}
		<svelte:component this={module} {...modifiedGridColumn.content.component} />
	{/await}
</div>

<style global lang="scss">
	.grid {
		&__column {
			&::after {
				content: '';
				position: absolute;
				inset: 0;
				border: 15px solid rgba(0, 0, 0, 0.151);
				pointer-events: none;
			}
		}
	}
</style>
