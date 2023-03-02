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
            (List<ProjectListDTO>? projects, _) = await projectHttpClient.GetListAsync(new()
            {
                PageIndex = 1,
                PageSize = int.MaxValue
            });
            if (projects == null) return result;
            foreach (ProjectListDTO project in projects)
            {
                result.Add(new() { Value = project.ID, Name = $"{project.Name}[{project.Description}]" });
            }
            return result;
        }
        public static async Task<List<SelectDataModel<Guid>>> GetSelectDataModelAsync(this NamespaceHttpClient namespaceHttpClient, Guid? projectID = null)
        {
            List<SelectDataModel<Guid>> result = new();
            (List<NamespaceListDTO>? namespaces, _) = await namespaceHttpClient.GetListAsync(new()
            {
                ProjectID = projectID,
                PageIndex = 1,
                PageSize = int.MaxValue
            });
            if (namespaces == null) return result;
            foreach (NamespaceListDTO @namespace in namespaces)
            {
                result.Add(new() { Value = @namespace.ID, Name = $"{@namespace.Name}[{@namespace.Description}]" });
            }
            return result;
        }
    }
}
