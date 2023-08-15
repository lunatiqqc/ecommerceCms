import { readable } from 'svelte/store';

const _allComponents = import.meta.glob('@/components/cmsComponents/*', {
	eager: false,
	import: 'default'
});


export const allComponents = readable(_allComponents);
