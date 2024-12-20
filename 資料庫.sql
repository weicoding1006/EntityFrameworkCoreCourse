USE `EntityFrameworkCoreCourse`;
SELECT * FROM Coaches;
SELECT * FROM Teams;
SELECT * FROM Matches;
SELECT * FROM Leagues;
SELECT Teams.Name As TeamName,Coaches.Name AS CoachName FROM Teams
INNER JOIN Coaches
ON Teams.CoachId = Coaches.Id;