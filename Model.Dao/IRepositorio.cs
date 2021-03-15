using System.Collections.Generic;

namespace Model.Dao
{
    public interface IRepositorio<TEntity>
    {
        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        List<TEntity> FindAll();

        bool Find(TEntity entity);
    }
}
