using Meadow.Hardware;

using Moq;

namespace meadow_leds.Tests
{
    public class UnitTest1
    {
        private HeartBeatLedController _controller;
        private IPwmOutputController _pwmOutputControllerMoq;
        private IPin _pin1;
        private IPin _pin2;
        private IPin _pin3;

        public UnitTest1()
        {
            _controller = HeartBeatLedController.Current;
            _pwmOutputControllerMoq = Mock.Of<IPwmOutputController>();
            _pin1 = Mock.Of<IPin>();
            _pin2 = Mock.Of<IPin>();
            _pin3 = Mock.Of<IPin>();
        }

        [Fact]
        public void Test1()
        {
            _controller.Initialize(_pwmOutputControllerMoq,_pin1,_pin2,_pin3);
        }
    }
}