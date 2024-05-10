using System.ComponentModel;

namespace Maui_ImageShow_FlickeringIssue {
    public partial class MainPage : ContentPage {
        string ImageSource1 = "https://gongu.copyright.or.kr/gongu/wrt/cmmn/wrtFileImageView.do?wrtSn=11288734&filePath=L2Rpc2sxL25ld2RhdGEvMjAxNS8wMi9DTFM2OS9OVVJJXzAwMV8wMjIwX251cmltZWRpYV8yMDE1MTIwMw==&thumbAt=Y&thumbSe=b_tbumb&wrtTy=10006";
        string ImageSource2 = "https://gongu.copyright.or.kr/gongu/wrt/cmmn/wrtFileImageView.do?wrtSn=9046601&filePath=L2Rpc2sxL25ld2RhdGEvMjAxNC8yMS9DTFM2L2FzYWRhbFBob3RvXzI0MTRfMjAxNDA0MTY=&thumbAt=Y&thumbSe=b_tbumb&wrtTy=10004";

        public MainPage() {
            InitializeComponent();
            Image1.PropertyChanged += Image1PropertyChangeEvent;
            Image2.PropertyChanged += Image2PropertyChangeEvent;
        }

        bool toggleFlag = false;
        private void ShowToggle() {
            if (toggleFlag == true) {
                toggleFlag = false;
                MainThread.BeginInvokeOnMainThread(() => {
                    if (Image1.IsLoading == false) {
                        Image1.Source = ImageSource1;
                    }
                });
            } else {
                toggleFlag = true;
                MainThread.BeginInvokeOnMainThread(() => {
                    if (Image2.IsLoading == false) {
                        Image2.Source = ImageSource2;
                    }
                });
            }
        }

        private void Bnt_ChangeOnce(object sender, EventArgs e) {
            ShowToggle();
        }

        private void Bnt_ChangeStart(object sender, EventArgs e) {
            Thread thread = new Thread(() => {
                while (true) {
                    Thread.Sleep(50);
                    ShowToggle();
                }
            });
            thread.Start();
        }

        private void Image1PropertyChangeEvent(object? sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "IsLoading") {
                var image = sender as Image;
                if (image != null) {
                    if (image.IsLoading == false) {
                        Canvas1.IsVisible = true;
                        Canvas2.IsVisible = false;
                    }
                }
            }
        }
        private void Image2PropertyChangeEvent(object? sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "IsLoading") {
                var image = sender as Image;
                if (image != null) {
                    if (image.IsLoading == false) {
                        Canvas1.IsVisible = false;
                        Canvas2.IsVisible = true;
                    }
                }
            }
        }
    }
}
