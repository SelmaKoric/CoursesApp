using ComfyLearn.Data;
using ComfyLearn.Helper;
using ComfyLearn.Models;
using ComfyLearn.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace ComfyLearn.Hubs
{
    public class BlogHub : Hub
    {
        public async Task<Task> Like(string clanakId, string isLike)
        {
            var likeClanak = await SaveLike(clanakId, isLike);
            return Clients.All.SendAsync("updateLikeCount", likeClanak);
        }
        private async Task<ClanakLikesVM> SaveLike(string clanakId, string isLike)
        {
            int clanakIdInt = int.Parse(clanakId);
            bool isLiked = isLike == "1";

            var baseContext = this.Context.GetHttpContext();

            if (baseContext.User == null)
                return null;

            ApplicationDbContext context = baseContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            UserManager<User> userManager = baseContext.RequestServices.GetService(typeof(UserManager<User>)) as UserManager<User>;
            IEmailSender emailSender = baseContext.RequestServices.GetService(typeof(IEmailSender)) as IEmailSender;
            ILogger<BlogHub> logger = baseContext.RequestServices.GetService(typeof(ILogger<BlogHub>)) as ILogger<BlogHub>;

            string userId = userManager.GetUserId(baseContext.User);

            var item = context.Clanak.Include(x => x.ClanakLikes).Where(x => x.Id == clanakIdInt).FirstOrDefault();
            var liked = new ClanakLike
            {
                IpAddress = baseContext.Connection.RemoteIpAddress.ToString(),
                ClanakId = item.Id,
                Like = isLiked,
                UserId = userId,
                Datum = DateTime.Now
            };

            var dupe = item.ClanakLikes.FirstOrDefault(e => e.UserId == liked.UserId);
            if (dupe == null)
            {
                item.ClanakLikes.Add(liked);
            }
            else
            {
                if (dupe.Like == isLiked)
                {
                    context.Remove(dupe);
                }
                else
                {
                    dupe.Like = isLiked;
                }
            }
            context.SaveChanges();
            var clanak = context.Clanak.Include(x => x.ClanakLikes).Where(x => x.Id == item.Id).FirstOrDefault();

            if (clanak.Dislikes >= 3 && clanak.Aktivan)
            {
                try
                {
                    await emailSender.SendEmailAsync("koricselma999@gmail.com",
                        "ComfyLearn - Clanak dislike warning" + item.Id,
                        "Clanak ima preko 15 dislikes! Zatvara se.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
                clanak.Aktivan = false;
                context.SaveChanges();
            }

            return new ClanakLikesVM
            {
                Likes = clanak.Likes,
                Dislikes = clanak.Dislikes
            };
        }

    }
}