CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Coaches` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `ModifiedDate` datetime(6) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Coaches` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Leagues` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CreatedDate` datetime(6) NOT NULL,
    `ModifiedDate` datetime(6) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Leagues` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Matches` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `HomeTeamId` int NOT NULL,
    `AwayTeamId` int NOT NULL,
    `TicketPrice` decimal(65,30) NOT NULL,
    `Date` datetime(6) NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `ModifiedDate` datetime(6) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Matches` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Teams` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `LeagueId` int NOT NULL,
    `CoachId` int NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `ModifiedDate` datetime(6) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Teams` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `Teams` (`Id`, `CoachId`, `CreatedBy`, `CreatedDate`, `LeagueId`, `ModifiedBy`, `ModifiedDate`, `Name`)
VALUES (1, 0, NULL, TIMESTAMP '2024-12-12 06:35:56', 0, NULL, TIMESTAMP '0001-01-01 00:00:00', '測試隊伍1'),
(2, 0, NULL, TIMESTAMP '2024-12-12 06:35:56', 0, NULL, TIMESTAMP '0001-01-01 00:00:00', '測試隊伍2'),
(3, 0, NULL, TIMESTAMP '2024-12-12 06:35:56', 0, NULL, TIMESTAMP '0001-01-01 00:00:00', '測試隊伍3');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241212063556_加入更多Model', '8.0.11');

COMMIT;

START TRANSACTION;

ALTER TABLE `Leagues` ADD `Name` longtext CHARACTER SET utf8mb4 NOT NULL;

INSERT INTO `Leagues` (`Id`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `ModifiedDate`, `Name`)
VALUES (1, NULL, TIMESTAMP '0001-01-01 00:00:00', NULL, TIMESTAMP '0001-01-01 00:00:00', '測試聯盟1'),
(2, NULL, TIMESTAMP '0001-01-01 00:00:00', NULL, TIMESTAMP '0001-01-01 00:00:00', '測試聯盟2'),
(3, NULL, TIMESTAMP '0001-01-01 00:00:00', NULL, TIMESTAMP '0001-01-01 00:00:00', '測試聯盟3');

UPDATE `Teams` SET `CreatedDate` = TIMESTAMP '2024-12-13 02:59:38'
WHERE `Id` = 1;
SELECT ROW_COUNT();


UPDATE `Teams` SET `CreatedDate` = TIMESTAMP '2024-12-13 02:59:38'
WHERE `Id` = 2;
SELECT ROW_COUNT();


UPDATE `Teams` SET `CreatedDate` = TIMESTAMP '2024-12-13 02:59:38'
WHERE `Id` = 3;
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241213025938_增加初始League', '8.0.11');

COMMIT;

