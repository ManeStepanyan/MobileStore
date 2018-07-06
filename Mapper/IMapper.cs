namespace BusinessLayer
{
    /// <summary>
    /// Interface to provide mapping between classes (one to one mapping)
    /// </summary>
    /// <typeparam name="TS">Type of source</typeparam>
    /// <typeparam name="TD">Type of destionation</typeparam>
    public interface IMapper<TS, TD> where TS: new() where TD: new()
    {
        /// <summary>
        /// Map from source to destionation
        /// </summary>
        /// <param name="source">Source object</param>
        /// <returns></returns>
        TD Map(TS source);
        
        /// <summary>
        /// Map back 
        /// </summary>
        /// <param name="source">Souce object</param>
        /// <returns></returns>
        TS MapBack(TD source);
    }
}
