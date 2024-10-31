﻿using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using POC.Domain.Entities;

namespace POC.Frontend.Pages.Countries
{
    public partial class CountryForm
    {
        private EditContext editContext = null!;
        protected override void OnInitialized()
        {
            editContext = new(Country);
        }

        [EditorRequired, Parameter] public Country Country { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        public bool FormPostedSuccessfully { get; set; } = false;



        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();

            if (!formWasEdited || FormPostedSuccessfully)
            {
                var result = await SweetAlertService.FireAsync(new SweetAlertOptions
                {
                    Title = "Confirmación",
                    Text = "¿Deseas abandonar la página y perder los cambios?",
                    Icon = SweetAlertIcon.Question,
                    ShowCancelButton = true,
                });

                var confirm = !string.IsNullOrEmpty(result.Value);
                if (confirm)
                {
                    return;
                }

                context.PreventNavigation();
            }
        }
    }
}