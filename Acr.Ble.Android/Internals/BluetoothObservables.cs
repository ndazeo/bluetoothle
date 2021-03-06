using System;
using System.Reactive.Linq;
using Android.Bluetooth;

#if BLE
namespace Acr.Ble.Internals
#else
namespace Acr.Bluetooth.Internals
#endif
{
    public static class BluetoothObservables
    {
        public static IObservable<object> WhenAdapterStatusChanged()
        {
            return AndroidObservables.WhenIntentReceived(BluetoothAdapter.ActionStateChanged);
        }


        public static IObservable<object> WhenAdapterDiscoveryStarted()
        {
            return AndroidObservables.WhenIntentReceived(BluetoothAdapter.ActionDiscoveryStarted);
        }


        public static IObservable<object> WhenAdapterDiscoveryFinished()
        {
            return AndroidObservables.WhenIntentReceived(BluetoothAdapter.ActionDiscoveryFinished);
        }


        public static IObservable<BluetoothDevice> WhenDeviceNameChanged()
        {
            return WhenDeviceEventReceived(BluetoothDevice.ActionNameChanged);
        }


        public static IObservable<BluetoothDevice> WhenDeviceEventReceived(string action)
        {
            return AndroidObservables
                .WhenIntentReceived(action)
                .Select(intent =>
                {
                    var device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                    return device;
                });
        }
    }
}