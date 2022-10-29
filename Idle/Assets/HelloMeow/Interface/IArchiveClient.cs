namespace HM.Interface
{
    public interface IArchiveClient
    {
        void OnArchiveWillLoad(IArchive archive);
        void OnArchiveWillSave(IArchive archive);
    }
}