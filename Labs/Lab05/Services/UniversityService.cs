using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

using Lab05SC.Data;

namespace Lab05SC
{
    public partial class UniversityService
    {
        UniversityContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly UniversityContext context;
        private readonly NavigationManager navigationManager;

        public UniversityService(UniversityContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task Exportacademic_year_plansToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/academic_year_plans/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/academic_year_plans/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task Exportacademic_year_plansToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/academic_year_plans/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/academic_year_plans/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void Onacademic_year_plansRead(ref IQueryable<Lab05SC.Models.University.academic_year_plan> items);

        public async Task<IQueryable<Lab05SC.Models.University.academic_year_plan>> Getacademic_year_plans(Query query = null)
        {
            var items = Context.academic_year_plans.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            Onacademic_year_plansRead(ref items);

            return await Task.FromResult(items);
        }

        partial void Onacademic_year_planGet(Lab05SC.Models.University.academic_year_plan item);
        partial void OnGetacademic_year_planByYearPlanId(ref IQueryable<Lab05SC.Models.University.academic_year_plan> items);


        public async Task<Lab05SC.Models.University.academic_year_plan> Getacademic_year_planByYearPlanId(Guid yearplanid)
        {
            var items = Context.academic_year_plans
                              .AsNoTracking()
                              .Where(i => i.year_plan_id == yearplanid);

 
            OnGetacademic_year_planByYearPlanId(ref items);

            var itemToReturn = items.FirstOrDefault();

            Onacademic_year_planGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void Onacademic_year_planCreated(Lab05SC.Models.University.academic_year_plan item);
        partial void OnAfteracademic_year_planCreated(Lab05SC.Models.University.academic_year_plan item);

        public async Task<Lab05SC.Models.University.academic_year_plan> Createacademic_year_plan(Lab05SC.Models.University.academic_year_plan academicyearplan)
        {
            Onacademic_year_planCreated(academicyearplan);

            var existingItem = Context.academic_year_plans
                              .Where(i => i.year_plan_id == academicyearplan.year_plan_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.academic_year_plans.Add(academicyearplan);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(academicyearplan).State = EntityState.Detached;
                throw;
            }

            OnAfteracademic_year_planCreated(academicyearplan);

            return academicyearplan;
        }

        public async Task<Lab05SC.Models.University.academic_year_plan> Cancelacademic_year_planChanges(Lab05SC.Models.University.academic_year_plan item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void Onacademic_year_planUpdated(Lab05SC.Models.University.academic_year_plan item);
        partial void OnAfteracademic_year_planUpdated(Lab05SC.Models.University.academic_year_plan item);

        public async Task<Lab05SC.Models.University.academic_year_plan> Updateacademic_year_plan(Guid yearplanid, Lab05SC.Models.University.academic_year_plan academicyearplan)
        {
            Onacademic_year_planUpdated(academicyearplan);

            var itemToUpdate = Context.academic_year_plans
                              .Where(i => i.year_plan_id == academicyearplan.year_plan_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(academicyearplan);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfteracademic_year_planUpdated(academicyearplan);

            return academicyearplan;
        }

        partial void Onacademic_year_planDeleted(Lab05SC.Models.University.academic_year_plan item);
        partial void OnAfteracademic_year_planDeleted(Lab05SC.Models.University.academic_year_plan item);

        public async Task<Lab05SC.Models.University.academic_year_plan> Deleteacademic_year_plan(Guid yearplanid)
        {
            var itemToDelete = Context.academic_year_plans
                              .Where(i => i.year_plan_id == yearplanid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            Onacademic_year_planDeleted(itemToDelete);


            Context.academic_year_plans.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfteracademic_year_planDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportcoursesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/courses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/courses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportcoursesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/courses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/courses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OncoursesRead(ref IQueryable<Lab05SC.Models.University.course> items);

        public async Task<IQueryable<Lab05SC.Models.University.course>> Getcourses(Query query = null)
        {
            var items = Context.courses.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OncoursesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OncourseGet(Lab05SC.Models.University.course item);
        partial void OnGetcourseByCourseId(ref IQueryable<Lab05SC.Models.University.course> items);


        public async Task<Lab05SC.Models.University.course> GetcourseByCourseId(Guid courseid)
        {
            var items = Context.courses
                              .AsNoTracking()
                              .Where(i => i.course_id == courseid);

 
            OnGetcourseByCourseId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OncourseGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OncourseCreated(Lab05SC.Models.University.course item);
        partial void OnAftercourseCreated(Lab05SC.Models.University.course item);

        public async Task<Lab05SC.Models.University.course> Createcourse(Lab05SC.Models.University.course course)
        {
            OncourseCreated(course);

            var existingItem = Context.courses
                              .Where(i => i.course_id == course.course_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.courses.Add(course);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(course).State = EntityState.Detached;
                throw;
            }

            OnAftercourseCreated(course);

            return course;
        }

        public async Task<Lab05SC.Models.University.course> CancelcourseChanges(Lab05SC.Models.University.course item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OncourseUpdated(Lab05SC.Models.University.course item);
        partial void OnAftercourseUpdated(Lab05SC.Models.University.course item);

        public async Task<Lab05SC.Models.University.course> Updatecourse(Guid courseid, Lab05SC.Models.University.course course)
        {
            OncourseUpdated(course);

            var itemToUpdate = Context.courses
                              .Where(i => i.course_id == course.course_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(course);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAftercourseUpdated(course);

            return course;
        }

        partial void OncourseDeleted(Lab05SC.Models.University.course item);
        partial void OnAftercourseDeleted(Lab05SC.Models.University.course item);

        public async Task<Lab05SC.Models.University.course> Deletecourse(Guid courseid)
        {
            var itemToDelete = Context.courses
                              .Where(i => i.course_id == courseid)
                              .Include(i => i.enrollments)
                              .Include(i => i.qr_sessions)
                              .Include(i => i.specialty_courses)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OncourseDeleted(itemToDelete);


            Context.courses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAftercourseDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportenrollmentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/enrollments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/enrollments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportenrollmentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/enrollments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/enrollments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnenrollmentsRead(ref IQueryable<Lab05SC.Models.University.enrollment> items);

        public async Task<IQueryable<Lab05SC.Models.University.enrollment>> Getenrollments(Query query = null)
        {
            var items = Context.enrollments.AsQueryable();

            items = items.Include(i => i.course);
            items = items.Include(i => i.group1);
            items = items.Include(i => i.student);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnenrollmentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnenrollmentGet(Lab05SC.Models.University.enrollment item);
        partial void OnGetenrollmentByEnrollmentId(ref IQueryable<Lab05SC.Models.University.enrollment> items);


        public async Task<Lab05SC.Models.University.enrollment> GetenrollmentByEnrollmentId(Guid enrollmentid)
        {
            var items = Context.enrollments
                              .AsNoTracking()
                              .Where(i => i.enrollment_id == enrollmentid);

            items = items.Include(i => i.course);
            items = items.Include(i => i.group1);
            items = items.Include(i => i.student);
 
            OnGetenrollmentByEnrollmentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnenrollmentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnenrollmentCreated(Lab05SC.Models.University.enrollment item);
        partial void OnAfterenrollmentCreated(Lab05SC.Models.University.enrollment item);

        public async Task<Lab05SC.Models.University.enrollment> Createenrollment(Lab05SC.Models.University.enrollment enrollment)
        {
            OnenrollmentCreated(enrollment);

            var existingItem = Context.enrollments
                              .Where(i => i.enrollment_id == enrollment.enrollment_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.enrollments.Add(enrollment);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(enrollment).State = EntityState.Detached;
                throw;
            }

            OnAfterenrollmentCreated(enrollment);

            return enrollment;
        }

        public async Task<Lab05SC.Models.University.enrollment> CancelenrollmentChanges(Lab05SC.Models.University.enrollment item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnenrollmentUpdated(Lab05SC.Models.University.enrollment item);
        partial void OnAfterenrollmentUpdated(Lab05SC.Models.University.enrollment item);

        public async Task<Lab05SC.Models.University.enrollment> Updateenrollment(Guid enrollmentid, Lab05SC.Models.University.enrollment enrollment)
        {
            OnenrollmentUpdated(enrollment);

            var itemToUpdate = Context.enrollments
                              .Where(i => i.enrollment_id == enrollment.enrollment_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(enrollment);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterenrollmentUpdated(enrollment);

            return enrollment;
        }

        partial void OnenrollmentDeleted(Lab05SC.Models.University.enrollment item);
        partial void OnAfterenrollmentDeleted(Lab05SC.Models.University.enrollment item);

        public async Task<Lab05SC.Models.University.enrollment> Deleteenrollment(Guid enrollmentid)
        {
            var itemToDelete = Context.enrollments
                              .Where(i => i.enrollment_id == enrollmentid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnenrollmentDeleted(itemToDelete);


            Context.enrollments.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterenrollmentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportgroupsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/groups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/groups/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportgroupsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/groups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/groups/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OngroupsRead(ref IQueryable<Lab05SC.Models.University.group> items);

        public async Task<IQueryable<Lab05SC.Models.University.group>> Getgroups(Query query = null)
        {
            var items = Context.groups.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OngroupsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OngroupGet(Lab05SC.Models.University.group item);
        partial void OnGetgroupByGroupId(ref IQueryable<Lab05SC.Models.University.group> items);


        public async Task<Lab05SC.Models.University.group> GetgroupByGroupId(Guid groupid)
        {
            var items = Context.groups
                              .AsNoTracking()
                              .Where(i => i.group_id == groupid);

 
            OnGetgroupByGroupId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OngroupGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OngroupCreated(Lab05SC.Models.University.group item);
        partial void OnAftergroupCreated(Lab05SC.Models.University.group item);

        public async Task<Lab05SC.Models.University.group> Creategroup(Lab05SC.Models.University.group _group)
        {
            OngroupCreated(_group);

            var existingItem = Context.groups
                              .Where(i => i.group_id == _group.group_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.groups.Add(_group);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(_group).State = EntityState.Detached;
                throw;
            }

            OnAftergroupCreated(_group);

            return _group;
        }

        public async Task<Lab05SC.Models.University.group> CancelgroupChanges(Lab05SC.Models.University.group item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OngroupUpdated(Lab05SC.Models.University.group item);
        partial void OnAftergroupUpdated(Lab05SC.Models.University.group item);

        public async Task<Lab05SC.Models.University.group> Updategroup(Guid groupid, Lab05SC.Models.University.group _group)
        {
            OngroupUpdated(_group);

            var itemToUpdate = Context.groups
                              .Where(i => i.group_id == _group.group_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(_group);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAftergroupUpdated(_group);

            return _group;
        }

        partial void OngroupDeleted(Lab05SC.Models.University.group item);
        partial void OnAftergroupDeleted(Lab05SC.Models.University.group item);

        public async Task<Lab05SC.Models.University.group> Deletegroup(Guid groupid)
        {
            var itemToDelete = Context.groups
                              .Where(i => i.group_id == groupid)
                              .Include(i => i.students)
                              .Include(i => i.enrollments)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OngroupDeleted(itemToDelete);


            Context.groups.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAftergroupDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task Exportqr_sessionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/qr_sessions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/qr_sessions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task Exportqr_sessionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/qr_sessions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/qr_sessions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void Onqr_sessionsRead(ref IQueryable<Lab05SC.Models.University.qr_session> items);

        public async Task<IQueryable<Lab05SC.Models.University.qr_session>> Getqr_sessions(Query query = null)
        {
            var items = Context.qr_sessions.AsQueryable();

            items = items.Include(i => i.course);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            Onqr_sessionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void Onqr_sessionGet(Lab05SC.Models.University.qr_session item);
        partial void OnGetqr_sessionByQrSessionId(ref IQueryable<Lab05SC.Models.University.qr_session> items);


        public async Task<Lab05SC.Models.University.qr_session> Getqr_sessionByQrSessionId(Guid qrsessionid)
        {
            var items = Context.qr_sessions
                              .AsNoTracking()
                              .Where(i => i.qr_session_id == qrsessionid);

            items = items.Include(i => i.course);
 
            OnGetqr_sessionByQrSessionId(ref items);

            var itemToReturn = items.FirstOrDefault();

            Onqr_sessionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void Onqr_sessionCreated(Lab05SC.Models.University.qr_session item);
        partial void OnAfterqr_sessionCreated(Lab05SC.Models.University.qr_session item);

        public async Task<Lab05SC.Models.University.qr_session> Createqr_session(Lab05SC.Models.University.qr_session qrsession)
        {
            Onqr_sessionCreated(qrsession);

            var existingItem = Context.qr_sessions
                              .Where(i => i.qr_session_id == qrsession.qr_session_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.qr_sessions.Add(qrsession);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(qrsession).State = EntityState.Detached;
                throw;
            }

            OnAfterqr_sessionCreated(qrsession);

            return qrsession;
        }

        public async Task<Lab05SC.Models.University.qr_session> Cancelqr_sessionChanges(Lab05SC.Models.University.qr_session item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void Onqr_sessionUpdated(Lab05SC.Models.University.qr_session item);
        partial void OnAfterqr_sessionUpdated(Lab05SC.Models.University.qr_session item);

        public async Task<Lab05SC.Models.University.qr_session> Updateqr_session(Guid qrsessionid, Lab05SC.Models.University.qr_session qrsession)
        {
            Onqr_sessionUpdated(qrsession);

            var itemToUpdate = Context.qr_sessions
                              .Where(i => i.qr_session_id == qrsession.qr_session_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(qrsession);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterqr_sessionUpdated(qrsession);

            return qrsession;
        }

        partial void Onqr_sessionDeleted(Lab05SC.Models.University.qr_session item);
        partial void OnAfterqr_sessionDeleted(Lab05SC.Models.University.qr_session item);

        public async Task<Lab05SC.Models.University.qr_session> Deleteqr_session(Guid qrsessionid)
        {
            var itemToDelete = Context.qr_sessions
                              .Where(i => i.qr_session_id == qrsessionid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            Onqr_sessionDeleted(itemToDelete);


            Context.qr_sessions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterqr_sessionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportspecialtiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/specialties/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/specialties/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportspecialtiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/specialties/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/specialties/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnspecialtiesRead(ref IQueryable<Lab05SC.Models.University.specialty> items);

        public async Task<IQueryable<Lab05SC.Models.University.specialty>> Getspecialties(Query query = null)
        {
            var items = Context.specialties.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnspecialtiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnspecialtyGet(Lab05SC.Models.University.specialty item);
        partial void OnGetspecialtyBySpecialtyId(ref IQueryable<Lab05SC.Models.University.specialty> items);


        public async Task<Lab05SC.Models.University.specialty> GetspecialtyBySpecialtyId(Guid specialtyid)
        {
            var items = Context.specialties
                              .AsNoTracking()
                              .Where(i => i.specialty_id == specialtyid);

 
            OnGetspecialtyBySpecialtyId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnspecialtyGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnspecialtyCreated(Lab05SC.Models.University.specialty item);
        partial void OnAfterspecialtyCreated(Lab05SC.Models.University.specialty item);

        public async Task<Lab05SC.Models.University.specialty> Createspecialty(Lab05SC.Models.University.specialty specialty)
        {
            OnspecialtyCreated(specialty);

            var existingItem = Context.specialties
                              .Where(i => i.specialty_id == specialty.specialty_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.specialties.Add(specialty);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(specialty).State = EntityState.Detached;
                throw;
            }

            OnAfterspecialtyCreated(specialty);

            return specialty;
        }

        public async Task<Lab05SC.Models.University.specialty> CancelspecialtyChanges(Lab05SC.Models.University.specialty item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnspecialtyUpdated(Lab05SC.Models.University.specialty item);
        partial void OnAfterspecialtyUpdated(Lab05SC.Models.University.specialty item);

        public async Task<Lab05SC.Models.University.specialty> Updatespecialty(Guid specialtyid, Lab05SC.Models.University.specialty specialty)
        {
            OnspecialtyUpdated(specialty);

            var itemToUpdate = Context.specialties
                              .Where(i => i.specialty_id == specialty.specialty_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(specialty);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterspecialtyUpdated(specialty);

            return specialty;
        }

        partial void OnspecialtyDeleted(Lab05SC.Models.University.specialty item);
        partial void OnAfterspecialtyDeleted(Lab05SC.Models.University.specialty item);

        public async Task<Lab05SC.Models.University.specialty> Deletespecialty(Guid specialtyid)
        {
            var itemToDelete = Context.specialties
                              .Where(i => i.specialty_id == specialtyid)
                              .Include(i => i.specialty_courses)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnspecialtyDeleted(itemToDelete);


            Context.specialties.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterspecialtyDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task Exportspecialty_coursesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/specialty_courses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/specialty_courses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task Exportspecialty_coursesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/specialty_courses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/specialty_courses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void Onspecialty_coursesRead(ref IQueryable<Lab05SC.Models.University.specialty_course> items);

        public async Task<IQueryable<Lab05SC.Models.University.specialty_course>> Getspecialty_courses(Query query = null)
        {
            var items = Context.specialty_courses.AsQueryable();

            items = items.Include(i => i.course);
            items = items.Include(i => i.specialty);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            Onspecialty_coursesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void Onspecialty_courseGet(Lab05SC.Models.University.specialty_course item);
        partial void OnGetspecialty_courseBySpecialtyCourseId(ref IQueryable<Lab05SC.Models.University.specialty_course> items);


        public async Task<Lab05SC.Models.University.specialty_course> Getspecialty_courseBySpecialtyCourseId(Guid specialtycourseid)
        {
            var items = Context.specialty_courses
                              .AsNoTracking()
                              .Where(i => i.specialty_course_id == specialtycourseid);

            items = items.Include(i => i.course);
            items = items.Include(i => i.specialty);
 
            OnGetspecialty_courseBySpecialtyCourseId(ref items);

            var itemToReturn = items.FirstOrDefault();

            Onspecialty_courseGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void Onspecialty_courseCreated(Lab05SC.Models.University.specialty_course item);
        partial void OnAfterspecialty_courseCreated(Lab05SC.Models.University.specialty_course item);

        public async Task<Lab05SC.Models.University.specialty_course> Createspecialty_course(Lab05SC.Models.University.specialty_course specialtycourse)
        {
            Onspecialty_courseCreated(specialtycourse);

            var existingItem = Context.specialty_courses
                              .Where(i => i.specialty_course_id == specialtycourse.specialty_course_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.specialty_courses.Add(specialtycourse);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(specialtycourse).State = EntityState.Detached;
                throw;
            }

            OnAfterspecialty_courseCreated(specialtycourse);

            return specialtycourse;
        }

        public async Task<Lab05SC.Models.University.specialty_course> Cancelspecialty_courseChanges(Lab05SC.Models.University.specialty_course item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void Onspecialty_courseUpdated(Lab05SC.Models.University.specialty_course item);
        partial void OnAfterspecialty_courseUpdated(Lab05SC.Models.University.specialty_course item);

        public async Task<Lab05SC.Models.University.specialty_course> Updatespecialty_course(Guid specialtycourseid, Lab05SC.Models.University.specialty_course specialtycourse)
        {
            Onspecialty_courseUpdated(specialtycourse);

            var itemToUpdate = Context.specialty_courses
                              .Where(i => i.specialty_course_id == specialtycourse.specialty_course_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(specialtycourse);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterspecialty_courseUpdated(specialtycourse);

            return specialtycourse;
        }

        partial void Onspecialty_courseDeleted(Lab05SC.Models.University.specialty_course item);
        partial void OnAfterspecialty_courseDeleted(Lab05SC.Models.University.specialty_course item);

        public async Task<Lab05SC.Models.University.specialty_course> Deletespecialty_course(Guid specialtycourseid)
        {
            var itemToDelete = Context.specialty_courses
                              .Where(i => i.specialty_course_id == specialtycourseid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            Onspecialty_courseDeleted(itemToDelete);


            Context.specialty_courses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterspecialty_courseDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportstudentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/students/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/students/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportstudentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/university/students/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/university/students/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnstudentsRead(ref IQueryable<Lab05SC.Models.University.student> items);

        public async Task<IQueryable<Lab05SC.Models.University.student>> Getstudents(Query query = null)
        {
            var items = Context.students.AsQueryable();

            items = items.Include(i => i.group1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnstudentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnstudentGet(Lab05SC.Models.University.student item);
        partial void OnGetstudentByStudentId(ref IQueryable<Lab05SC.Models.University.student> items);


        public async Task<Lab05SC.Models.University.student> GetstudentByStudentId(Guid studentid)
        {
            var items = Context.students
                              .AsNoTracking()
                              .Where(i => i.student_id == studentid);

            items = items.Include(i => i.group1);
 
            OnGetstudentByStudentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnstudentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnstudentCreated(Lab05SC.Models.University.student item);
        partial void OnAfterstudentCreated(Lab05SC.Models.University.student item);

        public async Task<Lab05SC.Models.University.student> Createstudent(Lab05SC.Models.University.student student)
        {
            OnstudentCreated(student);

            var existingItem = Context.students
                              .Where(i => i.student_id == student.student_id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.students.Add(student);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(student).State = EntityState.Detached;
                throw;
            }

            OnAfterstudentCreated(student);

            return student;
        }

        public async Task<Lab05SC.Models.University.student> CancelstudentChanges(Lab05SC.Models.University.student item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnstudentUpdated(Lab05SC.Models.University.student item);
        partial void OnAfterstudentUpdated(Lab05SC.Models.University.student item);

        public async Task<Lab05SC.Models.University.student> Updatestudent(Guid studentid, Lab05SC.Models.University.student student)
        {
            OnstudentUpdated(student);

            var itemToUpdate = Context.students
                              .Where(i => i.student_id == student.student_id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(student);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterstudentUpdated(student);

            return student;
        }

        partial void OnstudentDeleted(Lab05SC.Models.University.student item);
        partial void OnAfterstudentDeleted(Lab05SC.Models.University.student item);

        public async Task<Lab05SC.Models.University.student> Deletestudent(Guid studentid)
        {
            var itemToDelete = Context.students
                              .Where(i => i.student_id == studentid)
                              .Include(i => i.enrollments)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnstudentDeleted(itemToDelete);


            Context.students.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterstudentDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}