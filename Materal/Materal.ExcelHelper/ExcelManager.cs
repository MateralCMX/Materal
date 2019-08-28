using Materal.Common;
using Materal.ConvertHelper;
using Materal.ExcelHelper.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Materal.ExcelHelper
{
    public class ExcelManager
    {
        #region 读取
        /// <summary>
        /// 读取Excel到数据集
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="startRowNumbers">开始行数组</param>
        /// <returns>数据集</returns>
        public DataSet ReadExcelToDataSet(string filePath, params int[] startRowNumbers)
        {
            IWorkbook workbook = ReadExcelToWorkbook(filePath);
            return WorkbookToDataSet(workbook, startRowNumbers);
        }
        /// <summary>
        /// 读取Excel到数据集
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="startRowNumbers">开始行数组</param>
        /// <returns>数据集</returns>
        public DataSet ReadExcelToDataSet(FileStream fileStream, params int[] startRowNumbers)
        {
            IWorkbook workbook = ReadExcelToWorkbook(fileStream);
            return WorkbookToDataSet(workbook, startRowNumbers);
        }
        /// <summary>
        /// 读取Excel到工作簿
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns>工作簿对象</returns>
        public IWorkbook ReadExcelToWorkbook(string filePath)
        {
            if (!File.Exists(filePath)) throw new MateralException("文件不存在");
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook result = ReadExcelToWorkbook(fs);
                return result;
            }
        }
        /// <summary>
        /// 读取Excel到工作簿
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <returns>工作簿对象</returns>
        public IWorkbook ReadExcelToWorkbook(FileStream fileStream)
        {
            IWorkbook workbook = null;
            try
            {
                if (fileStream.Name.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                else if (fileStream.Name.IndexOf(".xls", StringComparison.Ordinal) > 0)
                {
                    workbook = new HSSFWorkbook(fileStream);
                }
            }
            catch (Exception ex)
            {
                throw new MateralException("不识别的Excel文件", ex);
            }
            return workbook;
        }
        /// <summary>
        /// 读取Excel到工作表组
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns>工作表组</returns>
        public List<ISheet> ReadExcelToSheets(string filePath)
        {
            IWorkbook workbook = ReadExcelToWorkbook(filePath);
            return GetAllSheets(workbook);
        }
        /// <summary>
        /// 读取Excel到工作表组
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <returns>工作表组</returns>
        public List<ISheet> ReadExcelToSheets(FileStream fileStream)
        {
            IWorkbook workbook = ReadExcelToWorkbook(fileStream);
            return GetAllSheets(workbook);
        }
        /// <summary>
        /// 获得所有工作表
        /// </summary>
        /// <param name="workbook">工作簿对象</param>
        /// <returns>工作表对象</returns>
        public List<ISheet> GetAllSheets(IWorkbook workbook)
        {
            var sheets = new List<ISheet>();
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                sheets.Add(workbook.GetSheetAt(i));
            }
            return sheets;
        }
        /// <summary>
        /// 工作表转换为数据表
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="startRowNum">开始行数</param>
        /// <returns>数据表</returns>
        public DataTable SheetToDataTable(ISheet sheet, int startRowNum = 0)
        {
            var result = new DataTable(sheet.SheetName);
            ExcelRowModel excelRowModel = GetRows(sheet, startRowNum);
            for (int i = 0; i < excelRowModel.MaxCellNum; i++)
            {
                var dataColumn = new DataColumn(i.ToString(), typeof(string));
                result.Columns.Add(dataColumn);
            }
            foreach (IRow row in excelRowModel.Rows)
            {
                DataRow dataRow = result.NewRow();
                bool isAdd = false;
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    if (row.GetCell(i).IsNullOrEmptyString()) continue;
                    isAdd = true;
                    dataRow[i] = row.GetCell(i).ToString();
                }
                if (isAdd)
                {
                    result.Rows.Add(dataRow);
                }
            }
            return result;
        }
        /// <summary>
        /// 获得行
        /// </summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="startRowNum">开始行数</param>
        /// <returns>工作行</returns>
        public ExcelRowModel GetRows(ISheet sheet, int startRowNum = 0)
        {
            var result = new ExcelRowModel { Rows = new List<IRow>() };
            if (sheet.LastRowNum < startRowNum)
            {
                throw new MateralException($"表{sheet.SheetName}无数据");
            }
            for (int i = startRowNum; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                result.Rows.Add(row);
            }
            return result;
        }
        /// <summary>
        /// 工作簿转换为数据集
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="startRowNumbers">开始行数组</param>
        /// <returns>数据集</returns>
        public DataSet WorkbookToDataSet(IWorkbook workbook, params int[] startRowNumbers)
        {
            #region 开始行数组
            if (startRowNumbers != null && startRowNumbers.Length != 0)
            {
                if (startRowNumbers.Length == 1)
                {
                    startRowNumbers = GetStartRowNumbers(workbook.NumberOfSheets, startRowNumbers[0]);
                }
                else if (startRowNumbers.Length != workbook.NumberOfSheets)
                {
                    throw new MateralException("提供的开始行数数量与数据表数量不匹配");
                }
            }
            else
            {
                startRowNumbers = GetStartRowNumbers(workbook.NumberOfSheets, 0);
            }
            #endregion
            var result = new DataSet();
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                DataTable dataTable = SheetToDataTable(workbook.GetSheetAt(i), startRowNumbers[i]);
                result.Tables.Add(dataTable);
            }
            return result;
        }
        #region 私有方法
        /// <summary>
        /// 获得开始行数组
        /// </summary>
        /// <param name="count">总数</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        private int[] GetStartRowNumbers(int count, int value)
        {
            var startRowNumbers = new int[count];
            for (int i = 0; i < count; i++)
            {
                startRowNumbers[i] = value;
            }
            return startRowNumbers;
        }
        #endregion
        #endregion
        #region 生成

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSet"></param>
        /// <param name="setStyle"></param>
        /// <param name="setTableHeads"></param>
        /// <returns></returns>
        public IWorkbook DataSetToWorkbook<T>(DataSet dataSet, Action<IWorkbook, ISheet, int, int, ICell> setStyle, params Func<IWorkbook, ISheet, int>[] setTableHeads) where T : IWorkbook
        {
            T workbook = default;
            workbook = workbook.GetDefaultObject<T>();
            #region 表头委托
            if (setTableHeads.Length != dataSet.Tables.Count)
            {
                switch (setTableHeads.Length)
                {
                    case 0:
                        setTableHeads = new Func<IWorkbook, ISheet, int>[dataSet.Tables.Count];
                        break;
                    case 1:
                        {
                            Func<IWorkbook, ISheet, int> temFunc = setTableHeads[0];
                            setTableHeads = new Func<IWorkbook, ISheet, int>[dataSet.Tables.Count];
                            for (int i = 0; i < dataSet.Tables.Count; i++)
                            {
                                setTableHeads[i] = temFunc;
                            }

                            break;
                        }
                    default:
                        throw new MateralException("提供的设置表头委托与表数量不匹配");
                }
            }
            #endregion
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                DataTableToSheet(workbook, dataSet.Tables[i], setStyle, setTableHeads[i]);
            }
            return workbook;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="dataTable"></param>
        /// <param name="configCell"></param>
        /// <param name="setTableHead"></param>
        /// <returns></returns>
        public ISheet DataTableToSheet(IWorkbook workbook, DataTable dataTable, Action<IWorkbook, ISheet, int, int, ICell> configCell, Func<IWorkbook, ISheet, int> setTableHead = null)
        {
            ISheet sheet = string.IsNullOrEmpty(dataTable.TableName) ? workbook.CreateSheet() : workbook.CreateSheet(dataTable.TableName);
            int rowNum = 0;
            if (setTableHead != null)
            {
                rowNum = setTableHead(workbook, sheet);
            }
            int columnNum = dataTable.Columns.Count;
            foreach (DataRow dr in dataTable.Rows)
            {
                IRow row = sheet.CreateRow(rowNum);
                for (var i = 0; i < columnNum; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(dr[i].ToString());
                    configCell?.Invoke(workbook, sheet, i, rowNum, cell);
                }
                rowNum++;
            }
            return sheet;
        }
        #endregion
    }
}
