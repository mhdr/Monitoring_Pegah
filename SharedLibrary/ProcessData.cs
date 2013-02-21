using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SharedLibraryMonitoringDevice;

namespace SharedLibrary
{
    public class ProcessData
    {
        public static ProcessDataResult GetData(string IPAddress, int IL_MODULE_number, int PD_IN_word)
        {
            try
            {
                //string url = string.Format("http://{0}/monitoring/17.18/processdata.xml", ip);
                Double ScaleCoefficient = Properties.Settings.Default.ScaleCoefficient;
                string url = string.Format("http://{0}/processdata.xml", IPAddress);
                XDocument xmlDoc = XDocument.Load(url);
                var q = from c in xmlDoc.Descendants("IL_MODULE")
                        where c.Attribute("number").Value.Equals(IL_MODULE_number.ToString())
                        select c;
                var w = from x in q.Elements("PD_IN")
                        where x.Attribute("word").Value.Equals(PD_IN_word.ToString())
                        select x.Value;

                int readData = Convert.ToInt32(w.FirstOrDefault());
                Double result = readData / ScaleCoefficient;

                return new ProcessDataResult(Math.Round(result, 2), false);

            }
            catch (Exception ex)
            {
                return new ProcessDataResult(0, true);
            }

            return new ProcessDataResult(0, true);
        }

    }
}
