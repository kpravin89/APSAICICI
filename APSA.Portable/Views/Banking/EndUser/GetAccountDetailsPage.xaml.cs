using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using APSA.Portable.Data.Services.Banking.EndUser;
using APSA.Portable.Models.APIModels.CoreBanking;

namespace APSA.Portable.Views.Banking.EndUser
{
    public partial class GetAccountDetailsPage : ContentPage
    {

        #region Fields

        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;

        AccountServices oAccountServices;

        #endregion

        #region Constructors

        public GetAccountDetailsPage()
        {
            InitializeComponent();

            oAccountServices = new AccountServices();

            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
            };

            zxing.OnScanResult += OnScanResult;

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = zxing.HasTorch,
                AutomationId = "zxingDefaultOverlay",
            };
            overlay.FlashButtonClicked += (sender, e) =>
            {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };

            ScanGrid.Children.Add(zxing);
            ScanGrid.Children.Add(overlay);

        }

        #endregion

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }

        #endregion

        #region Event Handlers

        public void OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

                // Stop analysis until we navigate away so we don't keep reading barcodes
                zxing.IsAnalyzing = false;

                var AccountBalance = await oAccountServices.GetAccountBalance(new BalanceEnquiryInput() { Client_id = AppStart.App.ParticipantID, Token = AppStart.App.AccessToken, AccountNo = result.Text });
                ScanGrid.IsVisible = false;
                BalanceLayout.IsVisible = true;


                ListView oListView = new ListView()
                {
                    ItemsSource = AccountBalance.Select(a => a.oBalanceEnquiryAccount),
                    ItemTemplate = new DataTemplate(() =>
                    {
                        Label accountnoLabel = new Label();
                        accountnoLabel.SetBinding(Label.TextProperty, "accountno");
                        Label balancetimeLabel = new Label();
                        balancetimeLabel.SetBinding(Label.TextProperty, "balancetimeLabel");
                        Label accounttypeLabel = new Label();
                        accounttypeLabel.SetBinding(Label.TextProperty, "accounttype");
                        Label balanceLabel = new Label();
                        balanceLabel.SetBinding(Label.TextProperty, "balance");

                        return new ViewCell()
                        {
                            View = new StackLayout()
                            {
                                Padding = new Thickness(0, 5),
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            accountnoLabel,
                                            balancetimeLabel,
                                            accounttypeLabel,
                                            balanceLabel
                                        }
                                        }
                                }
                            }
                        };
                    })
                };

                BalanceLayout.Children.Add(oListView);


            });
        }


        #endregion

    }
}
