CREATE PROCEDURE trainingapp.AddWorkoutRegion(
    p_WorkoutRegionId CHAR(36),
    p_WorkoutId CHAR(36),
    p_RegionId CHAR(36),
    p_CreatedBy CHAR(36),
    p_CreatedAt DATETIME,
    p_DeleteFlag BOOL
)
BEGIN
    INSERT INTO WorkoutRegion (WorkoutRegionId, WorkoutId, RegionId, CreatedBy, CreatedAt, DeleteFlag)
    VALUES (p_WorkoutRegionId, p_WorkoutId, p_RegionId, p_CreatedBy, p_CreatedAt, p_DeleteFlag);

	SELECT p_WorkoutRegionId;
END

CREATE PROCEDURE trainingapp.UpdateWorkoutRegion(
    p_WorkoutRegionId CHAR(36),
    p_WorkoutId CHAR(36),
    p_RegionId CHAR(36),
    p_UpdatedBy CHAR(36),
    p_UpdatedAt DATETIME
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

CREATE PROCEDURE trainingapp.DeleteWorkoutRegion(p_WorkoutRegionId CHAR(36))
BEGIN
    UPDATE WorkoutRegion SET DeleteFlag = TRUE WHERE WorkoutRegionId = p_WorkoutRegionId;

	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.GetRegionsByWorkoutId(p_WorkoutId CHAR(36))
BEGIN
    SELECT r.*
    FROM WorkoutRegion wr
    JOIN Region r ON wr.RegionId = r.RegionId
    WHERE wr.WorkoutId = p_WorkoutId AND wr.DeleteFlag = FALSE;
END 

