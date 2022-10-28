namespace HM.Interface
{
    public interface IVisitor<T>
    {
        void Visit(T target);
    }
}