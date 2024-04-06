CREATE TABLE IF NOT EXISTS pk_game_group_maps (
	id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
	
	game_id UUID NOT NULL REFERENCES pk_games(id),
	group_id UUID NOT NULL REFERENCES pk_game_groups(id),
	
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	deleted_at TIMESTAMP,

	CONSTRAINT group_map_unique UNIQUE (game_id, group_id)
);