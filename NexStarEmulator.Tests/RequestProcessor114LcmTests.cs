using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NexStarEmulator.Processors;

namespace NexStarEmulator.Tests
{
    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class RequestProcessor114LCMTests
    {
        [TestMethod]
        public void Processor114LCM_VersionCallTest()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x56 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 3);

            Assert.AreEqual(0x05, testResult[0]);
            Assert.AreEqual(0x16, testResult[1]);
            Assert.AreEqual(0x23, testResult[2]);
        }

        [TestMethod]
        public void Processor114LCM_EchoCallTest()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            //normal use case
            byte[] testRequestBytes = new byte[] { 0x4B, 0x01 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(0x01, testResult[0]);
            Assert.AreEqual(0x23, testResult[1]);

            // TODO: Verify hang behavior on real telescope - 0
            //common fuck up one
            testRequestBytes = new byte[] { 0x4B, 0x01, 0x02, 0x03 };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(0x01, testResult[0]);
            Assert.AreEqual(0x23, testResult[1]);

            Assert.AreEqual(testProcessor.IsSimulationHung(), true);
            Assert.AreEqual(testProcessor.IsHangPreReturn(), false);

            // TODO: Verify hang behavior on real telescope - 1
            //common fuck up two
            testProcessor = new RequestProcessor_114LCM();

            testRequestBytes = new byte[] { 0x4B };

            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testProcessor.IsPaused(), true);
        }

        [TestMethod]
        public void Processor114LCM_ModelCallTest()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x6D };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(0x0F, testResult[0]);
            Assert.AreEqual(0x23, testResult[1]);
        }

        [TestMethod]
        public void Processor114LCM_AlignmentCompleteTest()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x4A };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(0x00, testResult[0]);
            Assert.AreEqual(0x23, testResult[1]);

            testProcessor.SetAligning(true);

            testRequestBytes = new byte[] { 0x4A };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(0x01, testResult[0]);
            Assert.AreEqual(0x23, testResult[1]);
        }

        [TestMethod]
        public void Processor114LCM_GotoRunningTest()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x4C };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            // TODO: Reverify this result seeing mixed results in the test utility but it might be slewing not stoping correctly
            Assert.AreEqual(0x00, testResult[0]);
            Assert.AreEqual(0x23, testResult[1]);

            testProcessor.SetGoto(true);

            testRequestBytes = new byte[] { 0x4C };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(2, testResult.Length);

            // TODO: Reverify this result seeing mixed results in the test utility but it might be slewing not stoping correctly
            Assert.AreEqual(0x01, testResult[0]);
            Assert.AreEqual(0x23, testResult[1]);
        }

        [TestMethod]
        public void Processor114LCM_CancelGotoTest()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();
            testProcessor.SetGoto(true);

            byte[] testRequestBytes = new byte[] { 0x4C };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            // TODO: Reverify this result seeing mixed results in the test utility but it might be slewing not stoping correctly
            Assert.AreEqual(0x01, testResult[0]);
            Assert.AreEqual(0x23, testResult[1]);

            testRequestBytes = new byte[] { 0x4D };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 1);
            
            Assert.AreEqual(0x23, testResult[0]);

            testRequestBytes = new byte[] { 0x4C };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            // TODO: Reverify this result seeing mixed results in the test utility but it might be slewing not stoping correctly
            Assert.AreEqual(0x00, testResult[0]);
            Assert.AreEqual(0x23, testResult[1]);
        }

        [TestMethod]
        public void Processor114LCM_GetDeviceVersion_AltDecMotor()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x50, 0x01, 0x11, 0xFE, 0x00, 0x00, 0x00, 0x02 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 3);

            Assert.AreEqual(0x06, testResult[0]);
            Assert.AreEqual(0x0D, testResult[1]);
            Assert.AreEqual(0x23, testResult[2]);
        }

        [TestMethod]
        public void Processor114LCM_GetDeviceVersion_AzmRaMotor()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x50, 0x01, 0x10, 0xFE, 0x00, 0x00, 0x00, 0x02 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 3);

            Assert.AreEqual(testResult[0], 0x06);
            Assert.AreEqual(testResult[1], 0x0D);
            Assert.AreEqual(testResult[2], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_GetDeviceVersion_GPSUnit()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x50, 0x01, 0xB0, 0xFE, 0x00, 0x00, 0x00, 0x02 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 4);

            Assert.AreEqual(testResult[0], 0x00);
            Assert.AreEqual(testResult[1], 0x00);
            Assert.AreEqual(testResult[2], 0x00);
            Assert.AreEqual(testResult[3], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_GetDeviceVersion_RTC()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x50, 0x01, 0xB2, 0xFE, 0x00, 0x00, 0x00, 0x02 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 4);

            Assert.AreEqual(testResult[0], 0x00);
            Assert.AreEqual(testResult[1], 0x00);
            Assert.AreEqual(testResult[2], 0x00);
            Assert.AreEqual(testResult[3], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_GetTime()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x68 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 9);

            Assert.AreEqual(testResult[0], 0x14);
            Assert.AreEqual(testResult[1], 0x17);
            Assert.AreEqual(testResult[2], 0x06);
            Assert.AreEqual(testResult[3], 0x07);
            Assert.AreEqual(testResult[4], 0x1F);
            Assert.AreEqual(testResult[5], 0x0F);
            Assert.AreEqual(testResult[6], 0x3F);
            Assert.AreEqual(testResult[7], 0x01);
            Assert.AreEqual(testResult[8], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetTime()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x68 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 9);

            Assert.AreEqual(testResult[0], 0x14);
            Assert.AreEqual(testResult[1], 0x17);
            Assert.AreEqual(testResult[2], 0x06);
            Assert.AreEqual(testResult[3], 0x07);
            Assert.AreEqual(testResult[4], 0x1F);
            Assert.AreEqual(testResult[5], 0x0F);
            Assert.AreEqual(testResult[6], 0x3F);
            Assert.AreEqual(testResult[7], 0x01);
            Assert.AreEqual(testResult[8], 0x23);

            testResult[0] = 0x13;
            testResult[1] = 0x16;
            testResult[2] = 0x05;
            testResult[3] = 0x06;
            testResult[4] = 0x1D;
            testResult[5] = 0x0E;
            testResult[6] = 0x3E;
            testResult[7] = 0x00;

            byte[] setBytes = new byte[9];
            System.Buffer.BlockCopy(testResult, 0, setBytes, 1, 8);
            setBytes[0] = 0x48;

            byte[] termTest = testProcessor.ProcessRequestBytes(setBytes);
            Assert.AreEqual(0x23, termTest[0]);

            testRequestBytes = new byte[] { 0x68 };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 9);

            Assert.AreEqual(testResult[0], 0x13);
            Assert.AreEqual(testResult[1], 0x16);
            Assert.AreEqual(testResult[2], 0x05);
            Assert.AreEqual(testResult[3], 0x06);
            Assert.AreEqual(testResult[4], 0x1D);
            Assert.AreEqual(testResult[5], 0x0E);
            Assert.AreEqual(testResult[6], 0x3E);
            Assert.AreEqual(testResult[7], 0x00);
            Assert.AreEqual(testResult[8], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_GetLocation()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x77 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 9);

            Assert.AreEqual(testResult[0], 0x27);
            Assert.AreEqual(testResult[1], 0x0F);
            Assert.AreEqual(testResult[2], 0x1F);
            Assert.AreEqual(testResult[3], 0x00);
            Assert.AreEqual(testResult[4], 0x54);
            Assert.AreEqual(testResult[5], 0x2B);
            Assert.AreEqual(testResult[6], 0x1F);
            Assert.AreEqual(testResult[7], 0x01);
            Assert.AreEqual(testResult[8], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetLocation()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x77 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 9);

            Assert.AreEqual(testResult[0], 0x27);
            Assert.AreEqual(testResult[1], 0x0F);
            Assert.AreEqual(testResult[2], 0x1F);
            Assert.AreEqual(testResult[3], 0x00);
            Assert.AreEqual(testResult[4], 0x54);
            Assert.AreEqual(testResult[5], 0x2B);
            Assert.AreEqual(testResult[6], 0x1F);
            Assert.AreEqual(testResult[7], 0x01);
            Assert.AreEqual(testResult[8], 0x23);

            testResult[0] = 0x26;
            testResult[1] = 0x0E;
            testResult[2] = 0x1E;
            testResult[3] = 0x01;
            testResult[4] = 0x53;
            testResult[5] = 0x2A;
            testResult[6] = 0x1E;
            testResult[7] = 0x00;

            byte[] setBytes = new byte[9];
            System.Buffer.BlockCopy(testResult, 0, setBytes, 1, 8);
            setBytes[0] = 0x57;

            byte[] termTest = testProcessor.ProcessRequestBytes(setBytes);
            Assert.AreEqual(0x23, termTest[0]);

            testRequestBytes = new byte[] { 0x77 };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 9);

            Assert.AreEqual(testResult[0], 0x26);
            Assert.AreEqual(testResult[1], 0x0E);
            Assert.AreEqual(testResult[2], 0x1E);
            Assert.AreEqual(testResult[3], 0x01);
            Assert.AreEqual(testResult[4], 0x53);
            Assert.AreEqual(testResult[5], 0x2A);
            Assert.AreEqual(testResult[6], 0x1E);
            Assert.AreEqual(testResult[7], 0x00);
            Assert.AreEqual(testResult[8], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_GetTrackingMode()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x74 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(testResult[0], 0x00);
            Assert.AreEqual(testResult[1], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetTrackingModeAltAz()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] setTrackingBytes = new byte[] { 0x54, 0x01 };
            byte[] termTest = testProcessor.ProcessRequestBytes(setTrackingBytes);

            Assert.AreEqual(0x23, termTest[0]);

            byte[] testRequestBytes = new byte[] { 0x74 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(testResult[0], 0x00);
            Assert.AreEqual(testResult[1], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetTrackingModeEQNorth()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] setTrackingBytes = new byte[] { 0x54, 0x02 };
            byte[] termTest = testProcessor.ProcessRequestBytes(setTrackingBytes);

            Assert.AreEqual(0x23, termTest[0]);

            byte[] testRequestBytes = new byte[] { 0x74 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(testResult[0], 0x02);
            Assert.AreEqual(testResult[1], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetTrackingModeEQSouth()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] setTrackingBytes = new byte[] { 0x54, 0x03 };
            byte[] termTest = testProcessor.ProcessRequestBytes(setTrackingBytes);

            Assert.AreEqual(0x23, termTest[0]);

            byte[] testRequestBytes = new byte[] { 0x74 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(testResult[0], 0x03);
            Assert.AreEqual(testResult[1], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetTrackingModeOff()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] setTrackingBytes = new byte[] { 0x54, 0x00 };
            byte[] termTest = testProcessor.ProcessRequestBytes(setTrackingBytes);

            Assert.AreEqual(0x23, termTest[0]);

            byte[] testRequestBytes = new byte[] { 0x74 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 2);

            Assert.AreEqual(testResult[0], 0x00);
            Assert.AreEqual(testResult[1], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetVariableAZMSlew()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x50, 0x03, 0x10, 0x06, 0x14, 0x00, 0x00, 0x00 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 1);
            
            Assert.AreEqual(testResult[0], 0x23);

            testRequestBytes = new byte[] { 0x50, 0x03, 0x10, 0x07, 0x00, 0x00, 0x00, 0x00 };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 1);

            Assert.AreEqual(testResult[0], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetVariableDECSlew()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x50, 0x03, 0x11, 0x06, 0x14, 0x00, 0x00, 0x00 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 1);

            Assert.AreEqual(testResult[0], 0x23);

            testRequestBytes = new byte[] { 0x50, 0x03, 0x11, 0x07, 0x00, 0x00, 0x00, 0x00 };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 1);

            Assert.AreEqual(testResult[0], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetFixedAZMSlew()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x50, 0x02, 0x10, 0x24, 0x07, 0x00, 0x00, 0x00 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 1);

            Assert.AreEqual(testResult[0], 0x23);

            testRequestBytes = new byte[] { 0x50, 0x02, 0x10, 0x25, 0x00, 0x00, 0x00, 0x00 };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 1);

            Assert.AreEqual(testResult[0], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_SetFixedDECSlew()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x50, 0x02, 0x11, 0x24, 0x07, 0x00, 0x00, 0x00 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 1);

            Assert.AreEqual(testResult[0], 0x23);

            testRequestBytes = new byte[] { 0x50, 0x02, 0x11, 0x25, 0x00, 0x00, 0x00, 0x00 };
            testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(testResult.Length, 1);

            Assert.AreEqual(testResult[0], 0x23);
        }

        [TestMethod]
        public void Processor114LCM_JunkCommand()
        {
            RequestProcessor_114LCM testProcessor = new RequestProcessor_114LCM();

            byte[] testRequestBytes = new byte[] { 0x19 };
            byte[] testResult = testProcessor.ProcessRequestBytes(testRequestBytes);

            Assert.AreEqual(null, testResult);
            Assert.AreEqual(true, testProcessor.IsPaused());
        }
    }
}
