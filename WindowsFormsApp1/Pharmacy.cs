namespace OptimumPharmacy
{
    // Класс Аптека
    public class Pharmacy
    {
        public int idPharmacy { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public string city { get; set; }
        public string namePharmacy { get; set; }
        public string address { get; set; }
        public string site { get; set; }
        public string phone { get; set; }
        public double timeOpening { get; set; }
        public double timeClosing { get; set; }

        public Pharmacy() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Id">ID аптеки</param>
        /// <param name="X">Координата Х аптеки</param>
        /// <param name="Y"Координата Y аптеки></param>
        /// <param name="City">Город аптеки</param>
        /// <param name="Name">Название аптеки</param>
        /// <param name="Address">Адрес аптеки</param>
        /// <param name="Site">Сайт аптеки</param>
        /// <param name="Phone">Телефон аптеки</param>
        /// <param name="TimeOpen">Время открытия работы аптеки</param>
        /// <param name="TimeClose">Время закрытия работы аптеки</param>
        public Pharmacy(int Id, double X, double Y, string City, string Name, string Address, string Site, 
            string Phone, double TimeOpen, double TimeClose)
        {
            idPharmacy = Id;
            x = X;
            y = Y;
            city = City;
            namePharmacy = Name;
            address = Address;
            site = Site;
            phone = Phone;
            timeOpening = TimeOpen;
            timeClosing = TimeClose;
        }
    }
}