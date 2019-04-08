namespace TaxFormGeneratorApi.Dtos
{
    public class RefreshTokenResponseDto
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}