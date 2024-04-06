DO $$
BEGIN
	IF NOT EXISTS (
		SELECT 1
		FROM pg_type
		WHERE typname = 'pk_extended_localization'
	) THEN
		CREATE TYPE pk_extended_localization AS (
			code TEXT,
			value TEXT,
			extended TEXT
		);
	END IF;

END$$;