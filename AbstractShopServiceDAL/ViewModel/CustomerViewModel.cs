﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }
    }
}
