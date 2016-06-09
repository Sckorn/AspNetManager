DROP TABLE tprofiles;

DROP SEQUENCE tprofiles_seq;

CREATE SEQUENCE tprofiles_seq START 1;

CREATE TABLE tprofiles (
	profileid		int4			NOT NULL DEFAULT nextval('tprofiles_seq'::regclass),
	accountid		int4			NOT NULL,
	firstname		varchar(128)	NOT NULL,
	middlename		varchar(128)	NULL,
	lastname		varchar(128)	NOT NULL,
	sex				int4			NOT NULL DEFAULT 1,
	dtbirth			timestamp		NOT NULL,
	avatar			BYTEA			NULL,
	nationality		character(3)	NOT NULL,
	countrycode		character(3)	NOT NULL,
	city			int4			NOT NULL,
	managerid		int4			NOT NULL,
	teamid			int4			NOT NULL,
	dtinsert		timestamp		NOT NULL,
	dtupdate		timestamp		NULL,
	recstat			int4			NOT NULL DEFAULT 1
);

ALTER TABLE ONLY tprofiles
    ADD CONSTRAINT profileid_pkey PRIMARY KEY (profileid);

ALTER TABLE ONLY tprofiles
    ADD CONSTRAINT account_profile_fkey FOREIGN KEY (accountid) REFERENCES taccounts(accountid);

ALTER TABLE ONLY tprofiles
    ADD CONSTRAINT country_profile_fkey FOREIGN KEY (countrycode) REFERENCES country(code);

ALTER TABLE ONLY tprofiles
    ADD CONSTRAINT city_profile_fkey FOREIGN KEY (city) REFERENCES city(id);
