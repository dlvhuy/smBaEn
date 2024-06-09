using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Helper.Implements;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Hubs.ImplementHubs;

using SocialMedia.Models;
using SocialMedia.Profiles;
using SocialMedia.Repositories.Implementations;
using SocialMedia.Repositories.Interfaces;
using System.Runtime;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMedia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var env = builder.Environment;
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SociaMediaContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("databaseConnection")));
            builder.Services.AddSignalR(option =>
            {
                option.ClientTimeoutInterval = TimeSpan.FromMinutes(30);
                option.EnableDetailedErrors = true;
                option.KeepAliveInterval = TimeSpan.FromMinutes(30);
            });

            ///https://www.youtube.com/watch?v=mpBPXl7dFgA automapper advanced
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //database
            builder.Services.AddScoped<ICommentPost,CommentPostRepostitory>();
            builder.Services.AddScoped<IGroup,GroupRepository>();
            builder.Services.AddScoped<IInforUser,InfoUserRepository>();
            builder.Services.AddScoped<ILikePost,LikePostRepository>();
            builder.Services.AddScoped<IMemberGroup,MemberGroupRepository>();
            builder.Services.AddScoped<IPost,PostRepository>();
            builder.Services.AddScoped<IPostContent, PostContentRepository>();
            builder.Services.AddScoped<IRegister_SignIn,Register_SignInRepository>();
            builder.Services.AddScoped<INotifications,NotificationRepository>();
            builder.Services.AddScoped<IFriends,FriendRepository>();

            //helpper
            builder.Services.AddScoped<IToken, Token>();
            builder.Services.AddScoped<IManageImage, ManageImage>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //automapper
          
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
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath,"Image")),
                RequestPath = "/Image"
            });

        
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHub<PostHub>("post-hub");
            });

            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}