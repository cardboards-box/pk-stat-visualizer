<template>
    <NuxtLayout>
        <NuxtPage />
    </NuxtLayout>
</template>

<script setup lang="ts">
const { accessibilityMode } = useSettingsHelper();

// onMounted(() => nextTick(() => setTimeout(async () => {

// }, 100)));

const ACCESS_STYLES = [
    {
        tag: '--color-muted',
        normal: '#555',
        access: '#fff'
    }, {
        tag: '--color',
        normal: '#dcddde',
        access: '#fff'
    }, {
        tag: '--font-size',
        normal: '14px',
        access: '16px'
    }
]

const setAccessibility = (value: boolean) => {
    if (!process.client) return;

    for(let style of ACCESS_STYLES) {
        document.documentElement.style.setProperty(style.tag, style[value ? 'access' : 'normal']);
    }
}

useSeoMeta({
    title: 'PK Stat Visualizer | Cardboard Box',
    ogTitle: 'PK Stat Visualizer | Cardboard Box',
    description: 'Visualizing stat changes in Pokémon games.',
    ogDescription: 'Visualizing stat changes in Pokémon games.',
    keywords: 'Pokemon Stats IV EV DV CardboardBox',
    ogImageUrl: '/logos/logo.png',

})

watch(() => accessibilityMode.value, setAccessibility, { immediate: true });
</script>
