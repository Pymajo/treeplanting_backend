using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using netgo.treeplanting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Database
{
    public static class DbContextUtility
    {
        public const string IsDeletedProperty = "Deleted";

        public static readonly MethodInfo PropertyMethod = typeof(EF)
            .GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public)
            !.MakeGenericMethod(typeof(bool));

        public static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            ParameterExpression parm = Expression.Parameter(type, "it");
            MethodCallExpression prop = Expression.Call(PropertyMethod, parm, Expression.Constant(IsDeletedProperty));
            BinaryExpression condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
            LambdaExpression lambda = Expression.Lambda(condition, parm);
            return lambda;
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Seedling> Seedling { get; set; } = null!;
        public DbSet<PlantingPlace> PlantingPlace { get; set; } = null!;
       
        public DbSet<Treeschool> Treeschool { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (IMutableEntityType entity in builder.Model.GetEntityTypes())
            {
                if (entity.ClrType.GetProperty(DbContextUtility.IsDeletedProperty) != null)
                {
                    builder.Entity(entity.ClrType)
                        .HasQueryFilter(DbContextUtility.GetIsDeletedRestriction(entity.ClrType));
                }
            }

            base.OnModelCreating(builder);

            ApplyConfigurations(builder);

            //make referential delete behaviour restrict instead of cascade for everything
            foreach (IMutableForeignKey relationship in builder.Model.GetEntityTypes()
                .SelectMany(x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            AddSqliteSpecifics(builder);
        }

        private static void ApplyConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new SeedlingConfiguartion());
            builder.ApplyConfiguration(new TreeschoolConfiguration());
        }

        private void AddSqliteSpecifics(ModelBuilder builder)
        {
            if (Database.ProviderName != "Microsoft.EntityFrameworkCore.Sqllite")
            {
                return;
            }

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset));

                foreach (var property in properties)
                {
                    builder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
                }
            }
        }
    }
}
