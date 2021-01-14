using System;
using System.Data;
using System.Collections.Generic;

namespace FileReaderWriterForCSV
{
    /// <summary>CSV Reader</summary>
    public class FileReaderWriterForCSV : FileReaderWriter.FileReaderWriter
    {

        protected override string DirectoryPath { set; get; }
        protected override string FileName { set; get; }
        public FileReaderWriterForCSV(string directoryPath, string fileName)
        {
            DirectoryPath = directoryPath;
            FileName = fileName;
        }

        const string StringSplit = ",";

        static public DataTable CSV_StringToDataTable(bool bHasColumn,string csv)
        {
            DataTable DataTableResult = new DataTable();

            List<string> list = new List<string>();
            string[] DataLines = csv.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int DataLinesCount = DataLines.Length;
            if (DataLinesCount <= 0) return DataTableResult;

            int startIndex = 0;
            string[] cols = DataLines[0].Split(StringSplit.ToCharArray());
            int colCount = cols.Length;

            if (bHasColumn)
            {
                for (int i = 0; i < colCount; i++)
                {
                    DataTableResult.Columns.Add(cols[i]);
                }
                startIndex = 1;
            }
            else
            {
                for (int i = 0; i < colCount; i++)
                {
                    DataTableResult.Columns.Add(DataTableResult.Columns.Count.ToString());
                }
            }

            for (int i = startIndex; i < DataLinesCount; i++)
            {
                string[] dataArray = DataLines[i].Split(StringSplit.ToCharArray());
                int newColCount = dataArray.Length;
                int CurColCount = DataTableResult.Columns.Count;
                int ColDiffCount = newColCount - CurColCount;
                if (ColDiffCount > 0)
                {
                    for (int c = 0; c < ColDiffCount; c++)
                    {
                        DataTableResult.Columns.Add(DataTableResult.Columns.Count.ToString());
                    }
                }

                DataRow row = DataTableResult.NewRow();
                row.ItemArray = dataArray;

                DataTableResult.Rows.Add(row);

                if (i > 33)
                {
                }
            }


            return DataTableResult;
        }

        protected DataTable Load(bool bHasColumn)
        {
            DataTable DataTableResult = new DataTable();

            string tmp = base.Read();

            DataTableResult = CSV_StringToDataTable(bHasColumn, tmp);

            return DataTableResult;
        }

    }
}
