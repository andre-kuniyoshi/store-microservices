using Core.Common;
using Dapper;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Infra.Data.Context;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Inventory.Infra.Data.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseControlEntity
    {
        protected readonly IDbContext Context;
        protected readonly string TableName;
        private IEnumerable<PropertyInfo> GetProperties => typeof(TEntity).GetProperties();

        public GenericRepository(IDbContext context, string tableName)
        {
            Context = context;
            TableName = tableName;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Connection.QueryAsync<TEntity>($"SELECT * FROM {TableName} WHERE Active=@Active AND Edited=@Edited",
                new { Active = true, Edited = false }, Context.Transaction ?? null);
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            return await Context.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT * FROM {TableName} WHERE {GetGuidColumnName(GetProperties)}=@Id AND Active=@Active",
                new { Id = id, Active = true, Edited = false }, Context.Transaction ?? null);
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            var query = GenerateInsertQuery();

            return await Context.Connection.ExecuteAsync(query, entity, Context.Transaction ?? null) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Context.Connection.ExecuteAsync($"DELETE FROM {TableName} WHERE {GetGuidColumnName(GetProperties)}=@Id",
                new { Id = id }, Context.Transaction ?? null) > 0;
        }

        public async Task<bool> DeleteLogicAsync(Guid id)
        {
            return await Context.Connection.ExecuteAsync($"UPDATE {TableName} SET Active=@Active WHERE {GetGuidColumnName(GetProperties)}=@Id",
                new { Id = id, Active = false }, Context.Transaction ?? null) > 0;
        }

        public async Task<bool> Exist(Guid id)
        {
            return await Context.Connection.QuerySingleOrDefaultAsync<TEntity>($"SELECT {GetGuidColumnName(GetProperties)} FROM {TableName} WHERE {GetGuidColumnName(GetProperties)}=@Id",
                new { Id = id }, Context.Transaction ?? null) != null;
        }

        public async Task<bool> UpdateAsync(TEntity newEntity, Guid id)
        {
            var query = GenerateUpdateQuery(id);
            return await Context.Connection.ExecuteAsync(query, newEntity, Context.Transaction ?? null) > 0;
        }

        public async Task<bool> UpdateAsync(TEntity newEntity)
        {
            var query = GenerateUpdateQuery(newEntity.Id);
            return await Context.Connection.ExecuteAsync(query, newEntity, Context.Transaction ?? null) > 0;
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {TableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);

            var propertiesAttr = GenerateListOfPropertiesWithAttr(GetProperties);

            propertiesAttr.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");

            return insertQuery.ToString();
        }

        private string GenerateUpdateQuery(Guid? id)
        {
            var updateQuery = new StringBuilder($"UPDATE {TableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append($" WHERE {GetGuidColumnName(GetProperties)}=");

            if (id != null)
                updateQuery.Append($"'{id}'");
            else
                updateQuery.Append("@Id");

            return updateQuery.ToString();
        }

        private string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var attribute = (ColumnAttribute)Attribute.GetCustomAttribute(member, typeof(ColumnAttribute), false);
            return (attribute?.Name ?? member.Name).ToLower();
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties

                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)

                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"

                    select prop.Name

            ).ToList();
        }

        private static List<string> GenerateListOfPropertiesWithAttr(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties

                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)

                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"

                    let columnAttr = prop.GetCustomAttributes(typeof(ColumnAttribute), false)

                    select (columnAttr.Length <= 0 || (columnAttr[0] as ColumnAttribute)?.Name == null) ? prop.Name : (columnAttr[0] as ColumnAttribute)?.Name
            ).ToList();
        }

        private static string GetGuidColumnName(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties

                    let keyAttr = prop.GetCustomAttributes(typeof(KeyAttribute), false)

                    let columnAttr = prop.GetCustomAttributes(typeof(ColumnAttribute), false)

                    where columnAttr.Length > 0 && (columnAttr[0] as ColumnAttribute)?.Name != null && keyAttr != null

                    select (columnAttr[0] as ColumnAttribute)?.Name
            ).First();
        }
    }
}
