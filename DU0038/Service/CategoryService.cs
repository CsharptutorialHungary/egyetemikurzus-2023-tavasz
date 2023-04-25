using DU0038.Model;

namespace DU0038.Service;

public class CategoryService
{
    private static CategoryService? _instance;
    private static readonly object Padlock = new();
    private List<Category> _categories;

    private CategoryService()
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
    
    public List<string> GetCategoriesNameInOrder()
    {
        return _categories.Select(category => category.Name)
            .OrderBy(name => name).ToList();
    }
}