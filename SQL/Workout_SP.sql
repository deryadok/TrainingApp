CREATE PROCEDURE trainingapp.AddWorkout(
	p_WorkoutId CHAR(36),
    p_Name VARCHAR(255),
    p_Duration INT,
    p_Difficulty INT,
    p_CreatedBy VARCHAR(36),
    p_CreatedAt DATETIME,
    p_DeleteFlag BOOL)
begin
	INSERT INTO Workout (WorkoutId, Name, Duration, Difficulty, CreatedBy, CreatedAt, DeleteFlag)
    VALUES (p_WorkoutId, p_Name, p_Duration, p_Difficulty, p_CreatedBy, p_CreatedAt, p_DeleteFlag );

	SELECT p_WorkoutId;
end

CREATE PROCEDURE trainingapp.GetWorkoutById(IN p_WorkoutId CHAR(36))
BEGIN
    SELECT * FROM Workout WHERE WorkoutId = p_WorkoutId;
END 

CREATE PROCEDURE trainingapp.UpdateWorkout(
    p_WorkoutId CHAR(36),
    p_Name VARCHAR(255),
    p_Duration INT,
    p_Difficulty INT,
    p_UpdatedBy VARCHAR(36),
    p_UpdatedAt DATETIME
)
BEGIN
    UPDATE Workout
    SET Name = p_Name,
        Duration = p_Duration,
        Difficulty = p_Difficulty,
        UpdatedBy = p_UpdatedBy,
        UpdatedAt = p_UpdatedAt
    WHERE WorkoutId = p_WorkoutId;

	SELECT @rows;
end

CREATE PROCEDURE trainingapp.DeleteWorkout(IN p_WorkoutId CHAR(36))
BEGIN
    UPDATE Workout 
    SET DeleteFlag = TRUE 
    WHERE WorkoutId = p_WorkoutId;

	SELECT @rows;
end

CREATE PROCEDURE trainingapp.GetAllWorkouts()
BEGIN
    select * FROM Workout;
end

CREATE PROCEDURE trainingapp.GetFilteredWorkouts(
    p_Duration INT,
    p_Difficulty INT,
    p_RegionId VARCHAR(36)
)
BEGIN
    SELECT * FROM Workout w
    left join WorkoutRegion wr on w.WorkoutId = wr.WorkoutId
    WHERE 
        (p_Duration IS not NULL OR w.Duration = p_Duration) 
        AND (p_Difficulty is NOT NULL OR w.Difficulty = p_Difficulty) 
        AND (p_RegionId is NOT NULL OR wr.RegionId = p_RegionId);
end