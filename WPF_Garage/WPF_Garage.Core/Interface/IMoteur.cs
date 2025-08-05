namespace WPF_Garage.Core.Interface
{
    public interface IMoteur
    {
        public TypeMoteur Type { get; set; }
        public string Cylindre { get; set; }
        public double Prix { get; set; }
    }

    public enum TypeMoteur
    {
        DIESEL,
        ESSENCE,
        HYBRIDE,
        ELECTRIQUE
    }
}
