////using System;
////using OfficeOpenXml;

////namespace OLT.EPPlus
////{
////    /// <summary>
////    /// new ExcelRowFormula("4A: Time to Permanent Placement", "'Performance Worksheet'!AI2")
////    /// </summary>
////    public class OltExcelRowFormula : IOltExcelRowWriter
////    {

////        public OltExcelRowFormula(string label, string formula)
////        {
////            Label = label;
////            Formula = formula;
////        }


////        public string Label { get; set; }
////        public string Formula { get; set; }

////        public OltExcelCellStyle Style { get; set; } = new OltExcelCellStyle();

////        /// <summary>
////        /// writes row at given index and increments 
////        /// </summary>
////        /// <param name="worksheet"></param>
////        /// <param name="row"></param>
////        /// <returns>Next Row</returns>
////        public int Write(ExcelWorksheet worksheet, int row)
////        {
////            using (var r = worksheet.Cells[$"A{row}:F{row}"])
////            {
////                r.Style.Indent = 3;
////                r.Value = Label;
////                r.Merge = true;
////                r.Style.WrapText = true;
////            }



////            if (!Formula.IsEmpty())
////            {
////                worksheet.Cells[$"G{row}"].Formula = Formula;
////            }

////            return row + 1;
////        }
////    }
////}