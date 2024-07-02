using Npgsql;

namespace Takeaway.Services.CouponGrpc.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry!.Value;

            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var configuration = service.GetRequiredService<IConfiguration>();
                var logger = service.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating postresql database.");

                    using var connection = new NpgsqlConnection
                        (configuration.GetValue<string>("ConnectionStrings:CouponConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"create table Coupon(
												CouponId serial primary key not null,
												CouponCode varchar(24) not null,
												DiscountAmount float,
												MinAmount int
											) ";
                    command.ExecuteNonQuery();

                    command.CommandText = "insert into Coupon (CouponCode,DiscountAmount,MinAmount) values ('PAD1289',16,38);";
                    command.ExecuteNonQuery();

                    command.CommandText = "insert into Coupon (CouponCode,DiscountAmount,MinAmount) values ('PAD1287',12,38);";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postresql database.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postresql database.");
                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }

            return host;
        }
    }
}
