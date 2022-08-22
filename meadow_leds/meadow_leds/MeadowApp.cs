using System;
using System.Threading;
using System.Threading.Tasks;

using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;

namespace meadow_leds
{
    // Change F7FeatherV2 to F7FeatherV1 for V1.x boards
    public class MeadowApp : App<F7FeatherV1, MeadowApp>
    {
        private RgbPwmLed _onboardLed;

        public MeadowApp()
        {
            Initialize().Wait();

            // heart beat
            CycleOnboardLed(TimeSpan.FromMilliseconds(1000));

            MultiColorLedController.Current.SetColor(Color.Green);

        }

        private async Task Initialize()
        {
            Console.WriteLine("Initialize hardware...");

            _onboardLed = new RgbPwmLed(device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue,
                Meadow.Peripherals.Leds.IRgbLed.CommonType.CommonAnode);

            MultiColorLedController.Current.Initialize();

            await Task.CompletedTask;
        }

        private void CycleOnboardLed(TimeSpan duration)
        {
            Console.WriteLine("Cycle colors...");

            while (true)
            {
                ShowColor(Color.Red, duration);
                ShowColor(Color.Blue, duration);
            }
        }

        private void ShowColor(Color color, TimeSpan duration)
        {
            Console.WriteLine($"Color: {color}");
            _onboardLed.SetColor(color);
            Thread.Sleep(duration);
            _onboardLed.Stop();
        }
    }
}
