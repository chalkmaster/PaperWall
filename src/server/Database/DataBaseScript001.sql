-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.5.22 - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2013-07-03 00:52:21
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping database structure for paperwall
DROP DATABASE IF EXISTS `paperwall`;
CREATE DATABASE IF NOT EXISTS `paperwall` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `paperwall`;


-- Dumping structure for table paperwall.messages
DROP TABLE IF EXISTS `messages`;
CREATE TABLE IF NOT EXISTS `messages` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `Precision` double NOT NULL,
  `MessageText` varchar(200) NOT NULL,
  `Writter` varchar(200) NOT NULL,
  `PostedAt` datetime NOT NULL,
  `Removed` bit(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `Latitude` (`Latitude`),
  KEY `Longitude` (`Longitude`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
