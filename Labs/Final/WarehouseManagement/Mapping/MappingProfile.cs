using AutoMapper;
using WarehouseManagement.Migrations;
using WarehouseManagement.Models;

namespace WarehouseManagement.Mapping
{


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<academic_year_plan, AcademicYearPlanViewModel>();
            CreateMap<course, CourseViewModel>();
            CreateMap<enrollment, EnrollmentViewModel>();
            CreateMap<group, GroupViewModel>();
            CreateMap<qr_session, QRSessionViewModel>();
            CreateMap<specialty_course, SpecialtyCourseViewModel>();
            CreateMap<specialty, SpecialtyViewModel>();
            CreateMap<student, StudentViewModel>();
        }
    }


    public class AcademicYearPlanViewModel
    {
        public Guid year_plan_id { get; set; }
        public int academic_year { get; set; }
    }
    public class CourseViewModel
    {
        public Guid course_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    public class EnrollmentViewModel
    {
        public Guid enrollment_id { get; set; }
        public Guid student_id { get; set; }
        public Guid course_id { get; set; }
        public Guid group_id { get; set; }
    }
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class GroupViewModel
    {
        public Guid group_id { get; set; }
        public string name { get; set; }
    }
    public class QRSessionViewModel
    {
        public Guid qr_session_id { get; set; }
        public string qr_code { get; set; }
        public DateOnly expiration_time { get; set; }
        public Guid course_id { get; set; }
    }
    public class SpecialtyCourseViewModel
    {
        public Guid specialty_course_id { get; set; }
        public Guid specialty_id { get; set; }
        public Guid course_id { get; set; }
    }
    public class SpecialtyViewModel
    {
        public Guid specialty_id { get; set; }
        public string name { get; set; }
    }
    public class StudentViewModel
    {
        public Guid student_id { get; set; }
        public string full_name { get; set; } // Поєднує first_name та last_name
        public string email { get; set; }
        public string phone_number { get; set; }
        public Guid? group_id { get; set; } // Ідентифікатор групи студента
    }
    
}