import { Pokedex, type PokemonNature } from 'pokeapi-js-wrapper';

const instance = new Pokedex();

type Request<T> = (instance: Pokedex) => Promise<T>;

export const usePokemon = () => {
    const get = <T>(r: Request<T>, key: string) => useAsyncData(key, async () => await r(instance));

    const raws = {
        pokemon: (id: string | number) => get(i => i.getPokemonByName(id), 'pokemon-' + id),
        pokemons: (id: (string | number)[]) => get(i => i.getPokemonByName(id), 'pokemons-' + id.join('-')),
    }

    const fullPokemon = async (id: string | number) => {
        const data = await instance.getPokemonByName(id);

        const form = (data.forms.length >= 1 ? data.forms[0] : null)?.name;
        const apiForm = (form ? await instance.getPokemonFormByName(form) : undefined);

        const output = {
            pokemon: data,
            form: apiForm,
            sprite: apiForm?.sprites?.front_default
        };

        return output;
    }

    const allNatures = async () => {
        const STORAGE_KEY = 'pk-natures';

        const rawNatures = async () => {
            const natures = await instance.getNaturesList();
            const results =  await instance.getNatureByName(natures.results.map(n => n.name));

            localStorage.setItem(STORAGE_KEY, JSON.stringify(results));
            return results;
        }

        const fromStorage = () => {
            if (!process.client) return undefined;

            const data = localStorage.getItem(STORAGE_KEY);
            if (!data) return undefined;

            return <PokemonNature[]>JSON.parse(data);
        }

        return fromStorage() ?? await rawNatures();
    }

    return {
        get,
        fullPokemon: (id: string | number) => get(_ => fullPokemon(id), 'pokemon-' + id),
        allNatures: () => get(allNatures, 'natures'),
        rawNatures: allNatures,
        ...raws,
    }
}
