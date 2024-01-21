using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace OpenStudioIDE
{
    partial class OpenStudioDataDataSet
    {
        partial class ideDataTable
        {
            public void AddRow(string filename, string path, string location, DateTime lastmodified, string extention, bool? latest)
            {
                ideRow row = (ideRow)NewRow();
                row.BeginEdit();
                row["filename"] = filename;
                row["location"] = path;
                row["lastmodified"] = lastmodified;
                row["extention"] = extention;
                row["latest?"] = latest;
                row.EndEdit();

            }
            public void RemoveRow(string filename)
            {
                foreach (ideRow row in this.Rows)
                {
                    if (row["filename"].ToString() == filename)
                    {
                        row.Delete();
                        break;
                    }
                }
            }
            public void UpdateRow(string filename, string path, string location, DateTime lastmodified, string extention, bool? latest)
            {
                foreach (ideRow row in this.Rows)
                {
                    if (row["filename"].ToString() == filename)
                    {
                        row.BeginEdit();
                        row["filename"] = filename;
                        row["location"] = path;
                        row["lastmodified"] = lastmodified;
                        row["extention"] = extention;
                        row["latest?"] = latest;
                        row.EndEdit();
                        break;
                    }
                }
            }
            public void DumpDBToxml(string filename)
            {
                this.WriteXml(filename);
            }
            public void LoadDBFromxml(string filename)
            {
                this.ReadXml(filename);
            }
            public void ExportDBToCSV(string filename)
            {
                using (var writer = new StreamWriter(filename))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(this);
                }
            }   
        }
    }
}
