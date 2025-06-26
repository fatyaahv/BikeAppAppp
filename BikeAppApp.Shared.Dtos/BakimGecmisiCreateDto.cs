namespace BikeAppApp.Shared.Dtos
{
    public class BakimGecmisiCreateDto
    {
        public int MotosikletId { get; set; }
        public DateTime BakimTarihi { get; set; }
        public string Aciklama { get; set; } = null!;
        // Include only the fields required for creation (usually excludes Id)
    }
}
