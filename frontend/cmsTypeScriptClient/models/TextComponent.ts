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

import { exists, mapValues } from '../runtime';
import type { Component } from './Component';
import {
    ComponentFromJSON,
    ComponentFromJSONTyped,
    ComponentToJSON,
} from './Component';

/**
 * 
 * @export
 * @interface TextComponent
 */
export interface TextComponent extends Component {
    /**
     * 
     * @type {string}
     * @memberof TextComponent
     */
    text?: string | null;
}

/**
 * Check if a given object implements the TextComponent interface.
 */
export function instanceOfTextComponent(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function TextComponentFromJSON(json: any): TextComponent {
    return TextComponentFromJSONTyped(json, false);
}

export function TextComponentFromJSONTyped(json: any, ignoreDiscriminator: boolean): TextComponent {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        ...ComponentFromJSONTyped(json, ignoreDiscriminator),
        'text': !exists(json, 'text') ? undefined : json['text'],
    };
}

export function TextComponentToJSON(value?: TextComponent | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        ...ComponentToJSON(value),
        'text': value.text,
    };
}

