<script lang="ts">
	export let rightClickCoordinates;
	export let pageItem;
	export let handleAddPage;
	function moveElementByOffset(element) {
		const matrix = new DOMMatrix().translate(rightClickCoordinates[0], rightClickCoordinates[1]);
		element.style.transform = matrix;
	}
</script>

<div use:moveElementByOffset class="absolute inset-0 bg-red-700 px-4 py-4 w-fit h-fit z-10">
	<ul class="p-0">
		<li class="list-none">
			<button
				on:click={() => {
					handleAddPage(pageItem);
				}}
				class="whitespace-nowrap text-black">Add page</button
			>
		</li>
	</ul>
</div>
