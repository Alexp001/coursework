using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    [Serializable]
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}