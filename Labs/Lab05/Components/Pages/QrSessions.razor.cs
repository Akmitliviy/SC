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
    public partial class QrSessions
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

        protected IEnumerable<Lab05SC.Models.University.qr_session> qrSessions;

        protected RadzenDataGrid<Lab05SC.Models.University.qr_session> grid0;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            qrSessions = await UniversityService.Getqr_sessions(new Query { Filter = $@"i => i.qr_code.Contains(@0)", FilterParameters = new object[] { search }, Expand = "course" });
        }
        protected override async Task OnInitializedAsync()
        {
            qrSessions = await UniversityService.Getqr_sessions(new Query { Filter = $@"i => i.qr_code.Contains(@0)", FilterParameters = new object[] { search }, Expand = "course" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddQrSession>("Add qr_session", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<Lab05SC.Models.University.qr_session> args)
        {
            await DialogService.OpenAsync<EditQrSession>("Edit qr_session", new Dictionary<string, object> { {"qr_session_id", args.Data.qr_session_id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Lab05SC.Models.University.qr_session qrSession)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await UniversityService.Deleteqr_session(qrSession.qr_session_id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete qr_session"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await UniversityService.Exportqr_sessionsToCSV(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "course",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "qr_sessions");
            }

            if (args == null || args.Value == "xlsx")
            {
                await UniversityService.Exportqr_sessionsToExcel(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "course",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "qr_sessions");
            }
        }
    }
}