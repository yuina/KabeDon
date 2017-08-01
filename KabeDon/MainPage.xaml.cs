using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

        private int i = 1;

        static readonly Area[] areas = new[]
        {
            new Area {X = 396, Y = 238, Width = 320, Height =  108, Image = "Claudia4", Sound = "ah"},
            new Area {X = 365, Y = 215, Width = 380, Height =  529, Image = "Claudia2", Sound = "ah"},
            new Area {X = 276, Y = 340, Width =  65, Height =  415, Image = "Claudia3", Sound = "chui"},
            new Area {X = 739, Y = 340, Width =  65, Height =  415, Image = "Claudia3", Sound = "chui"},
            new Area {X = 271, Y = 772, Width = 590, Height = 1148, Image = "Claudia4", Sound = "oujougiwa"},
        };

        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            image.IsTapEnabled = false;

            var pos = e.GetPosition(image);

            var x = pos.X / image.ActualWidth * 1080;
            var y = pos.Y / image.ActualHeight * 1920;

            foreach (var area in areas)
            {
                if (area.X < x && x < area.X + area.Width && area.Y < y && y < area.Y +area.Height)
                {
                    var mediaUri = new Uri($"ms-appx:///Assets/Sound/{area.Sound}.mp3");
                    PlaySound(mediaUri);

                    var imageUri = new Uri($"ms-appx:///Assets/Image/{area.Image}.png");
                    ShowImage(imageUri);

                    break;
                }
            }
            await Task.Delay(1000);
            image.IsTapEnabled = true;
            ShowImage(new Uri("ms-appx:///Assets/Image/Claudia1.png"));
        }

        private async void PlaySound(Uri mediaUri)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(mediaUri);
            var stream = await file.OpenReadAsync();
            mediaElement.SetSource(stream, file.ContentType);
            mediaElement.Play();
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
