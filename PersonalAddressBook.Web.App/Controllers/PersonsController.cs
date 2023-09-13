using Microsoft.AspNetCore.Mvc;
using PersonalAddressBook.Web.App.Models;
using X.PagedList;

namespace PersonalAddressBook.Web.App.Controllers
{
    public class PersonsController : Controller
    {
        private AppDbContext _context;

        private readonly PersonRepository _personRepository;
        public PersonsController(AppDbContext context)
        {
            //DI Container
            //Dependency Injection Pattern
            _personRepository = new PersonRepository();
            _context = context;
        }

        public IActionResult Index(string searchString, int page2 = 1)
        {
            //isme göre arama işlemi

            var person = from p in _context.Person
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                person = person.Where(p => p.Name.Contains(searchString));
            }
            var personCount = _context.Person.ToList();
            ViewData["status"] = personCount.Count();

            return View(person.ToPagedList(page2, 7));

        }
        //veri silme
        public IActionResult Remove(int id)
        {
            var person = _context.Person.Find(id);
            _context.Person.Remove(person);
            _context.SaveChanges();
            TempData["Status"] = "Kişi Başarıyla Silindi !";
            return RedirectToAction("Index");
        }
        [HttpGet]
        //veri ekleme 
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Person newPerson)
        {
            //Person newPerson = new Person() { Name = Name, Surname = Surname, PhoneNumber = PhoneNumber, Email = Email, Address = Address };

            _context.Person.Add(newPerson);
            _context.SaveChanges();

            //Ürün başarıyla eklendi bildirimi verme
            TempData["Status"] = "Kişi Başarıyla Eklendi !";

            return RedirectToAction("Index");
        }

        //veri güncelleme
        [HttpGet]
        public IActionResult Update(int id)
        {
            var person = _context.Person.Find(id);
            return View(person);
        }
        [HttpPost]
        public IActionResult Update(Person updatePerson, int personId)
        {
            updatePerson.Id = personId;
            _context.Person.Update(updatePerson);
            _context.SaveChanges();

            //Ürün güncellendi bildirimi verme
            TempData["Status"] = "Kişi Başarıyla Güncellendi !";

            return RedirectToAction("Index");
        }

        //kişi listeleme 
        public IActionResult PersonsList(string searchString, int page = 1)
        {
            //isme göre arama işlemi

            var person = from p in _context.Person
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                person = person.Where(p => p.Name.Contains(searchString));
            }

            var personCount = _context.Person.ToList();
            ViewData["status"] = personCount.Count();

            return View(person.ToPagedList(page, 10));
        }

        // Kişi Detay getirme
        [HttpGet]
        public IActionResult PersonDetail(int id)
        {
            var person = _context.Person.Find(id);
            return View(person);
        }
    }
}
