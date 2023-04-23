using DU0038.Model;

namespace DU0038.Service;

public class CategoryService
{
    private static CategoryService? _instance = null;
    private static readonly object Padlock = new object();
    private List<Category> _categories;

    public CategoryService()
    {
        _categories = FileService.Instance.ReadCategoriesFromFile();
    }
    
    public static CategoryService Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new CategoryService();
            }
        }
    }

    public void AddCategory(string name, bool isIncome)
    {
        _categories.Add(new Category
        (
            Guid.NewGuid().ToString(),
            name,
            isIncome
        ));
    }

    public void SaveCategories()
    {
        FileService.Instance.WriteCategoriesToFile(_categories);
    }

    public List<Category> GetCategories()
    {
        return _categories;
    }
}