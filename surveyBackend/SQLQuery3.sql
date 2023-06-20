SELECT questionOptions.id, questionOptions.oKey, questionOptions.oLabel, questions.qKey FROM questionOptions
INNER JOIN questions ON questionOptions.qId = questions.id