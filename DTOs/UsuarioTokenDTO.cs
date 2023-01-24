namespace desafio_dotne.DTOs
{
    public class UsuarioTokenDTO
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
}
