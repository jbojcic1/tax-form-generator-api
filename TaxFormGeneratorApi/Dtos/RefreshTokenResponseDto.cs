namespace TaxFormGeneratorApi.Dtos
{
    public class RefreshTokenResponseDto
    {
        public string Token { get; set; }

        public int ExpiresIn { get; set; }
    }
}