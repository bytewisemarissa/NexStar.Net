using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NexStarEmulator.Processors;

namespace NexStarEmulator
{
    public class NexStarEmulator
    {
        private readonly string _portName;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ProcessorType _currentProcessorType;
        private Task _emulatorTask;

        public NexStarEmulator(string portName, ProcessorType processorType)
        {
            _portName = portName;
            _currentProcessorType = processorType;

            _cancellationTokenSource = new CancellationTokenSource();
            _emulatorTask = new Task(RunSimulation, _cancellationTokenSource.Token);
        }

        private void RunSimulation(object inCancelationToken)
        {
            CancellationToken cancellationToken = (CancellationToken) inCancelationToken;

            SerialPort simulatorSerialPort = new SerialPort(_portName)
            {
                BaudRate = 9600,
                ReadTimeout = 500,
                WriteTimeout = 4000,
                StopBits = StopBits.One,
                Parity = Parity.None
            };

            IProcessor requestProcessor = null;
            switch (_currentProcessorType)
            {
                case ProcessorType.Telescope_114LCM:
                    requestProcessor = new RequestProcessor_114LCM();
                    break;
            }

            try
            {
                simulatorSerialPort.Open();

                while (!cancellationToken.IsCancellationRequested)
                {
                    if (simulatorSerialPort.BytesToRead > 0)
                    {
                        byte[] incomingBytes = new byte[simulatorSerialPort.BytesToRead];
                        simulatorSerialPort.Read(incomingBytes, 0, simulatorSerialPort.BytesToRead);

                        byte[] returnBytes = requestProcessor.ProcessRequestBytes(incomingBytes);
                        simulatorSerialPort.Write(returnBytes, 0, returnBytes.Length);
                    }
                }
            }
            finally
            {
                if (simulatorSerialPort.IsOpen)
                {
                    simulatorSerialPort.Close();
                }
            }
        }
    }
}
