using Materal.ExcelHelper;
using MPB.Common;
using MPB.Domain;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MBP.ConsoleUI
{
    internal class Program
    {
        public static async Task Main()
        {
            var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models");
            Console.WriteLine($"modelPath:{modelPath}");
            DirectoryInfo modelDirectoryInfo = InitModelDirectory(modelPath);
            List<DomainModel> domainModels = await GetDomainModelsAsync(modelDirectoryInfo);
            domainModels = domainModels.OrderBy(m => m.Namespace).ToList();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Key");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("TableDescription");
            foreach (DomainModel domainModel in domainModels)
            {
                domainModel.Properties.Add(new PropertyModel
                {
                    Annotation = "唯一标识",
                    Name = "ID",
                    Type = "Guid"
                });
                domainModel.Properties.Add(new PropertyModel
                {
                    Annotation = "创建时间",
                    Name = "CreateTime",
                    Type = "Guid"
                });
                domainModel.Properties.Add(new PropertyModel
                {
                    Annotation = "修改时间",
                    Name = "UpdateTime",
                    Type = "Guid"
                });
                foreach (PropertyModel property in domainModel.Properties)
                {
                    dataTable.Rows.Add($"{domainModel.Name}_{property.Name}", property.Annotation, domainModel.Annotation);
                }
            }

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            ExcelManager excelManager = new ExcelManager();
            IWorkbook excel = excelManager.DataSetToWorkbook<XSSFWorkbook>(dataSet, null);
            var outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OutPut");
            DirectoryInfo directoryInfo = new DirectoryInfo(outputPath);
            if (!directoryInfo.Exists) directoryInfo.Create();
            var fileInfo = new FileInfo(Path.Combine(outputPath, "Data.xlsx"));
            if (fileInfo.Exists) fileInfo.Delete();
            using FileStream fileStream = fileInfo.Create();
            excel.Write(fileStream);
        }
        public static void WorkbookHandler(IWorkbook wb, ISheet sheet, int x, int y, ICell cell)
        {

        }
        /// <summary>
        /// 初始化模型文件夹
        /// </summary>
        /// <param name="modelPath"></param>
        /// <returns></returns>
        /// <exception cref="MPBException"></exception>
        private static DirectoryInfo InitModelDirectory(string modelPath)
        {
            DirectoryInfo modelDirectoryInfo = new(modelPath);
            if (!modelDirectoryInfo.Exists) throw new MPBException("模型文件夹不存在");
            return modelDirectoryInfo;
        }
        /// <summary>
        /// 获得领域模型
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private static async Task<List<DomainModel>> GetDomainModelsAsync(DirectoryInfo directoryInfo)
        {
            List<DomainModel> domainModels = new();
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (!fileInfo.Extension.Equals(".cs", StringComparison.OrdinalIgnoreCase)) continue;
                var domainModel = new DomainModel(fileInfo);
                if (!await domainModel.InitAsync()) continue;
                domainModels.Add(domainModel);
            }
            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                domainModels.AddRange(await GetDomainModelsAsync(subDirectoryInfo));
            }
            return domainModels;
        }
    }
}
