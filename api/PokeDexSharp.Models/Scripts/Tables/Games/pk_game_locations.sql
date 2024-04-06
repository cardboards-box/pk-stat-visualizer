CREATE TABLE IF NOT EXISTS pk_game_locations (
	id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
	
	key TEXT NOT NULL UNIQUE,
	name pk_localization[] NOT NULL DEFAULT '{}',
	region_id UUID NOT NULL REFERENCES pk_game_regions(id),
	
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	deleted_at TIMESTAMP
);