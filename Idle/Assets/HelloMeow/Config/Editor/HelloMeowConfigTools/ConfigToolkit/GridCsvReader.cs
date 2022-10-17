using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HM.EditorOnly
{
    public class GridCsvReader : IGridReader
    {
        private List<string> _rows;

        private int _curRowNo;

        private string LineBreak
        {
            get
            {
    #if UNITY_STANDALONE_OSX
                return "\r";
    #else
                return "\n";
    #endif
            }
        }
        public void Load(string path)
        {
            _rows = File.ReadAllText(path).Split(LineBreak.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            _curRowNo = 0;
        }

        public int Count()
        {
            return _rows.Count;
        }

        public bool HasNext()
        {
            return _curRowNo < Count();
        }

        public List<string> NextRow()
        {
            return _rows[_curRowNo++].Split(",".ToCharArray()).ToList();
        }
    }
}