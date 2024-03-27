<template>
    <Container
        :loading="pending"
        :error="error?.toString()"
        unwidth
    >
        <div class="flex fill skill-stats overflow">
            <aside class="flex row scroll-y">
                <div class="flex">
                    <Image :src="pokemon?.sprite ?? undefined" size="65px" />
                    <h2 class="pad-left center-vert">{{ pokemon?.pokemon?.name }}</h2>
                </div>

                <div class="flex">
                    <p class="center-vert fill">Nature: </p>
                    <SelectBox class="center-vert" v-model="nature" style="min-width: 150px;">
                        <option
                            v-for="nature in natures"
                            :key="nature.id"
                            :value="nature.name"
                        >
                            {{ nature.name }}
                            {{ natureBuff(nature) }}
                        </option>
                    </SelectBox>
                </div>

                <div class="flex margin-top margin-bottom">
                    <h2 class="fill"></h2>
                    <p class="margin-left stat">&nbsp;IV:</p>
                    <p class="margin-left stat">&nbsp;EV:</p>
                </div>

                <div class="flex" v-for="stat of statMap" :key="stat.key">
                    <p class="fill center-vert" :style="`color:${stat.color};`">{{ stat.name }}:</p>
                    <InputGroup
                        v-model="stats[stat.key].iv"
                        type="number" icon="" placeholder="IV"
                        min="0" :max="MAX_IV" step="1"
                        show-paste="false" show-remove="false"
                    >
                        <template #input>
                            <IconBtn
                                icon="keyboard_double_arrow_up"
                                inline
                                @click="stats[stat.key].iv = MAX_IV"
                                title="Max IV"
                                :disabled="stats[stat.key].iv === MAX_IV"
                            />
                            <IconBtn
                                icon="keyboard_double_arrow_down"
                                inline
                                @click="stats[stat.key].iv = 0"
                                title="Max IV"
                                :disabled="stats[stat.key].iv === 0"
                            />
                        </template>
                    </InputGroup>
                    <InputGroup
                        class="margin-left"
                        v-model="stats[stat.key].ev"
                        type="number" icon="" placeholder="EV"
                        min="0" step="5" :max="MAX_EV"
                        show-paste="false" show-remove="false"
                    >
                        <template #input>
                            <IconBtn
                                icon="keyboard_double_arrow_up"
                                inline
                                @click="stats[stat.key].ev = MAX_EV"
                                title="Max IV"
                                :disabled="stats[stat.key].ev === MAX_EV"
                            />
                            <IconBtn
                                icon="keyboard_double_arrow_down"
                                inline
                                @click="stats[stat.key].ev = 0"
                                title="Max IV"
                                :disabled="stats[stat.key].ev === 0"
                            />
                        </template>
                    </InputGroup>
                </div>

                <div class="flex margin-top">
                    <IconBtn
                        color="secondary"
                        icon="expand_less"
                        text="Max IVs"
                        @click="maxIvs"
                    />
                    <IconBtn
                        color="primary"
                        icon="refresh"
                        text="Compute"
                        class="pad-left"
                        @click="compute"
                    />
                </div>

                <p class="margin-top">Stat Requirements:</p>
                <ul>
                    <li :class="ivValid ? 'valid' : 'invalid'">Each IV has to be less than {{ MAX_IV + 1 }}</li>
                    <li :class="ivTotal > MAX_TOTAL_IV ? 'invalid' : 'valid'">Total IVs less than {{ MAX_TOTAL_IV }} (Current: {{ ivTotal }})</li>
                    <li :class="evValid ? 'valid' : 'invalid'">Each EV has to be less than {{ MAX_EV + 1 }}</li>
                    <li :class="evTotal > MAX_TOTAL_EV ? 'invalid' : 'valid'">Total EVs less than {{ MAX_TOTAL_EV }} (Current: {{ evTotal }})</li>
                </ul>
                <p class="margin-top">Optimize EVs for:</p>
                <ul>
                    <li v-for="stat of statMap" :key="stat.key">
                        <CheckBox v-model="optiStats[stat.key]">
                            {{ stat.name }} (Base: {{ baseData[stat.key] }})
                        </CheckBox>
                    </li>
                </ul>

            </aside>

            <div class="flex fill row" v-if="chartData">
                <Line :data="chartData" :options="<any>chartConfig" height="500px" />
            </div>
        </div>
    </Container>
</template>

<script lang="ts" setup>
import type { ChartOptions, ChartData } from 'chart.js';
import type { PokemonNature } from 'pokeapi-js-wrapper';
import { Line } from 'vue-chartjs';
import { DEFAULT_STATS, StatMap, type PokemonStats } from '~/models';

const {
    MAX_IV, MAX_EV,
    MAX_TOTAL_EV, MAX_TOTAL_IV,
    TIMEOUT
} = { MAX_IV: 31, MAX_EV: 252, MAX_TOTAL_EV: 510, MAX_TOTAL_IV: 186, TIMEOUT: 250 };

