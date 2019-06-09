//using Microsoft.EntityFrameworkCore;
//using SouthRealEstate.DAL.Entities;

//namespace SouthRealEstate.DAL
//{
//    public class RealEstateDbContext : DbContext
//    {
//        private string m_ConnectionString = null;
//        /* db sets */
//        public virtual DbSet<CityEntity> Cities { get; set; }
//        public virtual DbSet<ResidentalPropertyEntity> ResidentalProperties { get; set; }
        
//        /* static */
//        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(RealEstateDbContext));

//        public RealEstateDbContext()
//        {
//            string conString = @"server=127.0.0.1;port=3306;user id=user1; password=1qaz2wsx; database=realestate; pooling=true; CharSet=utf8; Allow User Variables=True; Convert Zero Datetime=True; default command timeout=720";

//            m_ConnectionString = conString;
//        }

//        public RealEstateDbContext(string connectionString)
//        {
//            m_ConnectionString = connectionString;
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseMySQL(m_ConnectionString);
//            /*
//            ILoggerFactory loggerFactory = new LoggerFactory();
//            loggerFactory.AddProvider(
//              new ConsoleLoggerProvider(
//                (text, logLevel) => logLevel >= LogLevel.Trace, true));
//            optionsBuilder.UseLoggerFactory(loggerFactory);
//            */
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            /* client IP entity */
//            ClientIpEntityConfiguration(modelBuilder);
//        }
        
//        private void ClientIpEntityConfiguration(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<CityEntity>(entity =>
//            {
//                modelBuilder.Entity<CityEntity>()
//                    .ToTable("cities2")
//                    .HasKey(c => new { c.Id });

//                /* client ID */
//                entity.Property(i => i.Name)
//                  .HasColumnName("name");
//            });


//            //modelBuilder.Entity<ResidentalPropertyEntity>(entity =>
//            //{
//            //    modelBuilder.Entity<ResidentalPropertyEntity>()
//            //        .ToTable("properties_residental")
//            //        .HasKey(c => new { c.Id });

//            //    /* client ID */
//            //    entity.Property(i => i.Name)
//            //      .HasColumnName("name");
//            //});
//        }
//    }
//}
