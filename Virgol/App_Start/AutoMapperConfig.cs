using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Virgol.Views.ViewModel;
using Virgol_Model;

namespace Virgol.App_Start
{
    public class AutoMapperConfig
    {

            public static IMapper mapper;
            public static void confiduration()
            {
                //کلاس هایی را که به انوان ابجکت به ابجکت دیگر کلاس می دهیم
               MapperConfiguration configuration = new MapperConfiguration(t => { 
                       t.CreateMap<Ctegory, CategoryViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                       t.CreateMap<CategoryViewModel, Ctegory>().IgnoreAllPropertiesWithAnInaccessibleSetter();

                       t.CreateMap<Article, ArticleViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                       t.CreateMap<ArticleViewModel, Article>().IgnoreAllPropertiesWithAnInaccessibleSetter();

                  
                   t.CreateMap<Role,RoleViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                   t.CreateMap<RoleViewModel, Role>().IgnoreAllPropertiesWithAnInaccessibleSetter();

                   t.CreateMap<User, UserViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                   t.CreateMap<UserViewModel, User>().IgnoreAllPropertiesWithAnInaccessibleSetter();

				   t.CreateMap<Slider, SliderViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
				   t.CreateMap<SliderViewModel, Slider>().IgnoreAllPropertiesWithAnInaccessibleSetter();
			   });

            mapper = configuration.CreateMapper();

            }
        }
}