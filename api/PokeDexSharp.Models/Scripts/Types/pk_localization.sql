DO $$
BEGIN
	IF NOT EXISTS (
		SELECT 1
		FROM pg_type
		WHERE typname = 'pk_localization'
	) THEN
		CREATE TYPE pk_localization AS (
			code TEXT,
			value TEXT
		);
	END IF;

END$$;