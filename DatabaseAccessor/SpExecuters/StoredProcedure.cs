using System.Collections.Generic;

namespace DatabaseAccess.SpExecuters
{
    /// <summary>
    /// Class for stored procedures
    /// </summary>
    internal class StoredProcedure
    {
        /// <summary>
        /// Gets os sets name of stored procedure
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the return data of stored procedure
        /// </summary>
        public StoredProcedureReturnData StoredProcedureReturnData { get; set; }

        /// <summary>
        /// Gets or sets parameters of stored procedure
        /// </summary>
        public IEnumerable<KeyValuePair<string,object>> Parameters { get; set; }
    }
}
