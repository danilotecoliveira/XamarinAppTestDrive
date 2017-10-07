using Android.OS;
using Android.App;
using Xamarin.Forms;
using TestDrive.Droid;
using Android.Content;
using TestDrive.Media;
using Android.Provider;
using Android.Content.PM;

[assembly: Dependency(typeof(MainActivity))]
namespace TestDrive.Droid
{
    [Activity(Label = "TestDrive", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ICamera
    {
        static Java.IO.File foto;

        public void TirarFoto()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            Java.IO.File diretorio = new Java.IO.File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "Imagens");

            if(!diretorio.Exists())
                diretorio.Mkdirs();

            foto = new Java.IO.File(diretorio, "MinhaFoto.jpg");
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(foto));
            var activity = Forms.Context as Activity;
            activity.StartActivityForResult(intent, 0);
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                byte[] bytes;
                using (var stream = new Java.IO.FileInputStream(foto))
                {
                    bytes = new byte[foto.Length()];
                    stream.Read(bytes);
                }
                MessagingCenter.Send(bytes, "FotoTirada");
            }
        }
    }
}

