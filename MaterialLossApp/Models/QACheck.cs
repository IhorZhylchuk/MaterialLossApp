namespace MaterialLossApp.Models
{
    public class QACheck
    {
        public bool Pasteryzacja { get; set; }
        public string PasteryzacjaKomentarz {get; set; } = string.Empty;
        public bool CiałaObce { get; set; }
        public string CiałaObceKomentarz { get; set; } = string.Empty;
        public bool DataOpakowania { get; set; }
        public string DataOpakowaniaKomentarz { get; set; } = string.Empty;
        public bool Receptura { get; set; }
        public string RecepturaKomentarz { get; set; } = string.Empty;
        public bool MetalDetektor { get; set; }
        public string MetalDetektorKomentarz { get; set; } = string.Empty;
        public bool Opakowanie { get; set; }
        public string OpakowanieKomentarz { get; set; } = string.Empty;
        public float Lepkość { get; set; }
        public float Ekstrakt { get; set; }
        public float Ph { get; set; }
        public float Temperatura { get; set; }
    }
}
