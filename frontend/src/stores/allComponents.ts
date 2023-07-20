import { readable } from 'svelte/store';

const _allComponents = import.meta.glob('@/components/cmsComponents/*', {
	eager: false,
	import: 'default'
});

console.log(_allComponents);

export const allComponents = readable(_allComponents);
