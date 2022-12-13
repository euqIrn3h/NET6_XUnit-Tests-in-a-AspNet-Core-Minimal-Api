﻿namespace MinimalApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public bool IsComplete { get; set; }
    }
}
