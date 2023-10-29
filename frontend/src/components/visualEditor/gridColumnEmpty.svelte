<script lang="ts">
	import { createEventDispatcher } from 'svelte';

	export let indexOfColumnInGridRow;
	export let componentDraggedDiscriminator;

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

		//if (gridRowNestingLevel === 1) {
		//	const column = CmsClient.GridColumnFromJSON({
		//		width: 1,
		//		columnStart: indexOfColumnInGridRow,
		//		component: CmsClient.ComponentFromJSONTyped(
		//			{ discriminator: componentDraggedDiscriminator },
		//			false
		//		)
		//	});
		//	$modifiedGridColumnStore.columns = [...gridRows[gridRowIndex].columns, column];
		//}
		//if (gridRowNestingLevel === 0) {
		//	const column = CmsClient.GridColumnFromJSON({
		//		width: 1,
		//		columnStart: indexOfColumnInGridRow,
		//		gridContent: CmsClient.GridContentFromJSONTyped(
		//			{
		//				gridRows: [
		//					CmsClient.GridRowFromJSONTyped(
		//						{
		//							columns: [
		//								CmsClient.GridColumnFromJSONTyped(
		//									{
		//										component: CmsClient.ComponentFromJSONTyped(
		//											{ discriminator: componentDraggedDiscriminator },
		//											false
		//										),
		//										columnStart: 0,
		//										width: 12
		//									},
		//									true
		//								)
		//							]
		//						},
		//						true
		//					)
		//				]
		//			},
		//			true
		//		)
		//	});
		//	gridRows[gridRowIndex].columns = [...gridRows[gridRowIndex].columns, column];
		//}
	}
</script>

<div
	data-grid-column-index={indexOfColumnInGridRow}
	class="relative col-start-{indexOfColumnInGridRow + 1}"
	on:dragover={(e) => {
		if (componentDraggedDiscriminator) {
			e.preventDefault();
		}
	}}
	on:drop|stopPropagation={() => {
		handleComponentDrop(indexOfColumnInGridRow);
	}}
/>
