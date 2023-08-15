<script lang="ts">
	export let styleContent: CmsClient.Padding['top'];

	function handlePaddingChange(e) {
		styleContent = e.target.value;
	}
</script>

<input on:change={handlePaddingChange} type="number" name="" max="12" min="0" id="" />
