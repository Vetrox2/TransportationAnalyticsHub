-- Doda³em w tabeli przejazdy atrybut [CenaPaliwa(zl/l)] MONEY NOT NULL CHECK ([CenaPaliwa(zl/l)] > 0)

USE RozliczeniePrzejazdowSamochodowCiezarowych
GO

-- Kierowcy

DROP VIEW IF EXISTS CzasPracyKierowcow
DROP VIEW IF EXISTS RozliczeniePracyKierowcow
DROP PROCEDURE IF EXISTS DodajKierowceZAdresem
DROP PROCEDURE IF EXISTS EdytujKierowceZAdresem
GO

CREATE VIEW CzasPracyKierowcow
AS
	SELECT K.KierowcaID, K.Imie, K.Nazwisko, SUM(FLOOR(CAST(DATEDIFF(MINUTE, P.DataRozpoczeciaPrzejazdu, P.DataZakonczeniaPrzejazdu) AS FLOAT) / 0.6)/100) [Czas Przejazdu (h)], 
			CONCAT(MONTH(P.DataZakonczeniaPrzejazdu), '.', YEAR(P.DataZakonczeniaPrzejazdu)) [Miesiac]
	FROM Kierowcy K JOIN Przejazdy P ON K.KierowcaID = P.KierowcaID
	GROUP BY K.KierowcaID, K.Imie, K.Nazwisko, CONCAT(MONTH(P.DataZakonczeniaPrzejazdu), '.', YEAR(P.DataZakonczeniaPrzejazdu))
GO

CREATE VIEW RozliczeniePracyKierowcow
AS
SELECT A.KierowcaID, A.Imie, A.Nazwisko, ROUND(SUM(A.[Czas Przejazdu (h)]*A.StawkaGodzinowaBruttoKierowcy),2) [Wynagrodzenie (zl)],A.Miesiac
FROM
	(SELECT K.KierowcaID, K.Imie, K.Nazwisko, SUM(FLOOR(CAST(DATEDIFF(MINUTE, P.DataRozpoczeciaPrzejazdu, P.DataZakonczeniaPrzejazdu) AS FLOAT) / 0.6)/100) [Czas Przejazdu (h)], 
			P.StawkaGodzinowaBruttoKierowcy, CONCAT(MONTH(P.DataZakonczeniaPrzejazdu), '.', YEAR(P.DataZakonczeniaPrzejazdu)) [Miesiac]
	FROM Kierowcy K JOIN Przejazdy P ON K.KierowcaID = P.KierowcaID
	GROUP BY K.KierowcaID, K.Imie, K.Nazwisko, P.StawkaGodzinowaBruttoKierowcy, CONCAT(MONTH(P.DataZakonczeniaPrzejazdu), '.', YEAR(P.DataZakonczeniaPrzejazdu))) A
	GROUP BY A.KierowcaID, A.Imie, A.Nazwisko, A.Miesiac
GO

CREATE PROCEDURE DodajKierowceZAdresem
    @Imie NVARCHAR(50),
    @Nazwisko NVARCHAR(50),
    @Pesel CHAR(11) = NULL,
    @StawkaGodzinowaBrutto MONEY,
    @DataUrodzenia DATE,
    @Kraj NVARCHAR(50),
    @Miejscowosc NVARCHAR(50),
    @Ulica NVARCHAR(50),
    @NumerBudynku NVARCHAR(20),
    @NumerLokalu NVARCHAR(20) = NULL,
    @KodPocztowy NVARCHAR(15) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AdresID INT;

    SELECT @AdresID = AdresID
    FROM Adresy
    WHERE Kraj = @Kraj
        AND Miejscowosc = @Miejscowosc
        AND Ulica = @Ulica
        AND NumerBudynku = @NumerBudynku
        AND ISNULL(NumerLokalu, '') = ISNULL(@NumerLokalu, '')
        AND ISNULL(KodPocztowy, '') = ISNULL(@KodPocztowy, '');

    IF @AdresID IS NULL
    BEGIN
        INSERT INTO Adresy (Kraj, Miejscowosc, Ulica, NumerBudynku, NumerLokalu, KodPocztowy)
        VALUES (@Kraj, @Miejscowosc, @Ulica, @NumerBudynku, @NumerLokalu, @KodPocztowy);

        SET @AdresID = SCOPE_IDENTITY();
    END;

    INSERT INTO Kierowcy (Imie, Nazwisko, Pesel, StawkaGodzinowaBrutto, DataUrodzenia, AdresID)
    VALUES (@Imie, @Nazwisko, @Pesel, @StawkaGodzinowaBrutto, @DataUrodzenia, @AdresID);
