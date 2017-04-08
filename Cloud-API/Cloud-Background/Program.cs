using System;
using System.Diagnostics;
using System.Linq;
using Cloud_Background.Models;

namespace Cloud_Background {
    public class Program {
        // Model de la base de dades per gestionar-la
        private static DatabaseContext db;

        public static void Main(string[] args) {
            db = new DatabaseContext();
            ProcessDevices();
        }
        //Registra error en la base de dades per a dispositius actius i connectats que no estiguin enviant dades
        private static void ProcessDevices() {
            //Selecció de dispositius actius i connectats
            var devs = (from d in db.Devices
                       where d.DeviceEnabled && d.DeviceConnected
                       select d).ToList();

            foreach (var dev in devs) {
                //Obtenció d'ultima data d'enviament de dades d'un dispositiu
                var lastdata = db.HistoricData.OrderByDescending(d => d.IDHistoricData)
                                              .FirstOrDefault(d => d.IDDevice == dev.IDDevice);
                //Si l'ultim enviament de dades fa més del doble de l'interval del dispositiu mes 1 registra error de dispositiu
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

            //Càlcul de nou id per a insercció en taula HistoricDevices en funció de l'ultim valor de clau primaria
            var lastHistoricDevice = db.HistoricDevices.OrderByDescending(d => d.IDHistoricDevices).FirstOrDefault();
            if (lastHistoricDevice == default(HistoricDevices)) histdev.IDHistoricDevices = 1;
            else histdev.IDHistoricDevices = lastHistoricDevice.IDHistoricDevices + 1;
            db.HistoricDevices.Add(histdev);
            
            //registrar error de dispositiu a la base de dades
            try {
                db.SaveChanges();
            } catch (Exception ex) {
                Debug.WriteLine(ex);
                return;
            }

            //consulta de nombre d'errors registrats per al dispositiu
            var numtimeout = db.HistoricDevices.Count(
                d => d.IDDevice == dev.IDDevice && d.IDDeviceAction == 8 &&
                     d.HistDeviceAux == "Device interval timeout");
            //registrar desconnexió de dispositiu si s'han registrat minim 3 timeouts
            if (numtimeout >= 3) {
                dev.DeviceConnected = false;
                db.Devices.Attach(dev);
                db.Entry(dev).Property(c => c.DeviceConnected).IsModified = true;

                //registrar desconnexió per acumulació de minim 3 timeouts d'interval de dispositiu
                histdev = new HistoricDevices {
                    IDDevice = dev.IDDevice,
                    HistDeviceDate = DateTime.Now,
                    IDDeviceAction = 2,
                    HistDeviceIPaddress = null,
                    HistDeviceAux = "Device disconnection due to timeout error"
                };

                //Càlcul de nou id per a insercció en taula HistoricDevices en funció de l'ultim valor de clau primaria
                lastHistoricDevice = db.HistoricDevices.OrderByDescending(d => d.IDHistoricDevices).FirstOrDefault();
                if (lastHistoricDevice == default(HistoricDevices)) histdev.IDHistoricDevices = 1;
                else histdev.IDHistoricDevices = lastHistoricDevice.IDHistoricDevices + 1;
                db.HistoricDevices.Add(histdev);

                //registrar desconnexió de dispositiu a la base de dades
                try {
                    db.SaveChanges();
                } catch (Exception ex) {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}
