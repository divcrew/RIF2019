using System.Runtime.Serialization;
using System.Collections.Generic;
using Google.Apis.Customsearch.v1.Data;


namespace JsonSerialize
{
    [DataContract]
    class ObjectToSer
    {
        [DataMember]
        private string searchBy { get; set; }
        [DataMember]
        private IList<Result> links { get; set; }
        public ObjectToSer(string searchBy, IList<Result> links)
        {
            this.searchBy=searchBy;
            this.links=links;
        }
    }
}