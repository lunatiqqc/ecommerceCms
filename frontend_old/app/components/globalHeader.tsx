import { Url } from "next/dist/shared/lib/router/router";
import Link from "next/link";

export default async function globalHeader() {
    const pagesClient = new CmsClient.PagesClient();

    const menuPages = await pagesClient.getPages();

    function RenderMenu({
        pages,
    }: {
        pages: NonNullable<typeof menuPages>;
    }): JSX.Element {
        return (
            <ul>
                {pages.map((page) => (
                    <li className='mx-4'>
                        <a href={page.url}>{page.title}</a>
                        {page.children && <RenderMenu pages={page.children} />}
                    </li>
                ))}
            </ul>
        );
    }

    return (
        <header>
            <nav>
                {menuPages && <RenderMenu pages={menuPages!}></RenderMenu>}{" "}
            </nav>
        </header>
    );
}
