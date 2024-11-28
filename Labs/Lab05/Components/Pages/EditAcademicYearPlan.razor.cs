using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Lab05SC.Components.Pages
{
    public partial class EditAcademicYearPlan
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public UniversityService UniversityService { get; set; }

        [Parameter]
        public Guid year_plan_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            academicYearPlan = await UniversityService.Getacademic_year_planByYearPlanId(year_plan_id);
        }
        protected bool errorVisible;
        protected Lab05SC.Models.University.academic_year_plan academicYearPlan;

        protected async Task FormSubmit()
        {
            try
            {
                await UniversityService.Updateacademic_year_plan(year_plan_id, academicYearPlan);
                DialogService.Close(academicYearPlan);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}