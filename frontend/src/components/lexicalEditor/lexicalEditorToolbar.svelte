<script lang="ts">
	import { onMount } from 'svelte';
	import Icon from '../icon.svelte';

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
		RangeSelection,
		TextFormatType,
		ElementNode,
		SerializedElementNode,
		$createParagraphNode as createParagraphNode,
		FORMAT_ELEMENT_COMMAND,
		$isRootOrShadowRoot as isRootOrShadowRoot,
		INDENT_CONTENT_COMMAND,
		OUTDENT_CONTENT_COMMAND
	} from 'lexical';

	import {
		$findMatchingParent as findMatchingParent,
		$getNearestBlockElementAncestorOrThrow as getNearestBlockElementAncestorOrThrow,
		$getNearestNodeOfType as getNearestNodeOfType,
		mergeRegister,
		addClassNamesToElement,
		removeClassNamesFromElement
	} from '@lexical/utils';

	import { $setBlocksType as setBlocksType } from '@lexical/selection';
	import { HeadingTagType, $createHeadingNode as createHeadingNode } from '@lexical/rich-text';

	export let editor: LexicalEditor;
	export let position: { clientX: number; clientY: number };
	export let selection: RangeSelection;
	export let formats;
	export let blockType;
	export let formatType;
	export let indent;
	export let isBubble: boolean = true;

	let toolbarRef: HTMLElement;

	$: if (toolbarRef && isBubble) {
		const toolbarRect = toolbarRef.getBoundingClientRect();
		if (position.globalClientY > 200) {
			toolbarRef.style.top = position.clientY - 100 + 'px';
		} else {
			toolbarRef.style.top = position.clientY + 50 + 'px';
		}

		toolbarRef.style.left = position.clientX - toolbarRect.width / 2 + 'px';

		if (toolbarRect.left < 0) {
			toolbarRef.style.left = 0;
		}

	}

	onMount(() => {
		if (isBubble) {
			const toolbarRect = toolbarRef.getBoundingClientRect();
			if (position.clientY > 200) {
				toolbarRef.style.top = position.clientY - 100 + 'px';
			} else {
				toolbarRef.style.top = position.clientY + 50 + 'px';
			}
			toolbarRef.style.left = position.clientX - toolbarRect.width / 2 + 'px';
		}
	});

	function setBlockType(blockType) {
		if (!isBubble) {
			editor.dispatchCommand({ type: 'SET_HEADING' }, blockType.value);
			return;
		}
		if (selection) {
			editor.update(() => {
				if (isRangeSelection(selection) || DEPRECATED_$isGridSelection(selection)) {
					let creationFunction;

					switch (blockType.type) {
						case 'heading':
							creationFunction = () => createHeadingNode(blockType.value);
							break;
						case 'paragraph':
							creationFunction = () => createParagraphNode();

							break;

						default:
							break;
					}

					setBlocksType(selection, creationFunction);
				}
			});
		}
	}

	function handleAddClassName(className) {
		editor.update(() => {
			if (isRangeSelection(selection)) {
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

				const elementKey = element.getKey();
				const elementDOM = editor.getElementByKey(elementKey);

				removeClassNamesFromElement(elementDOM, ...Object.values(fontSizeClassOptions));
				addClassNamesToElement(elementDOM, className);
			}
		});
	}

	$: if (selection) {
	}

	let fontSizeClassOptions = {
		'4xl': 'text-4xl',
		'3xl': 'text-3xl',
		'2xl': 'text-2xl',
		'x-large': 'text-xl',
		large: 'text-lg',
		standard: 'text-base'
	};

</script>

<div
	class=" {isBubble && 'absolute w-fit z-50'} flex {!isBubble &&
		'flex-wrap'} gap-4 bg-slate-400 px-2 py-2 rounded inset-0 h-fit"
	bind:this={toolbarRef}
