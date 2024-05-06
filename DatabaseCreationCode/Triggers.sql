USE RozliczeniePrzejazdowSamochodowCiezarowych
GO

--Kierowcy
DROP TRIGGER IF EXISTS Trigger_Stawka_Min;
GO
DROP INDEX IF EXISTS dbo.Kierowcy.Index_UniquePesel;
GO

CREATE TRIGGER Trigger_Stawka_Min ON RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Kierowcy
FOR INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @minimalnaStawkaBrutto MONEY = (SELECT StawkaMinimalnaBrutto FROM Konfiguracja);
	DECLARE @StawkaBrutto MONEY

	DECLARE curStawka SCROLL CURSOR
	FOR SELECT StawkaGodzinowaBrutto
		FROM inserted
		FOR READ ONLY;

	OPEN curStawka;
	FETCH NEXT
	FROM curStawka
	INTO @StawkaBrutto;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @StawkaBrutto < @minimalnaStawkaBrutto
		BEGIN
			RAISERROR('Stawka brutto poni¿ej stawki minimalnej!',16,1)
			ROLLBACK TRANSACTION
		END
		FETCH NEXT
		FROM curStawka
		INTO @StawkaBrutto;
	END
	CLOSE curStawka;
	DEALLOCATE curStawka;
END
GO

CREATE UNIQUE INDEX Index_UniquePesel
ON dbo.Kierowcy(Pesel)
WHERE Pesel IS NOT NULL
GO

--Przejazdy
DROP TRIGGER IF EXISTS Trigger_KosztyNull, Trigger_ZgodnoscDat;
GO

CREATE TRIGGER Trigger_KosztyNull ON RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Przejazdy
AFTER INSERT, UPDATE
AS
BEGIN
	IF EXISTS (SELECT DodatkoweKoszty FROM inserted WHERE DodatkoweKoszty IS NULL) 
	BEGIN
		UPDATE Przejazdy
		SET DodatkoweKoszty = 0
		FROM Przejazdy
		WHERE DodatkoweKoszty IS NULL
	END
END
GO

CREATE TRIGGER Trigger_ZgodnoscDat ON RozliczeniePrzejazdowSamochodowCiezarowych.dbo.Przejazdy
FOR INSERT, UPDATE
AS
	SET NOCOUNT ON;
	DECLARE @DataRozpoczecia DATETIME
	DECLARE @DataZakonczenia DATETIME

	DECLARE curData SCROLL CURSOR
	FOR SELECT DataRozpoczeciaPrzejazdu, DataZakonczeniaPrzejazdu
		FROM inserted
		FOR READ ONLY;

	OPEN curData;
	FETCH NEXT
	FROM curData
	INTO @DataRozpoczecia, @DataZakonczenia;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @DataRozpoczecia >= @DataZakonczenia
		BEGIN
			RAISERROR('Data zakoñczenia przejazdu nie mo¿e byæ wczeœniejsza ni¿ data rozpoczêcia!',15,1)
			ROLLBACK TRANSACTION
		END
		FETCH NEXT
		FROM curData
		INTO @DataRozpoczecia, @DataZakonczenia;
	END
	CLOSE curData;
	DEALLOCATE curData;
GO


