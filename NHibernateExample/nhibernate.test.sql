/* nhibernate.test database */

CREATE TABLE persons
(
	id SERIAL NOT NULL PRIMARY KEY,
	name varchar(100) NOT NULL,
	ssn	int NOT NULL
);

CREATE UNIQUE INDEX ON persons(ssn);


CREATE TABLE passport
(
	id SERIAL NOT NULL PRIMARY KEY,
	person_id INTEGER NOT NULL REFERENCES persons (id) UNIQUE,
	number INTEGER NOT NULL,
	issued DATE NOT NULL
);

CREATE UNIQUE INDEX ON passport (issued)