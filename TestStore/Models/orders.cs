using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TestStore.Models
{
    public class orders
    {
        public int Id { get; set; }
        public string custname { get; set; }
        public int total { get; set; }
        [BindProperty, DataType(DataType.Date)]
       public DateTime orderdate { get; set; }
    }
}