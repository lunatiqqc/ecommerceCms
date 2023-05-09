import { CmsClient } from "@/cmsTypescriptClient/cmsClient";
export default async function Products() {
    //console.log(data);

    console.log("before data fetch");
    const productsClient = new CmsClient.ProductsClient();
    const data: CmsClient.Product[] | null = await productsClient.getProducts();

    //console.log(data)

    if (!data) {
        return <div>no data</div>;
    }
    return (
        <div>
            <table>
                <thead>
                    <tr>
                        {Object.keys(new CmsClient.Product().toJSON()).map(
                            (key) => {
                                return <th key={key}>{key}</th>;
                            }
                        )}
                    </tr>
                </thead>
                <tbody>
                    {data.map((product) => {
                        return (
                            <tr key={product.id}>
                                {Object.values(product).map((value, i) => {
                                    return <td key={i}>{value.toString()}</td>;
                                })}
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
}
