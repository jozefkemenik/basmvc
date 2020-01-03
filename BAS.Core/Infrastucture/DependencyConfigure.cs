using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using BAS.Repository;

using BAS.Repository.Model;
//using BAS.Core.Services.Search;
//using BAS.Core.Services.Provider;


using BAS.Repository.UserProfile;
using BAS.Core.Services;

    namespace BAS.Core.Infrastucture
{
    public  class DependencyConfigure
    {
        public static void Initialize(Type typeMvc)
        {
            var builder = new ContainerBuilder();
            DependencyResolver.SetResolver(
                new Dependency(RegisterServices(builder, typeMvc))
                );
        }

        private static IContainer RegisterServices(ContainerBuilder builder, Type typeMvc)
        {

            builder.RegisterAssemblyTypes(typeMvc.Assembly).PropertiesAutowired();

            //deal with your dependencies here
            builder.RegisterType<Entities>().As<Entities>().InstancePerDependency();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
         
           // builder.RegisterType<SearchService>().As<ISearchService>();
            //builder.RegisterType<ProviderServices>().As<IProviderServices>();
            builder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>();
            builder.RegisterType<UserProfileService>().As<IUserProfileService>();
            builder.RegisterType<NewService>().As<INewService>();
            builder.RegisterType<EventService>().As<IEventService>();
       
            //builder.RegisterType<CategoryService>().As<ICategoryService>();

            //builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<AlbumService>().As<IAlbumService>();


            return
                builder.Build();
        }

    }
}
