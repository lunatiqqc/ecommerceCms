<script lang="ts">
	import { onMount } from 'svelte';
	import Quill from 'quill';
	import 'quill/dist/quill.bubble.css';
	import { clickOutside } from '@/lib/actions/clickOutside';
	//import Editor from '@tinymce/tinymce-svelte';
	//import lexicalRichText from '@lexical/rich-text/index';

	import { createEditor } from 'lexical';

	import * as test from 'lexical';
	import LexicalEditor from './lexicalEditor/lexicalEditor.svelte';

	//import { Editor } from '@tiptap/core';
	//import StarterKit from '@tiptap/starter-kit';
	//import BubbleMenu from '@tiptap/extension-bubble-menu';
	//import TipTap from './TipTap.svelte';

	export let textContent: { editorState: object; html: string };

	let quill = null;

	let editorRef: HTMLElement | null;

	//let elementRef;
	//
	//let menuElementRef;
	//
	//onMount(() => {
	//	const editor = new Editor({
	//		element: elementRef,
	//		extensions: [
	//			StarterKit,
	//			BubbleMenu.configure({
	//				element: menuElementRef
	//			})
	//		],
	//		content: '<p>Hello World!</p>'
	//	});
	//});

	//onMount(() => {
	//	const config = {
	//		namespace: 'MyEditor'
	//	};
	//
	//	const editor = createEditor(config);
	//
	//	const contentEditableElement = document.getElementById('editor');
	//
	//	editor.setRootElement(contentEditableElement);
	//});

	//onMount(() => {
	//	let container = editorRef;
	//
	//	var toolbarOptions = [
	//		['bold', 'italic', 'underline', 'strike'], // toggled buttons
	//		['blockquote'],
	//		//['blockquote', 'code-block'],
	//
	//		//[{ header: 1 }, { header: 2 }], // custom button values
	//		[{ list: 'ordered' }, { list: 'bullet' }],
	//		[{ script: 'sub' }, { script: 'super' }], // superscript/subscript
	//		[{ indent: '-1' }, { indent: '+1' }], // outdent/indent
	//		//[{ direction: 'rtl' }], // text direction
	//
	//		//[{ size: ['small', false, 'large', 'huge'] }], // custom dropdown
	//		[{ header: [1, 2, 3, 4, 5, 6, false] }],
	//
	//		[{ color: [] }, { background: [] }], // dropdown with defaults from theme
	//		[{ font: [] }],
	//		[{ align: [] }],
	//
	//		['clean'] // remove formatting button
	//	];
	//	quill = new Quill(container, {
	//		modules: {
	//			toolbar: toolbarOptions
	//		},
	//		//placeholder: 'Type something...',
	//		theme: 'bubble' // or 'bubble',
	//	});
	//
	//	console.log('textContent.deltaFormat', textContent.deltaFormat);
	//
	//	if (textContent.deltaFormat) {
	//		quill.setContents(JSON.parse(textContent.deltaFormat));
	//	}
	//
	//	quill.on('text-change', function (delta, oldDelta, source) {
	//		const HTML = editorRef.querySelector('.ql-editor').innerHTML;
	//
	//		textContent.html = HTML;
	//		textContent.deltaFormat = JSON.stringify(quill.getContents());
	//	});
	//
	//	quill.on('selection-change', function (range, oldRange, source) {
	//		if (range === null && oldRange !== null) {
	//			editorRef.querySelector('.ql-tooltip').classList.add('hidden');
	//		} else if (range !== null && oldRange === null) {
	//			editorRef.querySelector('.ql-tooltip').classList.remove('hidden');
	//		}
	//	});
	//});

	let visible = true;

	const distractionFreeBodyConfig = {
		width: 500,
		max_height: 500,
		max_width: 500,
		min_height: 100,
		min_width: 400,
		menubar: false,
		inline: true,
		skin: 'oxide-dark',
		plugins: [
			'autolink',
			'codesample',
			'link',
			'lists',
			'media',
			'powerpaste',
			'table',
			'image',
			'quickbars',
			'codesample',
			'help'
		],
		toolbar: false,
		quickbars_insert_toolbar: 'quicktable image media codesample',
		quickbars_selection_toolbar:
			'bold italic underline | blocks | blockquote quicklink alignleft aligncenter alignright alignfull',
		contextmenu: 'undo redo | inserttable | cell row column deletetable | help',
		powerpaste_word_import: 'clean',
		powerpaste_html_import: 'clean'
	};
	let showToolBar = false;
</script>

<!-- 
<div class="z-10 relative" bind:this={editorRef} id="editor">
	<slot />
</div> -->

<div class="relative z-50 h-full">
	<!-- <Editor
		conf={distractionFreeBodyConfig}
		scriptSrc="/static/tinymce/tinymce.min.js"
		inline={true}
		value={textContent.html}>test999</Editor
	> -->

	<!-- <div id="editor" /> -->
	<!-- <div data-bubble-menu bind:this={menuElementRef} />
	<div data-editor bind:this={elementRef} /> -->
	<!-- <TipTap /> -->
	<div
		class="h-full"
		use:clickOutside
		on:outclick={() => {
			showToolBar = false;
		}}
	>
		<LexicalEditor {textContent} />
	</div>
</div>

<!-- <style lang="scss">
	:global(.ql-tooltip) {
		width: 42vw;
	}
	:global(.ql-editor) {
		padding: 0;
	}
	@import 'node_modules/quill/dist/quill.bubble.css';
</style> -->
<style lang="scss">
	:global(.tox-pop) {
		max-width: unset !important;
	}
</style>
