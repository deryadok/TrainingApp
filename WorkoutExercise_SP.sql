CREATE PROCEDURE trainingapp.AddWorkoutExercise(
    p_WorkoutExerciseId CHAR(36),
    p_WorkoutId CHAR(36),
    p_ExerciseId CHAR(36),
    p_CreatedBy CHAR(36),
    p_CreatedAt DATETIME,
    p_DeleteFlag BOOL
)
BEGIN
    INSERT INTO WorkoutExercise (WorkoutExerciseId, WorkoutId, ExerciseId, CreatedBy, CreatedAt, DeleteFlag)
    VALUES (p_WorkoutExerciseId, p_WorkoutId, p_ExerciseId, p_CreatedBy, p_CreatedAt, p_DeleteFlag);

	SELECT p_WorkoutExerciseId;
END

CREATE PROCEDURE trainingapp.UpdateWorkoutExercise(
    p_WorkoutExerciseId CHAR(36),
    p_WorkoutId CHAR(36),
    p_ExerciseId CHAR(36),
    p_UpdatedBy CHAR(36),
    p_UpdatedAt DATETIME
)
BEGIN
    UPDATE WorkoutExercise
    SET WorkoutId = p_WorkoutId,
        ExerciseId = p_ExerciseId,
        UpdatedBy = p_UpdatedBy,
        UpdatedAt = p_UpdatedAt
    WHERE WorkoutExerciseId = p_WorkoutExerciseId AND DeleteFlag = FALSE;

	SELECT @rows;
END

CREATE PROCEDURE trainingapp.DeleteWorkoutExercise(p_WorkoutExerciseId CHAR(36))
BEGIN
    UPDATE WorkoutExercise SET DeleteFlag = TRUE WHERE WorkoutExerciseId = p_WorkoutExerciseId;
	SELECT @rows;
END

CREATE PROCEDURE trainingapp.GetExercisesByWorkoutId(p_WorkoutId CHAR(36))
BEGIN
    SELECT e.*
    FROM WorkoutExercise we
    JOIN Exercise e ON we.ExerciseId = e.ExerciseId
    WHERE we.WorkoutId = p_WorkoutId AND we.DeleteFlag = FALSE;
END