using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace TopicsBoard
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver =
              new CamelCasePropertyNamesContractResolver();

            //Below formatter is used for returning the Json result.
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);


            //For individual Topic Creation with collection of tags
           /* config.Routes.MapHttpRoute(
                     name: "TagsRouteCollection",
                     routeTemplate: "api/v1/topics/{topicid}/tags/{action}",
                     defaults: new { controller = "tags", action = "PostTags"}
                );*/

            //For individual Topic Creation without tag or single tag
            config.Routes.MapHttpRoute(
                     name: "TagsRoute",
                     routeTemplate: "api/v1/topics/{topicid}/tags/{id}",
                     defaults: new { controller = "tags", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                   name: "TagsCollectionRoute",
                   routeTemplate: "api/v1/topics/{topicid}/tagscollection/{id}",
                   defaults: new { controller = "tagscollection", id = RouteParameter.Optional }
              );
          
            config.Routes.MapHttpRoute(
                name: "FilesRoute",
                routeTemplate: "api/v1/files/{id}",
                defaults: new { controller = "files", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
              name: "FilesPurgeRoute",
              routeTemplate: "api/v1/filespurge/{id}",
              defaults: new { controller = "filespurge", id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/topics/{id}",
                defaults: new { controller = "topics", id = RouteParameter.Optional }
            );


            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}