-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 27, 2025 at 03:35 PM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pipeline`
--
CREATE DATABASE IF NOT EXISTS `pipeline` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `pipeline`;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` char(36) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` char(36) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `aspnetroles`
--

INSERT INTO `aspnetroles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
('11111111-1111-1111-1111-111111111111', 'Admin', 'ADMIN', NULL),
('22222222-2222-2222-2222-222222222222', 'User', 'USER', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` char(36) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext DEFAULT NULL,
  `UserId` char(36) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` char(36) NOT NULL,
  `RoleId` char(36) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `aspnetuserroles`
--

INSERT INTO `aspnetuserroles` (`UserId`, `RoleId`) VALUES
('78999ea0-6338-4a00-b990-8c2f32e1393c', '22222222-2222-2222-2222-222222222222'),
('7fc4fdae-4413-4114-b085-40e1eaa80142', '11111111-1111-1111-1111-111111111111'),
('8dfc4275-dee6-4da0-adf8-d9b1afaf9299', '22222222-2222-2222-2222-222222222222'),
('93233682-756a-4ca3-8759-e4ec37182f29', '22222222-2222-2222-2222-222222222222'),
('cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847', '22222222-2222-2222-2222-222222222222'),
('dd6f6963-4229-4921-a26f-06f5f221ff1b', '22222222-2222-2222-2222-222222222222');

-- --------------------------------------------------------

--
-- Table structure for table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` char(36) NOT NULL,
  `FirstName` longtext NOT NULL,
  `LastName` longtext NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext DEFAULT NULL,
  `SecurityStamp` longtext DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `aspnetusers`
--

INSERT INTO `aspnetusers` (`Id`, `FirstName`, `LastName`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('78999ea0-6338-4a00-b990-8c2f32e1393c', 'Tamás', 'Teszt', 'TamasTester', 'TAMASTESTER', 'tamastest@gmail.com', 'TAMASTEST@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAECP4TkmP22zofsbX4RkL37IOP3/mWad2k+iKtmIFOBl+qXo0g/TUmKGHY+47JAbf1g==', '50d77837-1e20-4bdc-8864-fdcf18cf7a60', '7ca47adf-c0ad-4eb8-9dc9-29b14f75af80', '1234567890', 0, 0, NULL, 0, 0),
('7fc4fdae-4413-4114-b085-40e1eaa80142', 'Admin', 'Admin', 'admin1', 'ADMIN', 'admin@gmail.com', 'ADMIN@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAEFPpwA+MJoUPWxOzrOxvkrztwBzkhNG9Vvg4j/id6W7LS+B4yxKeAvnVRboFlJcrEA==', '4ed5e6c2-e282-44e1-84e8-fbfddd72c6dd', 'c24dddba-85a6-4a33-9c79-a30254a1c605', '7777777777', 0, 0, NULL, 0, 0),
('8dfc4275-dee6-4da0-adf8-d9b1afaf9299', 'Zoltán', 'Zárás', 'ZoliZ', 'ZOLIZ', 'zoliz@gmail.com', 'ZOLIZ@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAEAceATwexw6a/dZ1VsxRGgfxnlAoiKZoG3pXM1UkpyAsq09BIa2fQvKXBRb6qqOKXg==', 'bbc99d98-1559-4bf8-a4d8-92a8d7931267', '6a7fe70c-cd0d-48bf-84eb-806d17c629ff', '777888999', 0, 0, NULL, 0, 0),
('93233682-756a-4ca3-8759-e4ec37182f29', 'Patrícia', 'Próba', 'PatiProba', 'PATIPROBA', 'patiproba@gmail.com', 'PATIPROBA@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAEGBwpxQ3hJPWxYd9VdlQfS4Sg1OXRsEmo/C7adQJCubHijSu0iHrAwvinWe0bIf5uQ==', '8fe6be20-d8d4-4797-8a2e-0507f8611d6d', 'df1ae909-2089-40c9-b3fb-181e16819561', '0987654321', 0, 0, NULL, 0, 0),
('cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847', 'Bence', 'Beállítás', 'BenceB', NULL, 'bbence@gmail.com', 'BBENCE@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAEB+WUk/xUbn+gov4uD7Ej8Q3pz3rM6cdI8eSKFOFJmyq5Jo1TLTFByB+qIXyvemqLw==', '1b868f53-84cc-4460-b645-e4597e4aae94', '3e7d899e-317d-4533-8303-b6f0a083bd57', '111222333', 0, 0, NULL, 0, 0),
('dd6f6963-4229-4921-a26f-06f5f221ff1b', 'Réka', 'Regisztráció', 'RekaR', 'REKAR', 'rekareg@gmail.com', 'REKAREG@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAEIDm5OMzv9xnDJmKz4kJjrgjKhGMc18VV035gwoWSzLPBLD5hYqftx43dKJt7z4kxA==', 'e78722f4-6b8a-4c07-a758-36aa55dd29f8', '8fc32e0d-5b1f-450a-858f-33d6b74f2296', '444555666', 0, 0, NULL, 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` char(36) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext DEFAULT NULL,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `charginginstances`
--

DROP TABLE IF EXISTS `charginginstances`;
CREATE TABLE IF NOT EXISTS `charginginstances` (
  `Id` char(36) NOT NULL,
  `ChargingPortId` char(36) DEFAULT NULL,
  `DeviceId` char(36) DEFAULT NULL,
  `Start` datetime(6) DEFAULT NULL,
  `End` datetime(6) DEFAULT NULL,
  `StartingPercentage` int(11) DEFAULT NULL,
  `EndPercentage` int(11) DEFAULT NULL,
  `DesiredEndPercentage` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ChargingInstances_ChargingPortId` (`ChargingPortId`),
  KEY `IX_ChargingInstances_DeviceId` (`DeviceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `charginginstances`
--

INSERT INTO `charginginstances` (`Id`, `ChargingPortId`, `DeviceId`, `Start`, `End`, `StartingPercentage`, `EndPercentage`, `DesiredEndPercentage`) VALUES
('2388c138-a8ea-48da-8140-5c8d87da6adf', 'd2dea098-a325-4a2f-923e-a581ddd4791b', '26940568-4647-49c7-80c7-e96ccc884ea1', '2024-02-15 08:00:00.000000', '2024-02-15 10:30:00.000000', 10, 82, 85),
('59d4f463-e491-4894-bfd8-91162d032cfe', 'a91026c8-1dc2-4311-adf5-49402703d000', '4e40698d-022f-4a3f-8a91-c36372e5ac99', '2024-02-10 14:30:00.000000', '2024-02-10 16:45:00.000000', 40, 100, 100),
('afd7de1c-cb15-4fab-8576-68effd4b7504', 'a91026c8-1dc2-4311-adf5-49402703d000', '97840601-53d2-47a9-9062-ed774142658d', '2024-02-12 17:00:00.000000', '2024-02-12 19:15:00.000000', 60, 100, 100),
('de29d60e-ce40-4bcd-b4f5-e6f2378cebd0', 'b20b2723-5dc1-411c-8a30-6a0655a8481a', '81fd7101-d207-42b3-97c2-bfc2f67c9bda', '2024-02-11 09:15:00.000000', '2024-02-11 11:30:00.000000', 50, 100, 100),
('dfc2430e-0125-4981-a109-2fca390ff039', '8f7f6784-4e76-4fba-9f44-e014fc3f5940', 'bd3cb4d1-4d28-44d5-bd28-dc167f8e121f', '2024-02-13 22:45:00.000000', '2024-02-14 01:00:00.000000', 70, 99, 99);

-- --------------------------------------------------------

--
-- Table structure for table `chargingports`
--

DROP TABLE IF EXISTS `chargingports`;
CREATE TABLE IF NOT EXISTS `chargingports` (
  `Id` char(36) NOT NULL,
  `IsBeingUsed` tinyint(1) NOT NULL,
  `IsDisabled` tinyint(1) NOT NULL,
  `ChargingStationId` char(36) NOT NULL,
  `PortNumber` int(11) NOT NULL,
  `MaxChargingSpeed` double NOT NULL,
  `IsCharging` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ChargingPorts_ChargingStationId` (`ChargingStationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `chargingports`
--

INSERT INTO `chargingports` (`Id`, `IsBeingUsed`, `IsDisabled`, `ChargingStationId`, `PortNumber`, `MaxChargingSpeed`, `IsCharging`) VALUES
('01d024a5-fe51-4419-a872-1a19f98db4c7', 1, 1, '4fd34718-3872-4bf1-8fb5-5b30849fc80e', 3, 1.2, 1),
('08bcd0e3-5f9c-48f7-b345-a22b4c1961cb', 0, 0, '625307d2-d501-41e5-8fdf-77d151bdd260', 3, 1.2, 0),
('11b58460-2e0d-4d58-a6f5-39a3945a6289', 1, 1, 'b3cab32d-4d07-4f9f-ad73-00d1ab6f909f', 4, 1.2, 0),
('1c1351ab-a67b-4f64-9859-8a6403e6ef95', 1, 0, 'a57aff6e-a64c-4938-9a2c-974fc2505780', 3, 1.2, 0),
('1dcebbab-37a2-4473-9aff-dbc89f5f843f', 1, 0, '625307d2-d501-41e5-8fdf-77d151bdd260', 2, 1.6, 1),
('206213c1-7437-4340-aedc-91d5ba483d77', 0, 0, 'e1621f6d-a8d5-4065-a537-0d2aa0adee96', 1, 1.6, 1),
('21176d38-9fbb-41e0-8a51-638250daaf40', 0, 1, '4db1ba58-c788-47cb-819c-08da571e5978', 2, 1.2, 0),
('2d1b1640-aae9-49db-aa61-c9331e6b1b7d', 1, 1, 'b3cab32d-4d07-4f9f-ad73-00d1ab6f909f', 2, 1.5, 0),
('445cba27-5324-4b2d-a00d-3d12f0a32645', 0, 0, '74178acf-f8d4-4c2b-b55c-6f136b13bc39', 2, 1.6, 1),
('494aebb3-23d3-4d91-99f8-7f86c60f5ea3', 0, 0, '4fd34718-3872-4bf1-8fb5-5b30849fc80e', 4, 1.5, 0),
('530e70de-8bdc-4037-8276-69ab27f71484', 0, 0, 'b3cab32d-4d07-4f9f-ad73-00d1ab6f909f', 47, 1.5, 0),
('68de7fec-ae57-43c3-920f-c976d9c1b464', 1, 1, 'a57aff6e-a64c-4938-9a2c-974fc2505780', 2, 1.6, 0),
('6e934b8f-e509-4376-be7e-71f16309e7fe', 0, 0, 'e1621f6d-a8d5-4065-a537-0d2aa0adee96', 4, 1.6, 1),
('82a4a1a6-210e-4c0e-b96f-b9d0109acd59', 0, 0, 'a57aff6e-a64c-4938-9a2c-974fc2505780', 1, 1.2, 1),
('838d79f1-89a9-4cc7-a992-febbb7804b14', 1, 0, 'b3cab32d-4d07-4f9f-ad73-00d1ab6f909f', 3, 1.6, 0),
('8a5a4cba-d12d-4353-92c1-d60eefdaa712', 0, 0, '4db1ba58-c788-47cb-819c-08da571e5978', 1, 1.5, 0),
('8f7f6784-4e76-4fba-9f44-e014fc3f5940', 1, 0, '2f3820bc-ba7d-487b-897e-12d2c5041ce5', 1, 1.5, 0),
('a05290a1-01fe-489a-b233-3063ab451048', 1, 0, '74178acf-f8d4-4c2b-b55c-6f136b13bc39', 1, 1.6, 0),
('a91026c8-1dc2-4311-adf5-49402703d000', 0, 1, '734aa62b-dcaa-4815-a659-24d1fcaa3b18', 1, 1.6, 0),
('ab0d6c6e-f47f-4ae1-b77c-beb93b136842', 1, 1, 'e1621f6d-a8d5-4065-a537-0d2aa0adee96', 2, 1.5, 0),
('ad6fab12-1c99-426e-a201-bc8e14e75b74', 0, 0, 'b3cab32d-4d07-4f9f-ad73-00d1ab6f909f', 45, 1.5, 0),
('b20b2723-5dc1-411c-8a30-6a0655a8481a', 1, 0, '734aa62b-dcaa-4815-a659-24d1fcaa3b18', 2, 1.5, 1),
('b42e7da5-cbb1-407f-9e1e-0bfa290db559', 0, 1, '0d46cb77-0134-4850-99ec-8ac8d7105c26', 1, 1.5, 1),
('c2efcc7e-b977-4c8f-ad61-a2a93432a11c', 1, 1, '625307d2-d501-41e5-8fdf-77d151bdd260', 1, 1.6, 1),
('c503e753-9388-4cd2-bb34-78ba2cc352e1', 1, 0, '0d46cb77-0134-4850-99ec-8ac8d7105c26', 3, 1.6, 1),
('cbfa84da-a918-4f6e-ae7c-2319fbdc911a', 0, 0, '74178acf-f8d4-4c2b-b55c-6f136b13bc39', 3, 1.2, 0),
('cebbe124-3172-40f5-bc7a-b420a9e65459', 1, 0, '4fd34718-3872-4bf1-8fb5-5b30849fc80e', 2, 1.6, 0),
('d128636e-95c4-4728-acc8-0468d02f54f7', 0, 0, 'a57aff6e-a64c-4938-9a2c-974fc2505780', 4, 1.6, 0),
('d1abc777-fa3d-425d-ada7-503c5de6b20c', 0, 0, 'b3cab32d-4d07-4f9f-ad73-00d1ab6f909f', 46, 1.5, 0),
('d2dea098-a325-4a2f-923e-a581ddd4791b', 0, 0, '2f3820bc-ba7d-487b-897e-12d2c5041ce5', 2, 1.6, 1),
('d972ac98-7f68-4690-a947-ba2f59d7a14c', 0, 0, 'e1621f6d-a8d5-4065-a537-0d2aa0adee96', 3, 1.5, 1),
('dd8dd71b-2961-449a-860d-dfef294d2a8d', 1, 1, '0d46cb77-0134-4850-99ec-8ac8d7105c26', 2, 1.5, 1),
('e726c61a-30d2-4b58-a8a1-ab23750d6250', 1, 1, '4fd34718-3872-4bf1-8fb5-5b30849fc80e', 1, 1.5, 0),
('eb59229f-a0d4-49a3-a1cc-f3504451ace1', 0, 0, '91b1377a-33b8-4bf7-943c-8299d7f78343', 1, 1.2, 1),
('ef0f137a-37bb-45a6-b830-460467f74ceb', 1, 1, 'b3cab32d-4d07-4f9f-ad73-00d1ab6f909f', 1, 1.2, 1),
('f11dac41-e9c0-4395-9e29-9aa65d4cee85', 0, 0, '91b1377a-33b8-4bf7-943c-8299d7f78343', 2, 1.6, 1),
('fd342c7a-e418-4480-8dfb-e3c8611cdcdf', 1, 1, '91b1377a-33b8-4bf7-943c-8299d7f78343', 3, 1.5, 1);

-- --------------------------------------------------------

--
-- Table structure for table `chargingstations`
--

DROP TABLE IF EXISTS `chargingstations`;
CREATE TABLE IF NOT EXISTS `chargingstations` (
  `Id` char(36) NOT NULL,
  `Name` longtext NOT NULL,
  `Latitude` double NOT NULL,
  `Longitude` double NOT NULL,
  `Address` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `chargingstations`
--

INSERT INTO `chargingstations` (`Id`, `Name`, `Latitude`, `Longitude`, `Address`) VALUES
('0d46cb77-0134-4850-99ec-8ac8d7105c26', 'Kecskemét Micromobility Hub', 46.9062, 19.6913, '23 Kecskemét Way'),
('1783ac8e-96fb-47c0-8c74-7bcac28cf383', 'Cluj-Napoca Micromobility Hub', 46.7712, 23.6236, '12 Cluj Avenue'),
('21aecd38-fae4-4f9b-b634-bb50ccaa32d8', 'Tatabánya Micromobility Hub', 47.584, 18.4039, '77 Tatabánya Route'),
('2a466d50-82c3-4e64-9d79-bdf59fe2f25b', 'Zalaegerszeg Micromobility Hub', 46.845, 16.8416, '35 Zalaegerszeg Boulevard'),
('2f3820bc-ba7d-487b-897e-12d2c5041ce5', 'Budapest Micromobility Hub - East', 47.50391, 19.11657, '45 Keleti út, Budapest'),
('340e8c31-6f52-4e7f-9f43-c2c1b9b9add4', 'Veszprém Micromobility Hub', 47.0933, 17.9115, '23 Veszprém Avenue'),
('4c4f3e6e-19d0-42e1-885f-35ec2e39fd1d', 'Eger Micromobility Hub', 47.9025, 20.3772, '45 Eger Main Road'),
('4db1ba58-c788-47cb-819c-08da571e5978', 'Pécs Micromobility Hub', 46.0727, 18.2323, '55 Pécs Drive'),
('4e5f4cf4-8ed7-491f-b18e-7dbeed52b219', 'Kaposvár Micromobility Hub', 46.3594, 17.7827, '33 Kaposvár Ring'),
('4fd34718-3872-4bf1-8fb5-5b30849fc80e', 'Győr Micromobility Hub', 47.6875, 17.6504, '33 Győr Center'),
('5a9d3f31-c85d-47b1-9d8e-8e80e4a0943e', 'Zagreb Micromobility Hub', 45.815, 15.9819, '90 Zagreb Boulevard'),
('625307d2-d501-41e5-8fdf-77d151bdd260', 'Debrecen Micromobility Hub', 47.5316, 21.6273, '90 Debrecen Ring'),
('734aa62b-dcaa-4815-a659-24d1fcaa3b18', 'Budapest Micromobility Hub - West', 47.49801, 19.03991, '101 Budapest Blvd'),
('74178acf-f8d4-4c2b-b55c-6f136b13bc39', 'Nyíregyháza Micromobility Hub', 47.9554, 21.7167, '72 Nyíregyháza Street'),
('7c414209-c0f1-4ef2-bc4d-863deec95bf2', 'Szeged Micromobility Hub - Újszeged', 46.2496, 20.1728, '22 Torontál tér, Szeged'),
('91b1377a-33b8-4bf7-943c-8299d7f78343', 'Berlin Micromobility Hub', 52.52, 13.405, '102 Berlin Platz'),
('9cb9a098-3cfb-46dc-bd72-4f2a938323f7', 'Békéscsaba Micromobility Hub', 46.6834, 21.0887, '14 Békéscsaba Ring'),
('a57aff6e-a64c-4938-9a2c-974fc2505780', 'Vienna Micromobility Hub', 48.2082, 16.3738, '88 Vienna Central'),
('b3cab32d-4d07-4f9f-ad73-00d1ab6f909f', 'Szeged Micromobility Hub', 46.253, 20.148, '12 Tisza Street, Szeged'),
('b5f85c22-9d87-4e5b-bf56-45a1dc0bfa9a', 'Szeged Micromobility Hub - Belváros', 46.253, 20.1414, '34 Kárász Street, Szeged'),
('bb0fdbc1-cc97-4601-9c81-ace35cf1aaba', 'Sopron Micromobility Hub', 47.6817, 16.5845, '66 Sopron Street'),
('c8c8281f-79b4-4a8a-b930-2c7af38d4e7b', 'Bratislava Micromobility Hub', 48.1486, 17.1077, '21 Bratislava Square'),
('dba2e8e4-6d61-49af-b74a-4b9d927d0b2e', 'Prague Micromobility Hub', 50.0755, 14.4378, '77 Prague Center'),
('e1621f6d-a8d5-4065-a537-0d2aa0adee96', 'Miskolc Micromobility Hub', 48.103, 20.778, '87 Miskolc Avenue'),
('e5bfb6da-4d5e-4c7c-9bcb-8b8dbf9a1a64', 'Szombathely Micromobility Hub', 47.2307, 16.6218, '58 Szombathely Road'),
('e8949cc0-566a-423c-beca-ef749acd972d', 'Székesfehérvár Micromobility Hub', 47.186, 18.4221, '11 Fehérvár Way'),
('eb8c3c7e-7044-4f32-9483-18a2a5f10ed9', 'Ljubljana Micromobility Hub', 46.0569, 14.5058, '54 Ljubljana Road'),
('f1e8c45b-2a91-4ff8-b3ec-24b2781c9d24', 'Brno Micromobility Hub', 49.1951, 16.6068, '63 Brno Main Street');

-- --------------------------------------------------------

--
-- Table structure for table `devices`
--

DROP TABLE IF EXISTS `devices`;
CREATE TABLE IF NOT EXISTS `devices` (
  `Id` char(36) NOT NULL,
  `DeviceType` int(11) NOT NULL,
  `Manufacturer` longtext DEFAULT NULL,
  `Model` longtext DEFAULT NULL,
  `Name` longtext NOT NULL,
  `ApplicationUserId` char(36) NOT NULL,
  `BatteryCapacity` double DEFAULT NULL,
  `MaxChargingSpeed` double DEFAULT NULL,
  `BatteryVoltage` double DEFAULT NULL,
  `Discriminator` varchar(13) NOT NULL,
  `DetachableBattery` tinyint(1) DEFAULT NULL,
  `IsFoldable` tinyint(1) DEFAULT NULL,
  `CanBeLocked` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Devices_ApplicationUserId` (`ApplicationUserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `devices`
--

INSERT INTO `devices` (`Id`, `DeviceType`, `Manufacturer`, `Model`, `Name`, `ApplicationUserId`, `BatteryCapacity`, `MaxChargingSpeed`, `BatteryVoltage`, `Discriminator`, `DetachableBattery`, `IsFoldable`, `CanBeLocked`) VALUES
('01239c68-890a-4186-b115-1bde2639063c', 0, 'ENGWE', 'EP-2 Pro', 'Gyárvárosi Cruiser', '78999ea0-6338-4a00-b990-8c2f32e1393c', 13000, 2, 35, 'EBike', 1, NULL, NULL),
('0eb8c9cd-475a-438d-ae3c-21884ea90bcc', 1, 'Segway', 'Ninebot MAX G30', 'Segway Explorer', '78999ea0-6338-4a00-b990-8c2f32e1393c', 15300, 0.6, 36, 'EScooter', NULL, 0, NULL),
('1cc80793-209f-4619-908e-0a580f5525db', 1, 'Dualtron', 'Thunder 2', 'Thunder Viharjáró', 'cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847', 36000, 2.5, 60, 'EScooter', NULL, 0, NULL),
('235a36c1-fec9-4c33-b0cb-85423990a137', 1, 'NIU', 'KQi3 Pro', 'NIU Futár', 'dd6f6963-4229-4921-a26f-06f5f221ff1b', 12800, 0.6, 48, 'EScooter', NULL, 1, NULL),
('26940568-4647-49c7-80c7-e96ccc884ea1', 0, 'HIMO', 'C26', 'Csendes Keringő', 'dd6f6963-4229-4921-a26f-06f5f221ff1b', 14400, 0.5, 48, 'EBike', 0, NULL, NULL),
('3f6eb602-3135-4ec1-b619-0c67e72c3637', 2, 'Meepo', 'Meepo V4 Shuffle', 'Meepo Cruiser', '93233682-756a-4ca3-8759-e4ec37182f29', 14400, 0.4, 36, 'ESkateBoard', NULL, NULL, 1),
('4e40698d-022f-4a3f-8a91-c36372e5ac99', 0, 'Fiido', 'M1 Pro', 'Túrázó M1', 'cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847', 17000, 0.7, 48, 'EBike', 0, NULL, NULL),
('659a4302-241a-45ba-9a7a-6d1aba3c8b71', 2, 'Boosted', 'Boosted Stealth', 'Boosted Beast', '78999ea0-6338-4a00-b990-8c2f32e1393c', 19900, 0.5, 44, 'ESkateBoard', NULL, NULL, 0),
('7a05a19d-1395-449e-afd6-6c4322fb0dd8', 0, 'ADO', 'DECE 300', 'Terepvadász', '93233682-756a-4ca3-8759-e4ec37182f29', 15000, 0.4, 48, 'EBike', 1, NULL, NULL),
('81fd7101-d207-42b3-97c2-bfc2f67c9bda', 1, 'Kaabo', 'Mantis 10 Pro', 'Mantis Sárkány', 'cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847', 18000, 1.2, 48, 'EScooter', NULL, 1, NULL),
('83cefd82-f987-4471-9293-5dd401931f0a', 1, 'Xiaomi', 'Mi Electric Scooter Pro 2', 'Városi Villám', '78999ea0-6338-4a00-b990-8c2f32e1393c', 12400, 0.5, 36, 'EScooter', NULL, 1, NULL),
('8721f5b0-cf96-4a80-aa1b-7b343be48af2', 1, 'E-TWOW', 'GT SE', 'GT Kompakt', 'cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847', 10400, 0.4, 36, 'EScooter', NULL, 1, NULL),
('97840601-53d2-47a9-9062-ed774142658d', 0, 'ENGWE', 'Engine Pro', 'Napi Ingázó', '78999ea0-6338-4a00-b990-8c2f32e1393c', 16000, 2.5, 48, 'EBike', 1, NULL, NULL),
('bb2868d7-1f6a-4651-aaca-8178c172590b', 2, 'Evolve', 'Bamboo GTR', 'Evolve Off-Road', 'dd6f6963-4229-4921-a26f-06f5f221ff1b', 18000, 0.6, 44, 'ESkateBoard', NULL, NULL, 1),
('bd3cb4d1-4d28-44d5-bd28-dc167f8e121f', 2, 'WowGo', 'WowGo 3X', 'WowGo Speedster', '8dfc4275-dee6-4da0-adf8-d9b1afaf9299', 15000, 0.5, 36, 'ESkateBoard', NULL, NULL, 0),
('c935f07a-263d-4be1-a3e6-99554f670aff', 1, 'Apollo', 'Phantom V3', 'Phantom Erőgép', '8dfc4275-dee6-4da0-adf8-d9b1afaf9299', 25200, 1.8, 60, 'EScooter', NULL, 0, NULL),
('cbed7941-6f04-4086-9a3e-8f46c9f91477', 0, 'TENWAYS', 'CGO800S', 'Stílusos Városi', 'cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847', 12000, 0.65, 36, 'EBike', 0, NULL, NULL),
('d59c2bdf-b80c-491a-ab25-590edb690df4', 0, 'HIMO', 'Z20', 'Kompakt Zöld', '93233682-756a-4ca3-8759-e4ec37182f29', 10000, 0.6, 36, 'EBike', 0, NULL, NULL),
('eafabb49-4083-4de4-8bd7-d092b27007ee', 0, 'ADO', 'A26', 'Hegyi Roham', 'dd6f6963-4229-4921-a26f-06f5f221ff1b', 16000, 0.7, 48, 'EBike', 0, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `errortickets`
--

DROP TABLE IF EXISTS `errortickets`;
CREATE TABLE IF NOT EXISTS `errortickets` (
  `Id` char(36) NOT NULL,
  `ChargingStationId` char(36) NOT NULL,
  `Description` longtext NOT NULL,
  `IsSolved` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ErrorTickets_ChargingStationId` (`ChargingStationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `errortickets`
--

INSERT INTO `errortickets` (`Id`, `ChargingStationId`, `Description`, `IsSolved`) VALUES
('1683a4ca-a195-4ca6-a4cb-2ee28d3ade3d', '734aa62b-dcaa-4815-a659-24d1fcaa3b18', 'Port 2 does not work.', 0),
('40018d28-d733-4589-a319-b320a66d3029', '2f3820bc-ba7d-487b-897e-12d2c5041ce5', 'Some people vandalised multiple ports', 1),
('723878fd-f94b-4e42-af02-a6cafb58eb21', '734aa62b-dcaa-4815-a659-24d1fcaa3b18', 'No power at the charging station', 1),
('b6105591-b25b-4c10-a402-772ec48b9b82', '2f3820bc-ba7d-487b-897e-12d2c5041ce5', 'Multiple ports are damaged', 0),
('cd1cbbad-bcfb-443b-b4b5-8751a7942d6c', '734aa62b-dcaa-4815-a659-24d1fcaa3b18', 'Port 3 is physically damaged and cannot be used.', 0);

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20250427133117_m1', '8.0.11');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetusertokens`
--
ALTER TABLE `aspnetusertokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `charginginstances`
--
ALTER TABLE `charginginstances`
  ADD CONSTRAINT `FK_ChargingInstances_ChargingPorts_ChargingPortId` FOREIGN KEY (`ChargingPortId`) REFERENCES `chargingports` (`Id`) ON DELETE SET NULL,
  ADD CONSTRAINT `FK_ChargingInstances_Devices_DeviceId` FOREIGN KEY (`DeviceId`) REFERENCES `devices` (`Id`) ON DELETE SET NULL;

--
-- Constraints for table `chargingports`
--
ALTER TABLE `chargingports`
  ADD CONSTRAINT `FK_ChargingPorts_ChargingStations_ChargingStationId` FOREIGN KEY (`ChargingStationId`) REFERENCES `chargingstations` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `devices`
--
ALTER TABLE `devices`
  ADD CONSTRAINT `FK_Devices_AspNetUsers_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `errortickets`
--
ALTER TABLE `errortickets`
  ADD CONSTRAINT `FK_ErrorTickets_ChargingStations_ChargingStationId` FOREIGN KEY (`ChargingStationId`) REFERENCES `chargingstations` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
