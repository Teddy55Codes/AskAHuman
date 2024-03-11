-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema AskAHuman
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema AskAHuman
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `AskAHuman` DEFAULT CHARACTER SET utf8 ;
USE `AskAHuman` ;

-- -----------------------------------------------------
-- Table `AskAHuman`.`Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `AskAHuman`.`Users` (
                                                   `Id` BIGINT NOT NULL AUTO_INCREMENT,
                                                   `Username` TEXT NOT NULL,
                                                   `PasswordHash` TEXT NOT NULL,
                                                   `PasswordSalt` TEXT NOT NULL,
                                                   `Reputation` BIGINT NOT NULL DEFAULT 0,
                                                   `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                                                   `LastOnlineAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                                                   PRIMARY KEY (`Id`))
    ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `AskAHuman`.`Roles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `AskAHuman`.`Roles` (
                                                   `Id` INT NOT NULL AUTO_INCREMENT,
                                                   `Name` TEXT NOT NULL,
                                                   PRIMARY KEY (`Id`))
    ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `AskAHuman`.`Permissions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `AskAHuman`.`Permissions` (
                                                         `Id` INT NOT NULL AUTO_INCREMENT,
                                                         `Name` TEXT NOT NULL,
                                                         PRIMARY KEY (`Id`))
    ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `AskAHuman`.`Roles_has_Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `AskAHuman`.`Roles_has_Users` (
                                                             `RoleId` INT NOT NULL,
                                                             `UserId` BIGINT NOT NULL,
                                                             PRIMARY KEY (`RoleId`, `UserId`),
                                                             INDEX `fk_Roles_has_Users_Users1_idx` (`UserId` ASC) VISIBLE,
                                                             INDEX `fk_Roles_has_Users_Roles_idx` (`RoleId` ASC) VISIBLE,
                                                             CONSTRAINT `fk_Roles_has_Users_Roles`
                                                                 FOREIGN KEY (`RoleId`)
                                                                     REFERENCES `AskAHuman`.`Roles` (`Id`)
                                                                     ON DELETE NO ACTION
                                                                     ON UPDATE NO ACTION,
                                                             CONSTRAINT `fk_Roles_has_Users_Users1`
                                                                 FOREIGN KEY (`UserId`)
                                                                     REFERENCES `AskAHuman`.`Users` (`Id`)
                                                                     ON DELETE NO ACTION
                                                                     ON UPDATE NO ACTION)
    ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `AskAHuman`.`Roles_has_Permissions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `AskAHuman`.`Roles_has_Permissions` (
                                                                   `RoleId` INT NOT NULL,
                                                                   `PermissionId` INT NOT NULL,
                                                                   PRIMARY KEY (`RoleId`, `PermissionId`),
                                                                   INDEX `fk_Roles_has_Permissions_Permissions1_idx` (`PermissionId` ASC) VISIBLE,
                                                                   INDEX `fk_Roles_has_Permissions_Roles1_idx` (`RoleId` ASC) VISIBLE,
                                                                   CONSTRAINT `fk_Roles_has_Permissions_Roles1`
                                                                       FOREIGN KEY (`RoleId`)
                                                                           REFERENCES `AskAHuman`.`Roles` (`Id`)
                                                                           ON DELETE NO ACTION
                                                                           ON UPDATE NO ACTION,
                                                                   CONSTRAINT `fk_Roles_has_Permissions_Permissions1`
                                                                       FOREIGN KEY (`PermissionId`)
                                                                           REFERENCES `AskAHuman`.`Permissions` (`Id`)
                                                                           ON DELETE NO ACTION
                                                                           ON UPDATE NO ACTION)
    ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `AskAHuman`.`Chats`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `AskAHuman`.`Chats` (
                                                   `Id` BIGINT NOT NULL AUTO_INCREMENT,
                                                   `UsersAnswererId` BIGINT NOT NULL,
                                                   `UsersQuestioningId` BIGINT NOT NULL,
                                                   `Completed` TINYINT NOT NULL DEFAULT 0,
                                                   PRIMARY KEY (`Id`, `UsersAnswererId`, `UsersQuestioningId`),
                                                   INDEX `fk_Chats_Users1_idx` (`UsersAnswererId` ASC) VISIBLE,
                                                   INDEX `fk_Chats_Users2_idx` (`UsersQuestioningId` ASC) VISIBLE,
                                                   CONSTRAINT `fk_Chats_Users1`
                                                       FOREIGN KEY (`UsersAnswererId`)
                                                           REFERENCES `AskAHuman`.`Users` (`Id`)
                                                           ON DELETE NO ACTION
                                                           ON UPDATE NO ACTION,
                                                   CONSTRAINT `fk_Chats_Users2`
                                                       FOREIGN KEY (`UsersQuestioningId`)
                                                           REFERENCES `AskAHuman`.`Users` (`Id`)
                                                           ON DELETE NO ACTION
                                                           ON UPDATE NO ACTION)
    ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `AskAHuman`.`Messages`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `AskAHuman`.`Messages` (
                                                      `Id` BIGINT NOT NULL AUTO_INCREMENT,
                                                      `ChatId` BIGINT NOT NULL,
                                                      `Content` TEXT NOT NULL,
                                                      `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                                                      PRIMARY KEY (`Id`, `ChatId`),
                                                      INDEX `fk_Messages_Chats1_idx` (`ChatId` ASC) VISIBLE,
                                                      CONSTRAINT `fk_Messages_Chats1`
                                                          FOREIGN KEY (`ChatId`)
                                                              REFERENCES `AskAHuman`.`Chats` (`Id`)
                                                              ON DELETE NO ACTION
                                                              ON UPDATE NO ACTION)
    ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- Default Data
INSERT INTO Roles (Name) VALUES ('Administrator');
INSERT INTO Roles (Name) VALUES ('Member');

INSERT INTO Permissions (Name) VALUES ('ViewAllChats');

INSERT INTO Roles_has_Permissions (RoleId, PermissionId) VALUES (1, 1);
