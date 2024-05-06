using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TransportationAnalyticsHub.MVVM.Model.DBModels;

public partial class RozliczeniePrzejazdowSamochodowCiezarowychContext : DbContext
{
    public RozliczeniePrzejazdowSamochodowCiezarowychContext()
    {
    }

    public RozliczeniePrzejazdowSamochodowCiezarowychContext(DbContextOptions<RozliczeniePrzejazdowSamochodowCiezarowychContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adresy> Adresies { get; set; }

    public virtual DbSet<CzasPracyKierowcow> CzasPracyKierowcows { get; set; }

    public virtual DbSet<Kierowcy> Kierowcies { get; set; }

    public virtual DbSet<Konfiguracja> Konfiguracjas { get; set; }

    public virtual DbSet<KosztyPrzejazdow> KosztyPrzejazdows { get; set; }

    public virtual DbSet<Przejazdy> Przejazdies { get; set; }

    public virtual DbSet<RodzajePaliwa> RodzajePaliwas { get; set; }

    public virtual DbSet<RozliczeniePracyKierowcow> RozliczeniePracyKierowcows { get; set; }

    public virtual DbSet<RozliczeniePracySamochodow> RozliczeniePracySamochodows { get; set; }

    public virtual DbSet<SamochodyCiezarowe> SamochodyCiezarowes { get; set; }

    public virtual DbSet<TypyTowaru> TypyTowarus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ZAKOLAK\\SQLEXPRESS;Database=RozliczeniePrzejazdowSamochodowCiezarowych;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adresy>(entity =>
        {
            entity.HasKey(e => e.AdresId).HasName("PK__Adresy__DA8DEA6C9E324DB6");

            entity.ToTable("Adresy");

            entity.Property(e => e.AdresId).HasColumnName("AdresID");
            entity.Property(e => e.KodPocztowy).HasMaxLength(15);
            entity.Property(e => e.Kraj).HasMaxLength(50);
            entity.Property(e => e.Miejscowosc).HasMaxLength(50);
            entity.Property(e => e.NumerBudynku).HasMaxLength(20);
            entity.Property(e => e.NumerLokalu).HasMaxLength(20);
            entity.Property(e => e.Ulica).HasMaxLength(50);
        });

        modelBuilder.Entity<CzasPracyKierowcow>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CzasPracyKierowcow");

            entity.Property(e => e.CzasPrzejazduH).HasColumnName("Czas Przejazdu (h)");
            entity.Property(e => e.Imie).HasMaxLength(50);
            entity.Property(e => e.KierowcaId).HasColumnName("KierowcaID");
            entity.Property(e => e.Miesiac)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Nazwisko).HasMaxLength(50);
        });

        modelBuilder.Entity<Kierowcy>(entity =>
        {
            entity.HasKey(e => e.KierowcaId).HasName("PK__Kierowcy__6DD07FBD6CE16E0B");

            entity.ToTable("Kierowcy", tb => tb.HasTrigger("Trigger_Stawka_Min"));

            entity.HasIndex(e => e.Pesel, "Index_UniquePesel")
                .IsUnique()
                .HasFilter("([Pesel] IS NOT NULL)");

            entity.Property(e => e.KierowcaId).HasColumnName("KierowcaID");
            entity.Property(e => e.AdresId).HasColumnName("AdresID");
            entity.Property(e => e.Imie).HasMaxLength(50);
            entity.Property(e => e.Nazwisko).HasMaxLength(50);
            entity.Property(e => e.Pesel)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StawkaGodzinowaBrutto).HasColumnType("money");

            entity.HasOne(d => d.Adres).WithMany(p => p.Kierowcies)
                .HasForeignKey(d => d.AdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Kierowcy__AdresI__3E52440B");
        });

        modelBuilder.Entity<Konfiguracja>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Konfigur__3214EC276F7BCFF3");

            entity.ToTable("Konfiguracja");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.StawkaMinimalnaBrutto).HasColumnType("money");
        });

        modelBuilder.Entity<KosztyPrzejazdow>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("KosztyPrzejazdow");

            entity.Property(e => e.DodatkoweKoszty).HasColumnType("money");
            entity.Property(e => e.KosztPaliwa).HasColumnName("Koszt paliwa");
            entity.Property(e => e.LacznyKoszt).HasColumnName("Laczny koszt");
            entity.Property(e => e.PensjaKierowcy).HasColumnName("Pensja kierowcy");
            entity.Property(e => e.PrzejazdId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PrzejazdID");
        });

        modelBuilder.Entity<Przejazdy>(entity =>
        {
            entity.HasKey(e => e.PrzejazdId).HasName("PK__Przejazd__04BBB103611D836E");

            entity.ToTable("Przejazdy", tb =>
                {
                    tb.HasTrigger("Trigger_KosztyNull");
                    tb.HasTrigger("Trigger_ZgodnoscDat");
                });

            entity.Property(e => e.PrzejazdId).HasColumnName("PrzejazdID");
            entity.Property(e => e.CenaPaliwaZlL)
                .HasColumnType("money")
                .HasColumnName("CenaPaliwa(zl/l)");
            entity.Property(e => e.DataRozpoczeciaPrzejazdu).HasColumnType("datetime");
            entity.Property(e => e.DataZakonczeniaPrzejazdu).HasColumnType("datetime");
            entity.Property(e => e.DodatkoweKoszty).HasColumnType("money");
            entity.Property(e => e.KierowcaId).HasColumnName("KierowcaID");
            entity.Property(e => e.LacznaOdlegloscPrzejazduKm).HasColumnName("LacznaOdlegloscPrzejazdu(km)");
            entity.Property(e => e.SamochodCiezarowyId).HasColumnName("SamochodCiezarowyID");
            entity.Property(e => e.SrednieSpalanieNa100km).HasComputedColumnSql("(([ZuzytePaliwo(L)]*(100))/[LacznaOdlegloscPrzejazdu(km)])", false);
            entity.Property(e => e.StawkaGodzinowaBruttoKierowcy).HasColumnType("money");
            entity.Property(e => e.TypTowaru).HasMaxLength(50);
            entity.Property(e => e.ZuzytePaliwoL).HasColumnName("ZuzytePaliwo(L)");

            entity.HasOne(d => d.Kierowca).WithMany(p => p.Przejazdies)
                .HasForeignKey(d => d.KierowcaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Przejazdy__Kiero__4D94879B");

            entity.HasOne(d => d.SamochodCiezarowy).WithMany(p => p.Przejazdies)
                .HasForeignKey(d => d.SamochodCiezarowyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Przejazdy__Samoc__4E88ABD4");

            entity.HasOne(d => d.TypTowaruNavigation).WithMany(p => p.Przejazdies)
                .HasForeignKey(d => d.TypTowaru)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Przejazdy__TypTo__4F7CD00D");

            entity.HasMany(d => d.Adres).WithMany(p => p.Przejazds)
                .UsingEntity<Dictionary<string, object>>(
                    "PunktyTrasy",
                    r => r.HasOne<Adresy>().WithMany()
                        .HasForeignKey("AdresId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PunktyTra__Adres__5AEE82B9"),
                    l => l.HasOne<Przejazdy>().WithMany()
                        .HasForeignKey("PrzejazdId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PunktyTra__Przej__59FA5E80"),
                    j =>
                    {
                        j.HasKey("PrzejazdId", "AdresId").HasName("PK_PunktTrasy");
                        j.ToTable("PunktyTrasy");
                        j.IndexerProperty<int>("PrzejazdId").HasColumnName("PrzejazdID");
                        j.IndexerProperty<int>("AdresId").HasColumnName("AdresID");
                    });
        });

        modelBuilder.Entity<RodzajePaliwa>(entity =>
        {
            entity.HasKey(e => e.NazwaPaliwa).HasName("PK__RodzajeP__D49B36C8254F9537");

            entity.ToTable("RodzajePaliwa");

            entity.Property(e => e.NazwaPaliwa).HasMaxLength(20);
        });

        modelBuilder.Entity<RozliczeniePracyKierowcow>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RozliczeniePracyKierowcow");

            entity.Property(e => e.Imie).HasMaxLength(50);
            entity.Property(e => e.KierowcaId).HasColumnName("KierowcaID");
            entity.Property(e => e.Miesiac)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Nazwisko).HasMaxLength(50);
            entity.Property(e => e.WynagrodzenieZl).HasColumnName("Wynagrodzenie (zl)");
        });

        modelBuilder.Entity<RozliczeniePracySamochodow>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RozliczeniePracySamochodow");

            entity.Property(e => e.CzasPrzejazduH).HasColumnName("Czas Przejazdu (h)");
            entity.Property(e => e.LacznaOdleglosc).HasColumnName("Laczna odleglosc");
            entity.Property(e => e.Miesiac)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Rejestracja)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.SamochodCiezarowyId).HasColumnName("SamochodCiezarowyID");
            entity.Property(e => e.SredniZaladunek).HasColumnName("Sredni zaladunek");
            entity.Property(e => e.SrednieSpalanie100km).HasColumnName("Srednie spalanie (100km)");
        });

        modelBuilder.Entity<SamochodyCiezarowe>(entity =>
        {
            entity.HasKey(e => e.SamochodCiezarowyId).HasName("PK__Samochod__96AC3CA3795A4B33");

            entity.ToTable("SamochodyCiezarowe");

            entity.HasIndex(e => e.Rejestracja, "UQ__Samochod__3517DB0A8FF792F2").IsUnique();

            entity.Property(e => e.SamochodCiezarowyId).HasColumnName("SamochodCiezarowyID");
            entity.Property(e => e.MaksymalnaLadownoscT).HasColumnName("MaksymalnaLadownosc(t)");
            entity.Property(e => e.MaksymalnaObjetoscZaladunkuM3).HasColumnName("MaksymalnaObjetoscZaladunku(m3)");
            entity.Property(e => e.Rejestracja)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.RodzajPaliwa).HasMaxLength(20);
            entity.Property(e => e.TypTowaru).HasMaxLength(50);

            entity.HasOne(d => d.RodzajPaliwaNavigation).WithMany(p => p.SamochodyCiezarowes)
                .HasForeignKey(d => d.RodzajPaliwa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Samochody__Rodza__46E78A0C");

            entity.HasOne(d => d.TypTowaruNavigation).WithMany(p => p.SamochodyCiezarowes)
                .HasForeignKey(d => d.TypTowaru)
                .HasConstraintName("FK__Samochody__TypTo__45F365D3");
        });

        modelBuilder.Entity<TypyTowaru>(entity =>
        {
            entity.HasKey(e => e.NazwaTypu).HasName("PK__TypyTowa__E14946462C3DEC0F");

            entity.ToTable("TypyTowaru");

            entity.Property(e => e.NazwaTypu).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
