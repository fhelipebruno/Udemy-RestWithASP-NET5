﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Udemy_RestWithASP_NET5.Model.Base {
    public class BaseEntity {
        [Column("id")]
        public long Id { get; set; }
    }
}