END;
GO

CREATE PROCEDURE EdytujKierowceZAdresem
	@KierowcaID INT,
    @Imie NVARCHAR(50),
    @Nazwisko NVARCHAR(50),
    @Pesel CHAR(11) = NULL,
    @StawkaGodzinowaBrutto MONEY,
    @DataUrodzenia DATE,
    @Kraj NVARCHAR(50),
    @Miejscowosc NVARCHAR(50),
    @Ulica NVARCHAR(50),
    @NumerBudynku NVARCHAR(20),
    @NumerLokalu NVARCHAR(20) = NULL,
    @KodPocztowy NVARCHAR(15) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AdresID INT;

    SELECT @AdresID = AdresID
    FROM Adresy
    WHERE Kraj = @Kraj
        AND Miejscowosc = @Miejscowosc
        AND Ulica = @Ulica
        AND NumerBudynku = @NumerBudynku
        AND ISNULL(NumerLokalu, '') = ISNULL(@NumerLokalu, '')
        AND ISNULL(KodPocztowy, '') = ISNULL(@KodPocztowy, '');

    IF @AdresID IS NULL
    BEGIN
        INSERT INTO Adresy (Kraj, Miejscowosc, Ulica, NumerBudynku, NumerLokalu, KodPocztowy)
        VALUES (@Kraj, @Miejscowosc, @Ulica, @NumerBudynku, @NumerLokalu, @KodPocztowy);

        SET @AdresID = SCOPE_IDENTITY();
    END;

    UPDATE Kierowcy 
	SET Imie = @Imie, Nazwisko = @Nazwisko, Pesel = @Pesel, StawkaGodzinowaBrutto = @StawkaGodzinowaBrutto, DataUrodzenia = @DataUrodzenia, AdresID = @AdresID
    WHERE KierowcaID = @KierowcaID
END;
GO

-- Samochody

DROP VIEW IF EXISTS RaportPracySamochodow
GO

CREATE VIEW RaportPracySamochodow
AS
	SELECT S.SamochodCiezarowyID, S.Rejestracja, SUM(FLOOR(CAST(DATEDIFF(MINUTE, P.DataRozpoczeciaPrzejazdu, P.DataZakonczeniaPrzejazdu) AS FLOAT) / 0.6)/100) [Czas Przejazdu (h)], 
			ROUND(AVG(P.SrednieSpalanieNa100km),2) [Srednie spalanie (100km)], AVG(P.WspolczynnikLadownosci) [Sredni zaladunek], SUM(P.[LacznaOdlegloscPrzejazdu(km)]) [Laczna odleglosc],
			CONCAT(MONTH(P.DataZakonczeniaPrzejazdu), '.', YEAR(P.DataZakonczeniaPrzejazdu)) [Miesiac]
	FROM SamochodyCiezarowe S JOIN Przejazdy P ON S.SamochodCiezarowyID = P.SamochodCiezarowyID
	GROUP BY S.SamochodCiezarowyID, S.Rejestracja, CONCAT(MONTH(P.DataZakonczeniaPrzejazdu), '.', YEAR(P.DataZakonczeniaPrzejazdu))
GO

-- Przejazdy

DROP VIEW IF EXISTS KosztyPrzejazdow
GO

CREATE VIEW KosztyPrzejazdow
AS
SELECT *, (P.[Koszt paliwa] + P.[Pensja kierowcy] + P.DodatkoweKoszty) [Laczny koszt]
FROM
	(SELECT PrzejazdID, ROUND([ZuzytePaliwo(L)] * [CenaPaliwa(zl/l)],2) [Koszt paliwa], 
	ROUND((FLOOR(CAST(DATEDIFF(MINUTE, DataRozpoczeciaPrzejazdu, DataZakonczeniaPrzejazdu) AS FLOAT) / 0.6)/100) * StawkaGodzinowaBruttoKierowcy,2) [Pensja kierowcy], DodatkoweKoszty
	FROM Przejazdy) P
	
GO