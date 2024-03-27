export const StatMap = {
    hp: {
        key: 'hp',
        name: 'HP',
        pkapi: 'hp',
        color: '#42f578'
    },
    attack: {
        key: 'attack',
        name: 'Atk',
        pkapi: 'attack',
        color: '#ff0000'
    },
    defense: {
        key: 'defense',
        name: 'Def',
        pkapi: 'defense',
        color: '#2200ff'
    },
    specialAttack: {
        key: 'specialAttack',
        name: 'Spec Atk',
        pkapi: 'special-attack',
        color: '#ff00cc',
    },
    specialDefense: {
        key: 'specialDefense',
        name: 'Spec Def',
        pkapi: 'special-defense',
        color: '#00fbff'
    },
    speed: {
        key: 'speed',
        name: 'Speed',
        pkapi: 'speed',
        color: '#fbff00'
    }
}

export type Stats = {
    [key in keyof typeof StatMap]: number;
}

export const DEFAULT_STATS: PokemonStats = {
    hp: { iv: 0, ev: 0 },
    attack: { iv: 0, ev: 0 },
    defense: { iv: 0, ev: 0 },
    specialAttack: { iv: 0, ev: 0 },
    specialDefense: { iv: 0, ev: 0 },
    speed: { iv: 0, ev: 0 },
}

export type StatResult = { level: number; } & Stats;

export type PokemonStats = {
    [key in keyof typeof StatMap]: {
        iv: number;
        ev: number;
    }
}
