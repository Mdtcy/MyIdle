using System.Text;

namespace HM.EditorOnly
{
    public class GridCsvWriter : GridLogWriter
    {
        public override void FinishGrid()
        {
            System.IO.File.WriteAllText(_outputPath, Data, Encoding.UTF8);
        }
    }
}