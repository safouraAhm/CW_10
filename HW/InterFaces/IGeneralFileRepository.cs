namespace HW.InterFaces;

public interface IGeneralFileRepository<T>
{
    void Add(T item);
    List<T> GetAll();
    T GetById(int id);
    bool Remove(int id);
}
