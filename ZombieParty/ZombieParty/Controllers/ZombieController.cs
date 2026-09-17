using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZombieParty.Models;
using ZombieParty.Models.Data;

namespace ZombieParty.Controllers
{
    public class ZombieController : Controller
    {
        private ZombiePartyDbContext _baseDonnees { get; set; }

        public ZombieController(ZombiePartyDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }

        public IActionResult Index()
        {
            List<Zombie> zombiesList = _baseDonnees.Zombies.OrderBy(z => z.Name).Include(z => z.ZombieType).ToList();

            return View(zombiesList);

        }

        public IActionResult Create()
        {
            ZombieVM zombieVM = new ZombieVM();
            zombieVM.ZombieTypeSelectList = _baseDonnees.ZombieTypes.Select(t => new SelectListItem
            {
                Text = t.TypeName,
                Value = t.Id.ToString()
            }).OrderBy(t => t.Text);

            return View(zombieVM);
        }

        // 2 & 3. Copie de GET Create modifiée sous le nom de Edit avec le paramètre id
        public IActionResult Edit(int id)
        {
            ZombieVM zombieVM = new ZombieVM();
            zombieVM.Zombie = _baseDonnees.Zombies.Find(id);
            zombieVM.ZombieTypeSelectList = _baseDonnees.ZombieTypes.Select(t => new SelectListItem
            {
                Text = t.TypeName,
                Value = t.Id.ToString()
            }).OrderBy(t => t.Text);

            return View(zombieVM);
        }
        public IActionResult StrongestZombies()
        {
            List<Zombie> zombiesList = _baseDonnees.Zombies
                .Where(z => z.Point >= 8)
                .OrderBy(z => z.Name)
                .Include(z => z.ZombieType)
                .ToList();

            return View(zombiesList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ZombieVM zombieVM)
        {
            //Si le modèle est valide le zombie est ajouté et nous sommes redirigé vers index.
            if (ModelState.IsValid)
            {
                _baseDonnees.Zombies.Add(zombieVM.Zombie);
                _baseDonnees.SaveChanges();
                TempData["Success"] = $"Zombie {zombieVM.Zombie.Name} added";
                return this.RedirectToAction("Index");
            }
            zombieVM.ZombieTypeSelectList = _baseDonnees.ZombieTypes.Select(t => new SelectListItem
            {
                Text = t.TypeName,
                Value = t.Id.ToString()
            }).OrderBy(t => t.Text);

            return View(zombieVM);
        }

        // 1 & 2. Copie de POST Create nommée Edit avec protection anti-falsification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ZombieVM zombieVM)
        {
            // 3. Modification pour mettre à jour le zombie existant (Le point d'arrêt va ici pour l'étape 4)
            if (ModelState.IsValid)
            {
                _baseDonnees.Zombies.Update(zombieVM.Zombie);
                _baseDonnees.SaveChanges();
                TempData["Success"] = $"Zombie {zombieVM.Zombie.Name} has been modified";
                return this.RedirectToAction("Index");
            }

            zombieVM.ZombieTypeSelectList = _baseDonnees.ZombieTypes.Select(t => new SelectListItem
            {
                Text = t.TypeName,
                Value = t.Id.ToString()
            }).OrderBy(t => t.Text);

            return View(zombieVM);
        }
    }
}
