<script lang="ts">
	import { createEventDispatcher } from 'svelte';
	import BaseComponent from '../baseComponent.svelte';
	import Icon from '../icon.svelte';
	import NestedGridContent from '../nestedGridContent.svelte';
	import GridColumnContent from '../gridColumnContent.svelte';
	import { writable } from 'svelte/store';

	export let gridColumn: CmsClient.GridColumn;

	export let indexOfColumnInGridRow;
	export let componentDraggedDiscriminator;
	export let gridRows: [CmsClient.GridRow];
	export let gridRowIndex: number;
	export let showGridOverlay: boolean;
	export let gridRowNestingLevel;
	export let parentGridRows;
	export let parentIndexOfColumnInGridRow;
	export let parentRowRef;

	export let mousemoveGridrow: boolean;

	export let columnRef: HTMLElement;

	const gridColumnStore = writable(gridColumn);

	

	const dispatch = createEventDispatcher();


	let hoveredSide;

	function handleMouseDown(e) {
		mouseDown = true;

		const { clientX, clientY, currentTarget } = e;
		if (hoveredSide) {
			mouseDownPositionBeforeMoving = { initialX: clientX, initialY: clientY };
			columnRectBeforeModification = columnRef.getBoundingClientRect();
			document.addEventListener('mousemove', handleDocumentMouseMoveWhilePressed);

			showGridOverlay = true;
		}
		document.addEventListener('mouseup', handleMouseUp);

		if (!$gridColumnStore.styling) {
			$gridColumnStore.styling = CmsClient.ContainerStylingFromJSON({})
		}

		const _hoveredSide = hoveredSide;

		function handleMouseUp() {
			mouseDown = false;
			if (widthDelta !== null && widthDelta !== undefined) {
				if (_hoveredSide === 'right') {
					$gridColumnStore.width += widthDelta;
					columnRef.style.removeProperty('width');
				}

				if (_hoveredSide === 'left') {
					$gridColumnStore.columnStart += widthDelta;
					$gridColumnStore.width -= widthDelta;
					columnRef.style.removeProperty('transform');
					columnRef.style.removeProperty('width');
				}
			}
			if (hoveredSide === 'bottom') {
			}
			showGridOverlay = false;
			hoveredSide = null;
			widthDelta = 0;
			document.removeEventListener('mousemove', handleDocumentMouseMoveWhilePressed);
			document.removeEventListener('mouseup', handleMouseUp);
		}

		function handleDocumentMouseMoveWhilePressed(e) {
			const { clientX, clientY, target } = e;
			const parentRowBoundingBox: DOMRect = parentRowRef.getBoundingClientRect();

			if ((_hoveredSide === 'right') | (_hoveredSide === 'left')) {
				//document.addEventListener('dragend', handleMouseUp);

				const mousexDelta = clientX - mouseDownPositionBeforeMoving.initialX;

				const tempWidthDelta =
					mousexDelta / (columnRectBeforeModification.width / $gridColumnStore.width);

				if (_hoveredSide === 'right') {
					if ($gridColumnStore.width + tempWidthDelta < 1) {
						return;
					}
					const newWidthValue = columnRectBeforeModification.width + mousexDelta;

					if (tempWidthDelta > 0) {
						const overlappedColumn = gridRows[gridRowIndex].columns?.find((item) => {
							return (
								item.columnStart + item.width > $gridColumnStore.columnStart &&
								$gridColumnStore.columnStart + $gridColumnStore.width + tempWidthDelta >=
									item.columnStart &&
								item !== $gridColumnStore
							);
						});

						if (overlappedColumn) {
							return;
						}
						if (columnRectBeforeModification.right + mousexDelta > parentRowBoundingBox.right) {
							columnRef.style.width =
								parentRowBoundingBox.right - columnRectBeforeModification.left + 'px';

							return;
						}
					}

					if (tempWidthDelta < 0) {
						if (
							columnRectBeforeModification.right + mousexDelta <
							columnRectBeforeModification.left
						) {
							return true;
						}
					}

					columnRef.style.width = newWidthValue + 'px';
				}
				if (_hoveredSide === 'left') {
					if (tempWidthDelta < 0) {
						const overlappedColumn = gridRows[gridRowIndex].columns?.find((item) => {
							return (
								item.columnStart + item.width <= $gridColumnStore.columnStart &&
								$gridColumnStore.columnStart + tempWidthDelta <= item.columnStart + item.width &&
								item !== $gridColumnStore
							);
						});

						if (overlappedColumn) {
							return;
						}

						if (columnRectBeforeModification.left + mousexDelta < parentRowBoundingBox.left) {
							const matrix = new DOMMatrix();

							const translateOffset = parentRowBoundingBox.left - columnRectBeforeModification.left;

							matrix.translateSelf(translateOffset, 0);

							const roundedDeltaWidth = $gridColumnStore.columnStart;

							const offsetWidthValue =
								columnRectBeforeModification.width * (roundedDeltaWidth / $gridColumnStore.width);

							//
							columnRef.style.width = columnRectBeforeModification.width + offsetWidthValue + 'px';
							columnRef.style.transform = matrix.toString();
							return;
						}
					}

					if (tempWidthDelta > 0) {
						if (
							columnRectBeforeModification.left +
								mousexDelta +
								columnRectBeforeModification.width / $gridColumnStore.width >
							columnRectBeforeModification.right
						) {
							return true;
						}
					}
					const matrix = new DOMMatrix();

					matrix.translateSelf(mousexDelta, 0);
					const offsetWidthValue = columnRectBeforeModification.width - mousexDelta;

					columnRef.style.width = offsetWidthValue + 'px';

					columnRef.style.transform = matrix.toString();
				}

				widthDelta = Math.round(
					mousexDelta / (columnRectBeforeModification.width / $gridColumnStore.width)
				);
			}

			if (_hoveredSide === 'bottom' || _hoveredSide === 'top') {
				const mouseyDelta = clientY - mouseDownPositionBeforeMoving.initialY;

				const newHeight = columnRectBeforeModification.height + mouseyDelta;

				if (_hoveredSide === 'bottom') {
					if (
						newHeight + ($gridColumnStore.styling.margin?.top || 0) >
						parentRowBoundingBox.height
					) {
						$gridColumnStore.styling.height =
							parentRowBoundingBox.height - ($gridColumnStore.styling.margin?.top || 0);
						return;
					}
					if (newHeight < 20) {
						return;
					}
					$gridColumnStore.styling.height = columnRectBeforeModification.height + mouseyDelta;

					return;
				}
				if (_hoveredSide === 'top') {
					if (clientY + 25 > columnRectBeforeModification.bottom) {
						return;
					}
					if (columnRectBeforeModification.top + mouseyDelta < parentRowBoundingBox.top) {
						const newHeight = columnRectBeforeModification.height - mouseyDelta;

						

						$gridColumnStore.styling.height = newHeight + mouseyDelta
						$gridColumnStore.styling.margin.top = 0;
						return;
					}

					$gridColumnStore.styling.margin.top =
						columnRectBeforeModification.top - parentRowBoundingBox.top + mouseyDelta;
					$gridColumnStore.styling.height = columnRectBeforeModification.height - mouseyDelta;
				}
			}
		}
	}

	function handleMouseMoveOver(e) {
		if (mouseDown) {
			return;
		}
		mouseover = true;

		const { clientX, clientY, currentTarget } = e;
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
			hoveredSide = 'left';
			return;
		}
		if (distanceRight <= threshold || clientX > left + width) {
			hoveredSide = 'right';
			return;
		}
		if (distanceTop <= threshold || clientY < top) {
			hoveredSide = 'top';
			return;
		}
		if (distanceBottom <= threshold || clientY > top + height) {
			hoveredSide = 'bottom';
			return;
		}

		hoveredSide = null;
	}

	const borderHoverCursorClass = {
		top: 'cursor-n-resize',
		right: 'cursor-e-resize',
		bottom: 'cursor-s-resize',
		left: 'cursor-w-resize'
	};

	const borderHoverClass: Record<string, string> = {
		top: 'border-t-orange-400 border-t-8',
		right: 'border-r-orange-400 border-r-8',
		bottom: 'border-b-orange-400 border-b-8',
		left: 'border-l-orange-400 border-l-8'
	};

	let columnRectBeforeModification: DOMRect;

	let mouseDownPositionBeforeMoving: { initialX: number; initialY: number };

	let widthDelta: number | null;

	let mouseDown = false;

	let mouseover = false;

	let self;
