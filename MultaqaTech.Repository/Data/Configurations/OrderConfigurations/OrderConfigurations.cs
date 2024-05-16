namespace MultaqaTech.Repository.Data.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.Property(o => o.Basket)
               .HasConversion(v => JsonSerializer
               .Serialize(v, (JsonSerializerOptions?)null), v => JsonSerializer
               .Deserialize<StudentBasket>(v, (JsonSerializerOptions?)null));
    }
}