using System;
using System.Threading.Tasks;

using Meadow;
using Meadow.Devices;

namespace meadow_leds
{
    // Change F7FeatherV2 to F7FeatherV1 for V1.x boards
    public class MeadowApp : App<F7FeatherV1, MeadowApp>
    {
        public MeadowApp()
        {
            Initialize().Wait(); 
        }

        private async Task Initialize()
        {
            Console.WriteLine("Initialize hardware...");

            ColorCyclingLedController.Current.Initialize(
                    Device,
                    Device.Pins.OnboardLedRed,
                    Device.Pins.OnboardLedGreen,
                    Device.Pins.OnboardLedBlue
                );            

            await Task.CompletedTask;
        }                
    }
}