const route = useRoute();
const { fullPokemon, allNatures } = usePokemon();
const { computeStats, computeChart, getBase } = usePokemonStats();
const { keys, debounce } = useUtils();

const rawLoading = ref(false);

const name = computed(() => route.params.name?.toString());
const { data: pokemon, pending: pokemonPending, error: pokemonError } = await fullPokemon(name.value);
const { data: natures, pending: naturesPending, error: naturesError } = await allNatures();

const pending = computed(() => rawLoading.value || pokemonPending.value || naturesPending.value);
const error = computed(() => naturesError.value?.toString() ?? pokemonError.value?.toString());

const nature = ref(route.query.nature?.toString() ?? 'hardy');
const natureData = computed(() => natures.value?.find(n => n.name === nature.value));
const baseData = computed(() => {
    if (!pokemon.value?.pokemon) return DEFAULT_STATS;

    return {
        hp: getBase(pokemon.value?.pokemon, 'hp'),
        attack: getBase(pokemon.value?.pokemon, 'attack'),
        defense: getBase(pokemon.value?.pokemon, 'defense'),
        specialAttack: getBase(pokemon.value?.pokemon, 'specialAttack'),
        specialDefense: getBase(pokemon.value?.pokemon, 'specialDefense'),
        speed: getBase(pokemon.value?.pokemon, 'speed')
    };
})
const statMap = ref<{
    key: keyof PokemonStats;
    name: string;
    color: string;
    pkapi: string;
}[]>(<any>keys(StatMap).map(key => StatMap[key]));
const stats = reactive<PokemonStats>({ ...DEFAULT_STATS });
const optiStats = reactive<{
    [key in keyof PokemonStats]: boolean;
}>({
    hp: false,
    attack: false,
    defense: false,
    specialAttack: false,
    specialDefense: false,
    speed: false
})
const evTotal = computed(() => keys(stats).reduce((acc, key) => acc + stats[key].ev, 0));
const ivTotal = computed(() => keys(stats).reduce((acc, key) => acc + stats[key].iv, 0));
const evValid = computed(() => {
    for(const key of keys(stats)) {
        const value = stats[key].ev;
        if (value > MAX_EV || value < 0) return false;
    }
    return true;
});
const ivValid = computed(() => {
    for(const key of keys(stats)) {
        const value = stats[key].iv;
        if (value > MAX_IV || value < 0) return false;
    }
    return true;
});

const chartData = ref<ChartData<'line'>>();
const chartConfig = ref<ChartOptions<'line'>>({
    responsive: true,
    maintainAspectRatio: false,
    scales: {
        x: {
            type: 'linear',
            title: {
                display: true,
                text: 'Level'
            },
            ticks: {
                stepSize: 5,
                min: 0,
                max: 100
            }
        },
        y: {
            title: {
                display: true,
                text: 'Stat Value'
            }
        }
    }
});

const natureBuff = (nature: PokemonNature) => {
    const increased = statMap.value.find(stat => stat.pkapi === nature.increased_stat?.name);
    const decreased = statMap.value.find(stat => stat.pkapi === nature.decreased_stat?.name);

    if (increased === decreased) return ' (Neutral)';

    return ` (+${increased?.name} / -${decreased?.name})`;
}

const maxIvs = () => {
    keys(stats)
        .forEach(key => stats[key].iv = MAX_IV);
}

const optimize = () => {
    const statCount = keys(optiStats)
        .filter(key => optiStats[key])
        .length;

    if (statCount === 0) return;

    let perStat = Math.floor(MAX_TOTAL_EV / statCount);
    if (perStat > MAX_EV) perStat = MAX_EV;
    let leftOver = MAX_TOTAL_EV - (perStat * statCount);

    keys(optiStats)
        .forEach(key => {
            const should = optiStats[key];

            if(should) {
                stats[key].ev = perStat;
                return;
            }

            if (leftOver >= 0 && leftOver > MAX_EV) {
                stats[key].ev = MAX_EV;
                leftOver -= MAX_EV;
                return;
            }

            stats[key].ev = leftOver;
            leftOver = 0;
        });
}

const compute = async () => {
    if (!natureData.value || !pokemon.value?.pokemon) return;

    const data = await computeStats(pokemon.value.pokemon, natureData.value, stats);
    chartData.value = computeChart(data);
}

const doCompute = debounce<void>(() => compute(), TIMEOUT);
const doOptimize = debounce<void>(() => optimize(), TIMEOUT);

watch([natureData, stats], () => doCompute(), { immediate: true });
watch(optiStats, () => doOptimize(), { immediate: true });
</script>

<style scoped lang="scss">
.skill-stats {
    aside {
        width: 475px;
        padding-right: 10px;
        margin-right: var(--margin);

        .stat {
            width: 140px;
        }

        .invalid {
            color: red;
        }

        ul {
            margin: 0;
        }
    }

}
</style>
