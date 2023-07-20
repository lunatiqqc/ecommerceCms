import { readable } from 'svelte/store';

const _allStyleDataEditorComponents = import.meta.glob('@/components/styleDataEditors/**', {
	eager: false,
	import: 'default'
});

export const allStyleDataEditorComponents = readable(_allStyleDataEditorComponents);
