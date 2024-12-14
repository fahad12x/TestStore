using System.ComponentModel.DataAnnotations;

namespace TestStore.Models
{
    public class items
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string discount { get; set; }

        [DataType(DataType.Date)]
        public DateTime pubdate { get; set; }
        public string  category { get; set; }
        public int quantity { get; set; }
        public string imgfile { get; set; }
       
    }
}
