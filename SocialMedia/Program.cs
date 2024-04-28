using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Helper.Implements;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Hubs.ImplementHubs;

using SocialMedia.Models;
using SocialMedia.Repositories.Implementations;
using SocialMedia.Repositories.Interfaces;
using System.Runtime;
using System.Text;

namespace SocialMedia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<SociaMediaContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("databaseConnection")));
            builder.Services.AddSignalR();
            //database
            builder.Services.AddScoped<ICommentPost,CommentPostRepostitory>();
            builder.Services.AddScoped<IGroup,GroupRepository>();
            builder.Services.AddScoped<IInforUser,InfoUserRepository>();
            builder.Services.AddScoped<ILikePost,LikePostRepository>();
            builder.Services.AddScoped<IMemberGroup,MemberGroupRepository>();
            builder.Services.AddScoped<IPost,PostRepository>();
            builder.Services.AddScoped<IRegister_SignIn,Register_SignInRepository>();

            //helpper
            builder.Services.AddScoped<IToken, Token>();
            builder.Services.AddScoped<IManageImage, ManageImage>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //hubs

            
            

            var SecretKey = builder.Configuration["AppSetting:SecretKey"];
            var SecretKeyBytes = Encoding.UTF8.GetBytes(SecretKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKeyBytes),

                    ClockSkew = TimeSpan.Zero
                };

            });
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                        
                    });
            });

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();

           


            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHub<PostHub>("post-hub");
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}