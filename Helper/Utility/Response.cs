namespace Utility
{
    /// <summary>
    /// Response
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Response"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; } 
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string? Message { get; set; }
    }

    /// <summary>
    /// Response Data Type of T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Utility.Response" />
    public class ResponseData<T> : Response
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T? Data { get; set; }
    }
}
