using System.Text;

namespace HM.EditorOnly
{
    public class GridLogWriter : IGridWriter
    {
        private StringBuilder _sb = new StringBuilder();

        private static readonly string _lb = "[LB]";

        protected string _outputPath;

        protected string Data => _sb.ToString();
        public virtual void NewGrid(string path)
        {
            _sb.Clear();
            _outputPath = path;
        }
        public virtual void NewRow()
        {
            if (_sb.Length <= 0)
            {
                return;
            }
            _sb.AppendLine();
        }

        public virtual void AppendCell(string value)
        {
            _sb.Append(value.Replace("\n", _lb).Replace("\r", ""));
            _sb.Append(",");
        }

        public virtual void FinishGrid()
        {
            HMLog.LogDebug(_sb.ToString());
        }
    }
}