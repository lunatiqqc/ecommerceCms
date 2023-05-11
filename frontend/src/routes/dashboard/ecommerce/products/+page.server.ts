import * as devalue from 'devalue';

export let csr = false;

export async function load() {
	const productsClient = new CmsClient.ProductsClient();
	const products = await productsClient.getProducts();

	const productModel = new CmsClient.Product();
	const productKeys = Object.keys(productModel);

	return {
		products: products ? (JSON.parse(JSON.stringify(products)) as typeof products) : null,
		productKeys: productKeys
	};
}
