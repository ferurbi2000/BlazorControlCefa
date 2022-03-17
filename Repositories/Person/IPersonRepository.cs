namespace BlazorControlCefa.Repositories.Person
{
    using BlazorControlCefa.Application.Dtos.Person;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;

    public interface IPersonRepository
    {
        Task<PagingResponse<Person>> GetAllPersonAsync(PersonParametersDto personParameters, MetaData metaData);
        Task<List<Person>> GetAllPersonForSelectionAsync();
        Task<List<Person>> GetAllEmployeeForSelectionAsync();
        Task<Person> GetPersonAsync(int id);
        Person GetPerson(int id);
        Task AddPerson(Person person);
        void DeletePerson(Person person);
        void UpdatePerson(Person person);
        bool Save();
        Task<bool> SaveAsync();
    }
}
