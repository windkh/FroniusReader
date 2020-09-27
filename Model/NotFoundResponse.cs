
namespace FroniusReader.Model
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using RestSharp;

    public class NotFoundResponse : IRestResponse
    {
        public IRestRequest Request 
        {
            get => null;
            set
            {
            }
        }
        public string ContentType 
        { 
            get => string.Empty;
            set
            {
            }
        }
        public long ContentLength 
        {
            get => 0;
            set
            {
            }
        }
        public string ContentEncoding
        {
            get => string.Empty;
            set
            {
            }
        }

        public string Content
        {
            get => null;
            set
            {
            }
        }
        public HttpStatusCode StatusCode
        {
            get => HttpStatusCode.NotFound;
            set
            {
            }
        }

        public bool IsSuccessful => false;

        public string StatusDescription
        {
            get => string.Empty;
            set
            {
            }
        }
        public byte[] RawBytes
        {
            get => null;
            set
            {
            }
        }
        public Uri ResponseUri
        {
            get => null;
            set
            {
            }
        }
        public string Server
        {
            get => null;
            set
            {
            }
        }

#pragma warning disable CS0618 // Type or member is obsolete
        public IList<RestResponseCookie> Cookies => null;
#pragma warning restore CS0618 // Type or member is obsolete

        public IList<Parameter> Headers => null;

        public ResponseStatus ResponseStatus 
        {
            get => ResponseStatus.Error;
            set
            {
            }
        }
        public string ErrorMessage
        {
            get => null;
            set
            {
            }
        }
        public Exception ErrorException
        {
            get => null;
            set
            {
            }
        }
        public Version ProtocolVersion
        {
            get => null;
            set
            {
            }
        }
    }
}
