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
    public partial class Students
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

        protected IEnumerable<Lab05SC.Models.University.student> students;

        protected RadzenDataGrid<Lab05SC.Models.University.student> grid0;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            students = await UniversityService.Getstudents(new Query { Filter = $@"i => i.first_name.Contains(@0) || i.last_name.Contains(@0) || i.email.Contains(@0) || i.phone_number.Contains(@0)", FilterParameters = new object[] { search }, Expand = "group1" });
        }
        protected override async Task OnInitializedAsync()
        {
            students = await UniversityService.Getstudents(new Query { Filter = $@"i => i.first_name.Contains(@0) || i.last_name.Contains(@0) || i.email.Contains(@0) || i.phone_number.Contains(@0)", FilterParameters = new object[] { search }, Expand = "group1" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddStudent>("Add student", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<Lab05SC.Models.University.student> args)
        {
            await DialogService.OpenAsync<EditStudent>("Edit student", new Dictionary<string, object> { {"student_id", args.Data.student_id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Lab05SC.Models.University.student student)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await UniversityService.Deletestudent(student.student_id);

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
                    Detail = $"Unable to delete student"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await UniversityService.ExportstudentsToCSV(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "group1",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "students");
            }

            if (args == null || args.Value == "xlsx")
            {
                await UniversityService.ExportstudentsToExcel(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "group1",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "students");
            }
        }
    }
}