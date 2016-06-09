DROP TABLE troles;

DROP SEQUENCE troles_seq;

CREATE SEQUENCE troles_seq START 1;

CREATE TABLE troles (
	roleid			int4 NOT NULL DEFAULT nextval('troles_seq'::regclass),
	roleguid		varchar(64) NOT NULL,
	rolename		varchar(128) NOT NULL,
	loweredrolename	varchar(128) NOT NULL,
	description		text		 NULL,
	dtinsert		timestamp		NOT NULL,
	dtupdate		timestamp		NULL,
	recstat			int4			NOT NULL DEFAULT 1
);

ALTER TABLE ONLY troles
    ADD CONSTRAINT troles_pkey PRIMARY KEY (roleid);

