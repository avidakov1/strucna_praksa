
DROP TABLE employee;
DROP TABLE person;
DROP TABLE company;

CREATE TABLE person (
	person_id INTEGER IDENTITY(1,1) PRIMARY KEY,
	first_name VARCHAR(30) NOT NULL,
	last_name VARCHAR(30) NOT NULL,
	p_address VARCHAR(50) NOT NULL,
	email VARCHAR(50) NOT NULL
);

CREATE TABLE company (
	company_id INTEGER IDENTITY(1,1) PRIMARY KEY,
	c_name VARCHAR(30) NOT NULL,
	c_address VARCHAR(50) NOT NULL
);

CREATE TABLE employee (
	person_id INTEGER REFERENCES person(person_id),
	company_id INTEGER REFERENCES company(company_id),
	PRIMARY KEY (person_id, company_id	),
	position VARCHAR(30) NOT NULL,
	pay FLOAT NOT NULL
);

INSERT INTO person VALUES ('Tom', 'Brown', 'home_address','Email_address');
SELECT * FROM person;
INSERT INTO company VALUES ('MyCompany', 'Company_address');
SELECT * FROM company;
INSERT INTO employee VALUES (1, 1, 'Banker', 3000);
SELECT * FROM employee;

SELECT first_name,last_name,position,pay,c_name
FROM person
RIGHT JOIN employee ON person.person_id = employee.person_id
RIGHT JOIN company ON employee.company_id = company.company_id;