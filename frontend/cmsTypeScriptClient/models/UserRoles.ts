/* tslint:disable */
/* eslint-disable */
/**
 * ecommerce, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


/**
 * 
 * @export
 */
export const UserRoles = {
    NUMBER_0: 0
} as const;
export type UserRoles = typeof UserRoles[keyof typeof UserRoles];


export function UserRolesFromJSON(json: any): UserRoles {
    return UserRolesFromJSONTyped(json, false);
}

export function UserRolesFromJSONTyped(json: any, ignoreDiscriminator: boolean): UserRoles {
    return json as UserRoles;
}

export function UserRolesToJSON(value?: UserRoles | null): any {
    return value as any;
}
