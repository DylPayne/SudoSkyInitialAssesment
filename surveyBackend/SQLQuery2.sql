CREATE TABLE questionOptions (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	oKey VARCHAR (30) NOT NULL,
	oLabel VARCHAR (100) NOT NULL,
	qId int FOREIGN KEY REFERENCES questions(id)
)

INSERT INTO questionOptions (oKey, oLabel, qId)
VALUES
('stackOverflow', 'Stack Overflow', 1),
('indeed', 'Indeed', 1),
('other', 'Other', 1),
('publicTransport', 'Easy to access by public transport', 2),
('car', 'Easy to access by car', 2),
('pleasant', 'In a pleasant area', 2),
('none', 'None of the above', 2),
('tidy', 'Tidy', 3),
('sloppy', 'Sloppy', 3),
('none', 'Did not notice', 3),
('veryDifficult', 'Very difficult', 4),
('difficult', 'Difficult', 4),
('moderate', 'Moderate', 4),
('easy', 'Easy', 4),
('enthusiastic', 'Enthusiastic', 5),
('polite', 'Polite', 5),
('organised', 'Organised', 5),
('none', 'Could not tell', 5)