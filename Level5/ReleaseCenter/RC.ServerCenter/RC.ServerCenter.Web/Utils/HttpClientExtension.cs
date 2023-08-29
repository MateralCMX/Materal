using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.HttpClient;

namespace RC.ServerCenter.Web
{
    public static class HttpClientExtension
    {
        public static async Task<List<SelectDataModel<Guid>>> GetSelectDataModelAsync(this ProjectHttpClient projectHttpClient)
        {
            List<SelectDataModel<Guid>> result = new();
            (List<ProjectListDTO>? datas, _) = await projectHttpClient.GetListAsync(new()
            {
                PageIndex = 1,
                PageSize = int.MaxValue
            });
            if (datas == null) return result;
            datas = datas.OrderBy(m => m.Name).ToList();
            foreach (ProjectListDTO item in datas)
            {
                result.Add(new() { Value = item.ID, Name = $"{item.Name}[{item.Description}]" });
            }
            return result;
        }
        public static async Task<List<SelectDataModel<Guid>>> GetSelectDataModelAsync(this NamespaceHttpClient namespaceHttpClient, Guid? projectID = null)
        {
            List<SelectDataModel<Guid>> result = new();
            (List<NamespaceListDTO>? datas, _) = await namespaceHttpClient.GetListAsync(new()
            {
                ProjectID = projectID,
                PageIndex = 1,
                PageSize = int.MaxValue
            });
            if (datas == null) return result;
            datas = datas.OrderBy(m => m.Name).ToList();
            foreach (NamespaceListDTO item in datas)
            {
                result.Add(new() { Value = item.ID, Name = $"{item.Name}[{item.Description}]" });
            }
            return result;
        }
    }
}
