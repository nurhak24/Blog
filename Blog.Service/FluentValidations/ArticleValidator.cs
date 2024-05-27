using Blog.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.FluentValidations
{
    public class ArticleValidator : AbstractValidator<Article>
    {
        public ArticleValidator() 
        {

            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(100)
                .WithName("Baslik");

            RuleFor(x => x.Content)
                .NotEmpty()
                .NotNull()
                .MinimumLength(30)
                .MaximumLength(1000)
                .WithName("icerik");
           
        
        }



    }
}
