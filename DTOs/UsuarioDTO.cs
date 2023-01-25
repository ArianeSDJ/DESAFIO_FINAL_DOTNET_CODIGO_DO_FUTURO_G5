namespace desafio_dotne.DTOs
{
    public class UsuarioDTO
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? ConfirmPassword { get; set; }
    }
}
