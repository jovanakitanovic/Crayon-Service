-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: localhost    Database: crayon
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `accountcustomerlink`
--

DROP TABLE IF EXISTS `accountcustomerlink`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accountcustomerlink` (
  `customerId` char(36) NOT NULL,
  `accountId` char(45) NOT NULL,
  `accountcustomerlinkId` char(45) NOT NULL,
  PRIMARY KEY (`accountcustomerlinkId`),
  UNIQUE KEY `accountcustomerlinkcol_UNIQUE` (`accountcustomerlinkId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accountcustomerlink`
--

LOCK TABLES `accountcustomerlink` WRITE;
/*!40000 ALTER TABLE `accountcustomerlink` DISABLE KEYS */;
INSERT INTO `accountcustomerlink` VALUES ('5a92cce9-2d7d-497c-8ea1-12dd98806ded','723e282a-1649-4752-94b8-96002f707f42','46484c0c-486d-4414-9c6a-70975c001c99'),('5a92cce9-2d7d-497c-8ea1-12dd98806ded','32030c31-f6b1-4144-b68c-528e3ddb94e8','492d0ae8-7868-43a5-abd4-4413c08ff43d'),('2db6fc25-143d-4be7-bfdb-b4bcfafc97f6','4d61db87-b587-4aa0-8c44-b9a09cb5b63a','6a7870c2-3349-4044-aee8-8eeb375efdb8'),('5a92cce9-2d7d-497c-8ea1-12dd98806ded','db1634fd-cf93-4f10-a1d8-0d1c830f7763','bdab5e3a-14af-4088-a0d8-e90d55ea9691');
/*!40000 ALTER TABLE `accountcustomerlink` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounts` (
  `accountId` char(36) NOT NULL,
  PRIMARY KEY (`accountId`),
  UNIQUE KEY `accountId_UNIQUE` (`accountId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounts`
--

LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` VALUES ('32030c31-f6b1-4144-b68c-528e3ddb94e8'),('4d61db87-b587-4aa0-8c44-b9a09cb5b63a'),('723e282a-1649-4752-94b8-96002f707f42'),('db1634fd-cf93-4f10-a1d8-0d1c830f7763');
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servicesubbscripitons`
--

DROP TABLE IF EXISTS `servicesubbscripitons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `servicesubbscripitons` (
  `accountId` char(36) NOT NULL,
  `serviceId` char(36) NOT NULL,
  `serviceName` varchar(45) NOT NULL,
  `quantity` int NOT NULL,
  `state` int NOT NULL,
  `validThrough` datetime NOT NULL,
  `ServiceSubscripitonId` char(36) NOT NULL,
  PRIMARY KEY (`ServiceSubscripitonId`),
  UNIQUE KEY `ServiceSubbscripitonId_UNIQUE` (`ServiceSubscripitonId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicesubbscripitons`
--

LOCK TABLES `servicesubbscripitons` WRITE;
/*!40000 ALTER TABLE `servicesubbscripitons` DISABLE KEYS */;
INSERT INTO `servicesubbscripitons` VALUES ('723e282a-1649-4752-94b8-96002f707f42','05fdd6ec-0fcc-4871-b46a-69c35c094162','Lucid Chart',50,3,'2025-07-03 12:58:45','10332284-1e0c-4132-a8b8-a310a3afb215'),('32030c31-f6b1-4144-b68c-528e3ddb94e8','05fdd6ec-0fcc-4871-b46a-69c35c094162','Lucid Chart',8,1,'2025-05-18 12:41:38','35159e3b-2f25-412d-a923-e60fa1671876'),('32030c31-f6b1-4144-b68c-528e3ddb94e8','05fdd6ec-0fcc-4871-b46a-69c35c094162','Lucid Chart',1234,1,'2025-07-03 12:48:48','7deb8e7e-94ea-4ca7-933f-8f7ce889e7f8'),('32030c31-f6b1-4144-b68c-528e3ddb94e8','05fdd6ec-0fcc-4871-b46a-69c35c094162','Lucid Chart',1000,0,'2025-05-09 13:33:06','93ae93b6-2052-470e-9392-8c0efa21fd6a'),('723e282a-1649-4752-94b8-96002f707f42','3690717e-33cd-45aa-9287-ab03cc0ffc66','Jira',200,3,'2025-07-03 12:58:45','b7eadb58-cc67-43c0-8c41-dc07eeab1007'),('32030c31-f6b1-4144-b68c-528e3ddb94e8','05fdd6ec-0fcc-4871-b46a-69c35c094162','Lucid Chart',110,3,'2025-05-15 12:43:57','f85f9278-9006-4714-b77e-58a6d84d6358');
/*!40000 ALTER TABLE `servicesubbscripitons` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-03 15:33:46
