using System;
using System.Threading.Tasks;
using System.Threading;

using Meadow.Foundation.Leds;
using Meadow.Foundation;
using Meadow.Hardware;

namespace meadow_leds
{
    internal class MultiColorLedController
    {
        private readonly Random _random;
        private RgbPwmLed _rgbPwmLed;
        private Task _animationTask = null;
        private CancellationTokenSource _cancellationTokenSource = null;        

        protected bool Initialized = false;        

        public static MultiColorLedController Current { get; private set; }

        private MultiColorLedController()
        {
            _random = new Random();
        }

        static MultiColorLedController()
        {
            Current = new MultiColorLedController();
        }

        public void Initialize(IPwmOutputController outputController, IPin redPwmPin, IPin greenPwmPin, IPin bluePwmPin)
        {
            if (Initialized)
            {
                return;
            }
            
            _rgbPwmLed = new RgbPwmLed(
                device: outputController,
                redPwmPin: redPwmPin,
                greenPwmPin: greenPwmPin,
                bluePwmPin: bluePwmPin);

            _rgbPwmLed.SetColor(Color.Red);

            Initialized = true;

            StartRunningColors();
        }

        private void Stop()
        {
            _rgbPwmLed.Stop();
            _cancellationTokenSource?.Cancel();
        }

        public void SetColor(Color color)
        {
            Stop();
            _rgbPwmLed.SetColor(color);
        }

        public void TurnOn()
        {
            Stop();
            _rgbPwmLed.SetColor(GetRandomColor());
            _rgbPwmLed.IsOn = true;
        }

        public void TurnOff()
        {
            Stop();
            _rgbPwmLed.IsOn = false;
        }

        public void StartBlink()
        {
            Stop();
            _rgbPwmLed.StartBlink(GetRandomColor());
        }

        public void StartPulse()
        {
            Stop();
            _rgbPwmLed.StartPulse(GetRandomColor());
        }

        public void StartRunningColors()
        {
            _rgbPwmLed.Stop();

            _animationTask = new Task(async () =>
            {
                _cancellationTokenSource = new CancellationTokenSource();
                await StartRunningColors(_cancellationTokenSource.Token);
            });
            _animationTask.Start();
        }

        protected async Task StartRunningColors(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                _rgbPwmLed.SetColor(GetRandomColor());
                await Task.Delay(1000);
            }
        }

        protected Color GetRandomColor()
        {
            return Color.FromHsba(_random.NextDouble(), 1, 1);
        }

    }
}
