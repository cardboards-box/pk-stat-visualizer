import type { ClassOptions, booleanish, booleanishext, StyleOptions } from "~/models";

export const useUtils = () => {
    const route = useRoute();

    const trigger = useState<boolean>('ui-refresh-trigger', () => false);

    (() => {
        if (!process.client || (window && 'watcherTimer' in window)) return;

        (<any>window).watcherTimer = setInterval(async () => {
            trigger.value = !trigger.value;
        }, 1000);
    })();


    /**
     * Stop a function from being called too often
     * @param fun The function that should be called
     * @param wait How long to wait before the function is called
     * @returns A wrapping function for the original function that is debounced
     */
    const debounce = <T>(fun: (arg: T) => void, wait: number) => {
        let timer: number;
        return (arg: T) => {
            if (timer) clearTimeout(timer);
            timer = <any>setTimeout(() => {
                fun(arg);
            }, wait);
        }
    }

    /**
     * Waits to execute a certain function again until a certain amount of time has passed
     * @param fun The function to be executed
     * @param wait How long to wait before the function can be called again
     * @returns A wrapping function for the original function that is throttled
     */
    const throttle = <T>(fun: (arg: T) => void, wait: number) => {
        let throttled = false;
        return (arg: T) => {
            if (throttled) return;

            fun(arg);
            throttled = true;
            setTimeout(() => {
                throttled = false;
            }, wait);
        }
    }

    /**
     * Returns the relative version of a date
     * @param inDate The date to check against
     * @param full whether or not to use the full format
     * @returns The relative version of the date
     */
    const dateFormatLocal = (inDate: string, full = false): string => {
        const date = Date.parse(inDate);
        const units = {
            year: 24 * 60 * 60 * 1000 * 365,
            month: (24 * 60 * 60 * 1000 * 365) / 12,
            day: 24 * 60 * 60 * 1000,
            hour: 60 * 60 * 1000,
            minute: 60 * 1000,
            second: 1000,
        };
        const elapsed = date - Date.now();
        for (const key in units) {
            const u = key as keyof typeof units;
            if (Math.abs(elapsed) > units[u] || u == "second") {
                try {
                    // @ts-ignore
                    const rtf = new Intl.RelativeTimeFormat("en", {
                        numeric: "auto",
                        style: !full ? "narrow" : undefined,
                    });
                    return rtf.format(Math.round(elapsed / units[u]), u);
                } catch (error) {
                    console.warn(error);
                    return `${-Math.round(elapsed / units[u])} ${u + (-Math.round(elapsed / units[u]) > 1 ? "s" : "")} ago`;
                }
            }
        }
        return "A long time ago";
    }

    /**
     * Returns a short version of the date
     * @param inDate The date to check against
     * @returns The shortened version of the date
     */
    const dateFormatMicro = (inDate: string): string => {
        const date = Date.parse(inDate);
        const units = {
            y: 24 * 60 * 60 * 1000 * 365,
            mo: (24 * 60 * 60 * 1000 * 365) / 12,
            d: 24 * 60 * 60 * 1000,
            h: 60 * 60 * 1000,
            m: 60 * 1000,
            s: 1000,
        };
        const elapsed = date - Date.now();
        for (const key in units) {
            const u = key as keyof typeof units;
            if (Math.abs(elapsed) > units[u] || u == "s") {
                return `${-Math.round(elapsed / units[u])}${u}`;
            }
        }
        return "∞";
    }

    /**
     * Converts the given byte count to a human readable format
     * @param size The byte count to convert
     * @returns The converted byte count
     */
    const fileSizeMicro = (size: number) => {
        const units = {
            gb: 1024 * 1024 * 1024,
            mb: 1024 * 1024,
            kb: 1024,
            b: 1,
        }

        for (const key in units) {
            const u = key as keyof typeof units;
            if (size > units[u] || u == "b") {
                return `${(size / units[u]).toFixed(2)}${u}`;
            }
        }
        return "∞";
    }

    /**
     * Clones the given object
     * @param val The object to clone
     * @returns The cloned value
     */
    const clone = <T>(val: T) => {
        if (val === undefined) return undefined;

        return <T>JSON.parse(JSON.stringify(val));
    }

    /**
     * Bitwise ORs all the given inputs together
     * @param inputs The inputs to OR together
     * @returns The ORed value
     */
    const shiftFlag = (inputs: number[]) => {
        let output = 0;

        for (let inp of inputs) {
            output = output | inp;
        }

        return output;
    };

    /**
     * Checks if the given input has the given flag
     * @param input The input to check
     * @param flag The flag to check for
     * @returns Whether or not the flag is present
     */
    const hasFlag = (input: number, flag: number) => (input & flag) !== 0;

    /**
     * Creates an array of all of the values that are present in the input
     * @param input The input to check
     * @param options The available flags
     * @returns The array of flags that are present
     */
    const unshiftFlag = (input: number, options: number[]) => {
        let output = [];

        for (let opt of options) {
            if (hasFlag(input, opt)) output.push(opt);
        }

        return output;
    }

    /**
     * Gets the current max width of the page from various document properties
     * @returns The current max width of the page
     */
    const getWidth = () => {
        return Math.max(
            document.body.scrollWidth,
            document.documentElement.scrollWidth,
            document.body.offsetWidth,
            document.documentElement.offsetWidth,
            document.documentElement.clientWidth
        );
    };

    /**
     * Checks if the current page size is under the given max width
     * @param maxWidth The max width to check against (defaults to 1050px)
     * @returns Whether or not the page is under the given max width
     */
    const isUnderMaxWidth = (maxWidth: number = 1050) => getWidth() <= maxWidth;

    /**
     * Gets all of the keys of the current object with the correct type (stupid typescript)
     * @param obj The object to get the keys from
     * @returns The keys of the object
     */
    const keys = <T extends object>(obj: T) => Object.keys(obj) as (keyof T)[];

    /**
     * Sets the title of the current page with consideration for the route
     * @param title The title to set the page to
     */
    const setTitle = (title: string) => {
        const admin = route.path.indexOf('/admin') !== -1;

        useSeoMeta({ title: `${title} | ${admin ? 'Admin Panel' : 'Dashboard'}`})
    }

    /**
     * Whether or not the given URL is external or something on the current site
     * @param url The url to check against
     * @returns Whether or not the URL is external
     */
    const isExternal = (url?: string) => !!url && url.toLowerCase().startsWith('http');

    /**
     * Requests a lock from the browser to prevent the browser from backgrounding the tab (helps keep websockets alive)
     * @param lockName The name of the lock to request
     * @param shared Whether or not the lock should be shared with other tabs
     * @returns A function that removes the lock
     */
    const requestLock = (
        lockName: string = 'tab_persistence_lock',
        shared: boolean = true
    ) => {
        type Resolver = () => void;

        if (!navigator ||
            !navigator.locks ||
            !navigator.locks.request) return () => {};

        let resolver: Resolver = () => {};

        const promise = new Promise((resolve) => {
            resolver = () => resolve(undefined);
        });

        navigator.locks.request(
            lockName,
            { mode: shared ? 'shared' : 'exclusive' },
            () => {
                return promise;
            });

        return resolver;
    }

    /**
     * A polyfill for generating UUIDs
     * @returns The UUID that has been generated
     */
    const uuid = () => {
        try {
            return crypto.randomUUID();
        } catch  {
            return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
                (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
            );
        }
    }

    /**
     * Gets all of the unique values from the given array based on the given predicate
     * @param arr The array to get the unique values from
     * @param pred The predicate to use to determine uniqueness
     * @returns The unique values from the array
     */
    const unique = <T>(arr: T[], pred: (item: T) => any) => {
        return [...new Map(arr.map(item => [pred(item), item])).values()];
    }

    /**
     * Serializes the given class options into a valid class list
     * @param classes The classes to serialize to a string
     * @returns The string representation of the classes
     */
    const serClass = (classes: ClassOptions) => {
        if (classes === undefined || classes === null) return '';

        if (typeof classes === 'string') return classes;
        if (Array.isArray(classes)) return classes.join(' ');

        return Object.keys(classes)
            .filter(key => classes[key])
            .join(' ');
    }

    /**
     * Serializes all of the class options into a valid class list
     * @param classes All of the classes to serialize
     * @returns The string representation of the classes
     */
    const serClasses = (...classes: ClassOptions[]): string => {
        return classes.map(serClass).join(' ');
    }

    /**
     * Checks if the given booleanish value is true
     * @param value The value to check
     * @returns Whether or not the value is true
     */
    const isTrue = (value?: booleanish | booleanishext) => {
        if (value === 'false' || value === '0') return false;
        return value === '' || value === 'true' || value === '1' || !!value;
    }

    /**
     * Serializes the given style options into a valid style string
     * @param style the style options
     * @returns the valid style string
     */
    const serStyle = (style: StyleOptions) => {
        if (style === undefined || style === null) return '';
        if (typeof style === 'string') return style;
        if (Array.isArray(style)) return style.join('; ');

        return Object.keys(style)
            .filter(key => style[key] !== undefined && style[key] !== null)
            .map(key => `${key}: ${style[key]}`)
            .join('; ');
    };

    /**
     * Serializes all of the given style options into a valid style string
     * @param styles The style options
     * @returns The valid style string
     */
    const serStyles = (...styles: StyleOptions[]): string => {
        return styles.map(serStyle).join('; ');
    }

    /**
     * Calculates the scroll percentage of the given scroller
     * @param scroller The element that was scrolled
     * @returns The scroll percentage
     */
    const scrollPercent = (scroller: HTMLElement) => {
        if (scroller.scrollHeight == scroller.clientHeight) return 100;
        return Math.floor(scroller.scrollTop / (scroller.scrollHeight - scroller.clientHeight) * 100);
    };

    /**
     * Writes the given text to the clipboard
     * @param text The text to write
     * @returns The promise that resolves when the text has been written to the clipboard
     */
    const writeToClipboard = (text: string) => {
        return navigator.clipboard.writeText(text);
    }

    /**
     * Gets the current site's base URL
     * @returns The current site's base URL
     */
    const baseUrl = () => {
        return `${window.location.protocol}//${window.location.host}`;
    }

    /**
     * Proxies the given image URL
     * @param url the URL to proxy
     * @param group the type of proxy to use
     * @param referer the optional referer to use
     * @returns the proxied URL
     */
    const proxy = (url: string, group: string = 'r6mt', referer?: string) => {
        const path = encodeURIComponent(url);
        let uri = `https://cba-proxy.index-0.com/proxy?path=${path}&group=${group}`;

        if (referer) uri += `&referer=${encodeURIComponent(referer)}`;

        return uri;
    }

    /**
     * Sets the SEO meta data for the given information
     * @param title The title of the page
     * @param description The description of the page
     * @param image The image to use
     */
    const setMeta = (title?: string, description?: string, image?: string) => {
        description ??= 'R6 Match Tracker - Keep tabs on how your favourite teams are doing!';
        title ??= 'R6 Match Tracker';
        image ??= '/logos/logo.png';

        useSeoMeta({
            title,
            ogTitle: title,
            description,
            ogDescription: description,
            ogImageUrl: image
        });
    }

    /**
     * Combines the given values into a human readable string
     * @param values The values to combine
     * @returns The concatenated string
     */
    function humanJoin(values: string[]): string;
    /**
     * Combines the given values into a human readable string
     * @param values The values to combine
     * @returns The concatenated string
     */
    function humanJoin(...values: string[]): string;
    /**
     * Combines the given values into a human readable string
     * @param value The values to combine
     * @param values The values to combine
     * @returns The concatenated string
     */
    function humanJoin(value: string | string[], ...values: string[]) {
        let all = [
            ...(Array.isArray(value) ? value : [value]),
            ...values
        ];

        if (all.length === 0) return '';
        if (all.length === 1) return all[0];

        return `${all.slice(0, -1).join(', ')} and ${all[all.length - 1]}`;
    }

    return {
        debounce,
        throttle,
        dateFormatLocal,
        dateFormatMicro,
        clone,
        shiftFlag,
        unshiftFlag,
        hasFlag,
        getWidth,
        isUnderMaxWidth,
        isExternal,
        keys,
        setTitle,
        requestLock,
        uuid,
        unique,
        serClasses,
        serStyles,
        isTrue,
        fileSizeMicro,
        scrollPercent,
        writeToClipboard,
        baseUrl,
        trigger,
        proxy,
        setMeta,
        humanJoin
    }
}
