<script lang="ts">
	import { createEventDispatcher, onMount } from 'svelte';
	import BaseComponent from '../baseComponent.svelte';
	import GridColumn from './gridColumn.svelte';
	import Icon from '../icon.svelte';
	import GridRowContent from '../gridRowContent.svelte';
	import { writable } from 'svelte/store';
	import { getContext } from 'svelte/internal';
	import GridColumnEmpty from './gridColumnEmpty.svelte';

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

	gridRowStore.subscribe((value) => {});

	let modifiedGridColumns: {
		originalIndex?: number;
		content?: CmsClient.GridColumn;
		skip: boolean;
	}[] = [];

	$: for (let i = 0; i < 12; i++) {
		let originalIndex: number | undefined;

		const columnContentAtIndex = $gridRowStore?.columns?.find((column, j) => {
			originalIndex = j;

			return column && i >= column.columnStart && i < column.columnStart + column.width;
		});

		if (columnContentAtIndex) {
			modifiedGridColumns[i] = {
				originalIndex: originalIndex,
				content: columnContentAtIndex,
				skip: false,
				key: i
			};

			for (let j = 0; j < (columnContentAtIndex.width || 1) - 1; j++) {
				modifiedGridColumns[i + 1 + j] = { key: i + 1, skip: true };
			}
			i = i + columnContentAtIndex.width - 1;
		} else {
			modifiedGridColumns[i] = { key: i, skip: false };
		}
	}

	$: console.log('modifiedGridColumns', modifiedGridColumns);

	let isHoveredWithDraggedComponent: string | null;

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

	function handleComponentDragOver(event: MouseEvent) {
		const elementClientSideHovered = getElementClientSide(event);

		console.log(elementClientSideHovered);

		if (elementClientSideHovered === 'top') {
			isHoveredWithDraggedComponent = true;
		}
	}

	const borderHoverCursorClass = {
		top: 'cursor-n-resize',
		right: 'cursor-e-resize',
		bottom: 'cursor-s-resize',
		left: 'cursor-w-resize'
	};

	let mouseDown;
	let mouseDragStartPosition;

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
	let columnRefs = [];

	$: columnRefsFiltered = columnRefs.filter((item) => item);

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

				if (newHeight < $gridRowStore.styling.height) {
					let overlappingNestedColumn;

					columnRefsFiltered.forEach((el) => {
						const nestedColumnRect = el.getBoundingClientRect();

						if (gridRowRect.top + newHeight < nestedColumnRect.bottom) {
							overlappingNestedColumn = nestedColumnRect.bottom;
						}
					});
					console.log(overlappingNestedColumn, newHeight);

					if (overlappingNestedColumn !== undefined) {
						$gridRowStore.styling.height = overlappingNestedColumn - gridRowRect.top;
						return;
					}
				}
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

	$: unique = {};
</script>