>
	<div class="first-of-type: border-r-2 flex">
		<button
			class="p-4 {formats.bold && 'rounded bg-slate-200'}"
			on:click={() => {
				editor.dispatchCommand(FORMAT_TEXT_COMMAND, 'bold');
			}}
			><Icon icon="ooui:bold-b" />
		</button>
		<button
			class="p-4 {formats.underline && 'rounded bg-slate-200'}"
			on:click={() => {
				editor.dispatchCommand(FORMAT_TEXT_COMMAND, 'underline');
			}}
		>
			<Icon icon="ooui:underline-u" />
		</button>
		<button
			class="p-4 {formats.italic && 'rounded bg-slate-200'}"
			on:click={() => {
				editor.dispatchCommand(FORMAT_TEXT_COMMAND, 'italic');
			}}
		>
			<Icon icon="ooui:italic-i" />
		</button>
	</div>
	<div class="flex">
		<button
			class="p-4 {formatType === 'text-left' && 'rounded bg-slate-200'}"
			on:click={() => {
				if (formatType === 'text-left') {
					editor.dispatchCommand(FORMAT_ELEMENT_COMMAND, '');
					return;
				}
				editor.dispatchCommand(FORMAT_ELEMENT_COMMAND, 'left');
			}}
		>
			<Icon icon="ooui:align-left" />
		</button>
		<button
			class="p-4 text-j{formatType === 'text-center' && 'rounded bg-slate-200'}"
			on:click={() => {
				if (formatType === 'text-center') {
					editor.dispatchCommand(FORMAT_ELEMENT_COMMAND, '');
					return;
				}
				editor.dispatchCommand(FORMAT_ELEMENT_COMMAND, 'center');
			}}
		>
			<Icon icon="ooui:align-center" />
		</button>
		<button
			class="p-4 {formatType === 'text-right' && 'rounded bg-slate-200'}"
			on:click={() => {
				if (formatType === 'text-right') {
					editor.dispatchCommand(FORMAT_ELEMENT_COMMAND, '');
					return;
				}
				editor.dispatchCommand(FORMAT_ELEMENT_COMMAND, 'right');
			}}
		>
			<Icon icon="ooui:align-right" />
		</button>
		<button
			class="p-4 {formatType === 'text-justify' && 'rounded bg-slate-200'}"
			on:click={() => {
				if (formatType === 'text-justify') {
					editor.dispatchCommand(FORMAT_ELEMENT_COMMAND, '');
					return;
				}
				editor.dispatchCommand(FORMAT_ELEMENT_COMMAND, 'justify');
			}}
		>
			<Icon icon="fontisto:justify" />
		</button>
	</div>
	<div class="border-l-2 flex">
		<button
			class="p-4 relative {indent > 0 && 'rounded bg-slate-200'}"
			on:click={() => {
				editor.dispatchCommand(INDENT_CONTENT_COMMAND, undefined);
			}}
		>
			{#if indent > 0}
				<div
					class="absolute rounded-full h-6 w-6 bg-red-950 text-slate-50 p-2 -right-1 -top-1 flex justify-center items-center"
				>
					{indent}
				</div>
			{/if}
			<Icon icon="ooui:indent-ltr" />
		</button>
		<button
			on:click={() => {
				editor.dispatchCommand(OUTDENT_CONTENT_COMMAND, undefined);
			}}
		>
			<Icon icon="ooui:indent-rtl" />
		</button>
	</div>

	<select
		class="rounded bg-slate-700 text-slate-50 p-4"
		name="headings"
		on:change={(e) => {
			if (e.target.value) {
				setBlockType({ type: 'heading', value: e.target.value });
			} else {
				setBlockType({ type: 'paragraph', value: null });
			}
		}}
	>
		<option class=" " selected={blockType === 'Paragraph'} value="">Paragraph</option>
		<option class=" {editor._config.theme.heading['h1']}" selected={blockType === 'h1'} value="h1"
			>h1</option
		>
		<option class=" {editor._config.theme.heading['h2']}" selected={blockType === 'h2'} value="h2"
			>h2</option
		>
		<option class=" {editor._config.theme.heading['h3']}" selected={blockType === 'h3'} value="h3"
			>h3</option
		>
		<option class=" {editor._config.theme.heading['h4']}" selected={blockType === 'h4'} value="h4"
			>h4</option
		>
		<option class=" {editor._config.theme.heading['h5']}" selected={blockType === 'h5'} value="h5"
			>h5</option
		>
		<option class=" {editor._config.theme.heading['h6']}" selected={blockType === 'h6'} value="h6"
			>h6</option
		>
	</select>
	{#if false}
		<select
			name=""
			id=""
			on:change={(e) => {
				handleAddClassName(e.target.value);
			}}
		>
			{#each Object.entries(fontSizeClassOptions) as [title, twClass]}
				<option value={twClass}>{title}</option>
			{/each}
		</select>
	{/if}
</div>
