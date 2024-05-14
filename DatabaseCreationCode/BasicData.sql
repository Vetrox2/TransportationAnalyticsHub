use RozliczeniePrzejazdowSamochodowCiezarowych

INSERT INTO Konfiguracja
VALUES (26)

INSERT INTO TypyTowaru
VALUES ('Paleta')
INSERT INTO TypyTowaru
VALUES ('Ch³odnia')

INSERT INTO RodzajePaliwa
VALUES ('Benzyna 95')
INSERT INTO RodzajePaliwa
VALUES ('Diesel')

DECLARE @KierowcaID INT
DECLARE @SamochodID INT 
DECLARE @AdresID INT 
DECLARE @PrzejazdID INT 

--Przejazd 1
EXEC DodajKierowceZAdresem 'Anna','Kowalska','95010112345',40.25,'19950101','Polska','Warszawa','Koszykowa','12',null,'00-001'
SET @KierowcaID = @@IDENTITY

INSERT INTO SamochodyCiezarowe
VALUES ('Paleta', 'Benzyna 95', 'SBI56B5',20,24,2002)
SET @SamochodID = @@IDENTITY

INSERT INTO Przejazdy
VALUES (@KierowcaID,@SamochodID,'Paleta',530,42,6.77,150,'20240402 10:00','20240402 18:00',32.5,0.8,0.54)
SET @PrzejazdID = @@IDENTITY

INSERT into Adresy
VALUES ('Polska','Warszawa','Prawdziwka','32',null,'02-973')
SET @AdresID = @@IDENTITY

INSERT INTO PunktyTrasy VALUES (@PrzejazdID,@AdresID)

INSERT into Adresy
VALUES ('Polska','Kraków','Krakowska','43',null,'33-332')
SET @AdresID = @@IDENTITY

INSERT INTO PunktyTrasy VALUES (@PrzejazdID,@AdresID)

--Przejazd 2
EXEC DodajKierowceZAdresem 'Cezary','Nowak','06241723458',31.5,'20060417','Polska','Bielsko-Bia³a','Faudzi Stok','30',null,'43-300'
SET @KierowcaID = @@IDENTITY

INSERT INTO SamochodyCiezarowe
VALUES ('Ch³odnia', 'Diesel', 'SBIX275',NULL,18,2005)
SET @SamochodID = @@IDENTITY

INSERT INTO Przejazdy
VALUES (@KierowcaID,@SamochodID,'Ch³odnia',2200,193.54,7.21,0,'20240415 12:00','20240417 18:02',28.5,null,0.95)
SET @PrzejazdID = @@IDENTITY

INSERT into Adresy
VALUES ('Polska','Kraków','M¹czna ','4',null,'31-227')
SET @AdresID = @@IDENTITY

INSERT INTO PunktyTrasy VALUES (@PrzejazdID,@AdresID)

INSERT into Adresy
VALUES ('Polska','Bielsko-Bia³a','Sarni Stok','83',null,'43-300')
SET @AdresID = @@IDENTITY

INSERT INTO PunktyTrasy VALUES (@PrzejazdID,@AdresID)