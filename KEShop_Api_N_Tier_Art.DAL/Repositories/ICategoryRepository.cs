using KEShop_Api_N_Tier_Art.DAL.Models;

namespace KEShop_Api_N_Tier_Art.DAL.Repositories
{
    public interface ICategoryRepository
    {
        // هون بنعمل انترفيس للكاتيجوري ريبوزيتوري


        // For example, you might have methods like GetById, GetAll, Add, Update, Delete, etc.
        // على سبيل المثال، يمكنك أن يكون لديك طرق مثل GetById و GetAll و Add و Update و Delete، إلخ.
        int Add(Category category);
        IEnumerable<Category> GetAll( bool withTracking= false);
        Category? GetById(int id);
        int Remove(Category category);
        int Update(Category category);

       








    }
}