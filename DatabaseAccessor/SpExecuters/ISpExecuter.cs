using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseAccess.SpExecuters
{
    /// <summary>
    /// Interface for accessing database
    /// </summary>
    public interface ISpExecuter
    {
        /// <summary>
        /// Creates new stored procedure executer
        /// </summary>
        /// <param name="cnnString">Connection string</param>
        ISpExecuter Create(string cnnString);

        /// <summary>
        /// Executes store procedure which return data is enumerable.
        /// </summary>
        /// <typeparam name="TResult">Type of Result.</typeparam>
        /// <param name="procedureName">Proceduer name</param>
        /// <param name="parameters">Procedure parametes</param>
        /// <returns>Enumerable of rows</returns>
        IEnumerable<TResult> ExecuteSp<TResult>(string procedureName, IEnumerable<KeyValuePair<string, object>> parameters = null)
            where TResult : class;

        /// <summary>
        /// Executes stored procedure which return data is one row.
        /// </summary>
        /// <typeparam name="TResult">Type of resutlt</typeparam>
        /// <param name="procedureName">Stored procedure name.</param>
        /// <param name="parameters">Stored proceduer parameters</param>
        /// <returns>Result which is one row in SQL table.</returns>
        TResult ExecuteEntitySp<TResult>(string procedureName, IEnumerable<KeyValuePair<string, object>> parameters = null)
            where TResult : class;

        /// <summary>
        /// Executes store procedure asynchronously which return data is enumerable.
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Enumerable of rows</returns>
        Task<IEnumerable<TResult>> ExecuteSpAsync<TResult>(string procedureName,
                    IEnumerable<KeyValuePair<string, object>> parameters = null) where TResult : class;

        /// <summary>
        /// Executes store procedure which return data is scalar.
        /// </summary>
        /// <typeparam name="TResult">Type of Result</typeparam>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Procedure Parameters</param>
        /// <returns>Scalar result</returns>
        TResult ExecuteScalarSp<TResult>(string procedureName, IEnumerable<KeyValuePair<string, object>> parameters = null)
            where TResult : class;

        /// <summary>
        /// Executes store procedure which doesn't have return data.
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Procedure parameters</param>
        /// <returns>Amount of affected rows</returns>
        int ExecuteSpNonQuery(string procedureName, IEnumerable<KeyValuePair<string, object>> parameters = null);
    }
}