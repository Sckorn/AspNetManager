DROP FUNCTION f_country_g(
	_countrycode	CHARACTER(3)
);

CREATE FUNCTION f_country_g(
	_countrycode	CHARACTER(3)
)
RETURNS TABLE (
	"name" TEXT
) AS
$BODY$

BEGIN

return query SELECT c.name from country c where code = _countrycode;

END;

$BODY$

LANGUAGE plpgsql;

select * from f_country_g('AFG');