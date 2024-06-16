-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema askahuman
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema askahuman
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `askahuman` DEFAULT CHARACTER SET latin1 ;
USE `askahuman` ;

-- -----------------------------------------------------
-- Table `askahuman`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `askahuman`.`users` (
    `Id` BIGINT(20) NOT NULL AUTO_INCREMENT,
    `Username` TEXT NOT NULL,
    `PasswordHash` TEXT NOT NULL,
    `PasswordSalt` TEXT NOT NULL,
    `Reputation` BIGINT(20) NOT NULL DEFAULT 0,
    `IsOnline` BOOLEAN NOT NULL DEFAULT 0,
    `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP(),
    `LastOnlineAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP(),
    PRIMARY KEY (`Id`))
    ENGINE = InnoDB
    AUTO_INCREMENT = 20
    DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `askahuman`.`chats`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `askahuman`.`chats` (
    `Id` BIGINT(20) NOT NULL AUTO_INCREMENT,
    `UsersAnswererId` BIGINT(20) NULL,
    `UsersQuestioningId` BIGINT(20) NOT NULL,
    `Completed` TINYINT(4) NOT NULL DEFAULT 0,
    `Title` TEXT NOT NULL,
    `Question` TEXT NOT NULL,
    PRIMARY KEY (`Id`),
    INDEX `fk_Chats_Users1_idx` (`UsersAnswererId` ASC) VISIBLE,
    INDEX `fk_Chats_Users2_idx` (`UsersQuestioningId` ASC) VISIBLE,
    CONSTRAINT `fk_Chats_Users1`
        FOREIGN KEY (`UsersAnswererId`)
            REFERENCES `askahuman`.`users` (`Id`)
            ON DELETE SET NULL
            ON UPDATE NO ACTION,
    CONSTRAINT `fk_Chats_Users2`
        FOREIGN KEY (`UsersQuestioningId`)
            REFERENCES `askahuman`.`users` (`Id`)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION)
    ENGINE = InnoDB
    AUTO_INCREMENT = 3
    DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `askahuman`.`messages`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `askahuman`.`messages` (
    `Id` BIGINT(20) NOT NULL AUTO_INCREMENT,
    `ChatId` BIGINT(20) NOT NULL,
    `Content` TEXT NOT NULL,
    `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP(),
    `AuthorId` BIGINT(20) NOT NULL,
    PRIMARY KEY (`Id`, `ChatId`),
    INDEX `fk_Messages_Chats1_idx` (`ChatId` ASC) VISIBLE,
    INDEX `fk_messages_users1_idx` (`AuthorId` ASC) VISIBLE,
    CONSTRAINT `fk_Messages_Chats1`
        FOREIGN KEY (`ChatId`)
            REFERENCES `askahuman`.`chats` (`Id`)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION,
    CONSTRAINT `fk_messages_users1`
        FOREIGN KEY (`AuthorId`)
            REFERENCES `askahuman`.`users` (`Id`)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION)
    ENGINE = InnoDB
    DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `askahuman`.`permissions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `askahuman`.`permissions` (
    `Id` INT(11) NOT NULL AUTO_INCREMENT,
    `Name` TEXT NOT NULL,
    PRIMARY KEY (`Id`))
    ENGINE = InnoDB
    AUTO_INCREMENT = 2
    DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `askahuman`.`roles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `askahuman`.`roles` (
    `Id` INT(11) NOT NULL AUTO_INCREMENT,
    `Name` TEXT NOT NULL,
    PRIMARY KEY (`Id`))
    ENGINE = InnoDB
    AUTO_INCREMENT = 3
    DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `askahuman`.`roles_has_permissions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `askahuman`.`roles_has_permissions` (
    `RoleId` INT(11) NOT NULL,
    `PermissionId` INT(11) NOT NULL,
    PRIMARY KEY (`RoleId`, `PermissionId`),
    INDEX `fk_Roles_has_Permissions_Permissions1_idx` (`PermissionId` ASC) VISIBLE,
    INDEX `fk_Roles_has_Permissions_Roles1_idx` (`RoleId` ASC) VISIBLE,
    CONSTRAINT `fk_Roles_has_Permissions_Permissions1`
        FOREIGN KEY (`PermissionId`)
            REFERENCES `askahuman`.`permissions` (`Id`)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION,
    CONSTRAINT `fk_Roles_has_Permissions_Roles1`
        FOREIGN KEY (`RoleId`)
            REFERENCES `askahuman`.`roles` (`Id`)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION)
    ENGINE = InnoDB
    DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `askahuman`.`roles_has_users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `askahuman`.`roles_has_users` (
    `RoleId` INT(11) NOT NULL,
    `UserId` BIGINT(20) NOT NULL,
    PRIMARY KEY (`RoleId`, `UserId`),
    INDEX `fk_Roles_has_Users_Users1_idx` (`UserId` ASC) VISIBLE,
    INDEX `fk_Roles_has_Users_Roles_idx` (`RoleId` ASC) VISIBLE,
    CONSTRAINT `fk_Roles_has_Users_Roles`
        FOREIGN KEY (`RoleId`)
            REFERENCES `askahuman`.`roles` (`Id`)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION,
    CONSTRAINT `fk_Roles_has_Users_Users1`
        FOREIGN KEY (`UserId`)
            REFERENCES `askahuman`.`users` (`Id`)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION)
    ENGINE = InnoDB
    DEFAULT CHARACTER SET = latin1;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
