using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fougerite;
using System.ComponentModel;
using System.Net;
using System.IO;

namespace IpTools
{
    public class IpToolsClass : Fougerite.Module
    {
        public override string Name { get { return "IpTools"; } }
        public override string Author { get { return "Salva/juli"; } }
        public override string Description { get { return "IpTools"; } }
        public override Version Version { get { return new Version("1.0"); } }

        private string red = "[color #FF0000]";
        private string blue = "[color #81F7F3]";
        private string green = "[color #82FA58]";
        private string yellow = "[color #F4FA58]";
        private string orange = "[color #FF8000]";

        public string dataweb = "http://ip-api.com/csv/";
        public string iptemp = "";

        public override void Initialize()
        {
            Hooks.OnPlayerConnected += OnPlayerConnected;
        }
        public override void DeInitialize()
        {
            Fougerite.Hooks.OnPlayerConnected -= OnPlayerConnected;
        }
        public void OnPlayerConnected(Fougerite.Player player)
        {
            BackgroundWorker BGW = new BackgroundWorker();
            BGW.DoWork += new DoWorkEventHandler(leerdatos1);
            BGW.RunWorkerAsync();
            iptemp = player.IP;
        }
        public void leerdatos1(object sender, DoWorkEventArgs e)
        {
            HttpWebRequest hRequest = ((HttpWebRequest)WebRequest.Create(dataweb + iptemp));
            hRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 4.01; Windows CE; Smartphone; 176x220)";
            StreamReader reader = new StreamReader(hRequest.GetResponse().GetResponseStream());
            string text = reader.ReadToEnd().Replace('"', ' ');
            string[] split = text.Split(new char[] { ',' });
            string status = split[0];
            string countryName = split[1];
            string countrycode = split[2];
            string region = split[4];
            string city = split[5];
            Server.GetServer().BroadcastFrom(Name, "It has connected from: [" + countrycode + "]" + countryName + " (" + region + " )");
            reader.Close();
        } 
    }
}
