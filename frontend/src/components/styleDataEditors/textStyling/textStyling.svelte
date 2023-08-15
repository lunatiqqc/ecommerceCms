<script lang="ts">
	import LexicalEditorToolbar from '@/components/lexicalEditor/lexicalEditorToolbar.svelte';
	import theme from '@/lib/lexicalEditor/theme';

	export let styleContent: CmsClient.TextContainerStyling;


	const lexicalStylingToCms = {
		FORMAT_TEXT_COMMAND: 'textFormats',
		FORMAT_ELEMENT_COMMAND: 'elementFormats',
		"SET_HEADING": "headingValue"
	};

	let formatType;

	const pseudoEditor = {
		dispatchCommand: ({ type }, value) => {

			if (type === 'FORMAT_TEXT_COMMAND') {

				if (value === 'bold') {
					if (styleContent[lexicalStylingToCms[type]][value] === CmsClient.BoldClass.fontBold) {
						styleContent[lexicalStylingToCms[type]][value] = null;

						return;
					}

					styleContent[lexicalStylingToCms[type]][value] = CmsClient.BoldClass.fontBold;
				}
				if (value === 'italic') {
					if (styleContent[lexicalStylingToCms[type]][value] === CmsClient.ItalicClass.italic) {
						styleContent[lexicalStylingToCms[type]][value] = null;

						return;
					}

					styleContent[lexicalStylingToCms[type]][value] = CmsClient.ItalicClass.italic;
				}
				if (value === 'underline') {
					if (
						styleContent[lexicalStylingToCms[type]][value] === CmsClient.UnderlineClass.underline
					) {
						styleContent[lexicalStylingToCms[type]][value] = null;

						return;
					}

					styleContent[lexicalStylingToCms[type]][value] = CmsClient.UnderlineClass.underline;
				}
			}
			if (type === 'FORMAT_ELEMENT_COMMAND') {

				styleContent[lexicalStylingToCms[type]] = CmsClient.ElementFormats[value];
			}
			if (type === 'SET_HEADING') {
				styleContent[lexicalStylingToCms[type]] = value
			}
		},
		_config: {
			theme: theme
		}
	};

	if (!styleContent.textFormats) {
		styleContent.textFormats = CmsClient.TextFormatsFromJSON({});
	}
</script>

<LexicalEditorToolbar
	isBubble={false}
	formats={styleContent.textFormats}
	blockType={{}}
	formatType={styleContent.elementFormats}
	editor={pseudoEditor}
/>
