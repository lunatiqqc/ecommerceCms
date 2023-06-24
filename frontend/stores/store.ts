import { type Writable, writable, derived } from 'svelte/store';

import type { load } from '@/src/routes/+layout';

export const store = writable<Awaited<ReturnType<typeof load>>>();
