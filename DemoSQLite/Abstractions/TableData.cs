﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSQLite.Abstractions
{
    public class TableData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
