using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using App.Data.Context;
using App.Data.Infrastructure;
using App.Data.Model;
using App.Data.Models;
using App.Data.Helpers;

namespace App.Data.Repositories
{
    public class AppRepository<T> : IAppRepository<T> where T : class
    {
        public Context.AppContext dbContext { get; private set; }
        protected IUnitOfWork<Context.AppContext> _unitOfWork;
        protected IConfiguration config;

        public AppRepository(IDbFactory<Context.AppContext> dbFactory, IUnitOfWork<Context.AppContext> unitOfWork)
        {
            dbContext = (Context.AppContext)dbFactory.Init();
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<T> GetAsync(long id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetAsync(Guid uid)
        {
            return await Task.Run(() => {
                PropertyInfo propIsDeleted = typeof(T).GetProperty(Constant.Field.IsDeleted, BindingFlags.Public | BindingFlags.Instance);
                if (null != propIsDeleted && propIsDeleted.CanWrite)
                {
                    Expression combined = null;
                    ParameterExpression argParam = Expression.Parameter(typeof(T));
                    Expression isDeleted = Expression.Property(argParam, Constant.Field.IsDeleted);
                    var val = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeleted, val);
                    combined = Expression.AndAlso(e1, e1);

                    PropertyInfo propUid = typeof(T).GetProperty(Constant.Field.Uid, BindingFlags.Public | BindingFlags.Instance);
                    if (null != propUid && propUid.CanWrite)
                    {
                        Expression nameUid = Expression.Property(argParam, Constant.Field.Uid);
                        var valUid = Expression.Constant(uid, typeof(Guid));
                        Expression e2 = Expression.Equal(nameUid, valUid);
                        combined = Expression.AndAlso(combined, e2);
                    }

                    var where = Expression.Lambda<Func<T, bool>>(combined, argParam);

                    return dbContext.Set<T>().Where(where).FirstOrDefault();
                }

                return null;
            });
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(long? schoolId = 0)
        {
            return await Task.Run(() => {
                PropertyInfo propIsDeleted = typeof(T).GetProperty(Constant.Field.IsDeleted, BindingFlags.Public | BindingFlags.Instance);
                if (null != propIsDeleted && propIsDeleted.CanWrite)
                {
                    Expression combined = null;
                    ParameterExpression argParam = Expression.Parameter(typeof(T));
                    Expression isDeleted = Expression.Property(argParam, Constant.Field.IsDeleted);
                    var val = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeleted, val);
                    combined = Expression.AndAlso(e1, e1);

                    var where = Expression.Lambda<Func<T, bool>>(combined, argParam);

                    return dbContext.Set<T>().Where(where);
                }

                return dbContext.Set<T>();
            });
        }

        public virtual async Task<IQueryable<T>> GetAllAsync(Filter filter)
        {
            return await Task.Run(() => {
                var skip = (filter.Page - 1) * filter.PageSize;

                PropertyInfo propIsDeleted = typeof(T).GetProperty(Constant.Field.IsDeleted, BindingFlags.Public | BindingFlags.Instance);
                if (null != propIsDeleted && propIsDeleted.CanWrite)
                {
                    Expression combined = null;
                    ParameterExpression argParam = Expression.Parameter(typeof(T));
                    Expression isDeleted = Expression.Property(argParam, Constant.Field.IsDeleted);
                    var value = Expression.Constant(false, typeof(bool));
                    Expression e1 = Expression.Equal(isDeleted, value);
                    combined = Expression.AndAlso(e1, e1);

                    PropertyInfo propName = typeof(T).GetProperty(Constant.Field.Name, BindingFlags.Public | BindingFlags.Instance);
                    if (null != propName && propName.CanWrite && !string.IsNullOrEmpty(filter.Query))
                    {
                        Expression name = Expression.Property(argParam, Constant.Field.Name);
                        MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var valName = Expression.Constant(filter.Query, typeof(string));
                        Expression e2 = Expression.Call(name, method, valName);
                        combined = Expression.AndAlso(combined, e2);
                    }

                    var where = Expression.Lambda<Func<T, bool>>(combined, argParam);

                    return dbContext.Set<T>().Where(where).Skip(skip).Take(filter.PageSize);
                }

                return dbContext.Set<T>().Skip(skip).Take(filter.PageSize);
            });
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            PropertyInfo propUid = typeof(T).GetProperty(Constant.Field.Uid, BindingFlags.Public | BindingFlags.Instance);
            if (null != propUid && propUid.CanWrite)
                propUid.SetValue(entity, Guid.NewGuid(), null);

            PropertyInfo propCreatedOn = typeof(T).GetProperty(Constant.Field.CreatedOn, BindingFlags.Public | BindingFlags.Instance);
            if (null != propCreatedOn && propCreatedOn.CanWrite)
                propCreatedOn.SetValue(entity, Helper.GetCurrentDateTime(), null);

            dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> AddWithoutSaveAsync(T entity)
        {
            return await Task.Run(() => {
                PropertyInfo propUid = typeof(T).GetProperty(Constant.Field.Uid, BindingFlags.Public | BindingFlags.Instance);
                if (null != propUid && propUid.CanWrite)
                    propUid.SetValue(entity, Guid.NewGuid(), null);

                PropertyInfo propCreatedOn = typeof(T).GetProperty(Constant.Field.CreatedOn, BindingFlags.Public | BindingFlags.Instance);
                if (null != propCreatedOn && propCreatedOn.CanWrite)
                    propCreatedOn.SetValue(entity, Helper.GetCurrentDateTime(), null);

                dbContext.Set<T>().Add(entity);
                return entity;
            });
        }

        public virtual async Task<T> UpdateAsync(T updated, long key)
        {
            if (updated == null)
                return null;

            T existing = await dbContext.Set<T>().FindAsync(key);
            if (existing != null)
            {
                PropertyInfo propUpdatedOn = typeof(T).GetProperty(Constant.Field.UpdatedOn, BindingFlags.Public | BindingFlags.Instance);
                if (null != propUpdatedOn && propUpdatedOn.CanWrite)
                    propUpdatedOn.SetValue(updated, Helper.GetCurrentDateTime(), null);

                dbContext.Entry(existing).CurrentValues.SetValues(updated);
                await dbContext.SaveChangesAsync();
            }
            return existing;
        }

        public virtual async Task<T> UpdateWithoutSaveAsync(T updated, long key)
        {
            if (updated == null)
                return null;

            T existing = await dbContext.Set<T>().FindAsync(key);
            if (existing != null)
            {
                PropertyInfo propUpdatedOn = typeof(T).GetProperty(Constant.Field.UpdatedOn, BindingFlags.Public | BindingFlags.Instance);
                if (null != propUpdatedOn && propUpdatedOn.CanWrite)
                    propUpdatedOn.SetValue(updated, Helper.GetCurrentDateTime(), null);

                dbContext.Entry(existing).CurrentValues.SetValues(updated);
            }
            return existing;
        }

        public virtual async Task<T> UpdateAsync(T updated, int key)
        {
            if (updated == null)
                return null;

            T existing = await dbContext.Set<T>().FindAsync(key);
            if (existing != null)
            {
                dbContext.Entry(existing).CurrentValues.SetValues(updated);
                await dbContext.SaveChangesAsync();
            }
            return existing;
        }

        public virtual async Task<T> UpdateAsync(T updated)
        {
            if (updated == null)
                return null;

            dbContext.Attach(updated);
            dbContext.Entry(updated).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return updated;
        }

        public virtual async Task<IQueryable<T>> Where(Expression<Func<T, bool>> where)
        {
            return await Task.Run(() => {
                return dbContext.Set<T>().Where(where);
            });
        }

        public virtual async Task<IQueryable<T>> Where(Expression<Func<T, bool>> where, int skip, int take)
        {
            return await Task.Run(() => {
                return dbContext.Set<T>().Where(where).Skip(skip).Take(take);
            });
        }

        public virtual async Task<long> DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<long> DeleteAsync(T entity, long id)
        {
            PropertyInfo propDeletedOn = entity.GetType().GetProperty(Constant.Field.DeletedOn, BindingFlags.Public | BindingFlags.Instance);
            if (null != propDeletedOn && propDeletedOn.CanWrite)
                propDeletedOn.SetValue(entity, Helper.GetCurrentDateTime(), null);

            PropertyInfo propIsDeleted = entity.GetType().GetProperty(Constant.Field.IsDeleted, BindingFlags.Public | BindingFlags.Instance);
            if (null != propIsDeleted && propIsDeleted.CanWrite)
            {
                propIsDeleted.SetValue(entity, true, null);
                await UpdateAsync(entity, id);
                return id;
            }

            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(T entity, int id)
        {
            PropertyInfo propDeletedOn = entity.GetType().GetProperty(Constant.Field.DeletedOn, BindingFlags.Public | BindingFlags.Instance);
            if (null != propDeletedOn && propDeletedOn.CanWrite)
                propDeletedOn.SetValue(entity, Helper.GetCurrentDateTime(), null);

            PropertyInfo propIsDeleted = entity.GetType().GetProperty(Constant.Field.IsDeleted, BindingFlags.Public | BindingFlags.Instance);
            if (null != propIsDeleted && propIsDeleted.CanWrite)
            {
                propIsDeleted.SetValue(entity, true, null);
                await UpdateAsync(entity, id);
                return id;
            }

            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<long> DeleteAsync(Expression<Func<T, bool>> where)
        {
            var entities = dbContext.Set<T>().Where(where);
            dbContext.Set<T>().RemoveRange(entities);
            return await dbContext.SaveChangesAsync();
        }

        public virtual IEnumerable<TResult> ExecuteSP<TResult>(string query, params SqlParameter[] SqlPrms) where TResult : new()
        {
            DataSet ds = new DataSet();
            string connectionString = dbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddRange(SqlPrms);
                    command.CommandTimeout = 0;
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        try
                        {
                            dataAdapter.SelectCommand = command;
                            dataAdapter.Fill(ds);
                        }
                        catch (Exception)
                        {

                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }

            return Helper.CreateListFromTable<TResult>(ds.Tables[0]);
        }

        public virtual async Task TruncateAsync(String TableName)
        {
            await dbContext.Database.ExecuteSqlRawAsync("Truncate Table " + TableName);
        }

        public virtual async Task SaveAsync()
        {
            dbContext.SaveChanges();
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task BeginTransaction()
        {
            await _unitOfWork.BeginTransaction();
        }

        public virtual async Task Rollback()
        {
            await _unitOfWork.Rollback();
        }

        public virtual async Task CommitAsync()
        {
            await _unitOfWork.CommitAsync();
        }
    }
}
