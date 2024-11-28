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
    public partial class EditQrSession
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
        public Guid qr_session_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            qrSession = await UniversityService.Getqr_sessionByQrSessionId(qr_session_id);

            coursesForcourseId = await UniversityService.Getcourses();
        }
        protected bool errorVisible;
        protected Lab05SC.Models.University.qr_session qrSession;

        protected IEnumerable<Lab05SC.Models.University.course> coursesForcourseId;

        protected async Task FormSubmit()
        {
            try
            {
                await UniversityService.Updateqr_session(qr_session_id, qrSession);
                DialogService.Close(qrSession);
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