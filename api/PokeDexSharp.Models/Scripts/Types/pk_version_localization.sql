DO $$
BEGIN
	IF NOT EXISTS (
		SELECT 1
		FROM pg_type
		WHERE typname = 'pk_version_localization'
	) THEN
		CREATE TYPE pk_version_localization AS (
			code TEXT,
			value TEXT,
			generation uuid
		);
	END IF;

END$$;