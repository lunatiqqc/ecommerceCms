<script lang="ts">
	import { ListItemNode, ListNode } from '@lexical/list';
	import { HeadingNode, QuoteNode, registerRichText } from '@lexical/rich-text';
	import {
		COMMAND_PRIORITY_CRITICAL,
		SELECTION_CHANGE_COMMAND,
		$getSelection as getSelection,
		$isRangeSelection as isRangeSelection,
		$getNodeByKey as getNodeByKey,
		DEPRECATED_$isGridSelection,
		type EditorThemeClasses,
		type LexicalEditor,
		createEditor,
		FORMAT_TEXT_COMMAND,
		$isRootOrShadowRoot as isRootOrShadowRoot,
		$isElementNode as isElementNode,
		$isParagraphNode as isParagraphNode,
		$getRoot as getRoot,
		$isTextNode as isTextNode,
		$createParagraphNode as createParagraphNode,
		$insertNodes as insertNodes
	} from 'lexical';

	import { getContext, onMount, setContext, type SvelteComponent } from 'svelte';
	import { writable } from 'svelte/store';
	import LexicalEditorToolbar from './lexicalEditorToolbar.svelte';
	import type { HtmlTag } from 'svelte/internal';

	import {
		$findMatchingParent as findMatchingParent,
		$getNearestBlockElementAncestorOrThrow as getNearestBlockElementAncestorOrThrow,
		$getNearestNodeOfType as getNearestNodeOfType,
		mergeRegister
	} from '@lexical/utils';
	import { $generateNodesFromDOM as generateNodesFromDOM } from '@lexical/html';

	import {
		$setBlocksType as setBlocksType,
		$getSelectionStyleValueForProperty as getSelectionStyleValueForProperty
	} from '@lexical/selection';
	import {
		HeadingTagType,
		$createHeadingNode as createHeadingNode,
		$isHeadingNode as isHeadingNode
	} from '@lexical/rich-text';
	import { text } from '@sveltejs/kit';
	import theme from '@/lib/lexicalEditor/theme';
	import { clickOutside } from '@/lib/actions/clickOutside';

	export let textContent: { editorState: object; html: string };
	let toolbar: {
		position?: {
			clientX: number;
			clientY: number;
		};
		visible: boolean;
	} = {
		visible: false
	};

	let isTextSelection = false;

	let composer: SvelteComponent;

	//const initialConfig = {
	//	namespace: 'Playground',
	//	theme,
	//	nodes: [HeadingNode, ListNode, ListItemNode, QuoteNode, HorizontalRuleNode, ImageNode],
	//	onError: (error: Error) => {
	//		throw error;
	//	}
	//};

	function setEditor(editor: LexicalEditor): void {
		setContext('editor', editor);
	}

	function getEditor(): LexicalEditor {
		return getContext('editor');
	}

	let editor = createEditor({
		editable: true,
		onError: (error) => {
			console.error(error);
		},
		theme: theme,
		nodes: [HeadingNode]
	});

	registerRichText(editor);
	setEditor(editor);
	const activeEditor = writable(editor);

	let formats = {
		bold: false,
		italic: false,
		underline: false
	};

	let indent = 0;

	onMount(() => {
		editor.setRootElement(editorRef);

		editor.update(() => {
			const editorState = textContent.editorState;
			if (editorState) {
				editor.setEditorState(editor.parseEditorState(editorState));

				return;
			}

			const root = getRoot();
			if (root.isEmpty()) {
				const paragraph = createParagraphNode();

				root.append(paragraph);
			}

			if (textContent.html) {
				console.log('parsedHtml');

				const parser = new DOMParser();
				const dom = parser.parseFromString(textContent.html, 'text/html');

				// Once you have the DOM instance it's easy to generate LexicalNodes.
				const nodes = generateNodesFromDOM(editor, dom);

				getRoot().select();

				// Insert them at a selection.
				console.log('nodes', nodes);

				insertNodes(nodes);
			}
		});
		mergeRegister(
			editor.registerUpdateListener(({ editorState }) => {
				editorState.read(() => {
					//selection = getSelection();
					//handleSelectionChange();

					const selection = editorState._selection;

					if (isRangeSelection(selection)) {
						formats.bold = selection.hasFormat('bold');
						formats.italic = selection.hasFormat('italic');
						formats.underline = selection.hasFormat('underline');

						const anchorNode = selection.anchor.getNode();

						let element =
							anchorNode.getKey() === 'root'
								? anchorNode
								: findMatchingParent(anchorNode, (e) => {
										const parent = e.getParent();
										return parent !== null && isRootOrShadowRoot(parent);
								  });

						if (element === null) {
							element = anchorNode.getTopLevelElementOrThrow();
						}

						if (isElementNode(element)) {
							indent = element.getIndent();
							formatType = CmsClient.ElementFormats[element.getFormatType()];
						}
					}
					textContent.editorState = JSON.stringify(editorState.toJSON());
					textContent.html = editorRef.innerHTML;
					localStorage.setItem('editorState', JSON.stringify(editorState.toJSON()));
				});
			}),
			editor.registerCommand(
				SELECTION_CHANGE_COMMAND,
				(_payload, newEditor) => {
					handleSelectionChange();
					$activeEditor = newEditor;
					return false;
				},
				COMMAND_PRIORITY_CRITICAL
			)
		);
	});

	let selection;

	let blockType;

	let formatType;

	function handleSelectionChange() {
		console.log('editor.getEditorState()', editor.getEditorState());
		console.log('selectionChange');

		selection = getSelection();

		const nativeSelection = window.getSelection();

		const rootElement = editor.getRootElement();

		if (selection && isRangeSelection(selection) && !selection.isCollapsed()) {
			console.log('trueeeasdasd');

			const anchorNode = selection.anchor.getNode();

			let element =
				anchorNode.getKey() === 'root'
					? anchorNode
					: findMatchingParent(anchorNode, (e) => {
							const parent = e.getParent();
							return parent !== null && isRootOrShadowRoot(parent);
					  });

			if (element === null) {
				element = anchorNode.getTopLevelElementOrThrow();
			}

			if (isElementNode(anchorNode)) {
				formatType = CmsClient.ElementFormats[element.getFormatType()];

				console.log('formatType', formatType);
			}

			if (isParagraphNode(element)) {
				formatType = CmsClient.ElementFormats[element.getFormatType()];
			}

			console.log('element', element);

			console.log('anchorNode', anchorNode);

			const elementKey = element.getKey();
			const elementDOM = editor.getElementByKey(elementKey);

			if (elementDOM !== null) {
				const type = isHeadingNode(element) ? element.getTag() : element.getType();
				blockType = type;
			}

			const rangeRect = getDOMRangeRect(nativeSelection, rootElement);

			console.log('rangeRect', rangeRect);

			const nodes = selection?.getNodes();

			console.log('nnodes', nodes);

			if (nodes) {
				const element = editor.getElementByKey(nodes[0].__key);

				console.log(element);

				if (element) {
					console.log('true2');

					const elementClientRect = element.getBoundingClientRect();

					const editorRect = editorRef.getBoundingClientRect();
					toolbar = {
						visible: true,
						position: {
							clientX: rangeRect.x - editorRect.x + rangeRect.width / 2,
							clientY: rangeRect.y - editorRect.y
						}
					};
				}
			}
		} else {
			toolbar.visible = false;
		}
	}

	let editorRef: HTMLElement;

	let client: { clientX: number; clientY: number };

	function getDOMRangeRect(nativeSelection: Selection, rootElement: HTMLElement): DOMRect {
		const domRange = nativeSelection.getRangeAt(0);

		let rect;

		if (nativeSelection.anchorNode === rootElement) {
			let inner = rootElement;
			while (inner.firstElementChild != null) {
				inner = inner.firstElementChild as HTMLElement;
			}
			rect = inner.getBoundingClientRect();
		} else {
			rect = domRange.getBoundingClientRect();
		}

		return rect;
	}
