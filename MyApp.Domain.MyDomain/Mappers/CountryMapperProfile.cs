using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.Abstractions.MyDomain.Entities;

namespace MyApp.Domain.MyDomain.Mappers
{
    public static class CountryMapperProfile
    {
        public static List<CountryContract> ToCountryContracts(this List<Country> countries)
        {
            return countries.Select(country => country.ToCountryContract()).ToList();
        }

        public static CountryContract ToCountryContract(this Country country)
        {
            return new CountryContract
            {
                Name = new CountryContract.CountryName
                {
                    Common = country.CommonName,
                    Official = country.OfficialName,
                    NativeName = country.NativeNameSpaCommon == null && country.NativeNameSpaOfficial == null
                        ? new CountryContract.NativeName()
                        : new CountryContract.NativeName
                        {
                            Spa = new CountryContract.NativeName.NativeNameSpa
                            {
                                Common = country.NativeNameSpaCommon ?? "",
                                Official = country.NativeNameSpaOfficial ?? ""
                            }
                        }
                },
                Capital = string.IsNullOrEmpty(country.Capital) ? new List<string>() : new List<string> { country.Capital },
                Borders = country.Borders?.Select(b => b.Name).ToList() ?? new List<string>()
            };
        }


        public static Country ToCountryEntity(this CountryContract contract)
        {
            return new Country
            {
                CommonName = contract.Name.Common,
                OfficialName = contract.Name.Official,
                NativeNameSpaCommon = contract.Name.NativeName?.Spa?.Common,
                NativeNameSpaOfficial = contract.Name.NativeName?.Spa?.Official,
                Capital = contract.Capital.FirstOrDefault() ?? "",
                Borders = contract.Borders.Select(b => new Border { Name = b }).ToList()
            };
        }

        public static List<Country> ToCountryEntities(this IEnumerable<CountryContract> contracts)
        {
            return contracts.Select(c => c.ToCountryEntity()).ToList();
        }
    }
}
