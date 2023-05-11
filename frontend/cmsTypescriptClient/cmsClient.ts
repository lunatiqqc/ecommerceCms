//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

export module CmsClient {
	export class BaseCmsClient {
		constructor() {}
	}

	export class PagesClient extends BaseCmsClient {
		private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
		private baseUrl: string;
		protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

		constructor(
			baseUrl?: string,
			http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }
		) {
			super();
			this.http = http ? http : (window as any);
			this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : 'http://localhost:5059';
		}

		getPages(): Promise<Page[] | null> {
			let url_ = this.baseUrl + '/api/Pages';
			url_ = url_.replace(/[?&]$/, '');

			let options_: RequestInit = {
				method: 'GET',
				headers: {
					Accept: 'application/json'
				}
			};

			return this.http.fetch(url_, options_).then((_response: Response) => {
				return this.processGetPages(_response);
			});
		}

		protected processGetPages(response: Response): Promise<Page[] | null> {
			const status = response.status;
			let _headers: any = {};
			if (response.headers && response.headers.forEach) {
				response.headers.forEach((v: any, k: any) => (_headers[k] = v));
			}
			if (status === 200) {
				return response.text().then((_responseText) => {
					let result200: any = null;
					let resultData200 =
						_responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
					if (Array.isArray(resultData200)) {
						result200 = [] as any;
						for (let item of resultData200) result200!.push(Page.fromJS(item));
					} else {
						result200 = <any>null;
					}
					return result200;
				});
			} else if (status !== 200 && status !== 204) {
				return response.text().then((_responseText) => {
					return throwException(
						'An unexpected server error occurred.',
						status,
						_responseText,
						_headers
					);
				});
			}
			return Promise.resolve<Page[] | null>(null as any);
		}
	}

	export class ProductsClient extends BaseCmsClient {
		private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
		private baseUrl: string;
		protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

		constructor(
			baseUrl?: string,
			http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }
		) {
			super();
			this.http = http ? http : (window as any);
			this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : 'http://localhost:5059';
		}

		getProducts(): Promise<Product[] | null> {
			let url_ = this.baseUrl + '/api/Products';
			url_ = url_.replace(/[?&]$/, '');

			let options_: RequestInit = {
				method: 'GET',
				headers: {
					Accept: 'application/json'
				}
			};

			return this.http.fetch(url_, options_).then((_response: Response) => {
				return this.processGetProducts(_response);
			});
		}

		protected processGetProducts(response: Response): Promise<Product[] | null> {
			const status = response.status;
			let _headers: any = {};
			if (response.headers && response.headers.forEach) {
				response.headers.forEach((v: any, k: any) => (_headers[k] = v));
			}
			if (status === 200) {
				return response.text().then((_responseText) => {
					let result200: any = null;
					let resultData200 =
						_responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
					if (Array.isArray(resultData200)) {
						result200 = [] as any;
						for (let item of resultData200) result200!.push(Product.fromJS(item));
					} else {
						result200 = <any>null;
					}
					return result200;
				});
			} else if (status !== 200 && status !== 204) {
				return response.text().then((_responseText) => {
					return throwException(
						'An unexpected server error occurred.',
						status,
						_responseText,
						_headers
					);
				});
			}
			return Promise.resolve<Product[] | null>(null as any);
		}

		deleteProduct(id: number): Promise<Product | null> {
			let url_ = this.baseUrl + '/api/Products/{id}';
			if (id === undefined || id === null) throw new Error("The parameter 'id' must be defined.");
			url_ = url_.replace('{id}', encodeURIComponent('' + id));
			url_ = url_.replace(/[?&]$/, '');

			let options_: RequestInit = {
				method: 'DELETE',
				headers: {
					Accept: 'application/json'
				}
			};

			return this.http.fetch(url_, options_).then((_response: Response) => {
				return this.processDeleteProduct(_response);
			});
		}

		protected processDeleteProduct(response: Response): Promise<Product | null> {
			const status = response.status;
			let _headers: any = {};
			if (response.headers && response.headers.forEach) {
				response.headers.forEach((v: any, k: any) => (_headers[k] = v));
			}
			if (status === 200) {
				return response.text().then((_responseText) => {
					let result200: any = null;
					let resultData200 =
						_responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
					result200 = resultData200 ? Product.fromJS(resultData200) : <any>null;
					return result200;
				});
			} else if (status !== 200 && status !== 204) {
				return response.text().then((_responseText) => {
					return throwException(
						'An unexpected server error occurred.',
						status,
						_responseText,
						_headers
					);
				});
			}
			return Promise.resolve<Product | null>(null as any);
		}
	}

	export class Page {
		id!: number;
		title?: string | undefined;
		url?: string | undefined;
		children?: Page[] | undefined;
		parent?: Page | undefined;
		visibleInMenu!: boolean;
		requiredRole?: UserRoles | undefined;
		isSystemPage!: boolean;

		init(_data?: any) {
			if (_data) {
				this.id = _data['id'];
				this.title = _data['title'];
				this.url = _data['url'];
				if (Array.isArray(_data['children'])) {
					this.children = [] as any;
					for (let item of _data['children']) this.children!.push(Page.fromJS(item));
				}
				this.parent = _data['parent'] ? Page.fromJS(_data['parent']) : <any>undefined;
				this.visibleInMenu = _data['visibleInMenu'];
				this.requiredRole = _data['requiredRole'];
				this.isSystemPage = _data['isSystemPage'];
			}
		}

		static fromJS(data: any): Page {
			data = typeof data === 'object' ? data : {};
			let result = new Page();
			result.init(data);
			return result;
		}

		toJSON(data?: any) {
			data = typeof data === 'object' ? data : {};
			data['id'] = this.id;
			data['title'] = this.title;
			data['url'] = this.url;
			if (Array.isArray(this.children)) {
				data['children'] = [];
				for (let item of this.children) data['children'].push(item.toJSON());
			}
			data['parent'] = this.parent ? this.parent.toJSON() : <any>undefined;
			data['visibleInMenu'] = this.visibleInMenu;
			data['requiredRole'] = this.requiredRole;
			data['isSystemPage'] = this.isSystemPage;
			return data;
		}
	}

	export enum UserRoles {
		Administrator = 0
	}

	export class Product {
		id!: number;
		name?: string | undefined;
		description?: string | undefined;
		price?: number | undefined;
		stockQuantity?: number | undefined;
		productCategory?: ProductCategory | undefined;
		productFields?: ProductField[] | undefined;

		init(_data?: any) {
			if (_data) {
				this.id = _data['id'];
				this.name = _data['name'];
				this.description = _data['description'];
				this.price = _data['price'];
				this.stockQuantity = _data['stockQuantity'];
				this.productCategory = _data['productCategory']
					? ProductCategory.fromJS(_data['productCategory'])
					: <any>undefined;
				if (Array.isArray(_data['productFields'])) {
					this.productFields = [] as any;
					for (let item of _data['productFields'])
						this.productFields!.push(ProductField.fromJS(item));
				}
			}
		}

		static fromJS(data: any): Product {
			data = typeof data === 'object' ? data : {};
			let result = new Product();
			result.init(data);
			return result;
		}

		toJSON(data?: any) {
			data = typeof data === 'object' ? data : {};
			data['id'] = this.id;
			data['name'] = this.name;
			data['description'] = this.description;
			data['price'] = this.price;
			data['stockQuantity'] = this.stockQuantity;
			data['productCategory'] = this.productCategory
				? this.productCategory.toJSON()
				: <any>undefined;
			if (Array.isArray(this.productFields)) {
				data['productFields'] = [];
				for (let item of this.productFields) data['productFields'].push(item.toJSON());
			}
			return data;
		}
	}

	export class ProductCategory {
		id!: number;
		name?: string | undefined;
		description?: string | undefined;
		parentCategory?: ProductCategory | undefined;
		subcategories?: ProductCategory[] | undefined;

		init(_data?: any) {
			if (_data) {
				this.id = _data['id'];
				this.name = _data['name'];
				this.description = _data['description'];
				this.parentCategory = _data['parentCategory']
					? ProductCategory.fromJS(_data['parentCategory'])
					: <any>undefined;
				if (Array.isArray(_data['subcategories'])) {
					this.subcategories = [] as any;
					for (let item of _data['subcategories'])
						this.subcategories!.push(ProductCategory.fromJS(item));
				}
			}
		}

		static fromJS(data: any): ProductCategory {
			data = typeof data === 'object' ? data : {};
			let result = new ProductCategory();
			result.init(data);
			return result;
		}

		toJSON(data?: any) {
			data = typeof data === 'object' ? data : {};
			data['id'] = this.id;
			data['name'] = this.name;
			data['description'] = this.description;
			data['parentCategory'] = this.parentCategory ? this.parentCategory.toJSON() : <any>undefined;
			if (Array.isArray(this.subcategories)) {
				data['subcategories'] = [];
				for (let item of this.subcategories) data['subcategories'].push(item.toJSON());
			}
			return data;
		}
	}

	export class ProductField {
		id!: number;
		name?: string | undefined;
		description?: string | undefined;
		fieldType?: string | undefined;
		isEnabled?: boolean | undefined;
		value?: string | undefined;

		init(_data?: any) {
			if (_data) {
				this.id = _data['id'];
				this.name = _data['name'];
				this.description = _data['description'];
				this.fieldType = _data['fieldType'];
				this.isEnabled = _data['isEnabled'];
				this.value = _data['value'];
			}
		}

		static fromJS(data: any): ProductField {
			data = typeof data === 'object' ? data : {};
			let result = new ProductField();
			result.init(data);
			return result;
		}

		toJSON(data?: any) {
			data = typeof data === 'object' ? data : {};
			data['id'] = this.id;
			data['name'] = this.name;
			data['description'] = this.description;
			data['fieldType'] = this.fieldType;
			data['isEnabled'] = this.isEnabled;
			data['value'] = this.value;
			return data;
		}
	}

	export class ApiException extends Error {
		override message: string;
		status: number;
		response: string;
		headers: { [key: string]: any };
		result: any;

		constructor(
			message: string,
			status: number,
			response: string,
			headers: { [key: string]: any },
			result: any
		) {
			super();

			this.message = message;
			this.status = status;
			this.response = response;
			this.headers = headers;
			this.result = result;
		}

		protected isApiException = true;

		static isApiException(obj: any): obj is ApiException {
			return obj.isApiException === true;
		}
	}

	function throwException(
		message: string,
		status: number,
		response: string,
		headers: { [key: string]: any },
		result?: any
	): any {
		if (result !== null && result !== undefined) throw result;
		else throw new ApiException(message, status, response, headers, null);
	}

	export const window = { fetch: fetch };
}