</script>

<div
	class="relative h-full"
	use:clickOutside
	on:outclick={() => {
		toolbar.visible = false;
	}}
>
	{#if toolbar.visible}
		<LexicalEditorToolbar
			{activeEditor}
			bind:editor
			bind:selection
			bind:position={toolbar.position}
			bind:formats
			bind:blockType
			bind:formatType
			bind:indent
		/>
	{/if}
	<!-- <ToolbarRichText
				on:textselection={({ detail }) => {
					console.log('onTextSelection', detail);
				}}
			/> -->

	<div
		class="h-full !whitespace-nowrap"
		on:mousemove={(e) => {
			//client = { clientX: e.clientX, clientY: e.clientY };
			//console.log(client);
		}}
		contentEditable={true}
		bind:this={editorRef}
	/>

	<!-- 	<button
		on:click={() => {
			console.log(JSON.stringify(editor.getEditorState()));

			const state =
				'{"root":{"children":[{"children":[{"detail":0,"format":11,"mode":"normal","style":"","text":"wqeasdasdasdasd","type":"text","version":1}],"direction":"ltr","format":"center","indent":0,"type":"heading","version":1,"tag":"h2"}],"direction":"ltr","format":"","indent":0,"type":"root","version":1}}';

			editor.setEditorState(editor.parseEditorState(state));
		}}>test</button
	> -->
</div>
