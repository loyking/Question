using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Reflection;
using Newtonsoft.Json;

namespace ExcelTool
{
    public class ExcelAnalyzeService : IExcelAnalyzeService
    {
        public UploadExcelFileResult CheckExcelDatasEnableNull(ISheet sheet, List<Regular> list, Dictionary<int, string> dict, int rowCount)
        {
            var result = new UploadExcelFileResult();
            try
            {
                result.Success = true;

                // 记录单个sheet所有错误信息
                var sheetErrors = new List<ExcelFileErrorPosition>();
                // 表头结束行
                int lastHeaderRowIndex = list.Find(e => e.HeaderRegular != null).HeaderRegular["lastHeaderRow"];

                // 循环行数据
                for (int i = lastHeaderRowIndex; i <= rowCount; i++)
                {
                    // 标注该行是否出错
                    bool isrowfalse = false;
                    // 记录该行数据临时对象
                    var rowDatas = new List<string>();
                    // 记录该行错误列
                    var rowErrorCell = new List<int>();
                    // 记录该行错误列具体错误信息
                    var rowErrorMessages = new List<string>();
                    // 记录该行空值数
                    int nullcount = 0;


                    IRow dataRow = sheet.GetRow(i);
                    int cellCount = dict.Count;

                    // 循环列数据
                    for (int j = dataRow.FirstCellNum; j < cellCount; j++)
                    {
                        if (j == -1)
                            break;
                        string value = "";
                        Regular header = list.Find(h => h.HeaderText == dict[j]);
                        //value = dataRow.GetCell(j).ToString();
                        ICell cell = dataRow.GetCell(j);
                        if (cell != null)
                        {
                            switch (dataRow.GetCell(j).CellType)
                            {
                                case CellType.Formula:
                                    value = dataRow.GetCell(j).StringCellValue.ToString();
                                    break;
                                default:
                                    value = dataRow.GetCell(j).ToString();
                                    break;
                            }
                        }


                        // 记录可能出错数据
                        rowDatas.Add(value);

                        // 检查空值
                        if (!this.CheckNull(value, ref nullcount))
                        {
                            // 检查类型
                            if (!this.CheckDataType(header.DataType, value))
                            {
                                isrowfalse = true;
                                result.Success = false;
                                // 记录该行错误信息
                                rowErrorCell.Add(j + 1);
                                rowErrorMessages.Add("读取EXCEL数据时发生数据格式错误，请检查该行该列数据格式！");
                            }
                            else
                            {
                                if (header.DataType == "System.string" || header.DataType == "System.String")
                                {
                                    this.ReplaceSpace(ref value);
                                }
                            }
                        }
                    }
                    // 报错处理(空行不报错)
                    if (isrowfalse && nullcount < cellCount)
                    {
                        sheetErrors.Add(new ExcelFileErrorPosition
                        {
                            RowContent = rowDatas,
                            RowIndex = i + 1,
                            CellIndex = rowErrorCell,
                            ErrorMessage = rowErrorMessages
                        });
                    }
                }
                result.ExcelFileErrorPositions = sheetErrors;
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        private bool CheckDataType(string dataType, string value)
        {
            return true;
        }

        private bool CheckNull(string value, ref int nullcount)
        {
            if (value == null)
            {
                nullcount++;
                return false;
            }
            return true;
                
        }

        public IWorkbook CreateWorkBook(int edition, Stream excelFileStream)
        {
            switch (edition)
            {
                case 7:
                    return new XSSFWorkbook(excelFileStream);
                case 3:
                    return new HSSFWorkbook(excelFileStream);
                default:
                    return null;
            }
        }

        public List<TableDTO> GetExcelDatas<TableDTO>(ISheet sheet, string sheetName, List<Regular> list,
            Dictionary<int, string> dict, int rowCount)
        {
            // 返回数据对象集合
            var resultList = new List<TableDTO>();
            // 表头结束行
            int lastHeaderRowIndex = list.Find(e => e.HeaderRegular != null).HeaderRegular["lastHeaderRow"];

            // 循环行数据
            for (int i = lastHeaderRowIndex; i <= rowCount; i++)
            {
                // 产生一个新的泛型对象
                var model = Activator.CreateInstance<TableDTO>();
                // 记录该行空值数
                int nullcount = 0;

                IRow dataRow = sheet.GetRow(i);
                int cellCount = dict.Count;

                if (dataRow != null)
                {
                    // 循环列数据
                    for (int j = dataRow.FirstCellNum; j < cellCount; j++)
                    {
                        if (j == -1)
                            break;
                        string value = "";
                        Regular header = list.Find(h => h.HeaderText == dict[j]);
                        PropertyInfo prop = model.GetType().GetProperty(header.PropertyName);
                        //value = dataRow.GetCell(j).ToString();
                        ICell cell = dataRow.GetCell(j);
                        if (cell != null)
                        {
                            switch (dataRow.GetCell(j).CellType)
                            {
                                case CellType.Formula:
                                    value = dataRow.GetCell(j).StringCellValue.ToString();
                                    break;
                                case CellType.Numeric:
                                    short format = cell.CellStyle.DataFormat;

                                    if (format == 14 || format == 31 || format == 57 || format == 58 || format== 181)

                                    {

                                        DateTime date = cell.DateCellValue;

                                        value = date.ToString("yyyy-MM-dd");

                                    }
                                    else
                                    {
                                        value = dataRow.GetCell(j).ToString();
                                    }
                                    break;
                                default:
                                    value = dataRow.GetCell(j).ToString();
                                    break;
                            }
                        }                        

                        // 去除空值
                        this.ReplaceSpace(ref value);

                        if (value == "")
                        {
                            nullcount++;
                        }

                        // 赋值
                        switch (header.DataType)
                        {
                            case "System.double":
                                double valueDecimal;
                                if (double.TryParse(value, out valueDecimal))
                                {
                                    prop.SetValue(model, valueDecimal, null);
                                }
                                break;
                            case "System.Int16":
                                short valueInt16;
                                if (Int16.TryParse(value, out valueInt16))
                                {
                                    prop.SetValue(model, valueInt16, null);
                                }
                                break;
                            case "System.Int32":
                                int valueInt32;
                                if (Int32.TryParse(value, out valueInt32))
                                {
                                    prop.SetValue(model, valueInt32, null);
                                }
                                break;
                            case "System.Boolean":
                                bool valueBoolean;
                                if (Boolean.TryParse(value, out valueBoolean))
                                {
                                    prop.SetValue(model, valueBoolean, null);
                                }
                                break;
                            case "System.DateTime":
                                DateTime valueDateTime;
                                if (DateTime.TryParse(value, out valueDateTime))
                                {
                                    prop.SetValue(model, valueDateTime, null);
                                }
                                break;
                            default:
                                prop.SetValue(model, value, null);
                                break;
                        }
                    }
                }

                // 添加非空行数据到DTO
                if (nullcount < cellCount)
                {
                    resultList.Add(model);
                }
            }

            return resultList;
        }

        public int GetExcelEdition(string fileName)
        {
            var edition = 0;
            string[] items = fileName.Split(new char[] { '.' });
            int count = items.Length;
            switch (items[count - 1])
            {
                case "xls":
                    edition = 3;
                    break;
                case "xlsx":
                    edition = 7;
                    break;
                default:
                    break;
            }

            return edition;
        }

        public Dictionary<int, string> GetExcelHeaders(ISheet sheet, ref UploadExcelFileResult uploadExcelFileResult,
            List<Regular> list)
        {
            int firstHeaderRowIndex = list.Find(e => e.HeaderRegular != null).HeaderRegular["firstHeaderRow"];
            int lastHeaderRowIndex = list.Find(e => e.HeaderRegular != null).HeaderRegular["lastHeaderRow"];

            var dict = new Dictionary<int, string>();

            try
            {
                // 循环获得表头
                for (int i = firstHeaderRowIndex - 1; i < lastHeaderRowIndex; i++)
                {
                    IRow headerRow = sheet.GetRow(i);
                    int cellCount = headerRow.LastCellNum;

                    for (int j = headerRow.FirstCellNum; j < cellCount; j++)
                    {
                        if (!string.IsNullOrEmpty(headerRow.GetCell(j).StringCellValue.Trim()))
                        {
                            // 根据 键－值 是否已存在做不同处理
                            //TODO 代码待重构！！！
                            try
                            {
                                string oldValue = dict[j];
                                dict.Remove(j);
                                dict.Add(j, oldValue + headerRow.GetCell(j).StringCellValue.Trim());
                            }
                            catch (Exception)
                            {
                                dict.Add(j, headerRow.GetCell(j).StringCellValue.Trim());
                            }
                        }
                    }
                }
                #region  针对人才信息表头的自定义处理（为重复表头列初始化编号）
                /*起始时间（包含）1-4结束时间（等于）1-4工作单位（等于）1-4职务（等于）1-4表彰时间（包含）1-3
                         表彰称号（等于）1-3授予单位（等于）1-3奖励时间（包含）1-3奖励等级（等于）1-3项目名称（等于）1-3
                             */
                int workStartTime = 1, workEndTime = 1, workUnit = 1, workPost = 1;
                int grantTime = 1, grantTitle = 1, grantUnit = 1;
                int rewardTime = 1, projectName = 1, rewardLevel = 1; 
                #endregion
                // 遍历表头字典，消除空格
                for (int i = 0; i < dict.Count; i++)
                {
                    var value = dict[i];
                    this.ReplaceSpace(ref value);
                    //#region 针对人才信息表头的自定义处理（为重复表头列加编号）                    
                    ////起始时间（包含）1-4
                    //if (value.Contains("起始时间"))
                    //{
                    //    value = value + workStartTime;
                    //    workStartTime++;
                    //}
                    ////结束时间（等于）1-4
                    //else if(value== "结束时间")
                    //{
                    //    value = value + workEndTime;
                    //    workEndTime++;
                    //}
                    ////工作单位（等于）1-4
                    //else if(value == "工作单位")
                    //{
                    //    value = value + workUnit;
                    //    workUnit++;
                    //}
                    ////职务（等于）1-4
                    //else if(value == "职务")
                    //{
                    //    value = value + workPost;
                    //    workPost++;
                    //}
                    ////表彰时间（包含）1-3
                    //else if(value.Contains("表彰时间"))
                    //{
                    //    value = value + grantTime;
                    //    grantTime++;
                    //}
                    ////表彰称号（等于）1-3
                    //else if(value=="表彰称号")
                    //{
                    //    value = value + grantTitle;
                    //    grantTitle++;
                    //}
                    ////授予单位（等于）1-3
                    //else if(value == "授予单位")
                    //{
                    //    value = value + grantUnit;
                    //    grantUnit++;
                    //}
                    ////奖励时间（包含）1-3
                    //else if (value.Contains("奖励时间"))
                    //{
                    //    value = value + rewardTime;
                    //    rewardTime++;
                    //}
                    ////奖励等级（等于）1-3
                    //else if (value == "奖励等级")
                    //{
                    //    value = value + rewardLevel;
                    //    rewardLevel++;
                    //}
                    ////项目名称（等于）1-3
                    //else if(value == "项目名称")
                    //{
                    //    value = value + projectName;
                    //    projectName++;
                    //}
                    //#endregion
                    dict[i] = value;
                }
                // 检查表头模板是否被修改
                for (int count = 0; count < dict.Count; count++)
                {
                    Regular header = list.Find(h => h.HeaderText == dict[count]);

                    if (header == null)
                    {
                        uploadExcelFileResult.Success = false;
                        uploadExcelFileResult.Message =
                            string.Format("读取EXCEL表头模板时发生错误，可能造成原因是：EXCEL模板被修改【{0}--{1}】！请下载最新EXCEL模板！",sheet.SheetName,dict[count]);
                    }
                }
            }
            catch (Exception e)
            {
                uploadExcelFileResult.Success = false;
                uploadExcelFileResult.Message = "读取EXCEL表头模板时发生错误，可能造成原因是：EXCEL模板被修改！请下载最新EXCEL模板！";
            }
            var jsonDict = JsonConvert.SerializeObject(dict);
            return dict;
        }
        // 去除空值
        public void ReplaceSpace(ref string cellValue)
        {
            cellValue = TruncateString(cellValue, new char[] { ' ' }, new char[] { '　' });
        }

        // 对字符串做空格剔除处理
        private string TruncateString(string originalWord, char[] spiltWord1, char[] spiltWord2)
        {
            var result = "";
            var valueReplaceDbcCase = originalWord.Split(spiltWord1);

            if (valueReplaceDbcCase.Count() > 1)
            {
                for (int i = 0; i < valueReplaceDbcCase.Count(); i++)
                {
                    if (valueReplaceDbcCase[i] != "" && valueReplaceDbcCase[i] != " " &&
                        valueReplaceDbcCase[i] != "　")
                    {
                        result += TruncateString(valueReplaceDbcCase[i], spiltWord2, new char[0]);
                    }
                }
            }
            else
            {
                if (spiltWord2.Any())
                {
                    result = TruncateString(originalWord, spiltWord2, new char[0]);
                }
                else
                {
                    result = originalWord;
                }
            }

            return result;
        }
    }
}
