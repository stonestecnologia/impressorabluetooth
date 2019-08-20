
using Acr.UserDialogs;
using ImpressoraBluetooth.Models;
using ImpressoraBluetooth.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ImpressoraBluetooth.Pages
{
    public partial class PrintConfigPage : ContentPage
    {
        private readonly IBluetoothService service = DependencyService.Get<IBluetoothService>();
        public PrintConfigPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BuscarDispositivos();
        }

        private void BuscarDispositivos()
        {
            List<Bluetooth> list = service.PairedDevicesList();
            pk_impressoras.ItemsSource = list;
            string dado = Utils.GetDataInMemory("impressora");

            if (!string.IsNullOrEmpty(dado))
            {
                pk_impressoras.SelectedItem = list.FirstOrDefault(i => dado.Equals(i.Name));
            }

        }

        public void TestarImpressao_Clicked(object sender, System.EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Por favor, aguarde..."))
            {
                try
                {
                    if (pk_impressoras.SelectedItem is Bluetooth item)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(ESCPOS.INIT_PRINTER);
                        sb.Append(ESCPOS.BUZZ); // Bip impressora
                        sb.Append(ESCPOS.LF);
                        sb.Append(ESCPOS.LF);
                        sb.Append(ESCPOS.TEXT_CENTER + "HELLO WORD" + ESCPOS.LF);
                        sb.Append(ESCPOS.TEXT_CENTER + "IMPRESSORAS BLUETOOTH" + ESCPOS.LF);
                        sb.Append(ESCPOS.TEXT_CENTER + "Compartilhe conhecimento" + ESCPOS.LF);
                        sb.Append(ESCPOS.LF);

                        service.PrintText(sb.ToString(), item.Address);
                    }
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message);
                }
            }
        }

        void Salvar_Clicked(object sender, System.EventArgs e)
        {
            if (pk_impressoras.SelectedItem is Bluetooth item)
            {
                Utils.SavesInMemory("impressora", item.Address);
                UserDialogs.Instance.Alert("Impressora salva com sucesso!");
            }
            else
            {
                UserDialogs.Instance.Alert("Selecione uma impressora");
            }
        }
    }
}