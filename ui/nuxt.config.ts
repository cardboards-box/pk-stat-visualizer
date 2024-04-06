// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
    devtools: { enabled: true },
    ssr: false,

    app: {
        head: {
            link: [
                { rel: 'preconnect', href: 'https://fonts.gstatic.com' },
                { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&amp;display=swap' },
                { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Kolker+Brush&display=swap' },
                { rel: 'stylesheet', href: 'https://fonts.googleapis.com/icon?family=Material+Icons' },
                { rel: 'stylesheet', href: 'https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200' },
            ],
            noscript: [
                { children: 'JavaScript is required' }
            ]
        }
    },

    imports: {
        dirs: [
            'composables/**'
        ]
    },

    css: [
        '@/node_modules/highlight.js/styles/vs2015.css',
        '@/styles/variables.scss',
        '@/styles/reset.scss',
        '@/styles/styles.scss',
        '@/styles/layout.scss',
        '@/styles/controls.scss',
    ],
    components: [
        '~/components',
        '~/components/buttons',
        '~/components/controls',
        '~/components/general',
        '~/components/layouts',
        '~/components/stages',
        '~/components/tabs',
    ],

    modules: ["@nuxt/image"]
})
