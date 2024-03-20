namespace MultaqaTech.APIs.Helpers;

public class GenericPictureUrlResolver<TSource, TDestination>(IConfiguration configuration) : IValueResolver<TSource, TDestination, string> where TSource : BaseEntityWithPictureUrl
{
    private readonly IConfiguration _configuration = configuration;

    public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
            return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";

        return string.Empty;
    }
}
