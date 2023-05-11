<script lang="ts">
	export let data;
</script>

<ul>
	{#each data.endpoints as endpoint}
		<li>
			<a href={endpoint}>{endpoint}</a>
		</li>
	{/each}
</ul>

<slot />
