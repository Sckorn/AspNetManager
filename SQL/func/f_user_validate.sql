DROP FUNCTION f_user_validate(
	_username	varchar(128),
	_email		varchar(128)
);

CREATE FUNCTION f_user_validate(
	_username	varchar(128),
	_email		varchar(128)
) RETURNS INT4 AS

$BODY$

DECLARE 
	_exist	int4;

BEGIN

_exist = COALESCE((select a.accountid from taccounts a where a.username = _username and a.recstat > 0 limit 1), 0);

if(_exist > 0) then

return _exist;

end if;

_exist = COALESCE((select a.accountid from taccounts a where a.email = _email and a.recstat > 0), 0);

return _exist;

END;

$BODY$

LANGUAGE plpgsql;

select * from f_user_validate('', '') as value;