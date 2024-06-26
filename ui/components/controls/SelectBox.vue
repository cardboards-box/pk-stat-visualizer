<template>
    <div class="select-styled" :class="actClasses" :style="actStyles">
        <select v-model="value" :disabled="isTrue(disabled)" :aria-disabled="isTrue(disabled)">
            <slot />
        </select>
    </div>
</template>

<script setup lang="ts">
import type { ClassOptions, StyleOptions, booleanishext } from '~/models';
const { isTrue, serClasses, serStyles } = useUtils();

const props = defineProps<{
    modelValue: unknown;
    disabled?: booleanishext;
    transparent?: booleanishext;
    fill?: booleanishext;
    style?: StyleOptions;
    'class'?: ClassOptions;
}>();

const emits = defineEmits<{
    (e: 'update:modelValue', v: unknown): void;
}>();

const value = computed({
    get: () => props.modelValue,
    set: (val: any) => emits('update:modelValue', val)
});

const actStyles = computed(() => serStyles(props.style));
const actClasses = computed(() => serClasses({
    'transparent': isTrue(props.transparent),
    'fill': isTrue(props.fill),
}, props.class));

</script>

<style lang="scss">
$ctrl-bg: var(--bg-color-accent);

.select-styled {
    position: relative;
    background-color: $ctrl-bg;
    color: var(--color);
    padding: 10px;
    border-radius: var(--brd-radius);
    outline: none;
    border: 1px solid #{$ctrl-bg};
    font-family: var(--font-family);
    transition: all 250ms;

    select {
        background-color: transparent;
        -moz-appearance: none;
        -webkit-appearance: none;
        appearance: none;
        padding: 0;
        margin: 0;
        width: 100%;
        height: 100%;
        border: none;

        option {
            padding: 5px;
        }
    }

    &:after {
        content: '\0025BC';
        color: white;
        font-size: 10px;
        pointer-events: none;
        position: absolute;
        top: 50%;
        right: 10px;
        transform: translateY(-50%);
    }

    &.transparent {
        background-color: transparent;
        border: 1px solid transparent;
    }
}</style>
