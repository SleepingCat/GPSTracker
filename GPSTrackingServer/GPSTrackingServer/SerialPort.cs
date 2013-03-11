using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace GPSTrackingServer
{
    class SP
    {

        private SerialPort _port;

        public SP()
        {
            _port = new SerialPort();
            _port.BaudRate = 115200; // 9600
            _port.DataBits = 8;
            _port.Handshake = Handshake.None;
            _port.Parity = Parity.None;
            _port.PortName = "COM44";
            _port.ReadBufferSize = _port.WriteBufferSize = 1024;
            _port.ReadTimeout = _port.WriteTimeout = 100;
            _port.DataReceived += new SerialDataReceivedEventHandler(_port_DataReceived);
            _port.Open();
        }

        void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
        }
    }
}
