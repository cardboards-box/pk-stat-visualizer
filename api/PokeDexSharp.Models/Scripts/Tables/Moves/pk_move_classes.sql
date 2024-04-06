CREATE TABLE IF NOT EXISTS pk_move_classes (
	id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
	
	key TEXT NOT NULL UNIQUE,
	name pk_localization[] NOT NULL DEFAULT '{}',
	description pk_localization[] NOT NULL DEFAULT '{}',
	
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	deleted_at TIMESTAMP
);