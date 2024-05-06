USE master

DROP DATABASE IF EXISTS RozliczeniePrzejazdowSamochodowCiezarowych;
CREATE DATABASE RozliczeniePrzejazdowSamochodowCiezarowych;

USE RozliczeniePrzejazdowSamochodowCiezarowych;

CREATE TABLE RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Konfiguracja(
	ID INT PRIMARY KEY CHECK(ID = 1),
	StawkaMinimalnaBrutto MONEY NOT NULL
);

CREATE TABLE RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Adresy(
	AdresID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Kraj NVARCHAR(50) NOT NULL,
	Miejscowosc NVARCHAR(50) NOT NULL,
	Ulica NVARCHAR(50) NOT NULL,
	NumerBudynku NVARCHAR(20) NOT NULL,
	NumerLokalu NVARCHAR(20),
	KodPocztowy NVARCHAR(15)
);

CREATE TABLE RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Kierowcy(
	KierowcaID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Imie NVARCHAR(50) NOT NULL,
	Nazwisko NVARCHAR(50) NOT NULL,
	Pesel CHAR(11) CHECK (Pesel NOT LIKE '%[^0-9]%' AND LEN(Pesel) = 11),
	StawkaGodzinowaBrutto MONEY NOT NULL,
	DataUrodzenia DATE NOT NULL CHECK((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),FORMAT(DataUrodzenia, 'yyyyMMdd'),112))/10000 >= 18),
	AdresID INT FOREIGN KEY REFERENCES RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Adresy(AdresID) NOT NULL
);

CREATE TABLE RozliczeniePrzejazdowSamochodowCiezarowych.dbo.TypyTowaru(
	NazwaTypu NVARCHAR(50) PRIMARY KEY NOT NULL
);

CREATE TABLE RozliczeniePrzejazdowSamochodowCiezarowych.dbo.RodzajePaliwa(
	NazwaPaliwa NVARCHAR(20) PRIMARY KEY NOT NULL
);

CREATE TABLE RozliczeniePrzejazdowSamochodowCiezarowych.dbo.SamochodyCiezarowe(
	SamochodCiezarowyID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	TypTowaru NVARCHAR(50) FOREIGN KEY REFERENCES RozliczeniePrzejazdowSamochodowCiezarowych.dbo.TypyTowaru(NazwaTypu),
	RodzajPaliwa NVARCHAR(20) FOREIGN KEY REFERENCES RozliczeniePrzejazdowSamochodowCiezarowych.dbo.RodzajePaliwa(NazwaPaliwa) NOT NULL,
	Rejestracja VARCHAR(8) UNIQUE NOT NULL CHECK(Rejestracja NOT LIKE '%[^QWERTYUIOPLKJHGFDSAZXCVBNM1234567890]%'),
	[MaksymalnaObjetoscZaladunku(m3)] FLOAT CHECK ([MaksymalnaObjetoscZaladunku(m3)] >= 0),
	[MaksymalnaLadownosc(t)] FLOAT  NOT NULL CHECK ([MaksymalnaLadownosc(t)] >= 0),
	RokProdukcji SMALLINT  NOT NULL CHECK(RokProdukcji <= YEAR(GETDATE()))
);

CREATE TABLE RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Przejazdy(
	PrzejazdID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	KierowcaID INT FOREIGN KEY REFERENCES RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Kierowcy(KierowcaID) NOT NULL,
	SamochodCiezarowyID INT FOREIGN KEY REFERENCES RozliczeniePrzejazdowSamochodowCiezarowych.dbo.SamochodyCiezarowe(SamochodCiezarowyID) NOT NULL,
	TypTowaru NVARCHAR(50) FOREIGN KEY REFERENCES RozliczeniePrzejazdowSamochodowCiezarowych.dbo.TypyTowaru(NazwaTypu) NOT NULL,
	[LacznaOdlegloscPrzejazdu(km)] FLOAT NOT NULL CHECK ([LacznaOdlegloscPrzejazdu(km)] > 0), 
	[ZuzytePaliwo(L)] FLOAT NOT NULL CHECK ([ZuzytePaliwo(L)] > 0),
	[CenaPaliwa(zl/l)] MONEY NOT NULL CHECK ([CenaPaliwa(zl/l)] > 0),
	DodatkoweKoszty MONEY CHECK (DodatkoweKoszty >= 0), 
	SrednieSpalanieNa100km AS ([ZuzytePaliwo(L)]*100/[LacznaOdlegloscPrzejazdu(km)]),
	DataRozpoczeciaPrzejazdu DATETIME  NOT NULL CHECK(DataRozpoczeciaPrzejazdu < GETDATE()), 
	DataZakonczeniaPrzejazdu DATETIME  NOT NULL CHECK(DataZakonczeniaPrzejazdu < GETDATE()), 
	StawkaGodzinowaBruttoKierowcy MONEY NOT NULL, -- brak sprawdzenia warunku opisanego w poprzednim etapie, poniewa¿ stawka minimalna mog³a zmieniæ siê do czasu wprowadzenia danych co powodowa³oby b³¹d
	WspolczynnikObjetosci FLOAT CHECK(WspolczynnikObjetosci >=0 AND WspolczynnikObjetosci <=1),
	WspolczynnikLadownosci FLOAT  NOT NULL CHECK(WspolczynnikLadownosci >=0 AND WspolczynnikLadownosci <=1)
);

CREATE TABLE RozliczeniePrzejazdowSamochodowCiezarowych.dbo.PunktyTrasy(
	PrzejazdID INT FOREIGN KEY REFERENCES RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Przejazdy(PrzejazdID) NOT NULL,
	AdresID INT FOREIGN KEY REFERENCES RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Adresy(AdresID) NOT NULL,
	CONSTRAINT PK_PunktTrasy PRIMARY KEY (PrzejazdID, AdresID) 
);