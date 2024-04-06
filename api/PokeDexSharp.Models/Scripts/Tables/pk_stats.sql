CREATE TABLE IF NOT EXISTS pk_stats (
	id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
	
	key TEXT NOT NULL UNIQUE,
	name pk_localization[] NOT NULL DEFAULT '{}',
	class_id UUID NOT NULL REFERENCES pk_move_classes(id),
    is_battle_only BOOLEAN NOT NULL DEFAULT FALSE,
	
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	deleted_at TIMESTAMP
);
