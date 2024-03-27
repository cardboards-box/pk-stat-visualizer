import type { Pokemon, PokemonNature } from "pokeapi-js-wrapper";
import type { PokemonStats, StatResult } from "~/models";
import { StatMap } from "~/models";

export const usePokemonStats = () => {
    const { rawNatures } = usePokemon();
    const { keys } = useUtils();

    const regularStat = (
        level: number, base: number,
        iv: number, ev: number,
        buff: boolean, nerf: boolean
    ) => {
        const nature = buff && nerf ? 1 : buff ? 1.1 : nerf ? 0.9 : 1;
        return Math.floor((Math.floor(0.01 * (2 * base + iv + Math.floor(0.25 * ev)) * level) + 5) * nature);
    }

    const hpStat = (level: number, base: number, iv: number, ev: number) => {
        return Math.floor(0.01 * (2 * base + iv + Math.floor(0.25 * ev)) * level) + level + 10;
    }

    const determineNature = async (nature: PokemonNature | string) => {
        if (typeof nature !== 'string') return nature;

        const all = await rawNatures();
        return all.find(n => n.name === nature);
    }

    const getBase = (pokemon: Pokemon, key: keyof PokemonStats) => {
        return pokemon.stats.find(s => s.stat.name === StatMap[key].pkapi)?.base_stat ?? 0;
    }

    const natureEffect = (name: string, nature: PokemonNature) => {
        return {
            buff: nature.increased_stat?.name === name,
            nerf: nature.decreased_stat?.name === name
        }
    }

    const stateNatures = (nature: PokemonNature, pokemon: Pokemon, stats: PokemonStats) => {
        return keys(stats).map(key => ({
            ...StatMap[key],
            base: getBase(pokemon, key),
            ...stats[key],
            ...natureEffect(key, nature)
        }));
    }

    const computeStats = async (
        pokemon: Pokemon,
        nature: PokemonNature | string,
        stats: PokemonStats
    ) => {
        const actNature = await determineNature(nature);
        if (!actNature) return [];

        const actStats = stateNatures(actNature, pokemon, stats);
        return Array.from({ length: 100 }, (_, i) => {
            const level = i + 1;
            let output: StatResult = {
                level,
                hp: 0,
                attack: 0,
                defense: 0,
                specialAttack: 0,
                specialDefense: 0,
                speed: 0,
            };

            actStats.forEach(stat => {
                const key = <keyof StatResult>stat.key;
                output[key] = stat.key === 'hp' ?
                    hpStat(level, stat.base, stat.iv, stat.ev) :
                    regularStat(level, stat.base, stat.iv, stat.ev, stat.buff, stat.nerf);
            });

            return output;
        });
    }

    const computeChart = (data: StatResult[]) => {
        const labels = Array(100).fill(0).map((_, i) => (i + 1).toString());
        const stats = keys(StatMap);

        return {
            labels,
            datasets: stats.map(key => {
                return {
                    label: StatMap[key].name,
                    data: data.map(d => d[key]),
                    borderColor: StatMap[key].color
                }
            }),
        }
    };

    return {
        getBase,
        computeStats,
        computeChart
    }
}
