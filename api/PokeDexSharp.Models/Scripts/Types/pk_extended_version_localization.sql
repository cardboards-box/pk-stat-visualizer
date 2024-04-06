DO $$
BEGIN
	IF NOT EXISTS (
		SELECT 1
		FROM pg_type
		WHERE typname = 'pk_extended_version_localization'
	) THEN
		CREATE TYPE pk_extended_version_localization AS (
			code TEXT,
			value TEXT,
			extended TEXT,
			generation UUID
		);
	END IF;

END$$;