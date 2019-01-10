using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

    /// <summary>
    /// 工作簿信息类
    /// </summary>
    public class WorkbookInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// 主题信息
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 文件标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 文件副标题
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="company">公司名称</param>
        /// <param name="applicationName">ApplicationName</param>
        /// <param name="author">作者</param>
        /// <param name="subject">主题信息</param>
        /// <param name="title">文件标题</param>
        /// <param name="subTitle">文件副标题</param>
        public WorkbookInfo(string company , string applicationName , string author ,
            string subject , string title ,string subTitle )
        {
            this.Company = company;
            this.ApplicationName = applicationName;
            this.Author = author;
            this.Subject = subject;
            this.Title = title;
            this.SubTitle = subTitle;
        }
    }
    /// <summary>
    /// NpoiHelper帮助类
    /// </summary>
    public class NpoiHelper 
    {
        #region 字段
        private HSSFWorkbook _workbook;
        private WorkbookInfo _info;
        private DataTable _dataSource;
        #endregion

        #region 属性

        public WorkbookInfo Info
        {
            get { return this._info; }
        }

        public HSSFWorkbook Workbook
        {
            get { return this._workbook; }
        }

        public string[] TitleColumnNames { get; set; }
        public ICellStyle TitleCellStyle { get; set; }
        public ICellStyle ColumnTitleStyle { get; set; }
        public ICellStyle DefaultCellStyle { get; set; }
        public ICellStyle DateCellStyle { get; set; }
        #endregion

        #region 构造方法


        /// <summary>
        /// 使用DataTable做数据源的构造方法
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="info"></param>
        /// <param name="titleCellStyle"></param>
        /// <param name="columnTitleStyle"></param>
        /// <param name="defaultCellStyle"></param>
        /// <param name="dateCellStyle"></param>
        /// <param name="titleColumnNames"></param>
        public NpoiHelper(DataTable dataSource , WorkbookInfo info , ICellStyle titleCellStyle=null , ICellStyle columnTitleStyle=null ,
            ICellStyle defaultCellStyle=null , ICellStyle dateCellStyle=null , params string[] titleColumnNames)
        {
            this._workbook = new HSSFWorkbook();
            this._dataSource = dataSource;
            if (info == null)
                info = new WorkbookInfo("湖南青鸟科教集团","OA管理系统","LJG","","book1","");
            this._info = info;
            this.TitleColumnNames = titleColumnNames;
            BuilderStyle(titleCellStyle, columnTitleStyle, defaultCellStyle, dateCellStyle);
            InitWorkbook();
        }
        

    private void BuilderStyle(ICellStyle titleCellStyle, ICellStyle columnTitleStyle, ICellStyle defaultCellStyle, ICellStyle dateCellStyle)
        {
            this.TitleCellStyle = titleCellStyle;
            if (titleCellStyle == null)
            {
                //准备大标题单元格样式
                this.TitleCellStyle = this._workbook.CreateCellStyle();
                this.TitleCellStyle.Alignment = HorizontalAlignment.Center;
                this.TitleCellStyle.VerticalAlignment = VerticalAlignment.Center;
                this.TitleCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightCornflowerBlue.Index;
                this.TitleCellStyle.FillPattern = FillPattern.SolidForeground;
                //创建一个字体样式
                IFont font = this._workbook.CreateFont();
                //font.FontHeight = 20 * 20;
                font.FontHeightInPoints = 14;
                font.Boldweight = short.MaxValue;
                this.TitleCellStyle.SetFont(font);
            }
            this.ColumnTitleStyle = columnTitleStyle;
            if (columnTitleStyle == null)
            {
                //准备列标题单元格样式
                this.ColumnTitleStyle = this._workbook.CreateCellStyle();
                this.ColumnTitleStyle.WrapText = true;//设置单元格内容自行换行
                this.ColumnTitleStyle.Alignment = HorizontalAlignment.Center;
                this.ColumnTitleStyle.VerticalAlignment = VerticalAlignment.Center;
                //创建一个字体样式
                IFont font = this._workbook.CreateFont();
                font.FontHeightInPoints = 12;
                font.Boldweight = short.MaxValue;
                //font.FontHeight = 15 * 15;
                this.ColumnTitleStyle.SetFont(font);
                SetBorder(this.ColumnTitleStyle);
            }
            this.DefaultCellStyle = defaultCellStyle;
            if (defaultCellStyle == null)
            {
                //准备默认内容单元格样式
                this.DefaultCellStyle = this._workbook.CreateCellStyle();
                this.DefaultCellStyle.VerticalAlignment = VerticalAlignment.Center;
                IFont font = this._workbook.CreateFont();
                //font.FontHeight = 12*12;
                font.FontHeightInPoints = 12;
                this.DefaultCellStyle.SetFont(font);
                SetBorder(this.DefaultCellStyle);
            }
            this.DateCellStyle = dateCellStyle;
            if (dateCellStyle == null)
            {
                //准备日期单元格样式
                this.DateCellStyle = this._workbook.CreateCellStyle();
                this.DateCellStyle.VerticalAlignment = VerticalAlignment.Center;
                IFont font = this._workbook.CreateFont();
                //font.FontHeight = 12 * 12;
                font.FontHeightInPoints = 12;
                this.DateCellStyle.SetFont(font);
                IDataFormat format = this._workbook.CreateDataFormat();
                this.DateCellStyle.DataFormat = format.GetFormat("yyyy年m月d日");
                SetBorder(this.DateCellStyle);
            }
        }
        
        public void SetDateFormat(string dateFormat)
        {
            //准备日期单元格样式
            this.DateCellStyle = this._workbook.CreateCellStyle();
            this.DateCellStyle.VerticalAlignment = VerticalAlignment.Center;
            IFont font = this._workbook.CreateFont();
            //font.FontHeight = 12 * 12;
            font.FontHeightInPoints = 12;
            this.DateCellStyle.SetFont(font);
            IDataFormat format = this._workbook.CreateDataFormat();
            this.DateCellStyle.DataFormat = format.GetFormat(dateFormat);
            SetBorder(this.DateCellStyle);
        }
        /// <summary>
        /// 为指定的单元格样式，添加“边框”
        /// </summary>
        /// <param name="style"></param>
        private void SetBorder(ICellStyle style)
        {
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.TopBorderColor = HSSFColor.Black.Index;
        }
        #endregion

        #region 通过数据源直接生成规则EXCEL
        /// <summary>
        /// 初始化wookbook的文档信息
        /// </summary>
        private void InitWorkbook()
        {
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = _info.Company;
            _workbook.DocumentSummaryInformation = dsi;
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = _info.Author;
            si.ApplicationName = _info.ApplicationName;
            si.Subject = _info.Subject;
            si.Title = _info.Title;
            _workbook.SummaryInformation = si;

        }
        /// <summary>
        /// 根据数据源直接生成规则EXCEL表格
        /// </summary>
        /// <returns></returns>
        public HSSFWorkbook CreateExcel()
        {
            if (this._dataSource != null)
            {
                //创建工作表
                ISheet sheet = _workbook.CreateSheet("sheet1");
                //设置工作表的默认行高
                //sheet.DefaultRowHeightInPoints = 25;
                //创建表头
                int headRows = CreateHeader(sheet);
                //填充数据
                CreateData(sheet, headRows);
                return this._workbook;
            }
            else
            {
                throw new Exception("当前操作没有指定数据源，请使用自定义方法！");
            }
        }
        /// <summary>
        /// 创建表头与标题行
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns>已使用的行数</returns>
        private int CreateHeader(ISheet sheet)
        {
            int rowIndex = 0;
            if (this._info != null)
            {
                if (this._info.Title != "")
                {
                    IRow headerRow = sheet.CreateRow(rowIndex);
                    headerRow.Height = 25 * 20;
                    ICell headCell = headerRow.CreateCell(0);
                    headCell.CellStyle = this.TitleCellStyle;
                    headCell.SetCellValue(this._info.Title);
                    //设置单元格合并区别(firstRow,lastRow,firstColumn,lastColumn)
                    int colspan = this._dataSource != null ? this._dataSource.Columns.Count : this.TitleColumnNames.Length;
                    sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, colspan - 1));
                    rowIndex++;
                }
                if (this._info.SubTitle != "")
                {
                    IRow headerRow = sheet.CreateRow(rowIndex);
                    headerRow.Height = 23 * 20;
                    ICell headCell = headerRow.CreateCell(0);
                    //准备副标题单元格样式
                    ICellStyle style = this._workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Right;
                    style.VerticalAlignment = VerticalAlignment.Center;
                    style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightCornflowerBlue.Index;
                    style.FillPattern = FillPattern.SolidForeground;
                    //create a font style
                    IFont font = this._workbook.CreateFont();
                    font.FontHeightInPoints = 13;
                    style.SetFont(font);
                    headCell.CellStyle = style;
                    
                    headCell.SetCellValue(this._info.SubTitle);
                    //设置单元格合并区别(firstRow,lastRow,firstColumn,lastColumn)
                    int colspan = this._dataSource != null ? this._dataSource.Columns.Count : this.TitleColumnNames.Length;
                    sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, colspan - 1));
                    rowIndex++;
                }
            }
            IRow columnHeaderRow = sheet.CreateRow(rowIndex);
            columnHeaderRow.Height = 22 * 20;
            int colIndex = 0;
            if (this.TitleColumnNames.Length > 0)
            {
                foreach (string titleName in this.TitleColumnNames)
                {
                    ICell titleCell = columnHeaderRow.CreateCell(colIndex);
                    titleCell.CellStyle = this.ColumnTitleStyle;
                    titleCell.SetCellValue(titleName);
                    colIndex++;
                }
            }
            else
            {
                foreach (DataColumn dataColumn in this._dataSource.Columns)
                {
                    ICell titleCell = columnHeaderRow.CreateCell(colIndex);
                    titleCell.CellStyle = this.ColumnTitleStyle;
                    titleCell.SetCellValue(dataColumn.ColumnName);
                    colIndex++;
                }
            }

            #region 自动调整列宽
            //调整列宽
            //AutoColumnSize(sheet, rowIndex + 1, 0);
            #endregion
            return rowIndex + 1;
        }
        /// <summary>
        /// 创建规则表格时使用的生成数据区域单元格方法
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private int CreateData(ISheet sheet, int rowIndex)
        {
            int startIndex = rowIndex;

            foreach (DataRow row in this._dataSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                dataRow.Height = 22 * 20;
                foreach (DataColumn column in this._dataSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);
                    string drValue = row[column].ToString();
                    #region 字段类型处理
                    switch (column.DataType.ToString())
                    {
                        case "System.String": //字符串类型
                            newCell.SetCellValue(drValue);
                            newCell.CellStyle = this.DefaultCellStyle;
                            break;
                        case "System.DateTime": //日期类型
                            DateTime dateV;
                            if(DateTime.TryParse(drValue, out dateV))
                            {
                                newCell.SetCellValue(dateV);
                                newCell.CellStyle = this.DateCellStyle; //格式化显示
                            }
                            else
                            {
                                newCell.SetCellValue("");
                                newCell.CellStyle = this.DefaultCellStyle;
                            }
                            
                            break;
                        case "System.Boolean": //布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            newCell.CellStyle = this.DefaultCellStyle;
                            break;
                        case "System.Int16": //整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            newCell.CellStyle = this.DefaultCellStyle;
                            break;
                        case "System.Decimal": //浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            newCell.CellStyle = this.DefaultCellStyle;
                            break;
                        case "System.DBNull": //空值处理
                            newCell.SetCellValue("");
                            newCell.CellStyle = this.DefaultCellStyle;
                            break;
                        default:
                            newCell.SetCellValue(drValue);
                            newCell.CellStyle = this.DefaultCellStyle;
                            break;
                    }
                    #endregion
                    #region 插入留余空白列
                    //int iLastCell = sheet1.GetRow(4).LastCellNum + 1 - dtGetRowTable.Columns.Count;
                    //for (int i = 1; i < iLastCell; i++)
                    //{
                    //    HSSFCell newNullCell = dataRow.CreateCell(newCell.ColumnIndex + i) as HSSFCell;
                    //    newNullCell.SetCellValue("");
                    //    newNullCell.CellStyle = style;
                    //}
                    #endregion

                }

                #endregion
                rowIndex++;
            }
            //调整列宽
            AutoColumnSize(sheet, this._dataSource.Rows.Count + 1, startIndex -1);
            return rowIndex;
        }
        /// <summary>
        /// 共用的自动列宽方法
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="dataRows">总行数</param>
        /// <param name="startIndex">起始行号</param>
        private void AutoColumnSize(ISheet sheet, int dataRows, int startIndex)
        {
            int columns = this._dataSource != null ? this._dataSource.Columns.Count : this.TitleColumnNames.Length;
            #region 自动调整列宽
            //列宽自适应，只对英文和数字有效
            for (int i = startIndex; i <= startIndex + dataRows; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            //获取当前列的宽度，然后对比本列的长度，取最大值
            for (int columnNum = 0; columnNum <= columns; columnNum++)
            {
                int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
                for (int rowNum = startIndex; rowNum <= sheet.LastRowNum; rowNum++)
                {
                    IRow currentRow;
                    //当前行未被使用过
                    if (sheet.GetRow(rowNum) == null)
                    {
                        currentRow = sheet.CreateRow(rowNum);
                    }
                    else
                    {
                        currentRow = sheet.GetRow(rowNum);
                    }

                    if (currentRow.GetCell(columnNum) != null)
                    {
                        ICell currentCell = currentRow.GetCell(columnNum);
                        int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                        if (columnWidth < length)
                        {
                            columnWidth = length;
                        }
                    }
                }
                sheet.SetColumnWidth(columnNum,Math.Min(255*256,
                   columnWidth * this.ColumnTitleStyle.GetFont(this._workbook).FontHeightInPoints / 8 * 256));
            }
            #endregion
        }

        #endregion

        #region 不指定数据源自定义生成不规则EXCEL
        /// <summary>
        /// 创建自定义工作表
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public ISheet CreateCustomSheet(string sheetName )
        {
            //创建工作表
            ISheet sheet = _workbook.CreateSheet(sheetName);
            return sheet;
        }
        /// <summary>
        /// 根据titleColumnNames和WorkbookInfo.Title生成规则表头
        /// </summary>
        /// <returns></returns>
        public int CreateSigleSheetHeader(ISheet sheet)
        {
            return CreateHeader(sheet);
        }
        /// <summary>
        /// 创建自定义公式单元格
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="colIndex"></param>
        /// <param name="formula"></param>
        /// <returns></returns>
        public ICell CreateCustomFormulaCell(IRow dataRow, int colIndex, string formula)
        {
            ICell newCell = dataRow.CreateCell(colIndex);
            newCell.SetCellFormula(formula);
            newCell.CellStyle = this.DefaultCellStyle;
            return newCell;
        }
        /// <summary>
        /// 创建自定义数据单元格
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ICell CreateCustomDataCell(IRow dataRow, int colIndex, object value)
        {
            ICell newCell = dataRow.CreateCell(colIndex);
            string drValue = value.ToString();
            #region 字段类型处理
            switch (value.GetType().ToString())
            {
                case "System.String": //字符串类型
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = this.DefaultCellStyle;
                    break;
                case "System.DateTime": //日期类型
                    DateTime dateV;
                    DateTime.TryParse(drValue, out dateV);
                    newCell.SetCellValue(dateV);
                    newCell.CellStyle = this.DateCellStyle; //格式化显示
                    break;
                case "System.Boolean": //布尔型
                    bool boolV = false;
                    bool.TryParse(drValue, out boolV);
                    newCell.SetCellValue(boolV);
                    newCell.CellStyle = this.DefaultCellStyle;
                    break;
                case "System.Int16": //整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int intV = 0;
                    int.TryParse(drValue, out intV);
                    newCell.SetCellValue(intV);
                    newCell.CellStyle = this.DefaultCellStyle;
                    break;
                case "System.Decimal": //浮点型
                case "System.Double":
                    double doubV = 0;
                    double.TryParse(drValue, out doubV);
                    newCell.SetCellValue(doubV);
                    newCell.CellStyle = this.DefaultCellStyle;
                    break;
                case "System.DBNull": //空值处理
                    newCell.SetCellValue("");
                    newCell.CellStyle = this.DefaultCellStyle;
                    break;
                default:
                    newCell.SetCellValue(drValue);
                    newCell.CellStyle = this.DefaultCellStyle;
                    break;
            }
            #endregion

            return newCell;
        }
        /// <summary>
        /// 自动调整列宽
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dataRows"></param>
        /// <param name="startIndex"></param>
        public void AutoSizeToColumn(ISheet sheet, int dataRows, int startIndex)
        {
            AutoColumnSize(sheet, dataRows, startIndex);
        }
        #endregion

        #region 导出Excel文件
        /// <summary>
        /// 根据数据源得到用于在Web导出的内存流
        /// </summary>
        /// <returns></returns>
        private MemoryStream ExportWeb()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                this._workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }
        /// <summary>
        /// 生成Excel文件提供Web下载
        /// </summary>
        public void ExportDownloadFile()
        {
            if (this._dataSource != null)
            {
                CreateExcel();
            }
            byte[] bytes = ExportWeb().GetBuffer();
            string UserAgent = HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower();
            string filename = this._info.Title + ".xls";
            if (UserAgent.IndexOf("firefox") <= 0)//火狐,文件名不需要编码
            {
               filename = HttpUtility.UrlEncode(filename, Encoding.UTF8);
            }
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" +filename);
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.End();
        }
       
        /// <summary>
        /// 生成Excel文件，并按指定的路径存储
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string ExportLocalFile(string filePath)
        {
            if (this._dataSource != null)
            {
                CreateExcel();
            }
            string fileName = Path.Combine(filePath, this._info.Title + ".xls");
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                this._workbook.Write(fs);
            }
            return fileName;
        }
        #endregion
    }
