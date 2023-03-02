using Materal.Abstractions;
using Materal.BaseCore.HttpClient;
using RC.ServerCenter.DataTransmitModel.Server;
using RC.ServerCenter.HttpClient;

namespace RC.ServerCenter.Web
{
    public static class RCData
    {
        private static bool showDeploys = false;
        private static bool showEnvironments = false;

        public static List<SelectDataModel> Deploys { get; } = new();
        public static List<SelectDataModel> Environments { get; } = new();
        public static event Action? OnChangeDeploy;
        public static event Action? OnChangeEnvironment;
        public static string SelectedDeploy { get; set; } = string.Empty;
        public static string SelectedEnvironment { get; set; } = string.Empty;
        public static bool IsLoaded { get; private set; } = false;
        public static bool ShowDeploys
        {
            get => showDeploys;
            set
            {
                showDeploys = value;
                OnChangeDeploy?.Invoke();
            }
        }
        public static bool ShowEnvironments
        {
            get => showEnvironments;
            set
            {
                showEnvironments = value;
                OnChangeEnvironment?.Invoke();
            }
        }
        public static string GetUploadUrl(Guid id) => $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}{SelectedDeploy}/api/ApplicationInfo/UploadNewFile?id={id}";

        public static async Task InitAsync()
        {
            IsLoaded = true;
            ServerHttpClient serverHttpClient = MateralServices.GetService<ServerHttpClient>();
            {
                Deploys.Clear();
                List<DeployListDTO>? data = await serverHttpClient.GetDeployListAsync();
                if (data != null)
                {
                    foreach (DeployListDTO item in data)
                    {
                        Deploys.Add(new()
                        {
                            Value = item.Service,
                            Name = item.Name,
                        });
                    }
                    SelectedDeploy = Deploys.First().Value ?? string.Empty;
                }
            }
            {
                Environments.Clear();
                List<EnvironmentServerListDTO>? data = await serverHttpClient.GetEnvironmentServerListAsync();
                if (data != null)
                {
                    foreach (EnvironmentServerListDTO item in data)
                    {
                        Environments.Add(new()
                        {
                            Value = item.Service,
                            Name = item.Name,
                        });
                        SelectedEnvironment = Environments.First().Value ?? string.Empty;
                    }
                }
            }
        }
        public static void ChangeDeploy()
        {
            OnChangeDeploy?.Invoke();
        }
        public static void ChangeEnvironment()
        {
            OnChangeEnvironment?.Invoke();
        }
    }
}
