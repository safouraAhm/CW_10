namespace HW.InterFaces;

public interface IGeneralRepository<T>
{
    void Add(T item);
    List<T> GetAll();
    T GetById(int id);
    bool Remove(int id);
}
