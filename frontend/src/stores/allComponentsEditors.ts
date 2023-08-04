import { readable } from 'svelte/store';

const _allComponentsEditors = import.meta.glob('@/components/cmsComponentsEditors/*', {
	eager: false,
	import: 'default'
});

console.log(_allComponentsEditors);

export const allComponentsEditors = readable(_allComponentsEditors);
