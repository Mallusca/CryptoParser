using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CryptoParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1500;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            Form1_Shown(null, null);
        }
        private async void Form1_Shown(object sender, EventArgs e)
        {
            string eth = await Eth();
            string ltc = await Ltc();
            string bch = await BCH();
            string xrp = await XRP();
            textBox1.Text = Bitcoin() + eth + ltc+bch+xrp;

        }
        private string Bitcoin()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            string Response = wc.DownloadString("https://bitinfocharts.com/ru/bitcoin-birzhi-grafiki-obmena.html");
            string Rate = System.Text.RegularExpressions.Regex.Match(Response, @"<span itemprop=""price"">([0-9]{4}.[0-9]{2})</span>").Groups[1].Value;
            return "Биткоин: $" + Rate + "\r\n";
        }
        private async Task<string> Eth()
        {
            var config = new Configuration().WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync("https://bitinfocharts.com/ru/ethereum-birzhi-grafiki-obmena.html");
            var link = document.QuerySelector("span[itemprop='price']");
            return "Эфир: $" + link.TextContent+ "\r\n";
        }
        private async Task<string> Ltc()
        {
            var config = new Configuration().WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync("https://bitinfocharts.com/ru/litecoin-birzhi-grafiki-obmena.html");
            var link = document.QuerySelector("span[itemprop='price']");
            return "Лайт: $" + link.TextContent + "\r\n";
        }
        private async Task<string> BCH()
        {
            var config = new Configuration().WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync("https://coinmarketcap.com/currencies/bitcoin-cash/");
            var link = document.QuerySelector("span.cmc-details-panel-price__price");
            return "Биткойн Кэш: " + link.TextContent + "\r\n";
        }
        private async Task<string> XRP()
        {
            var config = new Configuration().WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync("https://coinmarketcap.com/currencies/xrp/");
            var link = document.QuerySelector("span.cmc-details-panel-price__price");
            return "Риппл: " + link.TextContent + "\r\n";
        }

    }
}




