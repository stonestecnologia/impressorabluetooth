namespace ImpressoraBluetooth.Models
{
    public class Bluetooth
    {
        public override string ToString()
        {
            return Name;
        }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
