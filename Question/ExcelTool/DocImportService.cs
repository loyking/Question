using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XWPF.UserModel;
using System.IO;
using HRInfoSys.Common;
using HRInfoSys.Models;

namespace ExcelTool
{
    public class DocImportService
    {
        /// <summary>
        /// 获取单元格文本
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static string GetTextFromCell(XWPFTableCell cell,bool wrap=false)
        {
            StringBuilder text = new StringBuilder();
            foreach (var para in cell.Paragraphs)
            {
                text.Append(para.ParagraphText);
                if (wrap)
                    text.Append("\r\n");
            }
            return text.ToString();
        }
        public static FinePerson ImportDoc(string docFilePath)
        {
            //string docContent = "";
            FinePerson model = new FinePerson();
            model.Rwards = new List<FinePersonRward>();
            using (FileStream fs = File.OpenRead(docFilePath))
            {
                XWPFDocument doc = new XWPFDocument(fs);

                var tables = doc.Tables;
                var contents = new StringBuilder();
                foreach (var table in tables)    //遍历表格
                {
                    int rowIndex = 0;
                    bool flag = true;                   
                    while(flag)//遍历行  
                    {
                        var row = table.GetRow(rowIndex);
                        var cells = row.GetTableCells();
                        switch (rowIndex)
                        {
                            case 0:
                                model.Name = GetTextFromCell(row.GetCell(1));
                                model.Sex = GetTextFromCell(row.GetCell(3));
                                model.Nation = GetTextFromCell(row.GetCell(5));
                                break;
                            case 1:
                                model.Birth = GetTextFromCell(row.GetCell(1));
                                model.NativePlace = GetTextFromCell(row.GetCell(3));
                                model.PoliticsFace = GetTextFromCell(row.GetCell(5));
                                break;
                            case 2:
                                model.HaveJobDate = GetTextFromCell(row.GetCell(1));
                                model.GraduateSchool = GetTextFromCell(row.GetCell(3));
                                model.Professional = GetTextFromCell(row.GetCell(5));
                                break;
                            case 3:
                                model.Education = GetTextFromCell(row.GetCell(1));
                                model.TechnologyTitle = GetTextFromCell(row.GetCell(3));
                                model.Health = GetTextFromCell(row.GetCell(5));
                                break;
                            case 4:
                                model.WorkUnitPost = GetTextFromCell(row.GetCell(1));
                                model.Telphone = GetTextFromCell(row.GetCell(3));
                                break;
                            case 5:
                                model.PartTimeJob = GetTextFromCell(row.GetCell(1),true);
                                break;
                            case 6:
                                model.MainResume = GetTextFromCell(row.GetCell(1),true);
                                break;
                            case 7:
                            case 8:
                                rowIndex++;
                                continue;
                            default:
                                if (row.GetTableCells().Count == 3)
                                {
                                    string text = GetTextFromCell(row.GetCell(0));
                                    if (!string.IsNullOrEmpty(text))
                                    {
                                        model.Rwards.Add(new FinePersonRward()
                                        {
                                            GrantTime = GetTextFromCell(row.GetCell(0)),
                                            GrantTitle = GetTextFromCell(row.GetCell(1)),
                                            GrantUnit = GetTextFromCell(row.GetCell(2))
                                        });
                                    }                                    
                                }else
                                {
                                    string text = GetTextFromCell(row.GetCell(0),true);
                                    if(text.Replace(" ", "") == "科研成果\r\n" || string.IsNullOrEmpty(text))
                                    {
                                        rowIndex++;
                                        continue;
                                    }else
                                    {
                                        model.Achievement = text;
                                        flag = false;
                                    }
                                }
                                break;
                        }
                        rowIndex++;
                    }
                }

                StringBuilder pics = new StringBuilder();
                foreach (var image in doc.AllPictures)
                {
                    string imageName = image.FileName;
                    int imageFormat = image.GetPictureType();
                    byte[] imageDatas = image.Data;
                    string path = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/finePersons/");
                    string filename = "img" + DateTime.Now.ToString("yyyyMMdd_") + Guid.NewGuid().ToString().Substring(0, 8);
                    if (pics.Length > 0)
                        pics.Append(",");
                    
                    string fullname = ImageHelper.CreateImageFromBytes(System.IO.Path.Combine(path, filename), imageDatas);
                    pics.Append(System.IO.Path.GetFileName(fullname));
                    //contents.AppendFormat("文件：{0}\r\n", System.IO.Path.GetFileName(fullname));
                }
                model.Pictures = pics.ToString();
                //docContent = contents.ToString();
                //Tools.WriteLog(docContent);
                return model;
            }
        }
    }
}
