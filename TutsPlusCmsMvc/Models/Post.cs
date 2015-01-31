﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutsPlusCmsMvc.Models
{
    public class Post
    {
        private IList<string> _tags = new List<string>();
        
        [Display(Name="Slug")]
        public string Id { get; set; }
        
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Post Content")]
        public string Content { get; set; }

        [Display(Name = "Date Created")]
        public DateTime Created { get; set; }

        [Display(Name = "Date Published")]
        public DateTime? Published { get; set; }
        
        public string AuthorId { get; set; }

        publicv

        public IList<string> Tags
        {
            get { return _tags; } 
            set { _tags = value; }
        }

        public string CombinedTags
        {
            get { return string.Join(",",_tags); }
            set { _tags = value.Split(',').Select(s => s.Trim()).ToList<string>(); }
        }
    }
}