</script>


<div
	draggable="false"
	data-js-gridcolumn-controls
	class="col-span-{$gridColumnStore.width}
			col-start-{$gridColumnStore.columnStart + 1}
			select-none
			relative
			z-50
			h-fit
			bg-red-800
			"
	style="margin-top:{$gridColumnStore.styling?.margin?.top}px"
	on:mousemove={(e) => {
		if (mouseDown && componentDraggedDiscriminator) {
			return;
		}

		var rect = e.currentTarget.getBoundingClientRect();
		var mouseX = e.clientX - rect.left;
		var mouseY = e.clientY - rect.top;

		// Check if mouse coordinates are within the element boundaries
		if (true || (mouseX >= 0 && mouseX <= rect.width && mouseY >= 0 && mouseY <= rect.height)) {
			// Mouse is within the boundaries of the currentTarget element
			// Perform your desired actions here
		}
		handleMouseMoveOver(e);
		dispatch('mouseovergridcolumn');
	}}
	on:mouseleave={() => {
		mouseover = false;
		dispatch('mouseleavegridcolumn');

		if (!mouseDown) {
			hoveredSide = undefined;
		}
	}}
	on:mousedown|stopPropagation={(e) => {
		handleMouseDown(e);
	}}
>
	{#if mouseover}
		<icon
			on:mousemove|stopPropagation={() => {
				hoveredSide = undefined;
			}}
			on:click={() => {
				dispatch('setConfigurableContent', gridColumnStore);
			}}
			class="absolute flex items-center h-fit top-1/2 -translate-y-1/2 w-8 right-full"
		>
			<Icon class="w-full h-full mr-2" size={100} icon="fluent:column-triple-edit-20-filled" />
		</icon>
	{/if}
	<GridColumnContent
		visualEditor="true"
		bind:node={columnRef}
		{gridColumnStore}
		bind:styling={$gridColumnStore.styling}
		class="relative h-full
				{mouseover && 'bg-slate-300 bg-opacity-30'}
				"
	>
		{#if hoveredSide !== undefined}
			<div
				class="absolute w-full h-full z-20 pointer-events-none
				{borderHoverClass[hoveredSide]}
				"
			/>
		{/if}

		{#if mousemoveGridrow}
			<div class="absolute w-full h-full z-20 pointer-events-none border-2 border-dashed" />
		{/if}

		{#if $gridColumnStore.component}
			<BaseComponent
				{componentDraggedDiscriminator}
				gridColumn={$gridColumnStore}
				rowColumnIndex={indexOfColumnInGridRow}
				gridRows={$gridColumnStore.gridRows}
				on:setConfigurableContent
				bind:mousemoveGridrow
			/>
		{/if}
		{#if false && $gridColumnStore?.gridContent?.gridRows.length}
			<NestedGridContent
				bind:gridRows={$gridColumnStore.gridContent.gridRows}
				{componentDraggedDiscriminator}
				gridRowNestingLevel={gridRowNestingLevel + 1}
				parentGridRows={gridRows}
				parentGridRowIndex={gridRowIndex}
				parentIndexOfColumnInGridRow={$gridColumnStore.originalIndex}
			/>
		{/if}
	</GridColumnContent>
</div>
