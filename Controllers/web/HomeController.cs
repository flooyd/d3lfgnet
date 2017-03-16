using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using d3lfg.Services;
using Microsoft.AspNetCore.Identity;
using d3lfg.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace d3lfg.Controllers
{
  public class HomeController : Controller
  {
    private IConfigurationRoot _config;
    private BlizzardServices _blizzard;
    private UserManager<d3User> _userManager;
    private SignInManager<d3User> _signInManager;
    private ILogger _logger;
    private IRepository _repo;
    private Context _context;

    public HomeController(IConfigurationRoot config, BlizzardServices blizzard, UserManager<d3User> userManager,
      SignInManager<d3User> signInManager, ILoggerFactory loggerFactory, IRepository repo, Context context)
    {
      _config = config;
      _blizzard = blizzard;
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = loggerFactory.CreateLogger("Home Controller");
      _repo = repo;
      _context = context;
    }


    public IActionResult Index(string code)
    {
      _logger.LogCritical($"HOST: {Request.Host.Value}");
      //Maybe impossible for blizzard to redirect to https://localhost:port/home/login :(
      if (code != null)
      {
        return RedirectToAction("Login", new { oauthCode = code }); 
      }

      if(User.Identity.IsAuthenticated)
      {
        ViewBag.Battletag = User.Identity.Name;
      }

      return View();
    }

    public IActionResult BeginLogin()
    {
      return Redirect(_blizzard.GetAuthUri("us")); //region should be dynamic later
    }

    public async Task<IActionResult> Login(string oauthCode)
    {
      _logger.LogCritical(Request.Host.Value);
      var token = await _blizzard.GetToken(oauthCode);

      if (token["access_token"] == null)
      {
        return RedirectToAction("Index");
      }
      else
      {
        var tokenString = token["access_token"].ToString();
        string battleTag = await _blizzard.GetBattletag(tokenString);

        d3User user = new d3User()
        {
          Battletag = battleTag,
          UserName = battleTag
        };

        IdentityResult identityUser = await _userManager.CreateAsync(user);

        //only on first time
        if (identityUser.Succeeded)
        {
          await _repo.SaveChangesAsync();
        }

        //every time. user should never get here without something failing before
        await _signInManager.SignInAsync(user, true);

        return RedirectToAction("Index");
      }
    }

    public IActionResult About()
    {
      return View();
    }

    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }

  }
}
