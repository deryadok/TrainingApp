CREATE PROCEDURE trainingapp.AddExercise(
    IN p_ExerciseId CHAR(36),
    IN p_UserId CHAR(36),
    IN p_TotalDuration INT,
    IN p_CreatedBy CHAR(36),
    IN p_CreatedAt DATETIME,
    IN p_DeleteFlag BOOL
)
BEGIN
    INSERT INTO Exercise (ExerciseId, UserId, TotalDuration, CreatedBy, CreatedAt, DeleteFlag)
    VALUES (p_ExerciseId, p_UserId, p_TotalDuration, p_CreatedBy, p_CreatedAt, p_DeleteFlag);

	SELECT p_ExerciseId; 
END

CREATE PROCEDURE trainingapp.UpdateExercise(
    IN p_ExerciseId CHAR(36),
    IN p_TotalDuration INT,
    IN p_UpdatedBy CHAR(36),
    IN p_UpdatedAt DATETIME
)
BEGIN
    UPDATE Exercise
    SET TotalDuration = p_TotalDuration,
        UpdatedBy = p_UpdatedBy,
        UpdatedAt = p_UpdatedAt
    WHERE ExerciseId = p_ExerciseId AND DeleteFlag = FALSE;
	
	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.DeleteExercise(IN p_ExerciseId CHAR(36))
BEGIN
    UPDATE Exercise SET DeleteFlag = TRUE WHERE ExerciseId = p_ExerciseId;
	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.GetExerciseById(IN p_ExerciseId CHAR(36))
BEGIN
    SELECT * FROM Exercise WHERE ExerciseId = p_ExerciseId AND DeleteFlag = FALSE;
END

CREATE PROCEDURE trainingapp.GetAllExercises()
BEGIN
    SELECT * FROM Exercise WHERE DeleteFlag = FALSE;
END