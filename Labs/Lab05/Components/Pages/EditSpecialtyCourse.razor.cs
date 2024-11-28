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
    public partial class EditSpecialtyCourse
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
        public Guid specialty_course_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            specialtyCourse = await UniversityService.Getspecialty_courseBySpecialtyCourseId(specialty_course_id);

            specialtiesForspecialtyId = await UniversityService.Getspecialties();

            coursesForcourseId = await UniversityService.Getcourses();
        }
        protected bool errorVisible;
        protected Lab05SC.Models.University.specialty_course specialtyCourse;

        protected IEnumerable<Lab05SC.Models.University.specialty> specialtiesForspecialtyId;

        protected IEnumerable<Lab05SC.Models.University.course> coursesForcourseId;

        protected async Task FormSubmit()
        {
            try
            {
                await UniversityService.Updatespecialty_course(specialty_course_id, specialtyCourse);
                DialogService.Close(specialtyCourse);
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