<script lang="ts">
	import 'quill/dist/quill.bubble.css';
	import TextEditor from '../textEditor.svelte';

	export let textStyling: CmsClient.TextContainerStyling;
	export let editorState;
	export let html;
	let classes;

	$: if (textStyling) {
		console.log('textStyling3', textStyling);

		let arrayOfClassNames = [];

		if (textStyling.textFormats) {
			arrayOfClassNames = Object.values(textStyling.textFormats);
		}

		if (textStyling.elementFormats) {
			arrayOfClassNames = Object.values(textStyling.elementFormats);
		}
		if (textStyling.fontSizeClassOptions) {
			arrayOfClassNames = Object.values(textStyling.fontSizeClassOptions);
		}

		classes = arrayOfClassNames.join(' ');
	}
</script>

<div class="h-full {classes} {textStyling?.elementFormats}">
	<TextEditor textContent={{ editorState: editorState, html: html }} />
</div>
