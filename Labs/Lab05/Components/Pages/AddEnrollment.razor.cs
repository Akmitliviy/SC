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
    public partial class AddEnrollment
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

        protected override async Task OnInitializedAsync()
        {
            enrollment = new Lab05SC.Models.University.enrollment();

            studentsForstudentId = await UniversityService.Getstudents();

            coursesForcourseId = await UniversityService.Getcourses();

            groupsForgroupId = await UniversityService.Getgroups();
        }
        protected bool errorVisible;
        protected Lab05SC.Models.University.enrollment enrollment;

        protected IEnumerable<Lab05SC.Models.University.student> studentsForstudentId;

        protected IEnumerable<Lab05SC.Models.University.course> coursesForcourseId;

        protected IEnumerable<Lab05SC.Models.University.group> groupsForgroupId;

        protected async Task FormSubmit()
        {
            try
            {
                await UniversityService.Createenrollment(enrollment);
                DialogService.Close(enrollment);
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