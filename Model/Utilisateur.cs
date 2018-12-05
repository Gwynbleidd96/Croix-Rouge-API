﻿using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Diffuserimage = new HashSet<Diffuserimage>();
            Don = new HashSet<Don>();
            Lanceralerte = new HashSet<Lanceralerte>();
            Partagerimage = new HashSet<Partagerimage>();
        }

        public string Login { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public int NumGsm { get; set; }
        public DateTime DateNaissance { get; set; }
        public int Score { get; set; }
        public string FkLibelle { get; set; }
        public int FkAdresse { get; set; }
        public string FkGroupesanguin { get; set; }

        public Adresse FkAdresseNavigation { get; set; }
        public Groupesanguin FkGroupesanguinNavigation { get; set; }
        public Role FkLibelleNavigation { get; set; }
        public ICollection<Diffuserimage> Diffuserimage { get; set; }
        public ICollection<Don> Don { get; set; }
        public ICollection<Lanceralerte> Lanceralerte { get; set; }
        public ICollection<Partagerimage> Partagerimage { get; set; }
    }
}
