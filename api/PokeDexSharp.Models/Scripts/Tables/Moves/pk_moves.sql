DO $$
BEGIN
	IF NOT EXISTS (
		SELECT 1
		FROM pg_type
		WHERE typname = 'pk_stat_change'
	) THEN
		CREATE TYPE pk_stat_change AS (
			change INTEGER,
            id UUID
		);
	END IF;

END$$;

CREATE TABLE IF NOT EXISTS pk_move_ailments (
	id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
	
	key TEXT NOT NULL UNIQUE,
	name pk_localization[] NOT NULL DEFAULT '{}',

    class_id UUID NOT NULL REFERENCES pk_move_classes(id),
    generation_id UUID NOT NULL REFERENCES pk_game_generations(id),
    element_id UUID NOT NULL REFERENCES pk_elements(id),
    target_id UUID NOT NULL REFERENCES pk_move_targets(id),
    category_id UUID NOT NULL REFERENCES pk_move_categories(id),
    ailment_id UUID NOT NULL REFERENCES pk_move_ailments(id),

    stat_changes pk_stat_change[] NOT NULL DEFAULT '{}',
    effect pk_extended_localization[] NOT NULL DEFAULT '{}',
    flavor_text pk_version_localization[] NOT NULL DEFAULT '{}',
    effect_changes pk_extended_version_localization[] NOT NULL DEFAULT '{}',

    effect_chance INTEGER,
    power INTEGER,
    pp INTEGER,
    priority INTEGER,
    accuracy INTEGER,
    crit_rate INTEGER,
    drain INTEGER,
    flinch_chance INTEGER,
    healing INTEGER,
    stat_chance INTEGER,
    max_hits INTEGER,
    min_hits INTEGER,
    max_turns INTEGER,
    min_turns INTEGER,
    ailment_chance INTEGER,

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	deleted_at TIMESTAMP
);
