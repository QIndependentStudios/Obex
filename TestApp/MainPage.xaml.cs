using QIndependentStudios.Obex.Service;
using QIndependentStudios.Obex.Uwp;
using System;
using System.Linq;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Run();
        }

        private async void Run()
        {
            var deviceInfos = await DeviceInformation.FindAllAsync(
                   RfcommDeviceService.GetDeviceSelector(RfcommServiceId.FromUuid(Guid.Parse("00001132-0000-1000-8000-00805f9b34fb"))),
                   null);


            var device = await BluetoothDevice.FromIdAsync(deviceInfos.FirstOrDefault()?.Id);


            var mapRfcommServiceId = RfcommServiceId.FromUuid(Guid.Parse("00001132-0000-1000-8000-00805f9b34fb"));
            var services = await device.GetRfcommServicesForIdAsync(mapRfcommServiceId);
            var service = services.Services.FirstOrDefault();
            var connection = ObexConnection.Create(service);

            var client = new ObexServiceClient(connection, Guid.Parse("bb582b40-420c-11db-b0de-0800200c9a66"));

            await client.ConnectAsync();

            await client.GetFoldersAsync();
        }
    }
}
