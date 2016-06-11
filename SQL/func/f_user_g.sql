DROP FUNCTION f_user_g(
	_username	varchar(128),
	_userid		varchar(64),
	_isonline	int4
);

CREATE FUNCTION f_user_g(
	_username	varchar(128),
	_userid		varchar(64),
	_isonline	int4
)
RETURNS TABLE (
	"userID" 					varchar(64),
	"userName" 					varchar(128),
	"password"					varchar(20),
	"email"						varchar(128),
	"passwordQuestion"			varchar(128),
	"passwordAnswer"			varchar(128),
	"isApproved"				int4,
	"creationTime"				timestamp,
	"lastLoginTime"				timestamp,
	"lastPasswordChangeTime"	timestamp
) AS
$BODY$
DECLARE
	_sql	TEXT;
BEGIN

if (_username is NULL AND _userid is NULL) THEN
	RAISE EXCEPTION 'No arguments supplied!';
else
	if(_username = '' AND _userid = '') then
		RAISE EXCEPTION 'No arguments supplied!';
	end if;
end if;

_sql = ' select ' ||
				' a.userid ' ||
				' ,a.username ' ||
				' ,a.password ' ||
				' ,a.email ' ||
				' ,a.passwordquestion ' ||
				' ,a.passwordanswer ' ||
				' ,a.approved ' ||
				' ,a.dtinsert ' ||
				' ,a.dtlastlogin ' ||
				' ,a.dtlastpasswordchange ' ||
				' from taccounts a ' ||
				' where a.recstat > 0  and a.blocked = 0 ';
				

if(_username is NOT NULL) THEN

	if(_username <> '') THEN
	
		_sql = _sql || ' and a.username = ''' || _username || ''' ';
		
	end if;

ELSE

	if(_userid is NOT NULL) THEN
			if(_userid <> '') THEN
				_sql = _sql || ' and a.userid = ''' || _userid || ''' ';
			end if;
	end if;

END if;

_sql  = _sql || ' limit 1;'

return query EXECUTE _sql;

END;

$BODY$

LANGUAGE plpgsql;

select * from f_user_g('user1', '', 0);