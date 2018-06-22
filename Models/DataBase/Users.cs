namespace TelegramBot.Models.DataBase
{
    public class User
    {
        public string Id { get; set; }
        public long TelegramUserId { get; set; }
        public double S9Cost { get; set; } = 4500;
        public double L3PlusCost { get; set; } = 1500;
        public double D3Cost { get; set; } = 2700;
        public double B8Cost { get; set; } = 10000;
        public double Nvidia1070 { get; set; } = 4500;
        public double Nvidia1080Ti { get; set; } = 9000;
    }
}