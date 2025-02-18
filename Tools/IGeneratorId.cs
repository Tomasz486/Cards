namespace Cards
{
    /// <summary>
    ///     The ID generator interface.
    /// </summary>
    public interface IGeneratorId
    {
        /// <summary>
        ///     The method generates unique ID.
        /// </summary>
        /// <returns>
        ///     The unique ID.
        /// </returns>
        string GenerateUniqueId();
    }
}
