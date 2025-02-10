CREATE PROCEDURE trainingapp.AddExercise(
    p_ExerciseId CHAR(36),
    p_UserId CHAR(36),
    p_TotalDuration INT,
    p_CreatedBy CHAR(36),
    p_CreatedAt DATETIME,
    p_DeleteFlag BOOL
)
BEGIN
    INSERT INTO Exercise (ExerciseId, UserId, TotalDuration, CreatedBy, CreatedAt, DeleteFlag)
    VALUES (p_ExerciseId, p_UserId, p_TotalDuration, p_CreatedBy, p_CreatedAt, p_DeleteFlag);

	SELECT p_ExerciseId; 
END

CREATE PROCEDURE trainingapp.UpdateExercise(
    p_ExerciseId CHAR(36),
    p_TotalDuration INT,
    p_UpdatedBy CHAR(36),
    p_UpdatedAt DATETIME
)
BEGIN
    UPDATE Exercise
    SET TotalDuration = p_TotalDuration,
        UpdatedBy = p_UpdatedBy,
        UpdatedAt = p_UpdatedAt
    WHERE ExerciseId = p_ExerciseId AND DeleteFlag = FALSE;
	
	SELECT @rows;
END

CREATE PROCEDURE trainingapp.DeleteExercise(p_ExerciseId CHAR(36))
BEGIN
    UPDATE Exercise SET DeleteFlag = TRUE WHERE ExerciseId = p_ExerciseId;
	 	SELECT @rows;
END

CREATE PROCEDURE trainingapp.GetExerciseById(p_ExerciseId CHAR(36))
BEGIN
    SELECT * FROM Exercise WHERE ExerciseId = p_ExerciseId AND DeleteFlag = FALSE;
END

CREATE PROCEDURE trainingapp.GetAllExercises()
BEGIN
    SELECT * FROM Exercise WHERE DeleteFlag = FALSE;
END