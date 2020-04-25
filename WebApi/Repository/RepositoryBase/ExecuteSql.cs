﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace RepositoryFactory.RepositoryBase
{
	/// <summary>
	/// 执行Sql/存储过程返回实体列表方法
	/// </summary>
	public static class ExecuteSql
	{
		private static DbCommand CreateCommand(DatabaseFacade facade, string sql, out DbConnection connection, params object[] parameters)
		{
			try
			{
				var conn = facade.GetDbConnection();
				connection = conn;
				conn.Open();
				var cmd = conn.CreateCommand();
				if (facade.IsSqlServer())
				{
					cmd.CommandText = sql;
					if (parameters != null)
					{
						cmd.Parameters.AddRange(parameters);
					}
				}
				return cmd;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable SqlQuery(this DatabaseFacade facade, string sql, params object[] parameters)
		{
			try
			{
				var command = CreateCommand(facade, sql, out DbConnection conn, parameters);
				var reader = command.ExecuteReader();
				var dt = new DataTable();
				dt.Load(reader);
				reader.Close();
				conn.Close();
				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 执行Sql实体集合
		/// </summary>
		/// <typeparam name="TEntity">实体</typeparam>
		/// <param name="facade">DatabaseFacade(dbContext)</param>
		/// <param name="sql">Sql语句</param>
		/// <param name="parameters">输入参数</param>
		/// <returns></returns>
		public static List<TEntity> SqlQuery<TEntity>(this DatabaseFacade facade, string sql, params object[] parameters) where TEntity : class, new()
		{
			DataTable dt = SqlQuery(facade, sql, parameters);
			return ToList<TEntity>(dt);
		}

		public static List<TEntity> ToList<TEntity>(DataTable dt) where TEntity : class, new()
		{
			var propertyInfos = typeof(TEntity).GetProperties();
			var list = new List<TEntity>();
			foreach (DataRow row in dt.Rows)
			{
				var t = new TEntity();
				foreach (PropertyInfo p in propertyInfos)
				{
					if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
						p.SetValue(t, row[p.Name], null);
				}
				list.Add(t);
			}
			return list;
		}
	}
}