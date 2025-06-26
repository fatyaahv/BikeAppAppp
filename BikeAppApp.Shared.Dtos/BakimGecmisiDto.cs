namespace BikeAppApp.Shared.Dtos
{
    public class BakimGecmisiDto
    {
        public int BakimGecmisiId { get; set; }
        public int MotosikletId { get; set; }
        public DateTime BakimTarihi { get; set; }
        public string Aciklama { get; set; } = null!;
        // Add other properties as needed
    }
}
