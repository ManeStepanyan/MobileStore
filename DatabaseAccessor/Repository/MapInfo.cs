using DatabaseAccess.Repository;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DatabaseAccessor.Repository
{
    /// <summary>
    /// Class for mappinng information
    /// </summary>
    public class MapInfo
    {
        /// <summary>
        /// Mapping info file path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Gets or sets operations names
        /// </summary>
        public Dictionary<string, string> OpNames
        {
            get; private set;

        }

        /// <summary>
        /// Gets or sets return values
        /// </summary>
        public Dictionary<string, ReturnDataType> ReturnValues
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets parameters
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Parameters
        {
            get; private set;
        }

        /// <summary>
        /// Creates new instance of <see cref="MapInfo"/>
        /// </summary>
        /// <param name="path"></param>
        public MapInfo(string path)
        {
            this._path = path;

            this.GetMapInfo();
        }

        /// <summary>
        /// Gets mapping information from file
        /// </summary>
        private void GetMapInfo()
        {
            // getting xml document
            var xml = XDocument.Load(this._path);

            // getting operations
            var operations = xml.Element("operations").Elements("operation");

            // variables for storing
            var opNames = new Dictionary<string, string>();
            var returnValues = new Dictionary<string, ReturnDataType>();
            var parameters = new Dictionary<string, Dictionary<string, string>>();

            // loop over operations
            foreach (var operation in operations)
            {
                // getting operation name
                var opName = operation.Attribute("name");

                // getting stored procedure name
                var spName = operation.Element("spName");

                // adding names
                opNames.Add(opName.Value, spName.Value);

                // getting return values and adding
                var returnDataType = operation.Element("returnDataType");

                returnValues.Add(opName.Value,
                    (ReturnDataType)Enum.Parse(typeof(ReturnDataType), returnDataType.Value));

                // getting parameters xml element
                var paramsXML = operation.Element("parameters");

                // getting parameters if they exist
                if (paramsXML != null)
                {
                    var paramsList = new Dictionary<string, string>();

                    // loop over parameters
                    foreach (var parameter in paramsXML.Elements("parameter"))
                    {
                        var paramName = parameter.Element("parameterName");

                        var spParamName = parameter.Element("spParameterName");

                        paramsList.Add(paramName.Value, spParamName.Value);
                    }

                    // add parameters
                    parameters.Add(opName.Value, paramsList);
                }
                // otherwise add null indicating that this operation doesn't have parameters
                else parameters.Add(opName.Value, null);
            }

            // setting properties
            this.OpNames = opNames;
            this.ReturnValues = returnValues;
            this.Parameters = parameters;
        }
    }
}