using AutoMapper;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using Blog.Web.ResultMessages;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator validator;
        private readonly IToastNotification notification;

        public CategoryController(ICategoryService categoryService,IMapper mapper,IValidator validator, IToastNotification notification)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.notification = notification;
        }
        public async Task<IActionResult > Index()
        {

            var categories = await categoryService.GetAllCategoriesDeleted();

            return View(categories);
        }
        public async Task<IActionResult> DeletedCategory()
        {

            var categories = await categoryService.GetAllCategoriesNonDeleted();

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto) { 
            
            var map = mapper.Map<Category>(categoryAddDto);
            var result = await validator.ValidateAsync((IValidationContext)map);

            if (result.IsValid)
            {
                
                await categoryService.CreateCategoryAsync(categoryAddDto);
                notification.AddSuccessToastMessage(Messages.Category.Add(categoryAddDto.Name),new ToastrOptions {Title = "İşlem tamam" });
                return RedirectToAction("Index", "Category", new
                {
                    Area = "Admin"
                }); ;

            }
            result.AddToModelState(this.ModelState);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWithAjax([FromBody] CategoryAddDto categoryAddDto)
        {
            var map = mapper.Map<Category>(categoryAddDto);
            var result = await validator.ValidateAsync((IValidationContext)map);

            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddDto);
                notification.AddSuccessToastMessage(Messages.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = "İşlem tamam" });
                return Json(Messages.Category.Add(categoryAddDto.Name));
            }
            else
            {
                notification.AddErrorToastMessage(result.Errors.First().ErrorMessage, new ToastrOptions { Title = "İşlem başarısoız" });
                return Json(result.Errors.First().ErrorMessage);
            }

        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid categoryId)
        {

            var category = await categoryService.GetCategoryByGuid(categoryId);
            return View(new CategoryUpdateDto() { Id = category.Id, Name = category.Name });

        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {

            var map = mapper.Map<Category>(categoryUpdateDto);
            var result = await validator.ValidateAsync((IValidationContext)map);
            if (result.IsValid)
            {

                await categoryService.UpdateCategoryAsync(categoryUpdateDto);
                notification.AddSuccessToastMessage(Messages.Category.Add(categoryUpdateDto.Name), new ToastrOptions { Title = "İşlem tamam" });
                return RedirectToAction("Index", "Category", new
                {
                    Area = "Admin"
                }); ;

            }
            result.AddToModelState(this.ModelState);
            return View();

        }

        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var name = await categoryService.SafeDeleteCategoryAsync(categoryId);
            notification.AddSuccessToastMessage(Messages.Category.Delete(name), new ToastrOptions { Title = "Başarılı" });

            return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }
        public async Task<IActionResult> UndoDelete(Guid categoryId)
        {
            var name = await categoryService.UndoDeleteCategoryAsync(categoryId);
            notification.AddSuccessToastMessage(Messages.Category.Delete(name), new ToastrOptions { Title = "Başarılı" });

            return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }

    }
}
