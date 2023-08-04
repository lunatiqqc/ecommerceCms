<script lang="ts">
	import { createEventDispatcher, onMount } from 'svelte';
	import BaseComponent from './baseComponent.svelte';
	import GridColumn from './gridColumn.svelte';
	import Icon from './icon.svelte';
	import GridRowContent from './gridRowContent.svelte';
	import { writable } from 'svelte/store';
	import { getContext } from 'svelte/internal';

	export let gridRowIndex: number;
	export let gridRows: CmsClient.GridRow[];
	//export let configuredContent: { stylingConfiguration: string; content: CmsClient.GridRow };
	export let componentDraggedDiscriminator: string | undefined;
	export let gridRowNestingLevel = 0;
	export let parentGridRows;
	export let parentGridRowIndex;
	export let parentIndexOfColumnInGridRow;
	export let pageStore;
	export let gridRow: CmsClient.GridRow;

	let styleContent = getContext('styleContent');

	const dispatch = createEventDispatcher();

	const gridRowStore = writable(gridRow);

	gridRowStore.subscribe((value) => {
		console.log('gridRowStore updated', value);
	});

	console.log('pagestore from gridRow', pageStore);

	let modifiedGridColumns: Array<
		string | { originalIndex: number; content: CmsClient.GridColumn } | null
	> = [];

	$: for (let i = 0; i < 12; i++) {
		let originalIndex: number | undefined;

		const columnContentAtIndex = $gridRowStore?.columns?.find((column, j) => {
			originalIndex = j;

			return column && i >= column.columnStart && i < column.columnStart + column.width;
		});

		if (columnContentAtIndex) {
			modifiedGridColumns[i] = { originalIndex: originalIndex!, content: columnContentAtIndex };

			for (let j = 0; j < (columnContentAtIndex.width || 1) - 1; j++) {
				modifiedGridColumns[i + 1 + j] = 'skip';
			}
			i = i + columnContentAtIndex.width! - 1;
		} else {
			modifiedGridColumns[i] = null;
		}
	}

	let hoveredSideWithDraggedComponent: string | null;

	function getElementClientSide(event) {
		const { clientX, clientY, currentTarget } = event as MouseEvent & { target: HTMLElement };
		const threshold =
			15 + parseInt(window.getComputedStyle(currentTarget).getPropertyValue('border-width') || '0');
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

	function handleComponentDrag(event: MouseEvent) {
		//return

		//hoveredSideWithComponent = null;
		const elementClientSideHovered = getElementClientSide(event);
		if (elementClientSideHovered) {
			hoveredSideWithDraggedComponent = elementClientSideHovered;
			console.log(hoveredSideWithDraggedComponent);

			return;
		}
		//hoveredSideWithDraggedComponent = null;
	}

	const borderHoverCursorClass = {
		top: 'cursor-n-resize',
		right: 'cursor-e-resize',
		bottom: 'cursor-s-resize',
		left: 'cursor-w-resize'
	};

	let mouseDown;
	let mouseDragStartPosition;

	function handleDropComponent() {
		console.log('handleDropComponent', gridRowNestingLevel, hoveredSideWithDraggedComponent);

		if (componentDraggedDiscriminator) {
			let offsetNewRow = 0;

			switch (hoveredSideWithDraggedComponent) {
				case 'top':
					offsetNewRow = 0;
					break;

				case 'bottom':
					offsetNewRow = 1;
					break;

				default:
					break;
			}

			gridRows = [
				...gridRows.slice(0, gridRowIndex + offsetNewRow),

				CmsClient.GridRowFromJSONTyped(
					{
						columns: [
							CmsClient.GridColumnFromJSONTyped(
								{
									columnStart: 0,
									width: 12,
									component: CmsClient.ComponentFromJSONTyped(
										{
											discriminator: componentDraggedDiscriminator
										},
										false
									)
								},
								true
							)
						]
					},
					true
				),
				...gridRows.slice(gridRowIndex + offsetNewRow)
			];

			return;
		}
	}

	const borderHoverWithComponentClass: Record<string, string> = {
		top: 'border-t-purple-400 border-t-8',
		bottom: 'border-b-purple-400 border-b-8'
	};
	const borderHoveredSideClass: Record<string, string> = {
		//top: 'border-t-green-400 border-t-8',
		bottom: 'border-b-green-400 border-b-8'
	};

	let showGridOverlay: boolean = false;

	function handleSortingGridRow(draggedGridRowIndex) {
		[$gridRowStore, gridRows[draggedGridRowIndex]] = [gridRows[draggedGridRowIndex], $gridRowStore];
	}
	async function handleDeleteGridRow(draggedGridRowIndex, gridRowId) {
		gridRows = [
			...gridRows.slice(0, draggedGridRowIndex),
			...gridRows.slice(draggedGridRowIndex + 1)
		];
		const pagesClient = new CmsClient.PagesClient();
		await pagesClient.deleteGridRows({ requestBody: [gridRowId] });
	}

	let self;
	let self2;

	$: adjacentParentGridRowColumnPresentLeft =
		parentGridRows?.[parentGridRowIndex]?.columns[parentIndexOfColumnInGridRow - 1]?.gridContent
			?.gridRows?.[gridRowIndex];
	$: adjacentParentGridRowColumnPresentRight =
		parentGridRows?.[parentGridRowIndex]?.columns[parentIndexOfColumnInGridRow + 1]?.gridContent
			?.gridRows?.[gridRowIndex];
	$: adjacentParentGridRowColumnPresentBottom = parentGridRows?.[parentGridRowIndex + 1];
	$: adjacentParentGridRowColumnPresentTop = parentGridRows?.[parentGridRowIndex - 1];
	//{adjacentParentGridRowColumnPresentLeft && 'ml-0 pl-0'}
	//{adjacentParentGridRowColumnPresentRight && 'mr-0 pr-0'}
	//{adjacentParentGridRowColumnPresentBottom && 'mb-0 pb-0'}
	//{adjacentParentGridRowColumnPresentTop && 'mt-0 pt-0'}
	//{gridRows.length > 1 && gridRowIndex === 0 && 'mb-0 pb-0'}
	//{gridRowIndex > 0 && 'mt-0 pt-0'}

	let draggingOver = false;
	let dragging = false;

	onMount(() => {});

	let rowRef: HTMLElement;

	let mouseOverDraggableIcon = false;

	function handleAddGridColumn({ detail }) {
		const { columnStart, componentDiscriminator } = detail;

		//const column = CmsClient.GridColumnFromJSON({
		//	width: 1,
		//	columnStart: columnStart,
		//	component: CmsClient.ComponentFromJSON({ discriminator: componentDiscriminator })
		//});

		const column = CmsClient.GridColumnFromJSON({
			component: CmsClient.ComponentFromJSON({ discriminator: componentDiscriminator }),
			columnStart: columnStart,
			width: 1
		});
		$gridRowStore.columns = [...$gridRowStore.columns, column];
	}

	let hoveredSide;

	function handleMouseDown(e) {
		if (!$gridRowStore.styling) {
			$gridRowStore.styling = CmsClient.ContainerStylingFromJSON({});
		}

		const _hoveredSide = hoveredSide;

		const gridRowRect = rowRef.getBoundingClientRect();
		const { clientX, clientY, currentTarget } = e;
		let mouseDownPositionBeforeMoving = { initialX: clientX, initialY: clientY };

		function handleMouseMoveWhilePressed(e) {
			const { clientX, clientY, currentTarget } = e;

			if (_hoveredSide === 'bottom') {
				const mouseyDelta = clientY - mouseDownPositionBeforeMoving.initialY;

				const newHeight = gridRowRect.height + mouseyDelta;

				console.log(e.target);

				$gridRowStore.styling.height = newHeight;
			}
		}
		function handleMouseUp() {
			document.removeEventListener('mousemove', handleMouseMoveWhilePressed);
			document.removeEventListener('mouseup', handleMouseUp);
		}
		document.addEventListener('mousemove', handleMouseMoveWhilePressed);
		document.addEventListener('mouseup', handleMouseUp);
	}

	let mousemove = false;
	let mousemoveGridColumn = false;
</script>

{#if $gridRowStore}
	<!-- svelte-ignore a11y-mouse-events-have-key-events -->
	<!-- svelte-ignore a11y-click-events-have-key-events -->
	<div
		on:mousedown={(e) => {
			handleMouseDown(e);
		}}
		bind:this={self2}
		draggable={mouseOverDraggableIcon}
		data-grid-row-index={gridRowIndex}
		class="gridrow__controlswrapper grid col-span-12 grid-cols-12 h-fit relative
	mb-0 pb-0
	mt-0 pt-0 select-none

	"
		style={componentDraggedDiscriminator && `z-index:${gridRowIndex}`}
		on:dragenter={(e) => {
			console.log('dragover');

			//draggingOver = true;
			if (componentDraggedDiscriminator) {
				//hoveredSideWithDraggedComponent = null;
				handleComponentDrag(e, gridRowIndex);
			}
		}}
		on:dragleave={() => {
			console.log('dragLeave');

			//draggingOver = false;
			if (componentDraggedDiscriminator) {
				hoveredSideWithDraggedComponent = null;
			}
		}}
		on:dragstart|self={(e) => {
			dragging = true;

			e.preventDefault();

			e.dataTransfer?.setData('draggedGridRowIndex', gridRowIndex);
			e.dataTransfer?.setData('gridRowId', $gridRowStore.id);
		}}
		on:dragend={() => {
			dragging = false;
		}}
		on:drag={(e) => {}}
		on:drop|stopPropagation={(e) => {
			draggingOver = false;
			const draggedGridRowIndexData = e.dataTransfer?.getData('draggedGridRowIndex');
			if (draggedGridRowIndexData) {
				const draggedGridRowIndex = parseInt(draggedGridRowIndexData);
				handleSortingGridRow(draggedGridRowIndex);
				return;
			}
			handleDropComponent(e);
			hoveredSideWithDraggedComponent = null;
		}}
		on:contextmenu={(e) => {}}
		on:mousemove={(e) => {
			mousemove = true;
			hoveredSide = getElementClientSide(e);
		}}
		on:mouseleave={(event) => {
			mousemove = false;
			hoveredSide = null;
		}}
		on:click={(e) => {
			console.log(e);

			if (mousemoveGridColumn) {
				return;
			}
			//if (!$gridRowStore.styling) {
			//	$gridRowStore.styling = CmsClient.ContainerStylingFromJSON({});
			//}

			//console.log('self click');
			//
			//function callback(newContent) {
			//	$gridRowStore = newContent;
			//}
			//

			console.log('gridRowBeforeDispatch', $gridRowStore);

			styleContent.set($gridRowStore);

			//dispatch('setConfigurableContent', gridRowStore);
			//configuredContent =
		}}
	>
		{#if dragging}
			<icon
				class="absolute right-full self-center pr-8"
				on:drop={(e) => {
					const draggedGridRowIndexData = e.dataTransfer?.getData('draggedGridRowIndex');
					const draggedGridRowId = e.dataTransfer?.getData('gridRowId');
					if (draggedGridRowIndexData) {
						const draggedGridRowIndex = parseInt(draggedGridRowIndexData);
						handleDeleteGridRow(draggedGridRowIndex, draggedGridRowId);
						return;
					}
				}}
			>
				<Icon icon="carbon:trash-can" width={32} />
			</icon>
		{/if}
		{#if hoveredSide}
			<icon class="absolute flex items-center h-full w-5 z-20 right-full">
				<Icon
					width={10}
					viewBox={{ left: 10, top: 6, width: 12, height: 20 }}
					icon="carbon:draggable"
				/>
			</icon>
		{/if}
		<GridRowContent
			on:mouseovergridcolumn={() => {
				mouseover = false;
			}}
			class="grid col-span-12 grid-cols-12 relative min-h-[50px] border-green-950 z-0"
			bind:node={rowRef}
			styling={$gridRowStore.styling}
		>
			{#if hoveredSideWithDraggedComponent}
				<div
					class="h-full w-full absolute pointer-events-none
				z-50
				{borderHoverWithComponentClass[hoveredSideWithDraggedComponent]}"
				/>
			{/if}

			{#if hoveredSide}
				<div
					class="h-full w-full absolute
				z-10
				{borderHoveredSideClass[hoveredSide]}"
				/>
			{/if}
			{#if mousemove && !mousemoveGridColumn}
				<div
					class="h-full w-full absolute
				z-10
				border-dashed border-2 border-slate-800"
				/>
			{/if}
			{#if false && showGridOverlay}
				<div class="grid grid-cols-12 absolute z-10 w-full h-full">
					{#each new Array(12) as undefined, j}
						<div class="border-2 bg-slate-700 border-black" />
					{/each}
				</div>
			{/if}
			{#each modifiedGridColumns as modifiedGridColumn, j}
				<GridColumn
					bind:this={self2}
					bind:showGridOverlay
					bind:gridRows
					{gridRowIndex}
					bind:modifiedGridColumn
					indexOfColumnInGridRow={j}
					{componentDraggedDiscriminator}
					{gridRowNestingLevel}
					parentRowRef={rowRef}
					on:gridColumnAdded={handleAddGridColumn}
					on:mouseovergridcolumn={(e) => {
						mousemoveGridColumn = true;
					}}
					on:mouseleavegridcolumn={(e) => {
						mousemoveGridColumn = false;
					}}
					on:setConfigurableContent
				/>
			{/each}
		</GridRowContent>
		{#if hoveredSide}
			<icon class="right-0 w-5 absolute flex items-center h-full z-20 left-full">
				<Icon
					width={10}
					viewBox={{ left: 10, top: 6, width: 12, height: 20 }}
					icon="carbon:draggable"
				/>
			</icon>
		{/if}
		{#if hoveredSideWithDraggedComponent}
			<div class="h-24 w-full col-span-full bg-teal-900" />
		{/if}
	</div>
{/if}
