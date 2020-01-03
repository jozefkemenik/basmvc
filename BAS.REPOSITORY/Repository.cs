using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace BAS.Repository
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected BAS.Repository.Model.Entities DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="context">the current context</param>
        public Repository(BAS.Repository.Model.Entities dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {

            //var elist = DbContext.ChangeTracker.Entries();
            //foreach (var item in elist)
            //{
              
            //    item.State = EntityState.Unchanged;
            //}


            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            
           
            //var result = DbSet.Attach(entity);
            //DbContext.Entry(entity).State = EntityState.Modified;
            //return result;

        }


        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public new Type GetType()
        {
            return typeof(T);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public void SaveAll()
        {
            try
            {
                DbContext.Configuration.ValidateOnSaveEnabled = false;
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }



















    //public class Repository<T> : IRepository<T>, IDisposable where T : class
    //{
    //    public Repository(DbContext dbContext)
    //    {
    //        if (dbContext == null)
    //            throw new ArgumentNullException("dbContext");
    //        DbContext = dbContext;
    //        DbSet = DbContext.Set<T>();

    //    }

    //    protected DbContext DbContext { get; set; }

    //    protected DbSet<T> DbSet { get; set; }

    //    public virtual void Add(T entity)
    //    {
    //        DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
    //        if (dbEntityEntry.State != EntityState.Detached)
    //        {
    //            dbEntityEntry.State = EntityState.Added;
    //        }
    //        else
    //        {
    //            DbSet.Add(entity);
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        DbContext.Dispose();
    //    }

    //}
}
