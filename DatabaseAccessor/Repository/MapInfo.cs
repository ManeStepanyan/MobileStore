using DatabaseAccess.Repository;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DatabaseAccessor.Repository
{
    public class MapInfo
    {
        private string path;

        public Dictionary<string, string> OpNames
        {
            get; private set;

        }

        public Dictionary<string, ReturnDataType> ReturnValues
        {
            get; private set;
        }

        public Dictionary<string, List<KeyValuePair<string, string>>> Parameters
        {
            get; private set;
        }

        public MapInfo(string path)
        {
            this.path = path;

            this.GetMapInfo();
        }

        private void GetMapInfo()
        {
            var xml = XDocument.Load(this.path);

            var operations = xml.Elements("operation");

            var opNames = new Dictionary<string, string>();

            var returnValues = new Dictionary<string, ReturnDataType>();

            var parameters = new Dictionary<string, List<KeyValuePair<string, string>>>();

            foreach(var operation in operations)
            {
                var opName = operation.Attribute("name");

                var spName = operation.Element("spName");

                opNames.Add(opName.Value, spName.Value);

                var returnDataType = operation.Element("returnDataType");

                returnValues.Add(opName.Value, 
                    (ReturnDataType)Enum.Parse(typeof(ReturnDataType),returnDataType.Value));

                var paramsXML = operation.Element("parameters").Elements("parameter");

                var paramsList = new List<KeyValuePair<string, string>>();

                foreach (var parameter in paramsXML)
                {
                    var paramName = parameter.Element("parameterName");

                    var spParamName = parameter.Element("spParameterName");

                    paramsList.Add(new KeyValuePair<string, string>(
                        paramName.Value, spParamName.Value));
                }

                parameters.Add(opName.Value, paramsList);
            }

            this.OpNames = opNames;
            this.ReturnValues = returnValues;
            this.Parameters = parameters;
        }
    }
}