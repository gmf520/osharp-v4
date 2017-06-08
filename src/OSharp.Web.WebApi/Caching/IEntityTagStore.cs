namespace OSharp.Web.Http.Caching
{
    /// <summary>
    /// This is an interface representing an ETag store acting similar to a dictionary. 
    /// storing and retriving ETags.
    ///  
    /// In a single-server scenario, this could be an in-memory disctionary implementation
    /// while in a server farm, this will be a persistent store.
    /// </summary>
    public interface IEntityTagStore
    {
        bool TryGetValue(EntityTagKey key, out TimedEntityTagHeaderValue eTag);
        void AddOrUpdate(EntityTagKey key, TimedEntityTagHeaderValue eTag);
        bool TryRemove(EntityTagKey key);
        int RemoveAllByRoutePattern(string routePattern);
        void Clear();
    }
}
