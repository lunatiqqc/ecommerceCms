import { CmsClient } from '@/cmsTypescriptClient/cmsClient';

//console.log('window', window);

globalThis.CmsClient = CmsClient as typeof import('@/cmsTypescriptClient/cmsClient').CmsClient;
