<template>
    <Image
        :src="url.url"
        :default="default"
        :style="styles"
        :class="classes"
        :title="url.title"
        fit="contain"
    />
</template>

<script setup lang="ts">
import type { ClassOptions, StyleOptions, booleanish } from '~/models';

const { isTrue, serStyles, serClasses } = useUtils();

const props = defineProps<{
    value?: string;
    width?: string;
    height?: string;
    size?: string;
    round?: booleanish;
    default?: string;
    'class'?: ClassOptions;
    style?: StyleOptions;
    title?: string;
}>();

const actualValue = computed(() => props.value ?? '');

const styles = computed(() => {
    const styles: { [key: string]: string | undefined } = {
        'max-width': props.width || props.size,
        'max-height': props.height || props.size,
        'border-radius': isTrue(props.round) ? '50%' : undefined
    };

    if (url.value.url?.toLowerCase().endsWith('.svg') || !actualValue.value) {
        styles['height'] = props.height || props.size;
        styles['width'] = props.width || props.size;
    }

    return serStyles(styles, props.style);
});

const classes = computed(() => serClasses(props.class));

const url = computed(() => {
    const value = actualValue.value;

    if (!value) return { url: props.default, title: props.title };
    if (value.toLowerCase().startsWith('http')) return { url: value, title: props.title };

    const match =  /^<(a)?:(.*?):([0-9]{1,})>/.exec(value);
    if (match) {
        const { animated, name, id } = { animated: match[1], name: match[2], id: match[3] };
        const url = `https://cdn.discordapp.com/emojis/${id}.${animated ? 'gif' : 'png'}`;
        return {
            url: url,
            title: props.title ?? name ?? 'Discord Emote'
        }
    }

    return { url: props.default, title: props.title };
});
</script>

<style scoped lang="scss">
</style>
