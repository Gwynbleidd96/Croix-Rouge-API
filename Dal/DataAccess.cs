using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CroixRouge.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CroixRouge.DTO;


namespace CroixRouge.Dal
{
    public class DataAccess 
    {
        private bdCroixRougeContext _context;

        public DataAccess(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CroixRouge.Model.Adresse>> GetAdressesAsync(string ville = null)
        {
            return await _context.Adresse
            .Where(adr => ville == null || adr.Ville.Contains(ville))
            .OrderBy(adr => adr.Id)
            .ToArrayAsync();
        }

        public async Task AddAdresseAsync(CroixRouge.Model.Adresse adresse)
        {
            if (adresse != null)
            {
                _context.Adresse.Add(adresse);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAdresseAsync(CroixRouge.Model.Adresse adresse, CroixRouge.DTO.AdresseModel dto)
        {
            adresse.Ville = dto.Ville;
            adresse.Rue = dto.Rue; 
            adresse.Numero = dto.Numero;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(adresse).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAdresseAsync(CroixRouge.Model.Adresse adresse)
        {
            _context.Adresse.Remove(adresse);
            await _context.SaveChangesAsync();
        }

        public Task<CroixRouge.Model.Adresse> FindAdresseById(int id)
        {
            return _context.Adresse.FindAsync(id);
        }

        public async Task<IEnumerable<CroixRouge.Model.Alerte>> GetAlertesAsync(int? pageIndex=0, int? pageSize = 10, string nom = null)
        {
             return await _context.Alerte
            .Where(alerte => nom == null || alerte.Nom.Contains(nom))
            .OrderBy(alerte => alerte.Id)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
        }

        public async Task AddAlerteAsync(CroixRouge.Model.Alerte alerte)
        {
            if (alerte != null)
            {
                _context.Alerte.Add(alerte);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAlerteAsync(CroixRouge.Model.Alerte alerte, CroixRouge.DTO.AlerteModel dto)
        {
            alerte.Nom = dto.Nom;
            alerte.Contenu = dto.Contenu;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(alerte).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAlerteAsync(CroixRouge.Model.Alerte alerte)
        {
            _context.Alerte.Remove(alerte);
            await _context.SaveChangesAsync();
        }

        public Task<CroixRouge.Model.Alerte> FindAlerteById(int id)
        {
            return _context.Alerte.FindAsync(id);
        }

        public async Task<IEnumerable<CroixRouge.Model.Collecte>> GetCollectesAsync(int? pageIndex=0, int? pageSize = 10)
        {
             return await _context.Collecte
            .OrderBy(collecte => collecte.Id)
            .Include(c => c.Jourouverture)
                .ThenInclude(j => j.FkTrancheHoraireNavigation)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
        }

        public async Task AddCollecteAsync(CroixRouge.Model.Collecte collecte)
        {
            if (collecte != null)
            {
                _context.Collecte.Add(collecte);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCollecteAsync(CroixRouge.Model.Collecte collecte, CroixRouge.DTO.CollecteModel dto)
        {
            //fixme: améliorer cette implémentation
            collecte.Nom = dto.Nom;
            collecte.Latitude = dto.Latitude;
            collecte.Longitude = dto.Longitude;
            collecte.Telephone = dto.Telephone;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(collecte).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveCollecteAsync(CroixRouge.Model.Collecte collecte)
        {
            _context.Collecte.Remove(collecte);
            await _context.SaveChangesAsync();
        }

        public Task<CroixRouge.Model.Collecte> FindCollecteById(int id)
        {
            return _context.Collecte
            .Include(c => c.Jourouverture)
                .ThenInclude(j => j.FkTrancheHoraireNavigation)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CroixRouge.Model.Groupesanguin>> GetGroupesanguinsAsync()
        {
            return await _context.Groupesanguin
            .OrderBy(g => g.Nom)
            .ToArrayAsync();
        }

        public async Task<IEnumerable<CroixRouge.Model.Role>> GetRolesAsync()
        {
            return await _context.Role
            .ToArrayAsync();
        }

        public async Task<IEnumerable<CroixRouge.Model.Utilisateur>> GetUtilisateursAsync(int? pageIndex=0, int? pageSize = 20, string login = null)
        {
             return await _context.Utilisateur
            .Where(u => login == null || u.Login.Contains(login))
            .OrderBy(u => u.Login)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
        }

        public async Task AddUtilisateurAsync(CroixRouge.Model.Utilisateur utilisateur)
        {
            if (utilisateur != null)
            {
                _context.Utilisateur.Add(utilisateur);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUtilisateurAsync(CroixRouge.Model.Utilisateur utilisateur, CroixRouge.DTO.UtilisateurModel dto)
        {
            //fixme: améliorer cette implémentation
            utilisateur.Nom = dto.Nom;
            utilisateur.Mail = dto.Mail;
            utilisateur.Prenom = dto.Prenom;
            utilisateur.NumGsm = dto.NumGsm;
            utilisateur.DateNaissance = dto.DateNaissance;
            utilisateur.IsMale = dto.IsMale;
            utilisateur.Score = dto.Score;
            utilisateur.Password = dto.Password;
            
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            //_context.Entry(utilisateur).OriginalValues["Rv"] = dto.Rv;   --> ?


            _context.Entry(utilisateur).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUtilisateurAsync(Utilisateur utilisateur)
        {
            _context.Utilisateur.Remove(utilisateur);
            await _context.SaveChangesAsync();
        }

        public Task<CroixRouge.Model.Utilisateur> FindUtilisateurByLogin(string login)
        {
            return _context.Utilisateur.FindAsync(login);
        }
    }
}