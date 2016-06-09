DROP TABLE tusersinrole;

DROP SEQUENCE tusersinrole_seq;

CREATE SEQUENCE tusersinrole_seq START 1;

CREATE TABLE tusersinrole (
	accountid	int4 		NOT NULL,
	roleid		int4		NOT NULL,
	dtinsert	timestamp	NOT NULL,
	dtupdate	timestamp	NULL,
	recstat		int4		NOT NULL DEFAULT 1
);

ALTER TABLE ONLY tusersinrole
	ADD CONSTRAINT uinrole_pkey PRIMARY KEY (accountid, roleid);

ALTER TABLE ONLY tusersinrole
    ADD CONSTRAINT uinrole_account_fkey FOREIGN KEY (accountid) REFERENCES taccounts(accountid);

ALTER TABLE ONLY tusersinrole
    ADD CONSTRAINT uinrole_role_fkey FOREIGN KEY (roleid) REFERENCES troles(roleid);