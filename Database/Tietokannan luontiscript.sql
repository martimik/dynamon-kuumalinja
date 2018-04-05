-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema K8936_3
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema K8936_3
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `K8936_3` DEFAULT CHARACTER SET utf8 ;
USE `K8936_3` ;

-- -----------------------------------------------------
-- Table `K8936_3`.`userClass`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `K8936_3`.`userClass` (
  `ClassID` INT NOT NULL AUTO_INCREMENT,
  `description` VARCHAR(64) NULL,
  PRIMARY KEY (`ClassID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `K8936_3`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `K8936_3`.`user` (
  `userID` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(64) NULL,
  `password` VARCHAR(45) NULL,
  `classID` INT NOT NULL,
  PRIMARY KEY (`userID`),
  INDEX `fk_user_userClass_idx` (`classID` ASC),
  CONSTRAINT `fk_user_userClass`
    FOREIGN KEY (`classID`)
    REFERENCES `K8936_3`.`userClass` (`ClassID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `K8936_3`.`channel`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `K8936_3`.`channel` (
  `channelID` INT NOT NULL AUTO_INCREMENT,
  `channelName` VARCHAR(64) NULL,
  `channelPassword` VARCHAR(45) NULL,
  PRIMARY KEY (`channelID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `K8936_3`.`message`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `K8936_3`.`message` (
  `messageID` INT NOT NULL AUTO_INCREMENT,
  `channelID` INT NOT NULL,
  `userID` INT NOT NULL,
  `timeStamp` TIMESTAMP NULL,
  `content` VARCHAR(255) NULL,
  PRIMARY KEY (`messageID`),
  INDEX `fk_user_has_channel_channel1_idx` (`channelID` ASC),
  INDEX `fk_user_has_channel_user1_idx` (`userID` ASC),
  CONSTRAINT `fk_user_has_channel_user1`
    FOREIGN KEY (`userID`)
    REFERENCES `K8936_3`.`user` (`userID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_has_channel_channel1`
    FOREIGN KEY (`channelID`)
    REFERENCES `K8936_3`.`channel` (`channelID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `K8936_3`.`moderator`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `K8936_3`.`moderator` (
  `userID` INT NOT NULL,
  `channelID` INT NOT NULL,
  PRIMARY KEY (`userID`, `channelID`),
  INDEX `fk_user_has_channel_channel2_idx` (`channelID` ASC),
  INDEX `fk_user_has_channel_user2_idx` (`userID` ASC),
  CONSTRAINT `fk_user_has_channel_user2`
    FOREIGN KEY (`userID`)
    REFERENCES `K8936_3`.`user` (`userID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_has_channel_channel2`
    FOREIGN KEY (`channelID`)
    REFERENCES `K8936_3`.`channel` (`channelID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
