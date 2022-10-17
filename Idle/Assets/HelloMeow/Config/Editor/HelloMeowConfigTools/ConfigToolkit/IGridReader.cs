using System.Collections.Generic;

namespace HM.EditorOnly
{
    public interface IGridReader
    {
        void Load(string path);
        int Count();
        bool HasNext();
        List<string> NextRow();
    }
}