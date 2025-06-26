namespace BikeAppApp.Shared.Dtos
{
    public class BakimGecmisiUpdateDto
    {
        public int BakimGecmisiId { get; set; }  // Usually needed to identify the record
        public int MotosikletId { get; set; }
        public DateTime BakimTarihi { get; set; }
        public string Aciklama { get; set; } = null!;
        // All editable fields here
    }
}
