type params = { params: { endpoints: string[] } };

export default function Page({ params }: params) {
    return <h1>endpoint catchall</h1>;
}
// @ts-ignore:next-line
export const dynamicParams = true;

// Return a list of `params` to populate the [slug] dynamic segment
export async function generateStaticParams() {
    return [
        { endpoints: ["a", "1"] },
        { endpoints: ["b", "2"] },
        { endpoints: ["c", "3"] },
    ];
}
