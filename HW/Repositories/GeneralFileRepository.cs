using HW.Entities;
using HW.InterFaces;
using Newtonsoft.Json;
using System.Reflection;

namespace HW.Repositories;

public class GeneralFileRepository<T> : IGeneralRepository<T>
{
    readonly string _path;
    readonly PropertyInfo? _propertyInfo;
    public GeneralFileRepository(string? path = null)
    {
        _propertyInfo = typeof(T).GetProperty("Id");
        if (path == null)
        {
            if (!Directory.Exists("DataBase"))
                Directory.CreateDirectory("DataBase");
            _path = $"DataBase/{typeof(T).Name}.txt";
            if (!File.Exists(_path))
                File.Create(_path);
        }
        else
        {
            _path = path;
        }

    }
    public List<T> GetAll()
    {
        string txt = File.ReadAllText(_path);
        var items = JsonConvert.DeserializeObject<List<T>>(txt);
        return items ?? [];
    }

    public void Add(T item)
    {
        var items = GetAll();
        items.Add(item);
        UpdateList(items);
    }

    public T GetById(int id)
    {
        var items = GetAll();
        T? item = items.First(i => Convert.ToInt32(_propertyInfo.GetValue(i)) == id);
        return item;
    }

    public bool Remove(int id)
    {
        var items = GetAll();
        try
        {
            var ramItem = GetById(id);
            items.Remove(ramItem);
            UpdateList(items);
            return true;
        }
        catch(Exception)
        {
            return false;
        }
        
        
    }
    public void UpdateList(List<T> items)
    {
        string txt = JsonConvert.SerializeObject(items);
        File.WriteAllText(_path, txt);
    }
}
