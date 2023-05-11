<script lang="ts">
	export let data;
</script>

<table>
	{#if data.productKeys}
		<thead>
			<tr>
				{#each data.productKeys as productModelKey}
					<th>{productModelKey}</th>
				{/each}
			</tr>
		</thead>
	{/if}
	{#if data.products}
		<tbody>
			{#each data.products as product}
				<tr>
					{#each data.productKeys as productModelKey}
						<td>{product[productModelKey]}</td>
					{/each}
				</tr>
			{/each}
		</tbody>
	{/if}
</table>
