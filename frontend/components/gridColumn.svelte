<script lang="ts">
	import BaseComponent from './baseComponent.svelte';
	import Icon from './icon.svelte';
	import NestedGridContent from './nestedGridContent.svelte';

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

	function handleComponentDrop(indexOfColumnInGridRow) {
		console.log('handleComponentDrop');

		if (!componentDraggedDiscriminator) {
			return;
		}
		const column = CmsClient.GridColumnFromJSON({
			width: 1,
			columnStart: indexOfColumnInGridRow,
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
		});

		console.log('gridRowski', gridRows[gridRowIndex]);

		gridRows[gridRowIndex].columns = [...gridRows[gridRowIndex].columns, column];
		console.log('gridRowskiafter', gridRows[gridRowIndex]);
	}

	let hoveredSide;

	function handleMouseUp() {
		mouseDown = false;
		if (widthDelta !== null) {
			console.log('widthDelta', widthDelta);

			//console.log(newWidth);
			//
			//console.log(gridRow.columns[modifiedGridColumn.originalIndex]);

			if (hoveredSide === 'right') {
				gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].width += widthDelta;
			}

			if (hoveredSide === 'left') {
				console.log('left', gridRows[gridRowIndex]);

				gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].columnStart += widthDelta;
				gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].width -= widthDelta;
				columnRef.style.removeProperty('transform');
			}

			columnRef.style.removeProperty('width');
			//onsole.log('modifiedGridColumn2', modifiedGridColumn, gridRow.columns[0].width);
		}
		showGridOverlay = false;
		hoveredSide = null;
		document.removeEventListener('mousemove', handleDocumentMouseMoveWhilePressed);
		document.removeEventListener('mouseup', handleMouseUp);
	}

	function handleDocumentMouseMoveWhilePressed(e) {
		const { clientX, clientY, target } = e;

		//document.addEventListener('dragend', handleMouseUp);

		const mousexDelta = clientX - mouseDownPositionBeforeMoving.initialX;

		const tempWidthDelta =
			mousexDelta / (columnRectBeforeModification.width / modifiedGridColumn.content.width);

		const parentRowBoundingBox: DOMRect = parentRowRef.getBoundingClientRect();

		if (hoveredSide === 'right') {
			if (modifiedGridColumn.content.width + tempWidthDelta < 1) {
				return;
			}
			const newWidthValue = columnRectBeforeModification.width + mousexDelta;

			console.log(mousexDelta, columnRectBeforeModification.right, parentRowBoundingBox);
			if (tempWidthDelta > 0) {
				const overlappedColumn = gridRows[gridRowIndex].columns?.find((item) => {
					return (
						item.columnStart + item.width >
							gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].columnStart &&
						gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].columnStart +
							gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].width +
							tempWidthDelta >=
							item.columnStart &&
						item !== gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex]
					);
				});

				if (overlappedColumn) {
					return;
				}
				if (columnRectBeforeModification.right + mousexDelta > parentRowBoundingBox.right) {
					columnRef!.style.width =
						parentRowBoundingBox.right - columnRectBeforeModification.left + 'px';

					return;
				}
			}

			if (tempWidthDelta < 0) {
				if (columnRectBeforeModification.right + mousexDelta < columnRectBeforeModification.left) {
					return true;
				}
			}

			columnRef!.style.width = newWidthValue + 'px';
		}
		if (hoveredSide === 'left') {
			console.log(tempWidthDelta);

			if (tempWidthDelta < 0) {
				const overlappedColumn = gridRows[gridRowIndex].columns?.find((item) => {
					return (
						item.columnStart + item.width <
							gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].columnStart &&
						gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].columnStart +
							tempWidthDelta <=
							item.columnStart + item.width &&
						item !== gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex]
					);
				});

				console.log(columnRectBeforeModification, mousexDelta, parentRowBoundingBox.left);
				if (overlappedColumn) {
					return;
				}

				if (columnRectBeforeModification.left + mousexDelta < parentRowBoundingBox.left) {
					const matrix = new DOMMatrix();

					const translateOffset = parentRowBoundingBox.left - columnRectBeforeModification.left;

					matrix.translateSelf(translateOffset, 0);

					const roundedDeltaWidth =
						gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].columnStart;

					const offsetWidthValue =
						columnRectBeforeModification.width *
						(roundedDeltaWidth /
							gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex].width);

					console.log('offsetWidthValue', offsetWidthValue);

					//
					columnRef.style.width = columnRectBeforeModification.width + offsetWidthValue + 'px';
					columnRef!.style.transform = matrix.toString();
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

			columnRef!.style.width = offsetWidthValue + 'px';

			columnRef!.style.transform = matrix.toString();
		}

		widthDelta = Math.round(
			mousexDelta / (columnRectBeforeModification.width / modifiedGridColumn.content.width)
		);
	}

	function handleMouseDown(e) {
		mouseDown = true;

		console.log('mousedown');

		const { clientX, clientY, currentTarget } = e;
		if (hoveredSide) {
			mouseDownPositionBeforeMoving = { initialX: clientX, initialY: clientY };
			columnRectBeforeModification = columnRef.getBoundingClientRect();
			document.addEventListener('mousemove', handleDocumentMouseMoveWhilePressed);

			document.addEventListener('mouseup', handleMouseUp);
			showGridOverlay = true;
		} else {
			mouseDown = false;
		}
	}

	function handleMouseMoveOver(e) {
		//console.log('hoveredSide', hoveredSide, e.currentTarget);

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
			//console.log('hovered left side column');
		}
		if (distanceRight <= threshold || clientX > left + width) {
			hoveredSide = 'right';
			return;
			//console.log('hovered right side column');
		}
		if (distanceTop <= threshold || clientY < top) {
			hoveredSide = 'top';
			return;

			//console.log('hovered top side column');
		}
		if (distanceBottom <= threshold || clientY > top + height) {
			hoveredSide = 'bottom';
			return;

			//console.log('hovered bottom side column');
		}
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
			data-js-gridcolumn-controls
			on:mousedown|stopPropagation={(e) => {
				handleMouseDown(e);
			}}
			on:mousemove={(e) => {
				if (mouseDown) {
					return;
				}
				//console.log('mousemove', e.currentTarget);

				//console.log('mouseEnter', e.target);

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
			class="{gridRowNestingLevel === 0 && 'grid'}
		{gridRowNestingLevel === 1 && 'z-10'}

			
			col-span-{modifiedGridColumn.content.width}
			col-start-{modifiedGridColumn.content.columnStart + 1}
			{gridRowNestingLevel === 0 && '-m-4 p-4'}
			relative

			{mouseover && 'z-30'}
			"
		>
			<div class="absolute">{hoveredSide}</div>

			<article
				bind:this={columnRef}
				class="relative"
				data-gridcolumn-nested={gridRowNestingLevel > 0}
			>
				{#if hoveredSide}
					<div
						class="opacity-100 pointer-events-none absolute w-full h-full z-20 border-2 border-green-950
						{borderHoverClass[hoveredSide]}
						{gridRowNestingLevel > 0 && 'outline outline-2 -outline-offset-8'}
						"
					/>
				{/if}
				{#if modifiedGridColumn.content.component}
					<BaseComponent
						{componentDraggedDiscriminator}
						{modifiedGridColumn}
						rowColumnIndex={indexOfColumnInGridRow}
						gridRows={modifiedGridColumn.content.gridRows}
					/>
				{/if}
				{#if modifiedGridColumn.content?.gridRows?.length}
					<NestedGridContent
						bind:gridRows={gridRows[gridRowIndex].columns[modifiedGridColumn.originalIndex]
							.gridRows}
						{componentDraggedDiscriminator}
						gridRowNestingLevel={gridRowNestingLevel + 1}
						parentGridRows={gridRows}
						parentGridRowIndex={gridRowIndex}
						parentIndexOfColumnInGridRow={modifiedGridColumn.originalIndex}
					/>
				{/if}
			</article>
		</div>
	{/if}
{:else}
	<div
		data-grid-column-index={indexOfColumnInGridRow}
		class="border-2 bg-green-950 col-start-{indexOfColumnInGridRow + 1}"
		on:dragover|stopPropagation={(e) => {
			if (componentDraggedDiscriminator) {
				e.preventDefault();
			}
		}}
		on:drop|stopPropagation={() => {
			handleComponentDrop(indexOfColumnInGridRow);
		}}
	/>
{/if}
