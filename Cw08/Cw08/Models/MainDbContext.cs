using Microsoft.EntityFrameworkCore;
using System;

namespace Cw08.Models
{
    public class MainDbContext : DbContext
    {
        protected MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\zenek\\Documents\\databaseAPBD.mdf;Integrated Security=True;Connect Timeout=30");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(p =>
            {
                p.HasKey(e => e.IdPatient);
                p.Property(e => e.IdPatient).ValueGeneratedOnAdd();
                p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                p.Property(e => e.BirthDate).IsRequired().HasMaxLength(100);


                p.HasData(
                    new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", BirthDate = DateTime.Parse("2000-01-01") },
                    new Patient { IdPatient = 2, FirstName = "Adam", LastName = "Nowak", BirthDate = DateTime.Parse("2000-02-02") }

                );
            });

            modelBuilder.Entity<Doctor>(d =>
           {
               d.HasKey(e => e.IdDoctor);
               d.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
               d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
               d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
               d.Property(e => e.Email).IsRequired().HasMaxLength(100);

               d.HasData(
               new Doctor { IdDoctor = 1, FirstName = "Paweł", LastName = "Łoś", Email = "łośpaweł@gmail.com" },
               new Doctor { IdDoctor = 2, FirstName = "Jerzy", LastName = "Świeży", Email = "świeżyjerzy@gmail.com" }

           );
           });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.IdPrescription).ValueGeneratedOnAdd();
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();

                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);


                p.HasData(
               new Prescription { IdPrescription = 1, Date = DateTime.Parse("2022-01-01"), DueDate = DateTime.Parse("2022-11-11"), IdDoctor = 1, IdPatient = 2 },
               new Prescription { IdPrescription = 2, Date = DateTime.Parse("2022-02-02"), DueDate = DateTime.Parse("2022-12-12"), IdDoctor = 2, IdPatient = 2 }
           );
            });

            modelBuilder.Entity<Medicament>(med =>
            {
                med.HasKey(m => m.IdMedicament);
                med.Property(m => m.IdMedicament).ValueGeneratedOnAdd();
                med.Property(m => m.Name).IsRequired().HasMaxLength(100);
                med.Property(m => m.Description).IsRequired().HasMaxLength(100);
                med.Property(m => m.Type).IsRequired().HasMaxLength(100);

                med.HasData(
                    new Medicament { IdMedicament = 1, Name = "Apap", Description = "Na bóle głowy", Type = "Przeciwbólowy" },
                    new Medicament { IdMedicament = 2, Name = "No-Spa", Description = "Na ból brzucha", Type = "Przeciwbólowy" }
                    );

            });

            modelBuilder.Entity<PrescriptionMedicament>(pm =>
            {
                pm.HasKey(p => new
                {
                    p.IdMedicament,
                    p.IdPrescription
                });
                pm.Property(p => p.Dose);
                pm.Property(p => p.Details).IsRequired().HasMaxLength(100);

                pm.HasOne(p => p.Medicament).WithMany(p => p.PrescriptionMedicaments).HasForeignKey(p => p.IdMedicament).OnDelete(DeleteBehavior.Restrict);
                pm.HasOne(p => p.Prescription).WithMany(p => p.PrescriptionMedicaments).HasForeignKey(p => p.IdPrescription).OnDelete(DeleteBehavior.Restrict);


                pm.HasData(
                    new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 100, Details = "2 tabletki rano" },
                    new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 2, Dose = 10, Details = "2 tabletki rano i wieczorem" },
                    new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 1, Dose = 5, Details = "1 tabletka rano" }
                    );

            });




        }


    }
}
