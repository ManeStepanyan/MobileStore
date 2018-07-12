using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace DatabaseAccess.SpExecuters
{
    /// <summary>
    /// Class for accessing data from database executing procedures.
    /// Works only with MS SQL server. 
    /// </summary>
    public class SpExecuter:ISpExecuter
    {
        /// <summary>
        /// SQL server connection string
        /// </summary>
        private readonly string _connString;

        /// <summary>
        /// Dictionary cached properties
        /// </summary>
        private readonly Dictionary<Type, PropertyInfo[]> _cachedProperties;

        /// <summary>
        /// Gets connection string
        /// </summary>
        public string ConnectionString => this._connString;

        /// <summary>
        /// Creates new instance of <see cref="SpExecuter"/>
        /// </summary>
        public SpExecuter() { }

        /// <summary>
        /// Creates new instance of <see cref="DatabaseAccess.SpExecuter"/> with the given connection string.
        /// </summary>
        /// <param name="connString"></param>
        public SpExecuter(string connString)
        {
            // sets connection string
            this._connString = connString;

            // initializes dictionary
            this._cachedProperties = new Dictionary<Type, PropertyInfo[]>();
        }

        /// <summary>
        /// Creates new instance of <see cref="SpExecuter"/>.
        /// </summary>
        /// <param name="dataSource">Data Source</param>
        /// <param name="initialCatalog">Initial catalog</param>
        /// <param name="integratedSecurity">Integrated Security</param>
        public SpExecuter(string dataSource,string initialCatalog,bool integratedSecurity)
        {
            this._connString = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                InitialCatalog = initialCatalog,
                IntegratedSecurity = integratedSecurity
            }.ConnectionString;

            this._cachedProperties = new Dictionary<Type, PropertyInfo[]>();
        }

        /// <summary>
        /// Creates new stored procedure executer.
        /// </summary>
        /// <param name="cnnString">Connection string</param>
        /// <returns>Stored procedure executer.</returns>
        public ISpExecuter Create(string cnnString)
        {
            return new SpExecuter(cnnString);
        }

        /// <summary>
        /// Executes store procedure which return data is enumerable.
        /// </summary>
        /// <typeparam name="TResult">Type of Result.</typeparam>
        /// <param name="procedureName">Proceduer name</param>
        /// <param name="parameters">Procedure parametes</param>
        /// <returns>Enumerable of rows</returns>
        public IEnumerable<TResult> ExecuteSp<TResult>(string procedureName,IEnumerable<KeyValuePair<string,object>> parameters = null)
        {
            // returning result
            return (IEnumerable<TResult>)this.Execute<TResult>(new StoredProcedure
            {
                Name = procedureName,
                StoredProcedureReturnData = StoredProcedureReturnData.Enumerable,
                Parameters = parameters
            });
        }

        /// <summary>
        /// Executes stored procedure which return data is one row.
        /// </summary>
        /// <typeparam name="TResult">Type of resutlt</typeparam>
        /// <param name="procedureName">Stored procedure name.</param>
        /// <param name="parameters">Stored proceduer parameters</param>
        /// <returns>Result which is one row in SQL table.</returns>
        public TResult ExecuteEntitySp<TResult>(string procedureName,IEnumerable<KeyValuePair<string,object>> parameters = null)
        {
            // returning result
            return (TResult)this.Execute<TResult>(new StoredProcedure
            {
                Name = procedureName,
                StoredProcedureReturnData = StoredProcedureReturnData.OneRow,
                Parameters = parameters
            });
        }

        /// <summary>
        /// Executes store procedure asynchronously which return data is enumerable.
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Enumerable of rows</returns>
        public Task<IEnumerable<TResult>> ExecuteSpAsync<TResult>(string procedureName,
                    IEnumerable<KeyValuePair<string,object>> parameters = null)
        {
            var task =  new Task<IEnumerable<TResult>>(() =>
                    this.ExecuteSp<TResult>(procedureName, parameters));

            task.Start();

            return task;
        }

        /// <summary>
        /// Executes store procedure which return data is scalar.
        /// </summary>
        /// <typeparam name="TResult">Type of Result</typeparam>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Procedure Parameters</param>
        /// <returns>Scalar result</returns>
        public TResult ExecuteScalarSp<TResult>(string procedureName,IEnumerable<KeyValuePair<string,object>> parameters = null)
        {
            // returning result
            return (TResult)this.Execute<TResult>(new StoredProcedure
            {
                Name = procedureName,
                StoredProcedureReturnData = StoredProcedureReturnData.Scalar,
                Parameters = parameters
            });
        }

        /// <summary>
        /// Executes store procedure which doesn't have return data.
        /// </summary>
        /// <param name="procedureName">Procedure name</param>
        /// <param name="parameters">Procedure parameters</param>
        /// <returns>Amount of affected rows</returns>
        public int ExecuteSpNonQuery(string procedureName,IEnumerable<KeyValuePair<string,object>> parameters = null)
        {
            // returning amount of affected rows
            return (int)this.Execute<object>(new StoredProcedure
            {
                Name = procedureName,
                StoredProcedureReturnData = StoredProcedureReturnData.Nothing,
                Parameters = parameters
            });
        }

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="storedProcedure">Stored procedure</param>
        /// <returns>Result of stored procedure execution</returns>
        private object Execute<TResult>(StoredProcedure storedProcedure)
        {
            // checking argument
            if(string.IsNullOrEmpty(storedProcedure.Name))
            {
                throw new ArgumentException("Procedure name");
            }

            // establishing sql database connection
            using (var sqlConnection = new SqlConnection(this._connString))
            {
                // constructing command
                var sqlCommand = this.ConstructCommand(sqlConnection, storedProcedure);

                // opening connection
                sqlConnection.Open();

                // executing stored procedures depending on their type
                if(storedProcedure.StoredProcedureReturnData == StoredProcedureReturnData.Enumerable)
                {
                    // list of results
                    var list = new List<TResult>();

                    // executing reader and retrieving data
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            list.Add(this.RetrieveEnumerableFromReader<TResult>(reader));
                        }
                    }

                    // returning list of results
                    return list;
                }
                else if(storedProcedure.StoredProcedureReturnData == StoredProcedureReturnData.OneRow)
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        reader.Read();
                        return this.RetrieveEnumerableFromReader<TResult>(reader);
                    }
                }
                else if(storedProcedure.StoredProcedureReturnData == StoredProcedureReturnData.Scalar)
                {
                    // returning scalar result
                    return sqlCommand.ExecuteScalar();
                }
                else
                {
                    // returning amount of affected rows after non-query stored procedure execution
                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Constructs Sql Command
        /// </summary>
        /// <param name="sqlConnetion">Sql Connection</param>
        /// <param name="storedProcedure">Stored procedure</param>
        /// <returns>Constructed command</returns>
        private SqlCommand ConstructCommand(SqlConnection sqlConnetion,StoredProcedure storedProcedure)
        {            
            // constructing command
            var sqlCommand = new SqlCommand
            {
                CommandText = storedProcedure.Name,
                Connection = sqlConnetion,
                CommandType = CommandType.StoredProcedure
            };

            // if there are parameters then we need to add them to the command
            if(storedProcedure.Parameters != null)
            {
                foreach(var parameter in storedProcedure.Parameters)
                {
                    sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }                    
            }

            // returning constructed command
            return sqlCommand;
        }

        /// <summary>
        /// Retrieves data from reader
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="reader">Reader</param>
        /// <returns>Result</returns>
        private TResult RetrieveEnumerableFromReader<TResult>(SqlDataReader reader)
        {
            // checking argument
            if(reader == null)
            {
                throw new ArgumentNullException("Reader");
            }

            // creating result instance
            var result = Activator.CreateInstance<TResult>();

            var properties = null as PropertyInfo[];

            var resultType = result.GetType();

            // getting properties
            if(!this._cachedProperties.ContainsKey(resultType))
            {
                properties = resultType.GetProperties();
                this._cachedProperties.Add(resultType, properties);
            }
            else
            {
                properties = this._cachedProperties[resultType];
            }

            // setting result object properties
            foreach(var property in properties)
            {
                property.SetValue(result, reader[property.Name]);
            }

            // returning result
            return result;
        }
    }
}
