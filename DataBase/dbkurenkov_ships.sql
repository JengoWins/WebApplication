-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: dbkurenkov
-- ------------------------------------------------------
-- Server version	8.0.36

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
-- Table structure for table `ships`
--

DROP TABLE IF EXISTS `ships`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ships` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `Health` int NOT NULL,
  `Speed` int NOT NULL,
  `flexibility` varchar(45) NOT NULL,
  `TeamCrew` int NOT NULL,
  `HeavyWeapon` int NOT NULL,
  `MediumWeapon` int NOT NULL,
  `LightWeapon` int NOT NULL,
  `price` int NOT NULL,
  `imgWay` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ships`
--

LOCK TABLES `ships` WRITE;
/*!40000 ALTER TABLE `ships` DISABLE KEYS */;
INSERT INTO `ships` VALUES (2,'Ман-о-Вар',4940,15,'среднее',35,9,10,9,230,'../src/img/navy/man-o-war.jpg'),(3,'Линкор',4820,14,'низкое',31,7,8,7,202,'../src/img/navy/lincor.jpg'),(4,'Фрегат',3150,21,'среднее',23,6,6,9,131,'../src/img/navy/frigate.jpg'),(5,'Корабль-Мин',2140,24,'хорошее',30,0,8,5,110,'../src/img/navy/miner.jpg'),(6,'Тендер',2800,17,'среднее',15,0,4,1,110,'../src/img/navy/tender.jpg'),(7,'Быстрый Фрегат',2050,37,'хорошее',27,1,6,3,100,'../src/img/navy/fast_frigate.jpg'),(8,'Тяжелый разветчик',1660,39,'хорошее',18,1,8,9,100,'../src/img/navy/heavy_scout.jpg'),(9,'Штурмовой катер',1310,29,'хорошее',30,0,7,3,70,'../src/img/navy/shturm_kater.jpg'),(10,'Тяжелый эскорт',1560,24,'хорошее',15,0,5,9,62,'../src/img/navy/heavy_escort.jpg'),(11,'Катер',960,29,'хорошее',12,1,6,3,58,'../src/img/navy/kater.jpg'),(12,'Орудийная баржа',1150,0,'плохое',8,0,8,4,50,'../src/img/navy/weapon_barge.jpg'),(13,'Шлюп',650,32,'отличное',20,0,4,1,40,'../src/img/navy/sloop.jpg'),(14,'Торпедный катер',350,40,'отличное',5,2,0,1,19,'../src/img/navy/torpedo_boat.jpg');
/*!40000 ALTER TABLE `ships` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-23 20:19:24
