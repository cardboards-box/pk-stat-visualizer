
export type Book<T = any> = {
    cover: string;
    title: string;
    blur: boolean;
    link?: string;
    context?: T;
}

export const TABLEBOOK_DEFAULTS = (() => {

    const width = 18.5, heightMod = 1.2486;

    return {
        unit: 'em',
        width,
        height: width * heightMod,
        heightMod,
        margin: {
            y: 25,
            x: 0
        },
        push: 200,
        perspective: 60,
        rotateX: 58,
        rotateZ: -34,
        skewY: -7
    }
})();
