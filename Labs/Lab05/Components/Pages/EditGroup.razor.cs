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
    public partial class EditGroup
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
        public Guid group_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _group = await UniversityService.GetgroupByGroupId(group_id);
        }
        protected bool errorVisible;
        protected Lab05SC.Models.University.group _group;

        protected async Task FormSubmit()
        {
            try
            {
                await UniversityService.Updategroup(group_id, _group);
                DialogService.Close(_group);
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