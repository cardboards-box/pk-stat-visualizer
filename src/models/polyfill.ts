import type { AsyncData } from "#app";
import type { WritableComputedRef, Ref } from "vue";
import type { ApiResultData, ErrorResult } from "./base";
import { FetchError } from 'ofetch';

export type booleanish = boolean | 'true' | 'false' | '';
export type booleanishext = booleanish | '0' | '1';
export type Result<T> = AsyncData<ApiResultData<T> | null, FetchError<ErrorResult> | null>;
export type Dictionary<T> = { [key: string]: T };
export type KeyedDictionary<TKey extends string | number | symbol, TValue> = { [key in TKey]: TValue };
export type OKD<TKey extends string | number | symbol, TValue> = { [key in TKey]?: TValue };

export type ClassOptions = string | string[] | undefined | null | {
    [key: string]: booleanishext;
}

export type StyleOptions = string | string[] | undefined | null | {
    [key: string]: string | undefined | null;
}

export type RefNumber = Ref<number> | WritableComputedRef<number> | number;
