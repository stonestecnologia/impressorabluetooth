using ImpressoraBluetooth.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpressoraBluetooth.Service
{
    public interface IBluetoothService
    {
        List<Bluetooth> PairedDevicesList();
        Task PrintText(string text, string address);
    }
}