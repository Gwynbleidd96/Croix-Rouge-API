using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class UtilisateurDTO
    {
        [StringLength(50, MinimumLength=3)]
        [Required]
        public string Login { get; set; }
        [StringLength(200, MinimumLength=8)]
        [Required]
        public string Password { get; set; }
        [StringLength(100, MinimumLength=2)]
        public string Nom { get; set; }
        [StringLength(100, MinimumLength=2)]
        public string Prenom { get; set; }
        [StringLength(320, MinimumLength=4)]
        [Required]
        public string Mail { get; set; }
        [Required]
        public int NumGsm { get; set; }
        [Required]        
        public DateTime DateNaissance { get; set; }
        [Required]        
        public bool IsMale { get; set; }
        [Required]        
        public int Score { get; set; }
        [StringLength(200, MinimumLength=2)]
        [Required]        
        public string Ville { get; set; }
        [StringLength(200, MinimumLength=2)]
        [Required]           
        public string Rue { get; set; }
        [StringLength(4, MinimumLength=1)]
        public string Numero { get; set; }
        [StringLength(6, MinimumLength=1)]
        public string FkRole { get; set; }
        public byte[] Rv { get; set; }
        public string FkGroupesanguin;
        public UtilisateurDTO ()
        {

        }
    }
}