using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace d3lfg.Services
{
  public class BlizzardServices
  {
    private IConfigurationRoot _config;
    private HttpClient _client;
    private ILogger _logger;

    public BlizzardServices(IConfigurationRoot config, ILoggerFactory loggerFactory)
    {
      _config = config;
      _client = new HttpClient();
      _logger = loggerFactory.CreateLogger("Blizzard Services log");
    }

    public async Task<string> GetBattletag(string token)
    {
      var profileUri = $"https://us.api.battle.net/account/user?access_token={token}";
      var response = await _client.GetAsync(profileUri);
      var content = await response.Content.ReadAsStringAsync();
      var profileJObject = JObject.Parse(content);

      return profileJObject["battletag"].ToString();

      //for some reason, doing the same as GetToken returns a weird json object
      //http://stackoverflow.com/questions/37039869/webmethod-async-results-show-object
    }

    public async Task<JObject> GetToken(string code)
    {
      _logger.LogCritical("GETTING TOKEN");
      var clientId = _config.GetSection("BlizzardKey").Value;
      var secret = _config.GetSection("BlizzardSecret").Value;
      _logger.LogCritical($"CLIENT_ID: {clientId} ----- SECRET: {secret}");
      var redirect_uri = _config.GetSection("BlizzRedirect").Value;
      var tokenUri = $"https://us.battle.net/oauth/token?client_id={clientId}&client_secret={secret}&grant_type=authorization_code&code={code}&redirect_uri={redirect_uri}";

      var response = await _client.GetAsync(tokenUri);
      var content = await response.Content.ReadAsStringAsync();
      var tokenJObject = JObject.Parse(content);

      return tokenJObject;
    }

    public string GetAuthUri(string region)
    {
      var clientId = _config.GetSection("BlizzardKey").Value;
      var state = Guid.NewGuid().ToString("N");
      var redirectUri = _config.GetSection("BlizzRedirect").Value;
      var authUri = $"https://{region}.battle.net/oauth/authorize?client_id={clientId}&state={state}&redirect_uri={redirectUri}&response_type=code";
      
      return authUri;
    }
  }
}
