-- Veritabanını oluştur
CREATE DATABASE IF NOT EXISTS TrainingApp;
USE TrainingApp;

GRANT EXECUTE ON trainingapp.* TO 'root'@'localhost';
FLUSH PRIVILEGES;

-- User Tablosu 
CREATE TABLE IF NOT EXISTS User (
    UserId CHAR(36) PRIMARY KEY,
    Firstname VARCHAR(255) NOT NULL,
    Lastname VARCHAR(255) NOT NULL,
    Username VARCHAR(255) NOT NULL UNIQUE,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedBy CHAR(36),
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    UpdatedBy CHAR(36),
    DeleteFlag BOOL DEFAULT FALSE
);


-- Region Tablosu
CREATE TABLE IF NOT EXISTS Region (
    RegionId CHAR(36) PRIMARY KEY, 
    Name VARCHAR(100) NOT NULL unique,
    Description VARCHAR(100),
    CreatedAt DATETIME,
    CreatedBy CHAR(36),
    UpdatedAt DATETIME,
    UpdatedBy CHAR(36),
    DeleteFlag BOOL
);

-- Workout Tablosu
CREATE TABLE IF NOT EXISTS Workout (
   	WorkoutId CHAR(36) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Duration INT NOT NULL,
    Difficulty INT NOT NULL,
    CreatedAt DATETIME,
    CreatedBy CHAR(36),
    UpdatedAt DATETIME,
    UpdatedBy CHAR(36),
    DeleteFlag BOOL
);

-- Exercise Tablosu
CREATE TABLE IF NOT EXISTS Exercise (
    ExerciseId CHAR(36) PRIMARY KEY,
    UserId CHAR(36) NOT NULL, 
    TotalDuration INT NOT NULL,
    CreatedAt DATETIME,
    CreatedBy CHAR(36),
    UpdatedAt DATETIME,
    UpdatedBy CHAR(36),
    DeleteFlag BOOL,
    FOREIGN KEY (UserId) REFERENCES User(UserId) ON DELETE cascade
);

-- WorkoutRegion Tablosu (Many-to-Many ilişki için)
CREATE TABLE IF NOT EXISTS WorkoutRegion (
	WorkoutRegionId CHAR(36) PRIMARY KEY,
    WorkoutId CHAR(36) NOT NULL, 
    RegionId CHAR(36) NOT NULL, 
    CreatedAt DATETIME,
    CreatedBy CHAR(36),
    UpdatedAt DATETIME,
    UpdatedBy CHAR(36),
    DeleteFlag BOOL,
    FOREIGN KEY (WorkoutId) REFERENCES Workout(WorkoutId) ON DELETE CASCADE,
    FOREIGN KEY (RegionId) REFERENCES Region(RegionId) ON DELETE cascade
);

-- WorkoutExercise Tablosu (Many-to-Many ilişki için)
CREATE TABLE IF NOT EXISTS WorkoutExercise (
	WorkoutExerciseId CHAR(36) PRIMARY KEY,
    WorkoutId CHAR(36) NOT NULL, 
    ExerciseId CHAR(36) NOT NULL, 
    CreatedAt DATETIME,
    CreatedBy CHAR(36),
    UpdatedAt DATETIME,
    UpdatedBy CHAR(36),
    DeleteFlag BOOL,
    FOREIGN KEY (WorkoutId) REFERENCES Workout(WorkoutId) ON DELETE CASCADE,
    FOREIGN KEY (ExerciseId) REFERENCES Exercise(ExerciseId) ON DELETE cascade
);