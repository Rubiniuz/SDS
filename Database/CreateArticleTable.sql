CREATE TABLE `sds`.`article` (
  `ArticleId` INT NOT NULL AUTO_INCREMENT,
  `Sku` VARCHAR(24) NULL,
  `Name` VARCHAR(45) NULL,
  `Price` DECIMAL(12,4) NULL,
  PRIMARY KEY (`ArticleId`));