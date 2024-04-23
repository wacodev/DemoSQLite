using DemoSQLite.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSQLite.MVVM.Models
{
    public class MyTask : TableData
    {
        public string Description { get; set; }
        public bool IsChecked { get; set; }
        
        [Ignore]
        public TextDecorations TextDecoration { get; set; }
    }
}
