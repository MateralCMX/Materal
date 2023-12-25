using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WinFormsDemo
{
    public partial class MainForm : Form
    {
        private const string logMessage = "Hello World!";
        private readonly ILogger<MainForm> _logger;
        private readonly string _rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Log.log");
        private static int _index = 0;
        private static readonly object _lock = new();
        public MainForm()
        {
            _logger = Program.ServiceProvider.GetRequiredService<ILogger<MainForm>>();
            InitializeComponent();
        }
        private void BtnTrace_Click(object sender, EventArgs e) => _logger.LogTrace(logMessage);
        private void BtnDebug_Click(object sender, EventArgs e) => _logger.LogDebug(logMessage);
        private void BtnInformation_Click(object sender, EventArgs e) => _logger.LogInformation(logMessage);
        private void BtnWarning_Click(object sender, EventArgs e) => _logger.LogWarning(logMessage);
        private void BtnError_Click(object sender, EventArgs e) => _logger.LogError(logMessage);
        private void BtnCritical_Click(object sender, EventArgs e) => _logger.LogCritical(logMessage);
        private void MainForm_Load(object sender, EventArgs e) => loadLogTimer.Start();
        private void LoadLogTimer_Tick(object sender, EventArgs e)
        {
            lock (_lock)
            {
                loadLogTimer.Stop();
                if (File.Exists(_rootPath))
                {
                    string[] logs = File.ReadAllLines(_rootPath);
                    for (; _index < logs.Length; _index++)
                    {
                        richTextBoxLogInfo.AppendText($"{logs[_index]}\r\n");
                    }
                }
                loadLogTimer.Start();
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_rootPath)) return;
            lock (_lock)
            {
                loadLogTimer.Stop();
                richTextBoxLogInfo.Clear();
                _index = 0;
                File.Delete(_rootPath);
                loadLogTimer.Start();
            }
        }
    }
}