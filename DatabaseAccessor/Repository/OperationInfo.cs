using System.Collections.Generic;

namespace DatabaseAccess.Repository
{
    /// <summary>
    /// Class for operation information
    /// </summary>
    internal class OperationInfo
    {
        /// <summary>
        /// Gets or sets Operation name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Stored procedur name
        /// </summary>
        public string SpName { get; set; }

        /// <summary>
        /// Gets or sets return data type
        /// </summary>
        public ReturnDataType ReturnDataType { get; set; }

        /// <summary>
        /// Gets or sets Parameters names map info
        /// </summary>
        public Dictionary<string,string> ParametersMappInfo { get; set; }
    }
}
