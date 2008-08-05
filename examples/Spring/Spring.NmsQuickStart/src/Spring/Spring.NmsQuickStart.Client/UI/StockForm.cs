using System;
using System.Collections;
using System.Windows.Forms;
using Common.Logging;
using Spring.Context.Support;
using Spring.NmsQuickStart.Common.Bo;

namespace Spring.NmsQuickStart.Client.UI
{
    public partial class StockForm : Form
    {
        #region Logging Definition

        private static readonly ILog log = LogManager.GetLogger(typeof (StockForm));

        #endregion

        private StockController stockController;

        public StockForm()
        {
            InitializeComponent();
            stockController = ContextRegistry.GetContext()["StockController"] as StockController;
            stockController.StockForm = this;
        }

        public StockController Controller
        {
            set { stockController = value; }
        }

        private void OnSendTradeRequest(object sender, EventArgs e)
        {
            //In this simple example no data is collected from the view.
            //Instead a hardcoded trade request is created in the controller.
            tradeRequestStatusTextBox.Text = "Request Pending...";
            stockController.SendTradeRequest();
            log.Info("Sent trade request.");
        }

        public void UpdateTrade(Trade trade)
        {
            Invoke(new MethodInvoker(
                       delegate
                           {
                               tradeRequestStatusTextBox.Text = "Confirmed. " + trade.Ticker + " " + trade.Price;
                           }));
        }

        public void UpdateMarketData(IDictionary marketDataDict)
        {
            Invoke(new MethodInvoker(
                       delegate
                           {                               
                               marketDataListBox.Items.Add(marketDataDict["TICKER"] + " " + marketDataDict["PRICE"]);
                           }));
        }


    }
}