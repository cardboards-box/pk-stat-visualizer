CREATE TABLE IF NOT EXISTS pk_elements (
	id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
	
	key TEXT NOT NULL UNIQUE,
	name pk_localization[] NOT NULL DEFAULT '{}',
	
	class_id UUID NOT NULL REFERENCES pk_move_classes(id),
	damage_double_from UUID[] NOT NULL DEFAULT '{}',
	damage_double_to UUID[] NOT NULL DEFAULT '{}',
	damage_half_from UUID[] NOT NULL DEFAULT '{}',
	damage_half_to UUID[] NOT NULL DEFAULT '{}',
	damage_no_from UUID[] NOT NULL DEFAULT '{}',
	damage_no_to UUID[] NOT NULL DEFAULT '{}',

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	deleted_at TIMESTAMP
);