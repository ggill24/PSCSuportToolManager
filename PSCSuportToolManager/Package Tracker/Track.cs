using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PSCSuportToolManager.Package_Tracker
{
    public class Track
    {
        public static Carrier carrier { get; private set; }
        private static readonly string[] urls = { "https://www.fedex.com/apps/fedextrack/?action=track&trackingnumber=" };

        public static void package(long trackingNumber)
        {
            string url = urls[0];

            try
            {
                Process.Start(urls[0] + trackingNumber.ToString());
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }


        }

    }
}
