using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTime.Utils.Repositories
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    /// <typeparam name="T">Тип модели</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Добавить объект
        /// </summary>
        /// <param name="item">Добавляемый объект</param>
        /// <returns>Добавленный объект</returns>
        T Add(T item);

        /// <summary>
        /// Обновить объект
        /// </summary>
        /// <param name="item">Обновляемый объект</param>
        /// <returns>Обновленный объект</returns>
        T Update(T item);

        /// <summary>
        /// Удалить объект
        /// </summary>
        /// <param name="item">Удаляемый объек</param>
        void Delete(T item);

        /// <summary>
        /// Удалить объект по Id
        /// </summary>
        /// <param name="id">Id объекта</param>
        void Delete(int id);

        /// <summary>
        /// Получить всю коллекцию объекты
        /// </summary>
        /// <returns>Коллекция объектов</returns>
        IQueryable<T> Get();

        /// <summary>
        /// Получить объект по Id
        /// </summary>
        /// <param name="id">Id объекта</param>
        /// <returns>Объект или null, если не найден</returns>
        T Find(int id);
    }
}
