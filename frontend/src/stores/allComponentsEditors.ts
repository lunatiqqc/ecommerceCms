import { readable } from 'svelte/store';

const _allComponentsEditors = import.meta.glob('@/components/cmsComponentsEditors/*', {
	eager: false,
	import: 'default'
});


export const allComponentsEditors = readable(_allComponentsEditors);
