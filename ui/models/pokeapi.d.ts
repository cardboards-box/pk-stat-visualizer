declare module "pokeapi-js-wrapper" {
    export interface Pokemon {
        abilities: Ability[];
        base_experience: number;
        cries: Cries;
        forms: ApiResource[];
        game_indices: GameIndex[];
        height: number;
        held_items: HeldItem[];
        id: number;
        is_default: boolean;
        location_area_encounters: string;
        moves: Move[];
        name: string;
        order: number;
        past_abilities: any[];
        past_types: any[];
        species: ApiResource;
        sprites: Sprites;
        stats: Stat[];
        types: Type[];
        weight: number;
    }

    export interface Ability {
        ability: ApiResource;
        is_hidden: boolean;
        slot: number;
    }

    export interface ApiResource {
        name: string;
        url: string;
    }

    export interface Cries {
        latest: string;
        legacy: string;
    }

    export interface GameIndex {
        game_index: number;
        version: ApiResource;
    }

    export interface HeldItem {
        item: ApiResource;
        version_details: VersionDetail[];
    }

    export interface VersionDetail {
        rarity: number;
        version: ApiResource;
    }

    export interface Move {
        move: ApiResource;
        version_group_details: VersionGroupDetail[];
    }

    export interface VersionGroupDetail {
        level_learned_at: number;
        move_learn_method: ApiResource;
        version_group: ApiResource;
    }

    export interface Sprites {
        back_default: string | null;
        back_female: string | null;
        back_shiny: string | null;
        back_shiny_female: string | null;
        back_shiny_transparent: string | null;
        back_transparent: string | null;
        front_shiny_transparent: string | null;
        front_transparent: string | null;
        front_default: string | null;
        front_female: string | null;
        front_shiny: string | null;
        front_shiny_female: string | null;

        other?: { [key: string]: Sprites; }
        versions?: {
            [key: string]: {
                [key: string]: Sprites;
            }
        };
        animated?: Sprites;
    }

    export interface Stat {
        base_stat: number;
        effort: number;
        stat: ApiResource;
    }

    export interface Type {
        slot: number;
        type: ApiResource;
    }

    export interface ResourceList {
        count: number;
        next?: string;
        previous?: string;
        results: ApiResource[];
    }

    export interface PokemonForm {
        id: number;
        name: string;
        order: number;
        form_order: number;
        is_default: boolean;
        is_battle_only: boolean;
        is_mega: boolean;
        form_name: string;
        pokemon: ApiResource;
        sprites: Sprites;
        types: Type[];
        version_group: Pokemon;
    }

    export interface PokemonNature {
        decreased_stat: ApiResource | undefined;
        hates_flavor: ApiResource | undefined;
        id: number;
        increased_stat: ApiResource | undefined;
        likes_flavor: ApiResource | undefined;
        move_battle_style_preferences: MoveBattleStylePreference[];
        name: string;
        names: Name[];
        pokeathlon_stat_changes: PokeathlonStatChange[];
    }

    export interface MoveBattleStylePreference {
        high_hp_preference: number;
        low_hp_preference: number;
        move_battle_style: ApiResource;
    }

    export interface Name {
        language: ApiResource;
        name: string;
    }

    export interface PokeathlonStatChange {
        max_change: number;
        pokeathlon_stat: ApiResource;
    }

    export type NameIdsFunctions<T = object, K = string | number> = {
        (name: K): Promise<T>;
        (name: K[]): Promise<T[]>;
    }


    export class Pokedex {
        constructor(config?: {
            protocol: string,
            hostName: string,
            versionPath: string,
            offset: number
            limit: number,
            timeout: number,
            cache: boolean,
            cacheImages: boolean
        });

        getBerryFirmnessByName: NameIdsFunctions<object, string>;

        getContestEffectById: NameIdsFunctions<object, number>;
        getSuperContestEffectById: NameIdsFunctions<object, number>;
        getEvolutionChainById: NameIdsFunctions<object, number>;
        getMachineById: NameIdsFunctions<object, number>;
        getCharacteristicById: NameIdsFunctions<object, number>;

        getBerryByName: NameIdsFunctions;
        getBerryFlavorByName: NameIdsFunctions;
        getContestTypeByName: NameIdsFunctions;
        getEncounterMethodByName: NameIdsFunctions;
        getEncounterConditionByName: NameIdsFunctions;
        getEncounterConditionValueByName: NameIdsFunctions;
        getEvolutionTriggerByName: NameIdsFunctions;
        getGenerationByName: NameIdsFunctions;
        getPokedexByName: NameIdsFunctions;
        getVersionByName: NameIdsFunctions;
        getVersionGroupByName: NameIdsFunctions;
        getItemByName: NameIdsFunctions;
        getItemAttributeByName: NameIdsFunctions;
        getItemCategoryByName: NameIdsFunctions;
        getItemFlingEffectByName: NameIdsFunctions;
        getItemPocketByName: NameIdsFunctions;
        getMoveByName: NameIdsFunctions;
        getMoveAilmentByName: NameIdsFunctions;
        getMoveBattleStyleByName: NameIdsFunctions;
        getMoveCategoryByName: NameIdsFunctions;
        getMoveDamageClassByName: NameIdsFunctions;
        getMoveLearnMethodByName: NameIdsFunctions;
        getMoveTargetByName: NameIdsFunctions;
        getLocationByName: NameIdsFunctions;
        getLocationAreaByName: NameIdsFunctions;
        getPalParkAreaByName: NameIdsFunctions;
        getRegionByName: NameIdsFunctions;
        getAbilityByName: NameIdsFunctions;
        getEggGroupByName: NameIdsFunctions;
        getGenderByName: NameIdsFunctions;
        getGrowthRateByName: NameIdsFunctions;
        getPokeathlonStatByName: NameIdsFunctions;
        getPokemonEncounterAreasByName: NameIdsFunctions;
        getPokemonColorByName: NameIdsFunctions;
        getPokemonHabitatByName: NameIdsFunctions;
        getPokemonShapeByName: NameIdsFunctions;
        getPokemonSpeciesByName: NameIdsFunctions;
        getStatByName: NameIdsFunctions;
        getTypeByName: NameIdsFunctions;
        getLanguageByName: NameIdsFunctions;

        getPokemonByName: NameIdsFunctions<Pokemon>;
        getPokemonFormByName: NameIdsFunctions<PokemonForm>;
        getNatureByName: NameIdsFunctions<PokemonNature>;

        getEndpointsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getBerriesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getBerriesFirmnesssList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getBerriesFlavorsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getContestTypesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getContestEffectsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getSuperContestEffectsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getEncounterMethodsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getEncounterConditionsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getEncounterConditionValuesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getEvolutionChainsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getEvolutionTriggersList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getGenerationsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getPokedexsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getVersionsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getVersionGroupsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getItemsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getItemAttributesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getItemCategoriesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getItemFlingEffectsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getItemPocketsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getMachinesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getMovesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getMoveAilmentsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getMoveBattleStylesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getMoveCategoriesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getMoveDamageClassesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getMoveLearnMethodsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getMoveTargetsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getLocationsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getLocationAreasList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getPalParkAreasList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getRegionsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getAbilitiesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getCharacteristicsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getEggGroupsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getGendersList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getGrowthRatesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getNaturesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getPokeathlonStatsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getPokemonsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getPokemonColorsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getPokemonFormsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getPokemonHabitatsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getPokemonShapesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getPokemonSpeciesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getStatsList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getTypesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;
        getLanguagesList(dict?: { offset: number, limit: number }): Promise<ResourceList>;

        resource(param: string | string[]): Promise<object>;
    }
}
