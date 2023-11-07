using Infrastructure.Data;
using Microsoft.Extensions.Options;

namespace WebAPI.OptionsSetup
{
    public class MomoOptionsSetup : IConfigureOptions<MomoOptions>
    {
        private const string SectionName = "MomoConfig";
        private readonly IConfiguration configuration;

        public MomoOptionsSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(MomoOptions options)
        {
            configuration.GetSection(SectionName).Bind(options);
        }
    }
}
