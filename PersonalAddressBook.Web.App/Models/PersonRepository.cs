namespace PersonalAddressBook.Web.App.Models;

public class PersonRepository
{
    private static List<Person> _persons = new List<Person>()
    {
    };


    //yukarıdakı listemizi aşığıda tanımladığımız metotlarla berabaer dış dünyaya açacaz
    //yani başka classların bu repository üstünde işlem yapmamızı sağlayacağız

    // Tüm Kişileri Dönecek metodumuz
    public List<Person> GetAll() => _persons;

    //kişi ekleme
    public void Add(Person newPerson) => _persons.Add(newPerson);

    //kişi silme
    public void Remove(int id)
    {
        //bu person varmı yokmu onun kontrolu
        var hasPerson = _persons.FirstOrDefault(x => x.Id == id);
        if (hasPerson == null)
        {
            throw new Exception($"Bu id{id} ye sahip ürün bulunmamaktadır");
        }

        _persons.Remove(hasPerson);
    }

    //Ürün Güncelleme
    public void Update(Person updatePerson)
    {
        //önce güncellenecek nesne varmı bunu tespit edelim
        var hasPerson = _persons.FirstOrDefault(x => x.Id == updatePerson.Id);

        if (hasPerson == null)
        {
            throw new Exception($"Bu id{updatePerson.Id} ye sahip ürün bulunmamaktadır");
        }

        hasPerson.Name = updatePerson.Name;
        hasPerson.Surname = updatePerson.Surname;
        hasPerson.PhoneNumber = updatePerson.PhoneNumber;
        hasPerson.Email = updatePerson.Email;
        hasPerson.Address = updatePerson.Address;

        //dizindeki indexi bulma
        var index = _persons.FindIndex(x => x.Id == updatePerson.Id);

        _persons[index] = hasPerson;
    }
}
