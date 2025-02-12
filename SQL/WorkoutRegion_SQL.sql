CREATE PROCEDURE trainingapp.AddWorkoutRegion(
    IN p_WorkoutRegionId CHAR(36),
    IN p_WorkoutId CHAR(36),
    IN p_RegionId CHAR(36),
    IN p_CreatedBy CHAR(36),
    IN p_CreatedAt DATETIME,
    IN p_DeleteFlag BOOL
)
BEGIN
    INSERT INTO WorkoutRegion (WorkoutRegionId, WorkoutId, RegionId, CreatedBy, CreatedAt, DeleteFlag)
    VALUES (p_WorkoutRegionId, p_WorkoutId, p_RegionId, p_CreatedBy, p_CreatedAt, p_DeleteFlag);

	SELECT p_WorkoutRegionId;
END

CREATE PROCEDURE trainingapp.UpdateWorkoutRegion(
    IN p_WorkoutRegionId CHAR(36),
    IN p_WorkoutId CHAR(36),
    IN p_RegionId CHAR(36),
    IN p_UpdatedBy CHAR(36),
    IN p_UpdatedAt DATETIME
)
BEGIN
    UPDATE WorkoutRegion
    SET WorkoutId = p_WorkoutId,
        RegionId = p_RegionId,
        UpdatedBy = p_UpdatedBy,
        UpdatedAt = p_UpdatedAt
    WHERE WorkoutRegionId = p_WorkoutRegionId AND DeleteFlag = FALSE;
	
	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.DeleteWorkoutRegion(IN p_WorkoutRegionId CHAR(36))
BEGIN
    UPDATE WorkoutRegion SET DeleteFlag = TRUE WHERE WorkoutRegionId = p_WorkoutRegionId;

	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.GetRegionsByWorkoutId(IN p_WorkoutId CHAR(36))
BEGIN
    SELECT r.*
    FROM WorkoutRegion wr
    JOIN Region r ON wr.RegionId = r.RegionId
    WHERE wr.WorkoutId = p_WorkoutId AND wr.DeleteFlag = FALSE;
END 

CREATE PROCEDURE trainingapp.BulkInsertWorkoutRegions(IN p_Values TEXT)
BEGIN
    SET @sql = CONCAT('INSERT INTO WorkoutRegion (WorkoutRegionId, WorkoutId, RegionId, CreatedBy, CreatedAt, DeleteFlag) VALUES ', p_Values);
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;

    SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.BulkUpdateWorkoutRegions(
    IN p_WorkoutId CHAR(36),
    IN p_Updates TEXT
)
BEGIN
    -- Güncellenen değerleri içeren dinamik SQL sorgusu oluştur
    SET @sql = CONCAT('
        UPDATE WorkoutRegion 
        SET 
            RegionId = CASE WorkoutRegionId ', p_Updates, ' END,
            UpdatedAt = NOW()
        WHERE WorkoutId = "', p_WorkoutId, '";
    ');

    -- Dinamik sorguyu çalıştır
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
    
    SELECT ROW_COUNT(); 
END

CREATE PROCEDURE trainingapp.BulkSoftDeleteWorkoutRegions(
    IN p_WorkoutId CHAR(36),
    IN p_RegionIds TEXT
)
BEGIN
    START TRANSACTION;
    
    -- Dinamik SQL oluştur
    SET @sql = CONCAT('
        UPDATE WorkoutRegion 
        SET DeleteFlag = TRUE, UpdatedAt = NOW()
        WHERE WorkoutId = "', p_WorkoutId, '";
    ');

    -- Sorguyu çalıştır
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
    
    COMMIT;
    
    SELECT ROW_COUNT(); 
END