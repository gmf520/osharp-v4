namespace OSharp.Web.Http.Messages
{
    /// <summary>
    /// 提供统一资源标识符 (URI) 的对象表示形式和对 URI 各部分的轻松访问。
    /// </summary>
    public interface IApiResource
    {
        /// <summary>
        /// 设置 提供统一资源标识符 (URI) 。
        /// </summary>
        /// <param name="location"></param>
        void SetLocation(ResourceLocation location);
    }
}