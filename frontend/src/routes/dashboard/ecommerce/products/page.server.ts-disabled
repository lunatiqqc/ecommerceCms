export async function load() {
	const productsClient = new CmsClient.ProductsClient();
	const products = await productsClient.get();

	const productModel = new CmsClient.Product();
	const productKeys = Object.keys(productModel).map((key) => {
		return {
			key: key,
			type: true ? 'number' : productModel.getFieldType(key as keyof typeof productModel)
		};
	});

	return {
		products: products,
		productKeys: productKeys,
		productsClient: JSON.stringify(productsClient)
	};
}
