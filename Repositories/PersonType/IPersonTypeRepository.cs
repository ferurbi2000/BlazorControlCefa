namespace BlazorControlCefa.Repositories.PersonType
{
    using BlazorControlCefa.Application.Dtos.PersonType;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;

    public interface IPersonTypeRepository
    {
        Task<PagingResponse<PersonType>> GetAllPersonTypeAsync(PersonTypeParametersDto personTypeParameters, MetaData metaData);
        Task<List<PersonType>> GetAllPersonTypeForSelectionAsync();
        Task<PersonType> GetPersonTypeAsync(int id);
        PersonType GetPersonType(int id);
        Task AddPersonType(PersonType personType);
        void DeletePersonType(PersonType personType);
        void UpdatePersonType(PersonType personType);
        bool Save();
        Task<bool> SaveAsync();
    }
}
