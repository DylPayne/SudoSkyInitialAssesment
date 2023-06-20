CREATE TABLE questions (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	qKey VARCHAR (100) NOT NULL,
	qLabel VARCHAR (150) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	multiple_choice BIT NOT NULL DEFAULT 0
);

INSERT INTO questions (qKey, qLabel, multiple_choice)
VALUES
('discovery', 'How did you find out about this job opportunity?', 0),
('location', 'How do you find the company’s location?', 1),
('office', 'What was your impression of the office where you had the interview?', 0),
('challenging', 'How technically challenging was the interview?', 0),
('manager', 'How would you describe the manager that interviewed you?', 1)