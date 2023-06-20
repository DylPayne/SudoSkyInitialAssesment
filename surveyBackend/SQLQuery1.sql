CREATE TABLE questions (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	qKey VARCHAR (100) NOT NULL,
	qLabel VARCHAR (150) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO questions (qKey, qLabel)
VALUES
('discovery', 'How did you find out about this job opportunity?'),
('location', 'How do you find the company’s location?'),
('office', 'What was your impression of the office where you had the interview?'),
('challenging', 'How technically challenging was the interview?'),
('manager', 'How would you describe the manager that interviewed you?')