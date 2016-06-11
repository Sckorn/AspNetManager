DROP FUNCTION f_user_iu(
	_username				varchar(128),
	_password				varchar(20),
	_email					varchar(128),
	_passwordquestion		varchar(128),
	_passwordanswer			varchar(128),
	_isapproved				int4,
	_userid					varchar(64)
);

CREATE FUNCTION f_user_iu(
	_username						varchar(128),
	_password						varchar(20),
	_email							varchar(128),
	_passwordquestion		varchar(128),
	_passwordanswer			varchar(128),
	_isapproved					int4,
	_userid							varchar(64)
)
RETURNS INT4 AS 

$BODY$
DECLARE
	_exaccid		int4;
BEGIN

_exaccid = COALESCE((select a.accountid from taccounts a where a.username = _username and a.userid = _userid and recstat > 0 limit 1), 0);

if(_exaccid > 0) THEN
	UPDATE taccounts SET
		username = _username,
		password = _password,
		email = _email,
		passwordquestion = _passwordquestion,
		passwordanswer = _passwordanswer,
		approved = _isapproved,
		userid = _userid,
		dtupdate = now()
	WHERE accountid = _exaccid and recstat > 0;

	return _exaccid;
ELSE
	INSERT INTO taccounts(userid, username, password, email, passwordquestion, passwordanswer, approved, dtinsert, recstat)
	VALUES(_userid, _username, _password, _email, _passwordquestion, _passwordanswer, _isapproved, now(), 1);

	return currval('taccounts_seq'::regclass);
end if;


END;

$BODY$

LANGUAGE plpgsql;

