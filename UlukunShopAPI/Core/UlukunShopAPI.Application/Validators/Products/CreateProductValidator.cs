using FluentValidation;
using UlukunShopAPI.Application.ViewModels.Products;

namespace UlukunShopAPI.Application.Validators.Products;

public class CreateProductValidator:AbstractValidator<ProductCreateViewModel>
{
    public CreateProductValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Lutfen urun adini bos birakmayiniz.")
            .MaximumLength(150)
            .MinimumLength(5)
            .WithMessage("Lutfen urun adini 5 ile 100 karakter arasinda giriniz.");

        RuleFor(model => model.Stock)
            .NotEmpty()
            .NotNull()
            .WithMessage("Stok bilgisi bos olamaz")
            .Must(s => s >= 0)
            .WithMessage("stok bilgisini adam gibi giriniz.");
        
        RuleFor(model => model.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Fiyat bilgisi bos olamaz")
            .Must(s => s >= 0)
            .WithMessage("Fiyat bilgisini adam akilli giriniz.");
    }
}