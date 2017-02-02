using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace OOPS
{
    interface IEntityManager
    {
        Connection DBConnection { get; set; }
        // Dictionary<KeyValuePair<string, object>> Entity { get; }
        Dictionary<string, object> Entity { get; }
    }

    class Connection
    {

    }

    class ValidateClassesNMethods
    {
        public bool CheckClassesExists(string className)
        {
            // Console.WriteLine("Class Names:- {0}",classNames[0]);
            //foreach (string eachClass in classNames) {
            /* Type myType = Type.GetType(className);
              if (myType != null) {
                  return true;                
              }                     
          //}*/
            return false;
        }

        public bool CheckMethodExists(object className, string methodName)
        {
            var type = className.GetType();
            var method = type.GetMethod(methodName);
            if (method != null)
            {
                return true;
            }
            return false;
        }
    }

    class EntityManager : IEntityManager
    {
        public Dictionary<string, object> fetchEntities = new Dictionary<string, object>();
        private Connection connect;
        protected ValidateClassesNMethods validate;

        public Connection DBConnection
        {
            get { return connect; }
            set { connect = value; }
        }

        public Dictionary<string, object> Entity
        {
            get { return fetchEntities; }
        }

        public void createEntities(params string[] entities)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            //Console.WriteLine(String.Join (", ", entities));
            foreach (string entity in entities)
            {
                //Console.WriteLine("Entity Name:- {0}",entity);
                ///  if (validate.CheckClassesExists("Bugs"))  {
                fetchEntities.Add(entity, assembly.CreateInstance("OOPs." + entity));
                // }
                //Console.WriteLine("each assembly object {0}",assembly.CreateInstance("OOPs."+entity));
            }
        }
    }

    interface IBugs
    {
        long Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        Users Engineer { get; set; }
        Users Reporter { get; set; }
        DateTime CreatedOn { get; set; }
    }

    class Bugs : IBugs
    {
        protected long id;
        protected string title;
        protected string description;
        protected Users engineer;
        protected Users reporter;
        protected DateTime created;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Users Engineer
        {
            get { return engineer; }
            set { engineer = value; }
        }

        public Users Reporter
        {
            get { return reporter; }
            set { reporter = value; }
        }

        public DateTime CreatedOn
        {
            get { return created; }
            set { created = value; }
        }
    }

    class Users
    {
        protected int id;
        protected string name;
        protected string type;
        protected DateTime created;
        protected Bugs[] reportedBugs;
    }

    sealed class Configuration
    {
        private string[] entities = new string[] { "Bugs", "Users" };
        private string[] handlers = new string[] { };

        public string[] getEntities()
        {
            return entities;
        }

        public string[] getHandlers()
        {
            return handlers = entities;
        }

    }

    class Router
    {

    }

    class UsefulObjects
    {
        public static void Main(string[] args)
        {
            Configuration config = new Configuration();

            object handler = new object();
            HandlerProvider provider = new HandlerProvider();
            provider.createHandler(config.getHandlers());
            EntityManager em = new EntityManager();
            em.createEntities(config.getEntities());
            IEnumerable<KeyValuePair<string, object>> handlerType = provider.getHandlerByKey("Bugs");
            Console.WriteLine("Handler Object{0}",handlerType);


            foreach (KeyValuePair<string, object> element in handlerType)
            {
                Console.WriteLine("Handler Type Key :- {0} | Handler Type Value :- {1}",element.Key, element.Value);
            }


           
            /* foreach (KeyValuePair<string,object> element in em.Entity)
             {
                 Console.WriteLine(element.Value);
                 Console.WriteLine(element.Key);
                      //handler = new BugHandler((Bugs) element.Value);
                 provider.setHandler(element.Key);
                 provider.setHandlerEntity(element.Value);
               
             }*/
            //Console.WriteLine("Handler Object :- {0}", provider);
        }
    }

    class HandlerProvider
    {
        private Dictionary<string, object> handlers = new Dictionary<string, object>();

        public void createHandler(string[] handlerNames)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (string handler in handlerNames)
            {
                handlers.Add(handler, assembly.CreateInstance("OOPS." + handler));
            }

            foreach (KeyValuePair<string, object> handle in handlers)
            {
                Console.WriteLine(handle);
            }
        }

        public IEnumerable<KeyValuePair<string, object>> getHandlers()
        {
            return handlers;
        }
        
        public IEnumerable<KeyValuePair<string, object>> getHandlerByKey(string sKey = "")
        {
            IEnumerable<KeyValuePair<string, object>> handlerInfo = null;
            try
            {
                handlerInfo =
                from handler in handlers
                where handler.Key == sKey
                select handler;

                if (handlerInfo != null)
                {
                    return handlerInfo; 
                }
                else
                {
                    throw new Exception("Wrong key provided.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occured:- {0}", e);
            }

            return handlerInfo;
        }

        public void setEntities()
        {

        }

    }

    class BugsHandler
    {
        private Bugs handlerEntity;

        public BugsHandler(Bugs bugEntity)
        {
            this.handlerEntity = bugEntity;
        }
    }

    class UsersHandler
    {
        private Users handlerEntity;

        public UsersHandler(Users userEntity)
        {
            this.handlerEntity = userEntity;
        }
    }

    class Repository
    {
        protected string EntityName;
        protected string repo;
    } 
}
