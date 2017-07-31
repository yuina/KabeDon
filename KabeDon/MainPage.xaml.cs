using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace KabeDon
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            image.Tapped += Image_Tapped;
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var imageUri = new Uri("ms-appx:///Assets/Image/Claudia2.png");
            ShowImage(imageUri);
        }

        private async void ShowImage(Uri imageUri)
        {
            var bitmap = new BitmapImage();
            var file = await StorageFile.GetFileFromApplicationUriAsync(imageUri);
            var stream = await file.OpenReadAsync();
            await bitmap.SetSourceAsync(stream);
            image.Source = bitmap;
        }
    }
}
