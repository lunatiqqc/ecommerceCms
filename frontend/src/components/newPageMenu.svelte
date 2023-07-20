<script lang="ts">
	import { clickOutside } from '@/lib/actions/clickOutside';
	export let rightClickCoordinates;
	export let handleAddPage;
	export let handleDeletePage;
	export let outsideClickHandler;

	function moveElementByOffset(element) {
		const matrix = new DOMMatrix().translate(rightClickCoordinates[0], rightClickCoordinates[1]);
		element.style.transform = matrix;
	}
</script>

<div use:moveElementByOffset class="absolute inset-0 bg-red-700 px-4 py-4 w-fit h-fit z-10">
	<ul class="p-0">
		<li class="list-none">
			<button
				use:clickOutside
				on:outclick={outsideClickHandler}
				on:click={handleAddPage}
				class="whitespace-nowrap text-black">Add page</button
			>
		</li>
		<li class="list-none">
			<button
				use:clickOutside
				on:outclick={outsideClickHandler}
				on:click={handleDeletePage}
				class="whitespace-nowrap text-black">Remove page</button
			>
		</li>
	</ul>
</div>
