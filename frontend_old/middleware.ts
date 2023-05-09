import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';
 
export function middleware(request: NextRequest) {

  if (request.nextUrl.pathname.startsWith('/pages')) {
    return NextResponse.rewrite(new URL('/pages/', request.url));
  }
}