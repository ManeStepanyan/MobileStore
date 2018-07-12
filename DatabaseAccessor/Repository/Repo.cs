using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Generic;
using DatabaseAccess.SpExecuters;
using Microsoft.Extensions.Configuration;

namespace DatabaseAccess.Repository
{
    /// <summary>
    /// Repository class
    /// </summary>
    /// <typeparam name="TResult">Type of result.</typeparam>
    /// <typeparam name="TSpExecuter">Type of stored procedure executer.</typeparam>
    public class Repo<TResult, TSpExecuter> where TSpExecuter : ISpExecuter, new()
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _cnnString;

        /// <summary>
        /// Map settings XML file path
        /// </summary>
        private readonly string _mapSettingsPath;

        /// <summary>
        /// Map setting document
        /// </summary>
        private readonly XDocument _mapInfo;

        /// <summary>
        /// Stored procedure executer
        /// </summary>
        private readonly ISpExecuter _spExecuter;
        
        /// <summary>
        /// Creates new instance of <see cref="Repo"/>.
        /// Make sure that connection string is written in appsettings.json file with the name "Default".
        /// </summary>
        /// <param name="mappSettingsPath">
        /// Path of map settings XML file.Make sure that this file is defined using MapSchema.xsd.
        /// You can download MapSchema.xsd from https://github.com/DanielyanAndranik/Recipe/tree/sev/DatabaseAccessor/MapSchema.xsd
        /// </param>
        public Repo(string mappSettingsPath)
        {
            // setting map settings Path
            this._mapSettingsPath = mappSettingsPath;

            // laoding map settings
            this._mapInfo = XDocument.Load(this._mapSettingsPath);

            // getting connection string
            this._cnnString = this.GetCnnString();

            // creating stored procedure executer
            this._spExecuter = new TSpExecuter().Create(this._cnnString);
        }

        /// <summary>
        /// Executes opeation.
        /// </summary>
        /// <param name="opName">Operation name</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>result</returns>
        public object ExecuteOperation(string opName,IEnumerable<KeyValuePair<string,object>> parameters = null)
        {
            // getting operation info
            var operationInfo = this.GetOperationInfo(opName);

            // getting parameters
            var spParams = null as IEnumerable<KeyValuePair<string, object>>;

            if (parameters != null)
                spParams = this.ConstructParameters(operationInfo.ParametersMappInfo, parameters);
            else spParams = parameters;

            // executing specific operation
            if(operationInfo.ReturnDataType == ReturnDataType.Entity)
                return this._spExecuter.ExecuteEntitySp<TResult>(operationInfo.SpName, spParams);
            else if(operationInfo.ReturnDataType == ReturnDataType.Enumerable)
                return this._spExecuter.ExecuteSp<TResult>(operationInfo.SpName, spParams);
            else if(operationInfo.ReturnDataType == ReturnDataType.Scalar)
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
        public object ExecuteOperation(string opName,TResult entity)
        {
            var parameters = this.GetParameters(entity);

            return this.ExecuteOperation(opName, parameters);
        }

        /// <summary>
        /// Gets connection string from appsettings.json
        /// </summary>
        /// <returns>Connection string.</returns>
        private string GetCnnString()
        {
            // building config
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

            var config = builder.Build();

            // returning connection string
            return config.GetConnectionString("Default");
        }

        /// <summary>
        /// Gets operation info from map setting XML file
        /// </summary>
        /// <param name="operationName">Operation name</param>
        /// <returns>operation info</returns>
        private OperationInfo GetOperationInfo(string operationName)
        {
            var operationInfo = new OperationInfo();

            // getting operation node
            var opXml = this._mapInfo.XPathSelectElement(
                $"//operation[@name='{operationName}']");

            // getting sp name and return data type
            var spName = opXml.Element("spName").Value;

            var returnDataType = (ReturnDataType)Enum.Parse(
                    typeof(ReturnDataType),
                    opXml.Element("returnDataType").Value);

            operationInfo.SpName = spName;
            operationInfo.ReturnDataType = returnDataType;

            // getting parameters node
            var paramsXml = opXml.Element("parameters");

            if (paramsXml != null)
            {
                // getting parameters
                var parameters = paramsXml.Elements("parameter").ToDictionary(
                    element => element.Element("parameterName").Value,
                    element => element.Element("spParameterName").Value);

                operationInfo.ParametersMappInfo = parameters;
            }
            else operationInfo.ParametersMappInfo = null;

            // returning operation info
            return operationInfo;
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
        private IEnumerable<KeyValuePair<string,object>> GetParameters(TResult entity)
        {
            var properties = entity.GetType().GetProperties();

            return properties.Select(property =>
                    KeyValuePair.Create(property.Name, property.GetValue(entity)));
        }
    }
}