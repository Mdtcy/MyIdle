namespace HM.EditorOnly
{
    public interface IGridWriter
    {
        void NewGrid(string path);
        void NewRow();
        void AppendCell(string value);
        void FinishGrid();
    }
}