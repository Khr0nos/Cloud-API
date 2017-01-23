using System;
using System.Diagnostics;
using System.Linq;
using Cloud_Background.Models;

namespace Cloud_Background {
    public class Program {
        private static DatabaseContext db;

        public static void Main(string[] args) {
            db = new DatabaseContext();
            ProcessDevices();
        }

        private static void ProcessDevices() {
            var devs = from d in db.Devices
                where d.DeviceEnabled && d.DeviceConnected
                select d;

            var devices = devs.ToList();
            foreach (var dev in devices) {
                var lastdata =
                    db.HistoricData.OrderByDescending(d => d.IDHistoricData)
                        .FirstOrDefault(d => d.IDDevice == dev.IDDevice);
                if (DateTime.Now - lastdata?.HistDataDate >= TimeSpan.FromMilliseconds(dev.DeviceInterval * 2 + 1)) {
                    DeviceCommError(dev);
                }
            }
        }

        private static void DeviceCommError(Devices dev) {
            //registrar error per timeout d'interval de dispositiu
            var histdev = new HistoricDevices {
                IDDevice = dev.IDDevice,
                HistDeviceDate = DateTime.Now,
                IDDeviceAction = 8,
                HistDeviceIPaddress = null,
                HistDeviceAux = "Device interval timeout"
            };

            //Primer per ordre descendent == ultim per ordre normal
            var lastHistoricDevice = db.HistoricDevices.OrderByDescending(d => d.IDHistoricDevices).FirstOrDefault();
            if (lastHistoricDevice == default(HistoricDevices)) histdev.IDHistoricDevices = 1;
            else histdev.IDHistoricDevices = lastHistoricDevice.IDHistoricDevices + 1;
            db.HistoricDevices.Add(histdev);

            try {
                db.SaveChanges();
            } catch (Exception ex) {
                Debug.WriteLine(ex);
                return;
            }

            //registrar desconnexió de dispositiu si s'han registrat >= 3 timeouts
            var numtimeout = db.HistoricDevices.Count(
                d => d.IDDevice == dev.IDDevice && d.IDDeviceAction == 8 &&
                     d.HistDeviceAux == "Device interval timeout");
            if (numtimeout >= 3) {
                dev.DeviceConnected = false;
                db.Devices.Attach(dev);
                db.Entry(dev).Property(c => c.DeviceConnected).IsModified = true;

                histdev = new HistoricDevices {
                    IDDevice = dev.IDDevice,
                    HistDeviceDate = DateTime.Now,
                    IDDeviceAction = 2,
                    HistDeviceIPaddress = null,
                    HistDeviceAux = "Device disconnection due to timeout error"
                };

                lastHistoricDevice = db.HistoricDevices.OrderByDescending(d => d.IDHistoricDevices).FirstOrDefault();
                if (lastHistoricDevice == default(HistoricDevices)) histdev.IDHistoricDevices = 1;
                else histdev.IDHistoricDevices = lastHistoricDevice.IDHistoricDevices + 1;
                db.HistoricDevices.Add(histdev);

                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}
