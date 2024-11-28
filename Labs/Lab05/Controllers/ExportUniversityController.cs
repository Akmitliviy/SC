using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using Lab05SC.Data;

namespace Lab05SC.Controllers
{
    public partial class ExportUniversityController : ExportController
    {
        private readonly UniversityContext context;
        private readonly UniversityService service;

        public ExportUniversityController(UniversityContext context, UniversityService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/University/academic_year_plans/csv")]
        [HttpGet("/export/University/academic_year_plans/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> Exportacademic_year_plansToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.Getacademic_year_plans(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/academic_year_plans/excel")]
        [HttpGet("/export/University/academic_year_plans/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> Exportacademic_year_plansToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.Getacademic_year_plans(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/courses/csv")]
        [HttpGet("/export/University/courses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportcoursesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.Getcourses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/courses/excel")]
        [HttpGet("/export/University/courses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportcoursesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.Getcourses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/enrollments/csv")]
        [HttpGet("/export/University/enrollments/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportenrollmentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.Getenrollments(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/enrollments/excel")]
        [HttpGet("/export/University/enrollments/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportenrollmentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.Getenrollments(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/groups/csv")]
        [HttpGet("/export/University/groups/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportgroupsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.Getgroups(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/groups/excel")]
        [HttpGet("/export/University/groups/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportgroupsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.Getgroups(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/qr_sessions/csv")]
        [HttpGet("/export/University/qr_sessions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> Exportqr_sessionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.Getqr_sessions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/qr_sessions/excel")]
        [HttpGet("/export/University/qr_sessions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> Exportqr_sessionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.Getqr_sessions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/specialties/csv")]
        [HttpGet("/export/University/specialties/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportspecialtiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.Getspecialties(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/specialties/excel")]
        [HttpGet("/export/University/specialties/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportspecialtiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.Getspecialties(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/specialty_courses/csv")]
        [HttpGet("/export/University/specialty_courses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> Exportspecialty_coursesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.Getspecialty_courses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/specialty_courses/excel")]
        [HttpGet("/export/University/specialty_courses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> Exportspecialty_coursesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.Getspecialty_courses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/students/csv")]
        [HttpGet("/export/University/students/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportstudentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.Getstudents(), Request.Query, false), fileName);
        }

        [HttpGet("/export/University/students/excel")]
        [HttpGet("/export/University/students/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportstudentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.Getstudents(), Request.Query, false), fileName);
        }
    }
}
