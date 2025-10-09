using FluentValidation;
using WebApplication1.DTOs;
using System.Linq;

namespace WebApplication1.Validators
{
    //==================== CATEGORY VALIDATORS ====================
    
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nama kategori tidak boleh kosong.")
                .Length(2, 100).WithMessage("Nama kategori harus antara 2 dan 100 karakter.")
                .Must(BeOnlyLettersAndSpaces).WithMessage("Nama kategori hanya boleh berisi huruf dan spasi.");

            RuleFor(x => x.Image)
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.Image))
                .WithMessage("Image harus berupa URL yang valid.");
        }
        
        private bool BeOnlyLettersAndSpaces(string name) => !string.IsNullOrWhiteSpace(name) && name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        private bool BeAValidUrl(string? url) => !string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nama kategori tidak boleh kosong.")
                .Length(2, 100).WithMessage("Nama kategori harus antara 2 dan 100 karakter.")
                .Must(BeOnlyLettersAndSpaces).WithMessage("Nama kategori hanya boleh berisi huruf dan spasi.");

            RuleFor(x => x.Image)
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.Image))
                .WithMessage("Image harus berupa URL yang valid.");
        }
        
        private bool BeOnlyLettersAndSpaces(string name) => !string.IsNullOrWhiteSpace(name) && name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        private bool BeAValidUrl(string? url) => !string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    //==================== MENUCOURSE VALIDATORS ====================

    public class CreateMenuCourseDtoValidator : AbstractValidator<CreateMenuCourseDto>
    {
        public CreateMenuCourseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nama course tidak boleh kosong.")
                .Length(2, 255).WithMessage("Nama course harus antara 2 dan 255 karakter.")
                .Must(BeOnlyLettersAndSpaces).WithMessage("Nama course hanya boleh berisi huruf dan spasi.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Deskripsi tidak boleh melebihi 1000 karakter.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Harga harus lebih besar dari 0.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori yang valid harus dipilih.");

            RuleFor(x => x.Image)
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.Image))
                .WithMessage("Image harus berupa URL yang valid.");
        }
        
        private bool BeOnlyLettersAndSpaces(string name) => !string.IsNullOrWhiteSpace(name) && name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        private bool BeAValidUrl(string? url) => !string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    public class UpdateMenuCourseDtoValidator : AbstractValidator<UpdateMenuCourseDto>
    {
        public UpdateMenuCourseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nama course tidak boleh kosong.")
                .Length(2, 255).WithMessage("Nama course harus antara 2 dan 255 karakter.")
                .Must(BeOnlyLettersAndSpaces).WithMessage("Nama course hanya boleh berisi huruf dan spasi.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Deskripsi tidak boleh melebihi 1000 karakter.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Harga harus lebih besar dari 0.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori yang valid harus dipilih.");

            RuleFor(x => x.Image)
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.Image))
                .WithMessage("Image harus berupa URL yang valid.");
        }
        
        private bool BeOnlyLettersAndSpaces(string name) => !string.IsNullOrWhiteSpace(name) && name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        private bool BeAValidUrl(string? url) => !string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    //==================== SCHEDULE VALIDATORS ====================

    /// <summary>
    /// Validator untuk membuat Jadwal (Schedule) baru
    /// </summary>
    public class CreateScheduleDtoValidator : AbstractValidator<CreateScheduleDto>
    {
        public CreateScheduleDtoValidator()
        {
            RuleFor(x => x.ScheduleDate)
                .NotEmpty().WithMessage("Tanggal jadwal tidak boleh kosong.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Tanggal jadwal harus di masa depan.");
        }
    }

    //==================== MENUCOURSE_SCHEDULE VALIDATORS ====================

    /// <summary>
    /// Validator untuk mendaftarkan jadwal ke sebuah course
    /// </summary>
    public class CreateMenuCourseScheduleDtoValidator : AbstractValidator<CreateMenuCourseScheduleDto>
    {
        public CreateMenuCourseScheduleDtoValidator()
        {
            RuleFor(x => x.AvailableSlot)
                .GreaterThanOrEqualTo(0).WithMessage("Jumlah slot tidak boleh negatif.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status tidak boleh kosong.")
                .Length(1, 50).WithMessage("Status maksimal 50 karakter.");

            RuleFor(x => x.MenuCourseId)
                .GreaterThan(0).WithMessage("MenuCourseId harus valid.");

            RuleFor(x => x.ScheduleId)
                .GreaterThan(0).WithMessage("ScheduleId harus valid.");
        }
    }

    /// <summary>
    /// Validator untuk memperbarui pendaftaran jadwal
    /// </summary>
    public class UpdateMenuCourseScheduleDtoValidator : AbstractValidator<UpdateMenuCourseScheduleDto>
    {
        public UpdateMenuCourseScheduleDtoValidator()
        {
            RuleFor(x => x.AvailableSlot)
                .GreaterThanOrEqualTo(0).WithMessage("Jumlah slot tidak boleh negatif.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status tidak boleh kosong.")
                .Length(1, 50).WithMessage("Status maksimal 50 karakter.");
        }
    }
}