using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Generic;
using DatabaseAccess.SpExecuters;
using Microsoft.Extensions.Configuration;

namespace DatabaseAccess.Repository
{/// <summary>
 /// Repository class
 /// </summary>
 /// <typeparam name="TResult">Type of result.</typeparam>
    public class Repo<TResult>
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _cnnString;

        /// <summary>
        /// Map setting info
        /// </summary>
        private readonly MapInfo _mapInfo;

        /// <summary>
        /// Stored procedure executer
        /// </summary>
        private readonly ISpExecuter _spExecuter;

        /// <summary>
        /// Creates new instance of <see cref="Repo{TResult}"/>
        /// </summary>
        /// <param name="mapInfo">mapping information</param>
        /// <param name="spExecuter">stored procedure executer</param>
        public Repo(MapInfo mapInfo, ISpExecuter spExecuter)
        {
            // setting fields
            this._mapInfo = mapInfo;
            this._spExecuter = spExecuter;
        }

        /// <summary>
        /// Executes opeation.
        /// </summary>
        /// <param name="opName">Operation name</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>result</returns>
        public object ExecuteOperation(string opName, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            // getting operation info
            var operationInfo = this.GetOperationInfo(opName);

            // getting parameters
            var spParams = null as IEnumerable<KeyValuePair<string, object>>;

            if (parameters != null)
                spParams = this.ConstructParameters(operationInfo.ParametersMappInfo, parameters);
            else spParams = parameters;

            // executing specific operation
            if (operationInfo.ReturnDataType == ReturnDataType.Entity)
                return this._spExecuter.ExecuteEntitySp<TResult>(operationInfo.SpName, spParams);
            else if (operationInfo.ReturnDataType == ReturnDataType.Enumerable)
                return this._spExecuter.ExecuteSp<TResult>(operationInfo.SpName, spParams);
            else if (operationInfo.ReturnDataType == ReturnDataType.Scalar)
                return this._spExecuter.ExecuteScalarSp<object>(operationInfo.SpName, spParams);
            else
                return this._spExecuter.ExecuteSpNonQuery(operationInfo.SpName, spParams);
        }

        /// <summary>
        /// Executes operation.
        /// </summary>
        /// <param name="opName">Operation name.</param>
        /// <param name="entity">Entity</param>
        /// <returns>result</returns>
        public object ExecuteOperation(string opName, TResult entity)
        {
            var parameters = this.GetParameters(entity);

            return this.ExecuteOperation(opName, parameters);
        }

        /// <summary>
        /// Gets operation info from map information
        /// </summary>
        /// <param name="operationName">Operation name</param>
        /// <returns>operation info</returns>
        private OperationInfo GetOperationInfo(string operationName)
        {
            return new OperationInfo
            {
                Name = operationName,
                SpName = this._mapInfo.OpNames[operationName],
                ReturnDataType = this._mapInfo.ReturnValues[operationName],
                ParametersMappInfo = this._mapInfo.Parameters[operationName]
            };
        }

        /// <summary>
        /// Constructs parameters.
        /// </summary>
        /// <param name="mapInfo">Mao info</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>parameters</returns>
        private IEnumerable<KeyValuePair<string, object>> ConstructParameters(
            Dictionary<string, string> mapInfo, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return parameters.Select(kv =>
                    new KeyValuePair<string, object>(mapInfo[kv.Key], kv.Value));
        }

        /// <summary>
        /// Gets operation parameters.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>parameters</returns>
        private IEnumerable<KeyValuePair<string, object>> GetParameters(TResult entity)
        {
            var properties = entity.GetType().GetProperties();

            return properties.Select(property =>
                    KeyValuePair.Create(property.Name, property.GetValue(entity)));
        }
    }
}