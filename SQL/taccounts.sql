DROP TABLE taccounts;

DROP SEQUENCE taccounts_seq;

CREATE SEQUENCE taccounts_seq START 1;

CREATE TABLE taccounts (
	accountid				int4 			NOT NULL DEFAULT nextval('taccounts_seq'::regclass),
	userid					varchar(64)		NOT NULL,
	username				varchar(128) 	NOT NULL,
	password 				varchar(20)		NOT NULL,
	email					varchar(128)	NOT NULL,
	loweredemail			varchar(128)	NOT NULL,
	passwordquestion		varchar(128)	NOT NULL,
	passwordanswer			varchar(128)	NOT NULL,
	approved				int4			NOT NULL DEFAULT 0,
	blocked					int4			NOT NULL DEFAULT 0,
	failedpasswordcount		int4			NOT NULL DEFAULT 0,
	failedquestioncount		int4			NOT NULL DEFAULT 0,
	dtcreate				timestamp		NOT NULL,
	dtlastlogin				timestamp		NULL,
	dtlastpasswordchange	timestamp		NULL,
	dtfailedpassword		timestamp		NULL,
	dtfailquestion			timestamp		NULL,
	dtinsert				timestamp		NOT NULL,
	dtupdate				timestamp		NULL,
	recstat					int4			NOT NULL DEFAULT 1
);

ALTER TABLE ONLY taccounts
    ADD CONSTRAINT taccounts_pkey PRIMARY KEY (accountid);

