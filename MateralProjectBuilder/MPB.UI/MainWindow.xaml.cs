using MPB.Service;
using MPB.ServiceImpl;
using System;
using System.IO;
using System.Windows;

namespace MPB.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models");
            var config = new ProjectConfigModel
            {
                IsAddStatus = true,
                EnableWebAPI = true
            };
            var outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OutPut");
            IProjectManage projectManage = new DotNet5ProjectManage();
            await projectManage.CreateProjectAsync(modelPath, config, outputPath);
            MessageBox.Show("生成完毕");
        }
    }
}
