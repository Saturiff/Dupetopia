using Dupetopia.Properties;
using System;
using System.Windows;
using System.Windows.Input;

namespace Dupetopia
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadLastInput();
        }

        private void DoDupe()
        {
            if (string.IsNullOrEmpty(TB_WorldName.Text) 
                || string.IsNullOrEmpty(TB_MasterIslandName.Text) 
                || string.IsNullOrEmpty(TB_SubIslandName.Text))
            {
                TB_Info.Content = "所有欄位不可為空";

                return;
            }

            TB_Info.Content = "運作正常";

            string[] subIslands = TB_SubIslandName.Text.Split('|');
            
            new Duper(TB_WorldName.Text, TB_MasterIslandName.Text, subIslands);
        }

        private void ClickDrag(object sender, MouseButtonEventArgs e) => DragMove();

        private void LoadLastInput()
        {
            TB_WorldName.Text        = Settings.Default.worldFolderName;
            TB_MasterIslandName.Text = Settings.Default.masterIslandNum;
            TB_SubIslandName.Text    = Settings.Default.subIslandNum;
            CB_Topmost.IsChecked     = Settings.Default.topmost;
        }

        private void SaveLastInput()
        {
            Settings.Default.worldFolderName = TB_WorldName.Text;
            Settings.Default.masterIslandNum = TB_MasterIslandName.Text;
            Settings.Default.subIslandNum    = TB_SubIslandName.Text;
            Settings.Default.topmost         = CB_Topmost.IsChecked ?? false;
            Settings.Default.Save();
        }

        private void ClickDupe(object sender, RoutedEventArgs e) => DoDupe();
        private void ClickMinimize(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void ClickClose(object sender, RoutedEventArgs e)
        {
            SaveLastInput();

            Close();

            Environment.Exit(Environment.ExitCode);
        }

        private void Topmost_Checked(object sender, RoutedEventArgs e) => Topmost = true;

        private void Topmost_Unchecked(object sender, RoutedEventArgs e) => Topmost = false;
    }
}
