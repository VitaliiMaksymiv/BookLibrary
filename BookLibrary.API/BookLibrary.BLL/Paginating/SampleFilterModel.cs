using Newtonsoft.Json;

namespace BookLibrary.BLL.Paginating
{
    public class SampleFilterModel : FilterModelBase
    {
        public string SearchQuery { get; set; }

        public SampleFilterModel() : base()
        {
            this.PageSize = 5;
        }

        public override object Clone()
        {
            var jsonString = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject(jsonString, this.GetType());
        }
    }
}