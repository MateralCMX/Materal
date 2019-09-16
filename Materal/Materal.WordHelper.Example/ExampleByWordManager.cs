using Materal.WordHelper.Model;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Materal.WordHelper.Example
{
    public class ExampleByWordManager
    {
        public void CreateTemplate()
        {
            var wordManager = new WordManager();
            string documentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/本年度支部工作计划-模板.docx");
            XWPFDocument document = wordManager.ReadWord(documentPath);
            var template = new List<TemplateModel>
            {
                new StringTemplateModel
                {
                    Key = "Content",
                    Value = "这是年度工作计划的内容"
                },
                new StringTemplateModel
                {
                    Key = "Content2",
                    Value = "~！@#￥%……&*（）——+内嵌模板!@#$%^&*()_+"
                },
                new StringTemplateModel
                {
                    Key = "Text1",
                    Value = "表格内文本1"
                },
                new StringTemplateModel
                {
                    Key = "Text2",
                    Value = "表格内文本2"
                },
                new StringTemplateModel
                {
                    Key = "Text3",
                    Value = "表格内文本3"
                },
                new StringTemplateModel
                {
                    Key = "Text4",
                    Value = "表格内文本4"
                },
                new TableTemplateModel
                {
                    Key = "Table",
                    Value = GetData(),
                    StartRowNumber = 2,
                    OnSetCellText = null
                }
            };
            wordManager.ApplyToTemplate(document, template.ToArray());
            string documentSavePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/本年度支部工作计划.docx");
            if (File.Exists(documentSavePath)) File.Delete(documentSavePath);
            using (var fileStream = new FileStream(documentSavePath, FileMode.Create))
            {
                document.Write(fileStream);
            }
        }

        private DataTable GetData()
        {
            var dataTable = new DataTable
            {
                TableName = "Table1"
            };
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Age");
            dataTable.Columns.Add("Remark");
            for (var i = 0; i < 10; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = $"Materal{i}";
                dataRow[1] = $"{i + 20}岁";
                dataRow[2] = "这是备注";
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}