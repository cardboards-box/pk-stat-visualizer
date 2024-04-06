CREATE TABLE IF NOT EXISTS pk_game_groups (
	id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
	
	key TEXT NOT NULL UNIQUE,
	generation_id UUID NOT NULL REFERENCES pk_game_generations(id),
	move_learn_methods UUID[] NOT NULL DEFAULT '{}',
	ordinal INT NOT NULL DEFAULT 0,
	
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	deleted_at TIMESTAMP
);