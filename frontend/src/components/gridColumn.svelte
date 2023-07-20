<script lang="ts">
	import { createEventDispatcher } from 'svelte';
	import BaseComponent from './baseComponent.svelte';
	import Icon from './icon.svelte';
	import NestedGridContent from './nestedGridContent.svelte';
	import GridColumnContent from './gridColumnContent.svelte';

	export let modifiedGridColumn:
		| string
		| { originalIndex: number; content: CmsClient.GridColumn }
		| null;
	export let indexOfColumnInGridRow;
	export let componentDraggedDiscriminator;
	export let gridRows: [CmsClient.GridRow];
	export let gridRowIndex: number;
	export let showGridOverlay: boolean;
	export let gridRowNestingLevel;
	export let parentGridRows;
	export let parentIndexOfColumnInGridRow;
	export let parentRowRef;

	$: console.log(modifiedGridColumn);

	const dispatch = createEventDispatcher();

	function handleComponentDrop(indexOfColumnInGridRow) {
		console.log('handleComponentDrop', indexOfColumnInGridRow);

		if (!componentDraggedDiscriminator) {
			return;
		}

		dispatch('gridColumnAdded', {
			columnStart: indexOfColumnInGridRow,
			componentDiscriminator: componentDraggedDiscriminator
		});

		return;

		if (gridRowNestingLevel === 1) {
			const column = CmsClient.GridColumnFromJSON({
				width: 1,
				columnStart: indexOfColumnInGridRow,
				component: CmsClient.ComponentFromJSONTyped(
					{ discriminator: componentDraggedDiscriminator },
					false
				)
			});
			modifiedGridColumn.columns = [...gridRows[gridRowIndex].columns, column];
		}
		if (gridRowNestingLevel === 0) {
			const column = CmsClient.GridColumnFromJSON({
				width: 1,
				columnStart: indexOfColumnInGridRow,
				gridContent: CmsClient.GridContentFromJSONTyped(
					{
						gridRows: [
							CmsClient.GridRowFromJSONTyped(
								{
									columns: [
										CmsClient.GridColumnFromJSONTyped(
											{
												component: CmsClient.ComponentFromJSONTyped(
													{ discriminator: componentDraggedDiscriminator },
													false
												),
												columnStart: 0,
												width: 12
											},
											true
										)
									]
								},
								true
							)
						]
					},
					true
				)
			});
			gridRows[gridRowIndex].columns = [...gridRows[gridRowIndex].columns, column];
		}
	}

	let hoveredSide;

	function handleMouseDown(e) {
		mouseDown = true;

		if (!modifiedGridColumn.content.styling) {
			modifiedGridColumn.content.styling = CmsClient.ContainerStylingFromJSON({});
		}
		if (!modifiedGridColumn.content.styling.margin) {
			modifiedGridColumn.content.styling.margin = CmsClient.MarginFromJSON({});
		}

		const { clientX, clientY, currentTarget } = e;
		if (hoveredSide) {
			mouseDownPositionBeforeMoving = { initialX: clientX, initialY: clientY };
			columnRectBeforeModification = columnRef.getBoundingClientRect();
			document.addEventListener('mousemove', handleDocumentMouseMoveWhilePressed);

			showGridOverlay = true;
		}
		document.addEventListener('mouseup', handleMouseUp);

		const _hoveredSide = hoveredSide;

		function handleMouseUp() {
			mouseDown = false;
			if (widthDelta !== null && widthDelta !== undefined) {
				if (hoveredSide === 'right') {
					modifiedGridColumn.content.width += widthDelta;
				}

				if (hoveredSide === 'left') {
					modifiedGridColumn.content.columnStart += widthDelta;
					modifiedGridColumn.content.width -= widthDelta;
					columnRef.style.removeProperty('transform');
				}

				columnRef.style.removeProperty('width');
			}
			if (hoveredSide === 'bottom') {
			}
			showGridOverlay = false;
			hoveredSide = null;
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
					mousexDelta / (columnRectBeforeModification.width / modifiedGridColumn.content.width);

				if (_hoveredSide === 'right') {
					if (modifiedGridColumn.content.width + tempWidthDelta < 1) {
						return;
					}
					const newWidthValue = columnRectBeforeModification.width + mousexDelta;

					if (tempWidthDelta > 0) {
						const overlappedColumn = gridRows[gridRowIndex].columns?.find((item) => {
							return (
								item.columnStart + item.width > modifiedGridColumn.content.columnStart &&
								modifiedGridColumn.content.columnStart +
									modifiedGridColumn.content.width +
									tempWidthDelta >=
									item.columnStart &&
								item !== modifiedGridColumn.content
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
								item.columnStart + item.width <= modifiedGridColumn.content.columnStart &&
								modifiedGridColumn.content.columnStart + tempWidthDelta <=
									item.columnStart + item.width &&
								item !== modifiedGridColumn.content
							);
						});

						if (overlappedColumn) {
							return;
						}

						if (columnRectBeforeModification.left + mousexDelta < parentRowBoundingBox.left) {
							const matrix = new DOMMatrix();

							const translateOffset = parentRowBoundingBox.left - columnRectBeforeModification.left;

							matrix.translateSelf(translateOffset, 0);

							const roundedDeltaWidth = modifiedGridColumn.content.columnStart;

							const offsetWidthValue =
								columnRectBeforeModification.width *
								(roundedDeltaWidth / modifiedGridColumn.content.width);

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
								columnRectBeforeModification.width / modifiedGridColumn.content.width >
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
					mousexDelta / (columnRectBeforeModification.width / modifiedGridColumn.content.width)
				);
			}

			if (_hoveredSide === 'bottom' || _hoveredSide === 'top') {
				const mouseyDelta = clientY - mouseDownPositionBeforeMoving.initialY;

				const newHeight = columnRectBeforeModification.height + mouseyDelta;

				console.log('columnRectBeforeModification', columnRectBeforeModification);

				if (_hoveredSide === 'bottom') {
					if (
						newHeight + (modifiedGridColumn.content.styling.margin.top || 0) >
						parentRowBoundingBox.height
					) {
						modifiedGridColumn.content.styling.height =
							parentRowBoundingBox.height - (modifiedGridColumn.content.styling.margin.top || 0);
						return;
					}
					if (newHeight < 20) {
						return;
					}
					modifiedGridColumn.content.styling.height =
						columnRectBeforeModification.height + mouseyDelta;

					return;
				}
				if (_hoveredSide === 'top') {
					if (clientY + 25 > columnRectBeforeModification.bottom) {
						return;
					}
					if (columnRectBeforeModification.top + mouseyDelta < columnRectBeforeModification.top) {
						modifiedGridColumn.content.styling.height =
							columnRectBeforeModification.height + modifiedGridColumn.content.styling.margin.top ||
							0;
						modifiedGridColumn.content.styling.margin.top = null;
						return;
					}

					console.log(
						'parentRowBoundingBox',
						parentRowBoundingBox,
						'columnRectBeforeModification',
						columnRectBeforeModification
					);

					modifiedGridColumn.content.styling.margin.top =
						columnRectBeforeModification.top - parentRowBoundingBox.top + mouseyDelta;
					modifiedGridColumn.content.styling.height =
						columnRectBeforeModification.height - mouseyDelta;
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

	let columnRef: HTMLElement;

	let widthDelta: number | null;

	let mouseDown = false;

	let mouseover = false;

	let self;
</script>

<!-- svelte-ignore a11y-mouse-events-have-key-events -->
{#if modifiedGridColumn !== null}
	{#if typeof modifiedGridColumn == 'object'}
		<div
			draggable="false"
			data-js-gridcolumn-controls
			class="col-span-{modifiedGridColumn.content.width}
			col-start-{modifiedGridColumn.content.columnStart + 1}
			relative select-none
			"
		>
			<GridColumnContent
				bind:node={columnRef}
				bind:styling={modifiedGridColumn.content.styling}
				class="relative h-full z-30 overflow-hidden"
			>
				<div
					on:mousedown|stopPropagation={(e) => {
						handleMouseDown(e);
					}}
					on:mousemove={(e) => {
						if (mouseDown && componentDraggedDiscriminator) {
							return;
						}

						var rect = e.currentTarget.getBoundingClientRect();
						var mouseX = e.clientX - rect.left;
						var mouseY = e.clientY - rect.top;

						// Check if mouse coordinates are within the element boundaries
						if (mouseX >= 0 && mouseX <= rect.width && mouseY >= 0 && mouseY <= rect.height) {
							// Mouse is within the boundaries of the currentTarget element
							// Perform your desired actions here
							handleMouseMoveOver(e);
						}
					}}
					on:mouseout={() => {
						mouseover = false;
						if (!mouseDown) {
							hoveredSide = null;
						}
					}}
					on:drag|preventDefault
					class="absolute w-full h-full border-2 border-black z-10
					{borderHoverClass[hoveredSide]}
					"
				/>
				{#if modifiedGridColumn.content.component}
					<BaseComponent
						{componentDraggedDiscriminator}
						{modifiedGridColumn}
						rowColumnIndex={indexOfColumnInGridRow}
						gridRows={modifiedGridColumn.content.gridRows}
					/>
				{/if}
				{#if false && modifiedGridColumn.content?.gridContent?.gridRows.length}
					<NestedGridContent
						bind:gridRows={modifiedGridColumn.content.gridContent.gridRows}
						{componentDraggedDiscriminator}
						gridRowNestingLevel={gridRowNestingLevel + 1}
						parentGridRows={gridRows}
						parentGridRowIndex={gridRowIndex}
						parentIndexOfColumnInGridRow={modifiedGridColumn.originalIndex}
					/>
				{/if}
			</GridColumnContent>
		</div>
	{/if}
{:else}
	<div
		data-grid-column-index={indexOfColumnInGridRow}
		class="relative col-start-{indexOfColumnInGridRow + 1}"
		on:dragover|stopPropagation={(e) => {
			if (componentDraggedDiscriminator) {
				e.preventDefault();
			}
		}}
		on:drop|stopPropagation={() => {
			handleComponentDrop(indexOfColumnInGridRow);
		}}
	>
		<div class="absolute inset-0 w-full h-full outline-1 outline" />
	</div>
{/if}
