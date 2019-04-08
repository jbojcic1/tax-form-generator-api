namespace TaxFormGeneratorApi.Dtos
{
    public class AuthDto
    {
        public string AccessToken { get; set; }
        
        public int AccessExpiresIn { get; set; }
        
        public string RefreshToken { get; set; }
        
        public int RefreshExpiresIn { get; set; }
    }
}