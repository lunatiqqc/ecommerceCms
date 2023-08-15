<script lang="ts">
	export let styleContent: CmsClient.Padding;
	function handlePaddingChange(e) {
		styleContent.bottom = e.target.value;
		styleContent.top = e.target.value;
		styleContent.left = e.target.value;
		styleContent.right = e.target.value;
        styleContent = styleContent
	}
</script>

<input on:change={handlePaddingChange} type="number" name="" id="" max={12} min=0 />
<slot />