{#if $gridRowStore}
	<!-- svelte-ignore a11y-mouse-events-have-key-events -->
	<!-- svelte-ignore a11y-click-events-have-key-events -->
	<div
		on:mousedown={(e) => {
			handleMouseDown(e);
		}}
		draggable={mouseOverDraggableIcon}
		data-grid-row-index={gridRowIndex}
		class="gridrow__controlswrapper grid col-span-12 grid-cols-12 h-fit relative
	mb-0 pb-0
	mt-0 pt-0 select-none
		border-b-green-800



	"
		style={componentDraggedDiscriminator && `z-index:${gridRowIndex}`}
		on:dragenter={(e) => {
			//draggingOver = true;
		}}
		on:dragleave={(e) => {
			if (e.currentTarget.contains(e.relatedTarget)) {
				return;
			}
			e.preventDefault();

			//draggingOver = false;
			if (componentDraggedDiscriminator) {
				isHoveredWithDraggedComponent = false;
			}
		}}
		on:dragover={(e) => {
			if (componentDraggedDiscriminator) {
				handleComponentDragOver(e);
			}
			dispatch('dragover');
		}}
		on:dragend={() => {
			dispatch('dragend');
		}}
		on:mousemove={(e) => {
			mousemove = true;
			hoveredSide = getElementClientSide(e);
		}}
		on:mouseleave={(event) => {
			mousemove = false;
			hoveredSide = null;
		}}
	>
		{#if false && dragging}
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

		<icon
			draggable="true"
			on:drag={() => {
				dispatch('dragging', {});
			}}
			on:dragend={() => {
				dragging = false;
			}}
			on:dragstart={(e) => {
				dragging = true;

				e.dataTransfer?.setData('draggedGridRowIndex', gridRowIndex);
				e.dataTransfer?.setData('gridRowId', $gridRowStore.id);
			}}
			on:drop|preventDefault={(e) => {
				//console.log('dropping');
				//
				//draggingOver = false;
				//const draggedGridRowIndexData = e.dataTransfer?.getData('draggedGridRowIndex');
				//if (draggedGridRowIndexData) {
				//	const draggedGridRowIndex = parseInt(draggedGridRowIndexData);
				//	handleSortingGridRow(draggedGridRowIndex);
				//	return;
				//}
				//handleDropComponent(e);
				//isHoveredWithDraggedComponent = null;
			}}
			on:click={(e) => {
				if (mousemoveGridColumn) {
					return;
				}
				if (!$gridRowStore.styling) {
					$gridRowStore.styling = CmsClient.ContainerStylingFromJSON({});
				}

				//console.log('self click');
				//
				//function callback(newContent) {
				//	$gridRowStore = newContent;
				//}
				//

				//styleContent.set($gridRowStore);

				dispatch('setConfigurableContent', gridRowStore);
				//configuredContent =
			}}
			class="absolute flex items-center h-full right-full pr-12"
		>
			{#if mousemove}
				<Icon width={24} icon="fluent:dock-row-20-filled" />
			{/if}
		</icon>
		<GridRowContent
			on:mouseovergridcolumn={() => {
				mousemove = false;
			}}
			class="grid col-span-12 grid-cols-12 relative border-green-950 z-0
			pt-{$gridRowStore?.styling?.padding?.top}
			pr-{$gridRowStore?.styling?.padding?.right}
			pl-{$gridRowStore?.styling?.padding?.left}
			pb-{$gridRowStore?.styling?.padding?.bottom}
			mt-{$gridRowStore?.styling?.margin?.top}
			mr-{$gridRowStore?.styling?.margin?.right}
			ml-{$gridRowStore?.styling?.margin?.left}
			mb-{$gridRowStore?.styling?.margin?.bottom}
			{mousemove && 'bg-slate-300 bg-opacity-30'}
			"
			bind:node={rowRef}
			bind:styling={$gridRowStore.styling}
		>
			{#if false && isHoveredWithDraggedComponent}
				<div
					class="h-full w-full absolute pointer-events-none
				z-50
				border-b-4 border-red-950"
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
			{#if showGridOverlay}
				<div class="grid grid-cols-12 absolute z-10 w-full h-full">
					{#each new Array(12) as undefined, j}
						<div class="border-2 bg-slate-700 border-black" />
					{/each}
				</div>
			{/if}
			{#if isHoveredWithDraggedComponent}
				<div class="w-full col-span-full bg-white grid grid-cols-12 h-fit">
					{#each new Array(12) as number, i}
						<div
							data-grid-column-index-new={i}
							class="aspect-square outline-1 outline"
							on:dragover|preventDefault|stopPropagation
							on:drop={() => {
								dispatch('insertgridrow', { columnIndex: i });
								isHoveredWithDraggedComponent = false;
							}}
						>
							{#if true}
								<icon>
									<Icon strokeWidth="10" class=" w-full h-full " icon="ion:add" />
								</icon>
							{/if}
						</div>
					{/each}
				</div>
			{/if}

			{#each { length: 12 } as undefined, i}
				{@const skip = modifiedGridColumns[i].skip}

				{@const content = modifiedGridColumns[i].content}

				{#if !skip}
					{#if content}
						<GridColumn
							bind:columnRef={columnRefs[i]}
							bind:mousemoveGridrow={mousemove}
							bind:showGridOverlay
							bind:gridRows
							{gridRowIndex}
							bind:gridColumn={modifiedGridColumns[i].content}
							{componentDraggedDiscriminator}
							{gridRowNestingLevel}
							parentRowRef={rowRef}
							on:mouseovergridcolumn={(e) => {
								mousemoveGridColumn = true;
							}}
							on:mouseleavegridcolumn={(e) => {
								mousemoveGridColumn = false;
							}}
							on:setConfigurableContent
						/>
					{:else}
						<GridColumnEmpty
							on:gridColumnAdded={handleAddGridColumn}
							{componentDraggedDiscriminator}
							indexOfColumnInGridRow={i}
						/>
					{/if}
				{/if}
			{/each}
			{#if false}
				{#each modifiedGridColumns as modifiedGridColumn, j (modifiedGridColumn.key)}
					{#if !modifiedGridColumn.skip}
						{#if modifiedGridColumn.content}
							<GridColumn
								bind:columnRef={columnRefs[j]}
								bind:mousemoveGridrow={mousemove}
								bind:showGridOverlay
								bind:gridRows
								{gridRowIndex}
								bind:gridColumn={modifiedGridColumn.content}
								{componentDraggedDiscriminator}
								{gridRowNestingLevel}
								parentRowRef={rowRef}
								on:mouseovergridcolumn={(e) => {
									mousemoveGridColumn = true;
								}}
								on:mouseleavegridcolumn={(e) => {
									mousemoveGridColumn = false;
								}}
								on:setConfigurableContent
							/>
						{:else}
							<GridColumnEmpty
								on:gridColumnAdded={handleAddGridColumn}
								{componentDraggedDiscriminator}
								indexOfColumnInGridRow={j}
							/>
						{/if}
					{/if}
				{/each}
			{/if}
		</GridRowContent>
	</div>
{/if}
