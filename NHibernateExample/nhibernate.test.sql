/* nhibernate.test database */

CREATE TABLE people
(
	id SERIAL NOT NULL PRIMARY KEY,
	name varchar(100) NOT NULL,
	ssn	int NOT NULL
);

CREATE UNIQUE INDEX ON people (ssn)


CREATE TABLE parents
(
	id SERIAL NOT NULL PRIMARY KEY,
	person_id INTEGER REFERENCES people (id),
	first_child_birthdate DATE NOT NULL
);

CREATE UNIQUE INDEX ON parents (first_child_birthdate)