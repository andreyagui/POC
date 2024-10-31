using Microsoft.AspNetCore.Components;
using POC.Domain.Entities;
using POC.Application.Interfaces;

namespace POC.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        [Inject] private IRepository Repository { get; set; } = null!;

        public List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHppt = await Repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHppt.Response!;
        }
    }
}