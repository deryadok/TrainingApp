CREATE PROCEDURE trainingapp.AddRegion(
    IN p_RegionId CHAR(36),
    IN p_Name VARCHAR(100),
    IN p_Description VARCHAR(100),
    IN p_CreatedBy CHAR(36),
    IN p_CreatedAt DATETIME,
    IN p_DeleteFlag BOOL
)
BEGIN
    INSERT INTO Region (RegionId, Name, Description, CreatedBy, CreatedAt, DeleteFlag)
    VALUES (p_RegionId, p_Name, p_Description, p_CreatedBy, p_CreatedAt, p_DeleteFlag);

	SELECT p_RegionId;
END 

CREATE PROCEDURE trainingapp.UpdateRegion(
    IN p_RegionId CHAR(36),
    IN p_Name VARCHAR(100),
    IN p_Description VARCHAR(100),
    IN p_UpdatedBy CHAR(36),
    IN p_UpdatedAt DATETIME
)
BEGIN
    UPDATE Region
    SET Name = p_Name,
        Description = p_Description,
        UpdatedBy = p_UpdatedBy,
        UpdatedAt = p_UpdatedAt
    WHERE RegionId = p_RegionId AND DeleteFlag = FALSE;

	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.DeleteRegion(IN p_RegionId CHAR(36))
BEGIN
    UPDATE Region SET DeleteFlag = TRUE WHERE RegionId = p_RegionId;
	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.GetRegionById(IN p_RegionId CHAR(36))
BEGIN
    SELECT * FROM Region WHERE RegionId = p_RegionId AND DeleteFlag = FALSE;
END

CREATE PROCEDURE trainingapp.GetAllRegions()
BEGIN
    SELECT * FROM Region WHERE DeleteFlag = FALSE;
END