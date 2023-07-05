using Inkore.Coreworks.Localization;
using MCSkinn.Scripts.Models;
using MCSkinn.Scripts.Swatches;
using MCSkinn.Scripts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MCSkinn.Scripts.Macros;
using System.IO;

namespace MCSkinn.Pages
{
    /// <summary>
    /// PageSplash.xaml 的交互逻辑
    /// </summary>
    public partial class PageSplash : Page
    {
        static Thread _loaderThread;
        public PageSplash()
        {
            InitializeComponent();
        }
        public static string LoadingValue = "(nothing?)";

        public void SetLoadingString(string s)
        {
            LoadingValue = s;

            this.Dispatcher.Invoke(new Action(() =>
            {
                TextBlock_Status.Text = s;
            }));


        }

        Language LoadLanguages()
        {
            LanguageLoader.LoadLanguages(GlobalSettings.GetDataURI("Languages"));

            Language useLanguage = null;
            try
            {
                // stage 1 (prelim): if no language, see if our languages contain it
                if (string.IsNullOrEmpty(GlobalSettings.LanguageFile))
                {
                    useLanguage =
                        LanguageLoader.FindLanguage((CultureInfo.CurrentUICulture.IsNeutralCulture == false)
                                                        ? CultureInfo.CurrentUICulture.Parent.Name
                                                        : CultureInfo.CurrentUICulture.Name);
                }

                // stage 2: load from last used language
                if (useLanguage == null)
                    useLanguage = LanguageLoader.FindLanguage(GlobalSettings.LanguageFile);

                // stage 3: use English file, if it exists
                if (useLanguage == null)
                    useLanguage = LanguageLoader.FindLanguage("en-us");
            }
            catch(Exception ex)
            {
                Program.Log(ex, true);
            }
            finally
            {
                //stage 4: fallback to built -in English file
                if (useLanguage == null)
                {
                    //Program.Context.SplashForm.Dispatcher.Invoke((Action)(() => MessageBox.Show(this, "For some reason, the default language files were missing or failed to load (did you extract?) - we'll supply you with a base language of English just so you know what you're doing!")));
                    useLanguage = LoadDefault();
                }
            }

            return useLanguage;
        }
        public static Language LoadDefault()
        {
            Directory.CreateDirectory(GlobalSettings.GetDataURI("Languages"));
            using (var writer = new FileStream(MacroHandler.ReplaceMacros(GlobalSettings.GetDataURI("Languages\\en-us.lang")), FileMode.Create))
                writer.Write(Properties.Resources.English, 0, Properties.Resources.English.Length);

            using (var reader = new StreamReader(new MemoryStream(Properties.Resources.English), Encoding.Unicode))
            {
                Language lang = Inkore.Coreworks.Localization.Language.Parse(reader, "en-US.lang");
                Inkore.Coreworks.Localization.LanguageLoader.Languages.Add(lang);
                return lang;
            }
        }
        object _lockObj = new object();

        Action ErrorHandlerWrap(Action a)
        {
            return () =>
            {
                try
                {
                    a();
                }
                catch (Exception ex)
                {
                    if (ex is TaskCanceledException)
                        return;

                    Program.RaiseException(ex);
                }
            };
        }

        void PerformLoading()
        {
            SetLoadingString("Loading Languages...");

            var language = LoadLanguages();

            SetLoadingString("Initializing base forms...");

            Program.Page_Splash.Dispatcher.Invoke(ErrorHandlerWrap(() =>
            {
                Program.CurrentLanguage = language;

                Editor.MainForm.FinishedLoadingLanguages();
                Editor.MainForm.Initialize(language);
                Program.Page_Editor.RegisterLanguage();
                //LanguageWpf.Refresh();

            }));


            SetLoadingString("Loading swatches...");

            SwatchLoader.LoadSwatches();
            Program.Page_Splash.Dispatcher.Invoke(ErrorHandlerWrap(SwatchLoader.FinishedLoadingSwatches));

            SetLoadingString("Loading models...");

            ModelLoader.LoadModels();
            Program.Page_Splash.Dispatcher.Invoke(ErrorHandlerWrap(Editor.MainForm.FinishedLoadingModels));

            SetLoadingString("Loading skins...");

            SkinLoader.LoadSkins();

            Program.Page_Splash.Dispatcher.Invoke(ErrorHandlerWrap(Program.Context.DoneLoadingSplash));

            Program.Context.Form.Invoke(ErrorHandlerWrap(() =>
            {
                //Program.Context.SplashForm.Close();
                GC.Collect();
            }));
        }

        public static void BeginLoaderThread()
        {
            _loaderThread = new Thread(Program.Page_Splash.PerformLoading);
            _loaderThread.Start();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock_Version.Text = Program.Name + " v" + Program.VersionFull.ToString();
            SetLoadingString("Doing nothing yet...");

            _loaderThread = new Thread(Program.Page_Splash.PerformLoading);
            _loaderThread.Start();

        }
    }
}
