-- MariaDB dump 10.19-11.1.2-MariaDB, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: stock
-- ------------------------------------------------------
-- Server version	11.1.2-MariaDB-1:11.1.2+maria~ubu2204

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `stock`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `stock` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;

USE `stock`;

--
-- Table structure for table `Companies`
--

DROP TABLE IF EXISTS `Companies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Companies` (
  `CompanyId` int(11) NOT NULL AUTO_INCREMENT,
  `Identification` longtext NOT NULL,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`CompanyId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Companies`
--

LOCK TABLES `Companies` WRITE;
/*!40000 ALTER TABLE `Companies` DISABLE KEYS */;
INSERT INTO `Companies` VALUES
(1,'10.493.712/0001-43','Sarah e Leonardo Telas Ltda'),
(2,'69.892.936/0001-70','Luiz e Victor Esportes Ltda'),
(3,'61.492.159/0001-65','Marina e Murilo Adega ME'),
(4,'73.614.160/0001-02','Vitor e Rosângela Pizzaria Ltda'),
(5,'78.057.691/0001-57','Geraldo e Mirella Marketing Ltda'),
(6,'21.901.953/0001-09','Eduarda e Mariah Buffet Ltda'),
(7,'67.445.188/0001-52','Kamilly e Josefa Padaria ME'),
(8,'90.576.261/0001-47','Murilo e Isadora Informática ME'),
(9,'42.019.815/0001-62','Kamilly e Simone Pizzaria Delivery ME');
/*!40000 ALTER TABLE `Companies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `StockEvents`
--

DROP TABLE IF EXISTS `StockEvents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `StockEvents` (
  `EventId` int(11) NOT NULL AUTO_INCREMENT,
  `EventType` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `WalletId` int(11) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`EventId`),
  KEY `IX_StockEvents_CompanyId` (`CompanyId`),
  KEY `IX_StockEvents_WalletId` (`WalletId`),
  CONSTRAINT `FK_StockEvents_Companies_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `Companies` (`CompanyId`) ON DELETE CASCADE,
  CONSTRAINT `FK_StockEvents_Wallets_WalletId` FOREIGN KEY (`WalletId`) REFERENCES `Wallets` (`WalletId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `StockEvents`
--

LOCK TABLES `StockEvents` WRITE;
/*!40000 ALTER TABLE `StockEvents` DISABLE KEYS */;
INSERT INTO `StockEvents` VALUES
(2,0,10,15.000000000000000000000000000000,1,1),
(3,0,50,13.000000000000000000000000000000,1,1),
(4,0,25,27.000000000000000000000000000000,1,2),
(5,0,30,25.000000000000000000000000000000,1,2),
(8,0,10,11.000000000000000000000000000000,1,1),
(9,0,20,17.000000000000000000000000000000,1,3),
(10,0,17,15.000000000000000000000000000000,1,3),
(11,0,12,16.000000000000000000000000000000,1,3),
(12,0,18,13.000000000000000000000000000000,1,3),
(13,0,12,7.000000000000000000000000000000,1,5),
(14,0,14,9.000000000000000000000000000000,1,7),
(15,0,12,15.000000000000000000000000000000,2,4),
(16,0,13,14.000000000000000000000000000000,2,3),
(17,0,21,17.000000000000000000000000000000,1,5),
(18,0,10,13.120000000000000000000000000000,1,7),
(19,0,27,14.000000000000000000000000000000,1,7),
(20,0,13,15.350000000000000000000000000000,1,7);
/*!40000 ALTER TABLE `StockEvents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Summaries`
--

DROP TABLE IF EXISTS `Summaries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Summaries` (
  `SummaryId` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedAt` datetime(6) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `AveragePrice` decimal(65,30) NOT NULL,
  `WalletId` int(11) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  `LastProcessedId` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`SummaryId`),
  KEY `IX_Summaries_CompanyId` (`CompanyId`),
  KEY `IX_Summaries_WalletId` (`WalletId`),
  CONSTRAINT `FK_Summaries_Companies_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `Companies` (`CompanyId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Summaries_Wallets_WalletId` FOREIGN KEY (`WalletId`) REFERENCES `Wallets` (`WalletId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Summaries`
--

LOCK TABLES `Summaries` WRITE;
/*!40000 ALTER TABLE `Summaries` DISABLE KEYS */;
INSERT INTO `Summaries` VALUES
(11,'2023-09-11 09:07:16.154462',70,13.000000000000000000000000000000,1,1,8),
(12,'2023-09-11 09:07:34.490630',55,25.909090909090909090909090909000,1,2,5),
(13,'2023-09-11 12:10:41.481640',67,15.238805970149253731343283582000,1,3,12),
(14,'2023-09-11 13:32:37.414195',33,13.363636363636363636363636364000,1,5,17),
(15,'2023-09-11 13:52:28.999734',24,10.716666666666666666666666667000,1,7,18),
(16,'2023-09-11 14:23:20.632899',12,15.000000000000000000000000000000,2,4,15),
(17,'2023-09-11 14:23:20.737579',13,14.000000000000000000000000000000,2,3,16);
/*!40000 ALTER TABLE `Summaries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Wallets`
--

DROP TABLE IF EXISTS `Wallets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Wallets` (
  `WalletId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`WalletId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Wallets`
--

LOCK TABLES `Wallets` WRITE;
/*!40000 ALTER TABLE `Wallets` DISABLE KEYS */;
INSERT INTO `Wallets` VALUES
(1,'Fundo Imobiliario'),
(2,'Ações'),
(3,'Renda Fixa');
/*!40000 ALTER TABLE `Wallets` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES
('20230909024837_InitialCreate','7.0.10'),
('20230910005751_SummaryControl','7.0.10');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-12 12:54:06
