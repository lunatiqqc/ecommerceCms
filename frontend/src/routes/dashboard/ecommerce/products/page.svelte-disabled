<script lang="ts">
	//import { Skull } from 'lucide-svelte';

	export let data;
	let editedProducts: Array<number> = [];

	const productsClient = new CmsClient.ProductsClient();

	function handleInputValidation(e: InputEvent, input?: string, type?: string) {
		const isString = isNaN(Number(input));

		if (type == 'number' && isString) {
			e.preventDefault();
		}
	}

	function handleAddProduct() {
		data.products = [
			...data.products,
			data.productKeys.map((key) => {
				return { key: '' };
			})
		];
	}

	function handleDeleteProduct() {
		data.products = data.products?.filter((product, i) => {
			return i !== hoveredRowIndex;
		});
	}

	function handleEditedProducts(
		this: {
			originalContent?: string;
			prevValue?: string;
		},
		e: InputEvent & { target: HTMLElement },
		productIndex: number,
		originalContent: string
	) {
		if (!this.originalContent) {
			this.originalContent = originalContent;
		}

		if (this.prevValue) {
			this.prevValue = e.target.innerText;
		} else {
			this.prevValue = originalContent;
		}

		if (!editedProducts.includes(productIndex) && this.originalContent != e.target.innerText) {
			editedProducts = [...editedProducts, productIndex];
		} else if (this.originalContent == e.target.innerText) {
			editedProducts = editedProducts.filter((index) => {
				return index !== productIndex;
			});
		}
	}

	async function handlePostProduct(product) {
		const res = await productsClient.post(product);
	}

	let hoveredRowIndex = -1;
	let editableColumn = { rowIndex: -1, columnIndex: -1 };
</script>

<table>
	{#if data.productKeys}
		<thead>
			<tr>
				{#each data.productKeys as productModelKey}
					<th>{productModelKey.key}</th>
				{/each}
				<th>Edit</th>
			</tr>
		</thead>
	{/if}
	{#if data.products}
		<tbody>
			{#each data.products as product, i}
				{@const isHovered = hoveredRowIndex === i}
				{@const isLastColumnEditable =
					editableColumn.rowIndex === i &&
					editableColumn.columnIndex === data.productKeys.length - 1}
				<tr
					class={isHovered ? 'relative' : ''}
					on:mouseenter={() => {
						hoveredRowIndex = i;
					}}
					on:mouseleave={() => {
						hoveredRowIndex = -1;
					}}
				>
					{#each data.productKeys as { key, type }, j}
						<td
							on:focus={() => {
								editableColumn.rowIndex = i;
								editableColumn.columnIndex = j;
							}}
							on:blur={() => {
								editableColumn.rowIndex = -1;
								editableColumn.columnIndex = -1;
							}}
							contenteditable
							on:beforeinput={(e) => handleInputValidation(e, e.data, type)}
							on:input={(e) => {
								handleEditedProducts.call({}, e, i, product[key]);
							}}
						>
							{product[key] || ''}
						</td>
					{/each}

					<td>
						{#if data.test}
							<svelte:component this={data.test} />
						{/if}
					</td>
					{#if isHovered}
						<div
							class="absolute inset-0 left-auto right-0 w-fit"
							class:bottom-full={isLastColumnEditable}
							class:top-auto={isLastColumnEditable}
						>
							{#if product.id && editedProducts.includes(i)}
								<button on:click={() => handlePostProduct(product)} class="btn-green">Save</button>
							{:else if !product.id}
								<button class="btn-green" on:click={handlePostProduct}>Add</button>
							{/if}

							<button class="btn-red" on:click={handleDeleteProduct}>Delete</button>
						</div>
					{/if}
				</tr>
			{/each}

			<tr>
				<td colspan="12" class="border-0">
					<button on:click={() => handleAddProduct()} class="btn-blue block mx-auto"
						>Add product</button
					>
				</td>
			</tr>
		</tbody>
	{/if}
</table>
