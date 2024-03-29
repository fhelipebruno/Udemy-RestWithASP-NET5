﻿using System.ComponentModel.DataAnnotations.Schema;
using Udemy_RestWithASP_NET5.Hypermedia;
using Udemy_RestWithASP_NET5.Hypermedia.Abstract;
using Udemy_RestWithASP_NET5.Model.Base;

namespace Udemy_RestWithASP_NET5.Data.VO
{
    public class PersonVO : ISupportsHypermedia
    {        
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool Enabled { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }

}
