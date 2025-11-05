// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using System.Text;
// using System.Threading.Tasks;
// using GymSystemG2AL.Entities;
// using Microsoft.EntityFrameworkCore;

// public class GymSystemDBContext : DbContext
// {
//     // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     // {
//     //     optionsBuilder.UseSqlServer("Server=localhost;Database=GymSystemDB;User Id=sa;Password=Ethar2025@#;TrustServerCertificate=True;");
//     // }

//    public GymSystemDBContext(DbContextOptions<GymSystemDBContext> options): base(options)
//     {
//     }


//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

//     }

//     #region Tables
//     internal DbSet<Member> Members { get; set; }
//     internal DbSet<Trainer> Trainers{ get; set; }
//     internal DbSet<HealthRecord> HealthRecords { get; set; }
//     internal DbSet<Plan> Plans { get; set; }
//     internal DbSet<Category> Categories { get; set; }
//     internal DbSet<Session> Sessions { get; set; }
    
//     internal DbSet<MemberShip> MemberShips { get; set; }
//     internal DbSet<MemberSession> MemberSessions { get; set; }

    
//     #endregion
// }

using System.Reflection;
using GymSystemG2AL.Entities;
using Microsoft.EntityFrameworkCore;

public class GymSystemDBContext : DbContext
{
    public GymSystemDBContext(DbContextOptions<GymSystemDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    #region Tables
    public DbSet<Member> Members { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<HealthRecord> HealthRecords { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<MemberShip> MemberShips { get; set; }
    public DbSet<MemberSession> MemberSessions { get; set; }
    #endregion
}


