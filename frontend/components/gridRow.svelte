<script lang="ts">
	import { onMount } from 'svelte';
	import BaseComponent from './baseComponent.svelte';
	import GridColumn from './gridColumn.svelte';
	import Icon from './icon.svelte';

	export let gridRowIndex: number;
	export let gridRows: [CmsClient.GridRow];
	export let componentDraggedDiscriminator: string | undefined;
	export let gridRowNestingLevel = 0;
	export let parentGridRows;
	export let parentGridRowIndex;
	export let parentIndexOfColumnInGridRow;

	let modifiedGridColumns: Array<
		string | { originalIndex: number; content: CmsClient.GridColumn } | null
	> = [];

	$: for (let i = 0; i < 12; i++) {
		let originalIndex: number | undefined;

		const columnContentAtIndex = gridRows[gridRowIndex]?.columns?.find((column, j) => {
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

	//$: {
	//	console.log('gridRow', gridRow);
	//	console.log('modifiedGridColumns', modifiedGridColumns);
	//}

	let hoveredSideWithDraggedComponent: string | null;

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
			//console.log('hovered left side');
		}
		if (distanceRight <= threshold || clientX > left + width) {
			return 'right';
			//console.log('hovered right side');
		}
		if (distanceTop <= threshold || clientY < top) {
			return 'top';
		}
		if (distanceBottom <= threshold || clientY > top + height) {
			return 'bottom';
			//console.log('hovered bottom side');
		}
	}

	function handleComponentDrag(event: MouseEvent) {
		//return

		//hoveredSideWithComponent = null;
		const elementClientSideHovered = getElementClientSide(event);
		if (elementClientSideHovered) {
			hoveredSideWithDraggedComponent = elementClientSideHovered;
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

			if (gridRowNestingLevel === 0) {
				gridRows = [
					...gridRows.slice(0, gridRowIndex + offsetNewRow),
					CmsClient.GridRowFromJSONTyped(
						{
							columns: [
								CmsClient.GridColumnFromJSONTyped(
									{
										columnStart: 0,
										width: 12,
										gridRows: [
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
											)
										]
									},
									true
								)
							]
						},
						true
					),
					...gridRows.slice(gridRowIndex + offsetNewRow)
				];
			}

			console.log('gridRowNestingLevel', gridRowNestingLevel);

			if (gridRowNestingLevel === 1) {
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
			}
			return;
		}

		console.log('handleDropComponentafter', gridRows);
	}

	const borderHoverWithComponentClass: Record<string, string> = {
		top: 'border-t-orange-400 border-t-8',
		right: 'border-r-orange-400 border-r-8',
		bottom: 'border-b-orange-400 border-b-8',
		left: 'border-l-orange-400 border-l-8'
	};

	let showGridOverlay: boolean = false;

	let mouseover = false;

	function handleSortingGridRow(draggedGridRowIndex) {
		[gridRows[gridRowIndex], gridRows[draggedGridRowIndex]] = [
			gridRows[draggedGridRowIndex],
			gridRows[gridRowIndex]
		];
	}

	let self;
	let self2;

	$: adjacentParentGridRowColumnPresentLeft =
		parentGridRows?.[parentGridRowIndex]?.columns[parentIndexOfColumnInGridRow - 1]?.gridRows?.[
			gridRowIndex
		];
	$: adjacentParentGridRowColumnPresentRight =
		parentGridRows?.[parentGridRowIndex]?.columns[parentIndexOfColumnInGridRow + 1]?.gridRows?.[
			gridRowIndex
		];
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

	onMount(() => {
		console.log(self2);
	});

	let rowRef;
</script>

<!-- svelte-ignore a11y-mouse-events-have-key-events -->
<div
	bind:this={self2}
	draggable="true"
	data-grid-row-index={gridRowIndex}
	class="gridrow__controlswrapper grid col-span-12 grid-cols-12 h-fit relative
	border-black border-opacity-25
	bg-red-600 bg-opacity-0
	mb-0 pb-0
	mt-0 pt-0
	{adjacentParentGridRowColumnPresentLeft && 'ml-0 pl-0'}
	{adjacentParentGridRowColumnPresentRight && 'mr-0 pr-0'}
	{adjacentParentGridRowColumnPresentBottom || (true && '')}
	{adjacentParentGridRowColumnPresentTop || (true && 'mt-0 pt-0')}
	
	{dragging && 'z-0'}
	
	{gridRowNestingLevel === 0 && '-m-14 p-14'}
	{gridRowNestingLevel > 0 && '-m-10 p-10'}
	{mouseover && (gridRowNestingLevel === 0 ? 'group/one' : 'group/two')}
	"
	style={componentDraggedDiscriminator && `z-index:${gridRowIndex}`}
	on:dragover|preventDefault|stopPropagation={(e) => {
		draggingOver = true;
		if (componentDraggedDiscriminator) {
			console.log('dragging deez');

			//hoveredSideWithDraggedComponent = null;
			handleComponentDrag(e, gridRowIndex);
		}
	}}
	on:dragleave={() => {
		dragging = false;
		draggingOver = false;
		if (componentDraggedDiscriminator) {
			hoveredSideWithDraggedComponent = null;
		}
	}}
	on:dragstart|self={(e) => {
		dragging = true;
		if (!mouseover) {
			e.preventDefault();
		}
		e.dataTransfer?.setData('draggedGridRowIndex', gridRowIndex);
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
>
	{#if true}
		<icon
			on:mousemove={(e) => {
				mouseover = true;
			}}
			on:mouseout={(event) => {
				mouseover = false;
				//console.log('mouseover gridrow leave');
			}}
			class="absolute flex items-center h-full w-5 z-30"
		>
			<Icon
				width={10}
				viewBox={{ left: 10, top: 6, width: 12, height: 20 }}
				icon="carbon:draggable"
			/>
		</icon>
	{/if}
	<section
		class="grid col-span-12 grid-cols-12 relative min-h-[50px] border-green-950"
		bind:this={rowRef}
	>
		{#if mouseover || hoveredSideWithDraggedComponent || draggingOver}
			<div
				class="h-full w-full
				border-4 border-red-800 absolute
				z-20 pointer-events-none
				{hoveredSideWithDraggedComponent && borderHoverWithComponentClass[hoveredSideWithDraggedComponent]}"
			/>
		{/if}
		<div class="absolute">{gridRowNestingLevel}</div>
		{#if showGridOverlay}
			<div class="grid grid-cols-12 absolute z-10 w-full h-full opacity-30">
				{#each new Array(12) as undefined, j}
					<div class="border-2 border-black" />
				{/each}
			</div>
		{/if}
		{#each modifiedGridColumns as modifiedGridColumn, j}
			<GridColumn
				bind:this={self2}
				bind:showGridOverlay
				bind:gridRows
				{gridRowIndex}
				{modifiedGridColumn}
				indexOfColumnInGridRow={j}
				{componentDraggedDiscriminator}
				{gridRowNestingLevel}
				parentRowRef={rowRef}
			/>
		{/each}
	</section>
	{#if true}
		<icon class="right-0 w-5 absolute flex items-center h-full z-10">
			<Icon
				width={10}
				viewBox={{ left: 10, top: 6, width: 12, height: 20 }}
				class=""
				icon="carbon:draggable"
			/>
		</icon>
	{/if}
</div>
