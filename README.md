
# Project Title

This project is my initial assesment for SudoSky. 
## Installation

Download the 3 project files in this repo (survey, surveyAPI and surveyBackend). `survey` is the Angular frontend, `surveyAPI` is the ASP.NET Core Web API and `surveyBackend` is a CRUD program created with Razor which allows you to work with the data stored in the SQL server.


Run the following SQL queries to create the necessary tables (You can also download them from this repo):

```bash
-- questionOptions table
CREATE TABLE questionOptions (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    oKey VARCHAR (30) NOT NULL,
    oLabel VARCHAR (100) NOT NULL,
    qId int FOREIGN KEY REFERENCES questions(id),
    selections INT NOT NULL DEFAULT 0
)

INSERT INTO questionOptions (oKey, oLabel, qId) VALUES
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
```

```bash
-- questions table
CREATE TABLE questions (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	qKey VARCHAR (100) NOT NULL,
	qLabel VARCHAR (150) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	multiple_choice BIT NOT NULL DEFAULT 0
);

INSERT INTO questions (qKey, qLabel, multiple_choice) VALUES
    ('discovery', 'How did you find out about this job opportunity?', 0),
    ('location', 'How do you find the company’s location?', 1),
    ('office', 'What was your impression of the office where you had the interview?', 0),
    ('challenging', 'How technically challenging was the interview?', 0),
    ('manager', 'How would you describe the manager that interviewed you?', 1)
```
    