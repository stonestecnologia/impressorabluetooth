using Acr.UserDialogs;
using ImpressoraBluetooth.Service;
using Android.Bluetooth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(ImpressoraBluetooth.Droid.Services.BluetoothService))]
namespace ImpressoraBluetooth.Droid.Services
{
    class BluetoothService : IBluetoothService
    {
        private static BluetoothSocket _socket;
        private static BluetoothAdapter _adapter = BluetoothAdapter.DefaultAdapter;
        private string _address = null;

        private BluetoothDevice device = null;

        private readonly string UU_ID = "00001101-0000-1000-8000-00805f9b34fb";

        public List<ImpressoraBluetooth.Models.Bluetooth> PairedDevicesList()
        {
            List<ImpressoraBluetooth.Models.Bluetooth> devices = new List<ImpressoraBluetooth.Models.Bluetooth>();
            try
            {
                ICollection<BluetoothDevice> pareados = _adapter.BondedDevices;
                foreach (var device in pareados)
                {
                    devices.Add(new Models.Bluetooth()
                    {
                        Name = device.Name,
                        Address = device.Address
                    });
                }
            }
            catch { }
            return devices;
        }

        public async Task PrintText(string text, string address)
        {
            string printAdressMemory = Utils.GetDataInMemory("impressora");
            if (string.IsNullOrEmpty(printAdressMemory))
            {
                UserDialogs.Instance.Alert("Impressora não configurada.");
            }
            _address = address ?? printAdressMemory;

            if (_adapter == null)
            {
                UserDialogs.Instance.Alert("Adaptador Bluetooth não encontrado!");
            }
            if (string.IsNullOrEmpty(_address))
            {
                UserDialogs.Instance.Alert("Nenhuma Impressora foi selecionada, Por favor verifique o pareamento da impressora com seu aparelho e tente novamente.");
            }
            if (!_adapter.IsEnabled)
            {
                _adapter.Enable();
            }

            if (_adapter.StartDiscovery())
            {
                if (string.IsNullOrEmpty(_address))
                {
                    UserDialogs.Instance.Alert("Não foi possível localizar uma impressora, por favor, vá até a tela de configuração e realize uma conexão com a impressora.");
                }

                device = _adapter.GetRemoteDevice(_address);

                if (device == null)
                {
                    UserDialogs.Instance.Alert("Não foi possível estabelecer conexão com Impressora.");
                }

                _socket = device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(UU_ID));
            }

            try
            {
                if (!_socket.IsConnected)
                {
                    await _socket.ConnectAsync();
                }
            }
            catch
            {
                try
                {
                    _socket = (BluetoothSocket) device.Class.GetMethod("createRfcommSocket", Java.Lang.Integer.Type).Invoke(device, 1);
                    if (!_socket.IsConnected)
                    {
                        _socket.Connect();
                    }
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.Alert("Não foi possível estabelecer conexão com impressora." + e.Message);
                }
            }

            byte[] txt = Encoding.UTF8.GetBytes(text);
            var output = _socket.OutputStream;
            try
            {
                int tamanhoBuffer = 20;
                if (txt.Length > tamanhoBuffer)
                {
                    byte[] bf = new byte[tamanhoBuffer];
                    int pos = 0;
                    for (int i = 0; i < txt.Length; i++)
                    {
                        bf[pos++] = txt[i];
                        if (pos == tamanhoBuffer || (i == txt.Length - 1))
                        {
                            output.Write(bf, 0, bf.Length);
                            pos = 0;
                            bf = new byte[tamanhoBuffer];
                        }
                    }
                }
                else
                {
                    output.Write(txt, 0, txt.Length);
                }
                output.Flush();
            }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert("Não foi possível realizar impressão. Erro : " + e.Message);
            }
            finally
            {
                output.Close();
                _socket.Close();

                output.Dispose();
                _socket.Dispose();
            }

            await Task.CompletedTask;
        }    
    }
}