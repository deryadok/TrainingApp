CREATE PROCEDURE trainingapp.AddUser(
    p_UserId CHAR(36),
    p_Firstname VARCHAR(255),
    p_Lastname VARCHAR(255),
    p_Username VARCHAR(255),
    p_Email VARCHAR(255),
    p_Password VARCHAR(255),
    p_CreatedBy CHAR(36),
    p_CreatedAt DATETIME,
    p_DeleteFlag BOOL
)
BEGIN
    INSERT INTO User (UserId, Firstname, Lastname, Username, Email, Password, CreatedBy, CreatedAt, DeleteFlag)
    VALUES (p_UserId, p_Firstname, p_Lastname, p_Username, p_Email, p_Password, p_CreatedBy, p_CreatedAt, p_DeleteFlag);

    SELECT p_UserId;
END

CREATE PROCEDURE trainingapp.GetUserById(p_UserId CHAR(36))
BEGIN
    SELECT * FROM User WHERE UserId = p_UserId AND DeleteFlag = FALSE;
END

CREATE PROCEDURE trainingapp.UpdateUser(
    p_UserId CHAR(36),
    p_Firstname VARCHAR(255),
    p_Lastname VARCHAR(255),
    p_Username VARCHAR(255),
    p_Email VARCHAR(255),
    p_UpdatedBy CHAR(36),
    p_UpdatedAt DATETIME
)
BEGIN
    UPDATE User
    SET Firstname = p_Firstname,
        Lastname = p_Lastname,
        Username = p_Username,
        Email = p_Email,
        UpdatedBy = p_UpdatedBy,
        UpdatedAt = p_UpdatedAt
    WHERE UserId = p_UserId AND DeleteFlag = FALSE;

	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.DeleteUser(p_UserId CHAR(36))
BEGIN
    UPDATE User 
    SET DeleteFlag = TRUE 
    WHERE UserId = p_UserId;

	SELECT ROW_COUNT();
END

CREATE PROCEDURE trainingapp.GetAllUsers()
BEGIN
    SELECT * FROM User WHERE DeleteFlag = FALSE;
END

CREATE PROCEDURE trainingapp.LoginUser(
    p_Username VARCHAR(255),
    p_PasswordHash VARCHAR(255)
)
BEGIN
    DECLARE userExists INT;
    
    -- Kullanıcı var mı kontrol et
    SELECT COUNT(*) INTO userExists FROM User 
    WHERE Username = p_Username AND DeleteFlag = FALSE;

    IF userExists > 0 THEN
        -- Şifre eşleşmesi kontrolü 
        SELECT UserId, Firstname, Lastname, Username, Email 
        FROM User 
        WHERE Username = p_Username AND Password = p_PasswordHash AND DeleteFlag = FALSE;
    ELSE
        -- Kullanıcı bulunamazsa NULL döndür
        SELECT NULL AS UserId, NULL AS Firstname, NULL AS Lastname, NULL AS Username, NULL AS Email;
    END IF;
END

CREATE PROCEDURE trainingapp.GetUserByUsername(IN p_Username VARCHAR(255))
BEGIN
    SELECT * FROM User WHERE Username = p_Username AND DeleteFlag = FALSE;
END