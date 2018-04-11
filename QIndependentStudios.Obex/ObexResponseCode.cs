namespace QIndependentStudios.Obex
{
    /// <summary>
    /// Specifies the Obex response type.
    /// </summary>
    public enum ObexResponseCode : byte
    {
        /// <summary>
        /// Reserved.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Reserved.
        /// </summary>
        NoneFinal = 0x0F,
        /// <summary>
        /// Continue response code.
        /// </summary>
        Continue = 0x90,
        /// <summary>
        /// Ok response code.
        /// </summary>
        Ok = 0xA0,
        /// <summary>
        /// Created response code.
        /// </summary>
        Created = 0xA1,
        /// <summary>
        /// Accepted response code.
        /// </summary>
        Accepted = 0xA2,
        /// <summary>
        /// Non Authoritative response code.
        /// </summary>
        NonAuthoritative = 0xA3,
        /// <summary>
        /// No Content response code.
        /// </summary>
        NoContent = 0xA4,
        /// <summary>
        /// Reset Content response code.
        /// </summary>
        ResetContent = 0xA5,
        /// <summary>
        /// Partial Content response code.
        /// </summary>
        PartialContent = 0xA6,
        /// <summary>
        /// Multiple Choices response code.
        /// </summary>
        MultipleChoices = 0xB0,
        /// <summary>
        /// Moved Permamently response code.
        /// </summary>
        MovedPermamently = 0xB1,
        /// <summary>
        /// Moved Temporarily response code.
        /// </summary>
        MovedTemporarily = 0xB2,
        /// <summary>
        /// See Other response code.
        /// </summary>
        SeeOther = 0xB3,
        /// <summary>
        /// Not Modified response code.
        /// </summary>
        NotModified = 0xB4,
        /// <summary>
        /// Use Proxy response code.
        /// </summary>
        UseProxy = 0xB5,
        /// <summary>
        /// Bad Request response code.
        /// </summary>
        BadRequest = 0xC0,
        /// <summary>
        /// Unauthorized response code.
        /// </summary>
        Unauthorized = 0xC1,
        /// <summary>
        /// Payment Required response code.
        /// </summary>
        PaymentRequired = 0xC2,
        /// <summary>
        /// Forbidden response code.
        /// </summary>
        Forbidden = 0xC3,
        /// <summary>
        /// Continue response code.
        /// </summary>
        NotFound = 0xC4,
        /// <summary>
        /// Method Not Allowed response code.
        /// </summary>
        MethodNotAllowed = 0xC5,
        /// <summary>
        /// Not Acceptable response code.
        /// </summary>
        NotAcceptable = 0xC6,
        /// <summary>
        /// Proxy Authorization Required response code.
        /// </summary>
        ProxyAuthorizationRequired = 0xC7,
        /// <summary>
        /// Request Timeout response code.
        /// </summary>
        RequestTimeout = 0xC8,
        /// <summary>
        /// Conflict response code.
        /// </summary>
        Conflict = 0xC9,
        /// <summary>
        /// Gone response code.
        /// </summary>
        Gone = 0xCA,
        /// <summary>
        /// Length Required response code.
        /// </summary>
        LengthRequired = 0xCB,
        /// <summary>
        /// Precondition Failed response code.
        /// </summary>
        PreconditionFailed = 0xCC,
        /// <summary>
        /// Payload Too Large response code.
        /// </summary>
        PayloadTooLarge = 0xCD,
        /// <summary>
        /// Uri Too Large response code.
        /// </summary>
        UriTooLarge = 0xCE,
        /// <summary>
        /// Unsupported Media Type response code.
        /// </summary>
        UnsupportedMediaType = 0xCF,
        /// <summary>
        /// Internal Server Error response code.
        /// </summary>
        InternalServerError = 0xD0,
        /// <summary>
        /// Not Implemented response code.
        /// </summary>
        NotImplemented = 0xD1,
        /// <summary>
        /// Bad Gateway response code.
        /// </summary>
        BadGateway = 0xD2,
        /// <summary>
        /// Service Unavailable response code.
        /// </summary>
        ServiceUnavailable = 0xD3,
        /// <summary>
        /// Gateway Timeout response code.
        /// </summary>
        GatewayTimeout = 0xD4,
        /// <summary>
        /// Http Version Not Supported response code.
        /// </summary>
        HttpVersionNotSupported = 0xD5,
        /// <summary>
        /// Database Full response code.
        /// </summary>
        DatabaseFull = 0xE0,
        /// <summary>
        /// Database Locked response code.
        /// </summary>
        DatabaseLocked = 0xE1
    }
}
