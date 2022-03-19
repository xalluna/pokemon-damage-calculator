using System;
using System.Linq;
using System.Threading.Tasks;
using LearningStarter.Data;
using LearningStarter.Entities;
using LearningStarter.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LearningStarter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.AddHsts(options =>
            {
                options.MaxAge = TimeSpan.MaxValue;
                options.Preload = true;
                options.IncludeSubDomains = true;
            });

            services.AddDbContext<DataContext>(options =>
            {
                // options.UseInMemoryDatabase("FooBar");
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //TODO
            services.AddMvc();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });

            services.AddAuthorization();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Pokemon Database",
                    Version = "v1",
                    Description = "Description for the API goes here.",
                });

                c.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
                c.MapType(typeof(IFormFile), () => new OpenApiSchema { Type = "file", Format = "binary" });
            });

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "learning-starter-web/build";
            });

            services.AddHttpContextAccessor();

            // configure DI for application services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            dataContext.Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();
            
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            }); ;

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Learning Starter Server API V1");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(x => x.MapControllers());

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "learning-starter-web";
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3001");
                }
            });

            var numUsers = dataContext.Users.Count();

            if (numUsers == 0)
            {
                var seededUser = new User
                {
                    FirstName = "Seeded",
                    LastName = "User",
                    Username = "admin",
                    Password = "password"
                };
                
                dataContext.Users.Add(seededUser);
                dataContext.SaveChanges();
                
                seededUser = new User
                {
                    FirstName = "Ash",
                    LastName = "Katchum",
                    Username = "PikaPal10",
                    Password = "ILoveYouPikaChu"
                };
                
                dataContext.Users.Add(seededUser);
                dataContext.SaveChanges();
                
                seededUser = new User
                {
                    FirstName = "Samson",
                    LastName = "Oak",
                    Username = "ThickOaks",
                    Password = "AshsMomHasGotItGoingOn"
                };
                
                dataContext.Users.Add(seededUser);
                dataContext.SaveChanges();
            }

            var numAbilities = dataContext.Abilities.Count();

            if (numAbilities == 0)
            {
                var seededAbility = new Ability
                {
                    Name = "Overgrow"
                };

                dataContext.Abilities.Add(seededAbility);
                dataContext.SaveChanges();
                
                seededAbility = new Ability
                {
                    Name = "Torrent"
                };

                dataContext.Abilities.Add(seededAbility);
                dataContext.SaveChanges();
                
                seededAbility = new Ability
                {
                    Name = "Blaze"
                };

                dataContext.Abilities.Add(seededAbility);
                dataContext.SaveChanges();
            }

            var numExperienceCureves = dataContext.ExperienceCurves.Count();

            if (numExperienceCureves == 0 )
            {
                var seededExpCurve = new ExperienceCurve
                {
                    Name = "Erratic"
                };
                
                dataContext.ExperienceCurves.Add(seededExpCurve);
                dataContext.SaveChanges();
                
                seededExpCurve = new ExperienceCurve
                {
                    Name = "Fast"
                };
                
                dataContext.ExperienceCurves.Add(seededExpCurve);
                dataContext.SaveChanges();
                
                seededExpCurve = new ExperienceCurve
                {
                    Name = "Medium Fast"
                };
                
                dataContext.ExperienceCurves.Add(seededExpCurve);
                dataContext.SaveChanges();
                
                seededExpCurve = new ExperienceCurve
                {
                    Name = "Medium Slow"
                };
                
                dataContext.ExperienceCurves.Add(seededExpCurve);
                dataContext.SaveChanges();
                
                seededExpCurve = new ExperienceCurve
                {
                    Name = "Slow"
                };
                
                dataContext.ExperienceCurves.Add(seededExpCurve);
                dataContext.SaveChanges();
                
                seededExpCurve = new ExperienceCurve
                {
                    Name = "Fluctuating"
                };
                
                dataContext.ExperienceCurves.Add(seededExpCurve);
                dataContext.SaveChanges();

            }

            var numItems = dataContext.Items.Count();

            if (numItems == 0)
            {
                var seededItem = new Item
                {
                    Name = "Choice Scarf"
                };

                dataContext.Items.Add(seededItem);
                dataContext.SaveChanges();
                
                seededItem = new Item
                {
                    Name = "Choice Band"
                };

                dataContext.Items.Add(seededItem);
                dataContext.SaveChanges();

                seededItem = new Item
                {
                    Name = "Choice Specs"
                };

                dataContext.Items.Add(seededItem);
                dataContext.SaveChanges();
            }

            var numTypes = dataContext.Types.Count();

            if (numTypes == 0)
            {
                var seededType = new PType
                {
                    Name = "Normal"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();
                
                seededType = new PType
                {
                    Name = "Fire"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Water"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Grass"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Electric"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Bug"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Flying"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Fighting"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Psychic"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Rock"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Ground"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Poison"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Ice"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Ghost"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Dragon"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Dark"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Steel"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();

                seededType = new PType
                {
                    Name = "Fairy"
                };

                dataContext.Types.Add(seededType);
                dataContext.SaveChanges();
            }

            var numMoves = dataContext.Moves.Count();

            if (numMoves == 0)
            {
                var seededMove = new Move
                {
                    Name = "Tackle",
                    Accuracy = 100,
                    BasePower = 20,
                    SpeedPriority = 1,
                    MoveCategory = true,
                    PowerPoints = 40,
                    TypeId = 1,
                    IsContactOnHit = true,
                    IsBlockedByProtect = true
                };

                dataContext.Moves.Add(seededMove);
                dataContext.SaveChanges();
                
                seededMove = new Move
                {
                    Name = "Scratch",
                    Accuracy = 100,
                    BasePower = 20,
                    SpeedPriority = 1,
                    MoveCategory = true,
                    PowerPoints = 35,
                    TypeId = 1,
                    IsContactOnHit = true,
                    IsBlockedByProtect = true
                };

                dataContext.Moves.Add(seededMove);
                dataContext.SaveChanges();

                seededMove = new Move
                {
                    Name = "Screech",
                    Accuracy = 100,
                    SpeedPriority = 1,
                    PowerPoints = 40,
                    TypeId = 1,
                    IsBlockedByProtect = true,
                    IsSoundBased = true
                };

                dataContext.Moves.Add(seededMove);
                dataContext.SaveChanges();

                seededMove = new Move
                {
                    Name = "Growl",
                    Accuracy = 100,
                    SpeedPriority = 1,
                    PowerPoints = 40,
                    TypeId = 1,
                    IsBlockedByProtect = true
                };

                dataContext.Moves.Add(seededMove);
                dataContext.SaveChanges();

                seededMove = new Move
                {
                    Name = "Ember",
                    Accuracy = 100,
                    BasePower = 20,
                    SpeedPriority = 1,
                    MoveCategory = false,
                    PowerPoints = 35,
                    TypeId = 2,
                    IsContactOnHit = false,
                    IsBlockedByProtect = true,
                    IsPunchBased = false,
                    IsSoundBased = false,
                    IsAffectedByGravity = false,
                    IsDefrostOnUse = false,
                };

                dataContext.Moves.Add(seededMove);
                dataContext.SaveChanges();

                seededMove = new Move
                {
                    Name = "Water Gun",
                    Accuracy = 100,
                    BasePower = 20,
                    SpeedPriority = 1,
                    MoveCategory = false,
                    PowerPoints = 35,
                    TypeId = 3,
                    IsContactOnHit = false,
                    IsBlockedByProtect = true,
                    IsPunchBased = false,
                    IsSoundBased = false,
                    IsAffectedByGravity = false,
                    IsDefrostOnUse = false,
                };

                dataContext.Moves.Add(seededMove);
                dataContext.SaveChanges();

                seededMove = new Move
                {
                    Name = "Vine Whip",
                    Accuracy = 100,
                    BasePower = 20,
                    SpeedPriority = 1,
                    MoveCategory = true,
                    PowerPoints = 35,
                    TypeId = 4,
                    IsContactOnHit = false,
                    IsBlockedByProtect = true,
                    IsPunchBased = false,
                    IsSoundBased = false,
                    IsAffectedByGravity = false,
                    IsDefrostOnUse = false,
                };

                dataContext.Moves.Add(seededMove);
                dataContext.SaveChanges();


            }

        }
    }
}