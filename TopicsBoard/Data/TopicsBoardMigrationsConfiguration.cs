using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using WebMatrix.WebData;

namespace TopicsBoard.Data
{
    public class TopicsBoardMigrationsConfiguration : DbMigrationsConfiguration<TopicsBoardContext>
    {
        public TopicsBoardMigrationsConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;

        }

        protected override void Seed(TopicsBoardContext context)
        {
            base.Seed(context);
#if DEBUG
            if (context.Topics.Count() == 0)
            {
                var topic = new Topic()
                {
                    Title = "Nasdaq Trading",
                    Created = DateTime.Now,
                    Body = "It Keeps track of all the online and offline trading in place!",
                    TopicCreatedBy = "rahsah",
                    IsActive = true,
                    Tags = new List<Tag>()
          {
            new Tag()
            {
               TagDescription = "Nasdaq",
               IsActive = true,
               Created = DateTime.Now
            },
            new Tag()
            {
               TagDescription = "Google",
               IsActive = true,
               Created = DateTime.Now
            },
            new Tag()
            {
               TagDescription = "Microsoft",
               IsActive = true,
               Created = DateTime.Now
            },
          }
                };

                context.Topics.Add(topic);

                var anotherTopic = new Topic()
                {
                    Title = "Microsoft Investment",
                    Created = DateTime.Now,
                    Body = "Microsoft Investing heavily!",
                    TopicCreatedBy =  "rahsah",
                    TopicModifiedBy = "dheram",
                    IsActive = true,
                    Tags = new List<Tag>()
                    {
                        new Tag()
                        {
                            TagDescription = "Microsoft",
                            IsActive = true,
                            Created = DateTime.Now
                        }
                    }

                };

                context.Topics.Add(anotherTopic);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
                SeedUsersAndRoles();
            }
           
#endif
        }

        private void SeedUsersAndRoles()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                "UserProfile", "UserId", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;
            //Apply all combinations
            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (!roles.RoleExists("User"))
            {
                roles.CreateRole("User");
            }
            //Created Users
            if (membership.GetUser("rahsah", false) == null)
            {
                membership.CreateUserAndAccount("rahsah", "Password@2");
            }
            if (membership.GetUser("dheram", false) == null)
            {
                membership.CreateUserAndAccount("dheram", "Password@2");
            }
            if (membership.GetUser("anikum", false) == null)
            {
                membership.CreateUserAndAccount("anikum", "Password@2");
            }
            if (membership.GetUser("sreput", false) == null)
            {
                membership.CreateUserAndAccount("sreput", "Password@2");
            }
            if (membership.GetUser("rampra", false) == null)
            {
                membership.CreateUserAndAccount("rampra", "Password@1");
            }
            if (membership.GetUser("ranpan", false) == null)
            {
                membership.CreateUserAndAccount("ranpan", "Password@1");
            }
            if (membership.GetUser("dinheg", false) == null)
            {
                membership.CreateUserAndAccount("dinheg", "Password@1");
            }
       
            if (membership.GetUser("anipra", false) == null)
            {
                membership.CreateUserAndAccount("anipra", "Password@1");
            }
            if (membership.GetUser("kalrad", false) == null)
            {
                membership.CreateUserAndAccount("kalrad", "Password@1");
            }
            if (membership.GetUser("ramdas", false) == null)
            {
                membership.CreateUserAndAccount("ramdas", "Password@1");
            }
            if (membership.GetUser("suntha", false) == null)
            {
                membership.CreateUserAndAccount("suntha", "Password@1");
            }

            if (membership.GetUser("punram", false) == null)
            {
                membership.CreateUserAndAccount("punram", "Password@1");
            }
            //Assigned Roles
            if (!roles.GetRolesForUser("rahsah").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "rahsah" }, new[] { "Admin" });
            }
            if (!roles.GetRolesForUser("dheram").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "dheram" }, new[] { "Admin" });
            }
            if (!roles.GetRolesForUser("sreput").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "sreput" }, new[] { "Admin" });
            }
            if (!roles.GetRolesForUser("anikum").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "anikum" }, new[] { "Admin" });
            }
            if (!roles.GetRolesForUser("rampra").Contains("User"))
            {
                roles.AddUsersToRoles(new[] { "rampra" }, new[] { "User" });
            }
         
            if (!roles.GetRolesForUser("anipra").Contains("User"))
            {
                roles.AddUsersToRoles(new[] { "anipra" }, new[] { "User" });
            }
            if (!roles.GetRolesForUser("kalrad").Contains("User"))
            {
                roles.AddUsersToRoles(new[] { "kalrad" }, new[] { "User" });
            }
            if (!roles.GetRolesForUser("ramdas").Contains("User"))
            {
                roles.AddUsersToRoles(new[] { "ramdas" }, new[] { "User" });
            }
            if (!roles.GetRolesForUser("ranpan").Contains("User"))
            {
                roles.AddUsersToRoles(new[] { "ranpan" }, new[] { "User" });
            }
            if (!roles.GetRolesForUser("dinheg").Contains("User"))
            {
                roles.AddUsersToRoles(new[] { "dinheg" }, new[] { "User" });
            }
            if (!roles.GetRolesForUser("suntha").Contains("User"))
            {
                roles.AddUsersToRoles(new[] { "suntha" }, new[] { "User" });
            }
            if (!roles.GetRolesForUser("punram").Contains("User"))
            {
                roles.AddUsersToRoles(new[] { "punram" }, new[] { "User" });
            }
        }
    }
}