namespace AuthoriztionDemo.Models
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public int Expire { get; set; }
        public int RefreshExpire { get; set; }
    }
}