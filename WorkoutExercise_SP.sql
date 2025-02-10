CREATE PROCEDURE trainingapp.AddWorkoutExercise(
    IN p_WorkoutExerciseId CHAR(36),
    IN p_WorkoutId CHAR(36),
    IN p_ExerciseId CHAR(36),
    IN p_CreatedBy CHAR(36),
    IN p_CreatedAt DATETIME,
    IN p_DeleteFlag BOOL
)
BEGIN
    INSERT INTO WorkoutExercise (WorkoutExerciseId, WorkoutId, ExerciseId, CreatedBy, CreatedAt, DeleteFlag)
    VALUES (p_WorkoutExerciseId, p_WorkoutId, p_ExerciseId, p_CreatedBy, p_CreatedAt, p_DeleteFlag);

	SELECT p_WorkoutExerciseId;
END

CREATE PROCEDURE trainingapp.UpdateWorkoutExercise(
    IN p_WorkoutExerciseId CHAR(36),
    IN p_WorkoutId CHAR(36),
    IN p_ExerciseId CHAR(36),
    IN p_UpdatedBy CHAR(36),
    IN p_UpdatedAt DATETIME
)
BEGIN
    UPDATE WorkoutExercise
    SET WorkoutId = p_WorkoutId,
        ExerciseId = p_ExerciseId,
        UpdatedBy = p_UpdatedBy,
        UpdatedAt = p_UpdatedAt
    WHERE WorkoutExerciseId = p_WorkoutExerciseId AND DeleteFlag = FALSE;

	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.DeleteWorkoutExercise(IN p_WorkoutExerciseId CHAR(36))
BEGIN
    UPDATE WorkoutExercise SET DeleteFlag = TRUE WHERE WorkoutExerciseId = p_WorkoutExerciseId;
	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.GetExercisesByWorkoutId(IN p_WorkoutId CHAR(36))
BEGIN
    SELECT e.*
    FROM WorkoutExercise we
    JOIN Exercise e ON we.ExerciseId = e.ExerciseId
    WHERE we.WorkoutId = p_WorkoutId AND we.DeleteFlag = FALSE;
END

CREATE PROCEDURE trainingapp.BulkInsertWorkoutExercises(IN p_Values TEXT)
BEGIN
    SET @sql = CONCAT('INSERT INTO WorkoutExercise (WorkoutExerciseId, WorkoutId, ExerciseId, CreatedBy, CreatedAt, DeleteFlag) VALUES ', p_Values);
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;

    SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.BulkUpdateWorkoutExercises(
    IN p_WorkoutId CHAR(36),
    IN p_Updates TEXT
)
BEGIN
    -- Güncellenen değerleri içeren dinamik SQL sorgusu oluştur
    SET @sql = CONCAT('
        UPDATE WorkoutExercise 
        SET 
            ExerciseId = CASE WorkoutExerciseId ', p_Updates, ' END,
            UpdatedAt = NOW()
        WHERE WorkoutId = "', p_WorkoutId, '";
    ');

    -- Dinamik sorguyu çalıştır
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
    SELECT ROW_COUNT(); 
END

CREATE PROCEDURE trainingapp.BulkSoftDeleteWorkoutExercises(
    IN p_ExerciseId CHAR(36)
)
BEGIN
    START TRANSACTION;

    -- Dinamik SQL oluştur
    SET @sql = CONCAT('
        UPDATE WorkoutExercise 
        SET DeleteFlag = TRUE, UpdatedAt = NOW()
        WHERE ExerciseId = "', p_ExerciseId, '";
    ');

    -- Sorguyu çalıştır
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
    
    COMMIT;
    
    SELECT ROW_COUNT(); 
END