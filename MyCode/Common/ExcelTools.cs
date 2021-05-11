using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Common
{
    public class ExcelTools
    {

        public static List<T> Import<T>(Stream stream, string fileName, Func<ImportItemContext<T>, object> import = null, Func<ImportItemException<T>, bool> onException = null) where T : class, new()
        {
            DataTable dt = new DataTable();
            string fileExt = Path.GetExtension(fileName).ToLower();
            using (stream)
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                IWorkbook workbook;
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(stream); }
                else if (fileExt == ".xls") { workbook = new HSSFWorkbook(stream); }
                else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);
                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);

                for (int i = 0; i < header.LastCellNum; i++)
                {
                    dt.Columns.Add(new DataColumn(header.GetCell(i).StringCellValue));
                }
                //数据  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dr[j] = sheet.GetRow(i).GetCell(j);
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return Import(dt, import, onException);
        }

        public static List<T> Import<T>(DataTable dt, Func<ImportItemContext<T>, object> import = null, Func<ImportItemException<T>, bool> onException = null) where T : class, new()
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return Enumerable.Empty<T>().ToList();
            }

            List<T> list = new List<T>();
            var propertyInfos = typeof(T).GetProperties();

            var dict = propertyInfos.Where(a => a.CustomAttributes.Any(t => t.AttributeType == typeof(ExcelColumnAttribute))).ToDictionary(a => a.Name, a =>
                {
                    var columnAttr = (ExcelColumnAttribute)a.GetCustomAttributes(typeof(ExcelColumnAttribute), true).FirstOrDefault();
                    return columnAttr?.Name ?? a.Name;
                });

            var rowNumber = 0;
            bool isContinue = true;
            foreach (DataRow row in dt.Rows)
            {
                if (!isContinue)
                {
                    break;
                }

                rowNumber++;
                var item = new T();
                foreach (var propertyInfo in propertyInfos)
                {
                    if (dict.ContainsKey(propertyInfo.Name))
                    {
                        var excelColumnName = dict[propertyInfo.Name];
                        if (excelColumnName != null && dt.Columns.Contains(excelColumnName))
                        {
                            var value = row[excelColumnName] == DBNull.Value ? null : row[excelColumnName];
                            var importContext = new ImportItemContext<T>()
                            {
                                Item = item,
                                PropertyInfo = propertyInfo,
                                Row = row,
                                RowNumber = rowNumber,
                                Column = dt.Columns[excelColumnName],
                                ColumnValue = value

                            };

                            try
                            {

                                if (import != null)
                                {
                                    value = import(importContext);
                                }

                                value = Convert.ChangeType(value, propertyInfo.PropertyType);
                                propertyInfo.SetValue(item, value);

                            }
                            catch (Exception ex)
                            {
                                if (onException != null)
                                {
                                    isContinue = onException(new ImportItemException<T>()
                                    {
                                        ImportContext = importContext,
                                        Exception = ex
                                    });
                                }

                            }
                        }
                    }

                }
                list.Add(item);
            }
            return list;

        }


        public static void Export<T>(List<T> data, string path, Func<ExportItemContext<T>, string> export = null, Func<ExportItemException<T>, bool> onException = null)
        {
            var workbook = ExportWorkbook(data, export, onException);
            using (var file = File.Create(path))
            {
                workbook.Write(file);
            }

        }

        public static byte[] Export<T>(List<T> data, Func<ExportItemContext<T>, string> export = null, Func<ExportItemException<T>, bool> onException = null)
        {
            var workbook = ExportWorkbook(data, export, onException);
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                return ms.ToArray();
            }

        }

        private static IWorkbook ExportWorkbook<T>(List<T> data, Func<ExportItemContext<T>, string> export = null, Func<ExportItemException<T>, bool> onException = null)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Sheet1");

            var headRow = sheet.CreateRow(0);
            var props = typeof(T).GetProperties();

            var columnMaps = props.Where(a => a.CustomAttributes.Any(t => t.AttributeType == typeof(ExcelColumnAttribute)))
                .Select(a =>
                {
                    var columnAttr = (ExcelColumnAttribute)a.GetCustomAttributes(typeof(ExcelColumnAttribute), true).First();
                    return new
                    {
                        ColumnName = columnAttr.Name ?? a.Name,
                        PropertyInfo = a,
                        ColumnOrder = columnAttr.Order
                    };
                }).OrderBy(a => a.ColumnOrder).ToList();

            var orderedPropertyInfos = columnMaps.Select(a => a.PropertyInfo).ToList();
            for (var i = 0; i < columnMaps.Count; ++i)
            {
                headRow.CreateCell(i).SetCellValue(columnMaps[i].ColumnName);
            }


            var isContinue = true;
            for (var i = 0; i < data.Count; ++i)
            {
                if (!isContinue)
                {
                    break;
                }
                var item = data[i];
                var row = sheet.CreateRow(i + 1);
                for (var j = 0; j < orderedPropertyInfos.Count; ++j)
                {
                    var exportContext = new ExportItemContext<T>()
                    {
                        Item = item,
                        PropertyInfo = orderedPropertyInfos[j]
                    };
                    try
                    {
                        var value = orderedPropertyInfos[j].GetValue(item)?.ToString();
                        if (export != null)
                        {
                            value = export(exportContext);
                        }
                        row.CreateCell(j).SetCellValue(value);
                    }
                    catch (Exception ex)
                    {
                        if (onException != null)
                        {
                            isContinue = onException(new ExportItemException<T>()
                            {
                                Exception = ex,
                                ExportContext = exportContext
                            });
                        }
                    }
                }
            }

            return workbook;
        }
    }


    public class ExcelColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public int Order { get; set; }

        public ExcelColumnAttribute()
        {

        }
        public ExcelColumnAttribute(int order)
        {
            this.Order = order;
        }
        public ExcelColumnAttribute(string name)
        {
            this.Name = name;
        }

        public ExcelColumnAttribute(string name, int order)
        {
            this.Name = name;
            this.Order = order;
        }
    }


    public class ImportItemContext<T>
    {
        public T Item { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public int RowNumber { get; set; }

        public DataRow Row { get; set; }

        public DataColumn Column { get; set; }

        public object ColumnValue { get; set; }
    }

    public class ExportItemContext<T>
    {
        public T Item { get; set; }

        public PropertyInfo PropertyInfo { get; set; }
    }

    public class ImportItemException<T>
    {
        public ImportItemContext<T> ImportContext { get; set; }

        public Exception Exception { get; set; }
    }

    public class ExportItemException<T>
    {
        public ExportItemContext<T> ExportContext { get; set; }

        public Exception Exception { get; set; }
    }
}
