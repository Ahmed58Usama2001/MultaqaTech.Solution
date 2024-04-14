namespace MultaqaTech.APIs.Helpers;

public class GenericMediaUrlResolver<TSource, TDestination>(IConfiguration configuration) : IValueResolver<TSource, TDestination, string> where TSource : BaseEntityWithMediaUrl
{
    private readonly IConfiguration _configuration = configuration;

    public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.MediaUrl))
            return $"{_configuration["ApiBaseUrl"]}/{source.MediaUrl}";

        return string.Empty;
    }
}
